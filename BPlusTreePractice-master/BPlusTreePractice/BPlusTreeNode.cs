using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace BPlusTreePractice
{
  /// <summary>
  /// B+ 树节点
  /// </summary>
  public partial class BPlusTreeNode
  {
    #region Properties
        
    internal BPlusTree Tree { get; private set; }
    internal BPlusTreeNode Parent { get; private set; }
    internal int PositionInParent { get; private set; }
    internal bool IsLeaf { get; private set; }
    internal int Capacity { get; private set; }
    internal long BlockNumber { get; private set; }
    internal bool IsDirty { get; private set; }
        
    internal int Count
    {
      get
      {
        int inUsing = 0;

        for (int i = 0; i < this.Capacity; i++)
        {
          if (this._childKeys[i] == null)
          {
            break;
          }
          inUsing++;
        }

        return inUsing;
      }
    }

    #endregion

    #region Fields
        
    private BPlusTreeNode[] _childNodes;
    private string[] _childKeys;
    private long[] _childValues;

    #endregion

    #region Ctors
    public BPlusTreeNode(BPlusTree tree, BPlusTreeNode parent, int positionInParent, bool isLeaf)
    {
      if (tree == null)
        throw new ArgumentNullException("tree");

      this.Tree = tree;
      this.Parent = parent;
      this.IsLeaf = isLeaf;

      this.PositionInParent = -1;
      this.BlockNumber = StorageConstants.NullBlockNumber;
      this.Capacity = tree.NodeCapacity;
      this.IsDirty = true;

      this.Initialize();
            
      if (parent != null && positionInParent >= 0)
      {
        if (positionInParent > this.Capacity)
        {
          throw new BPlusTreeException("The position in parent is beyond the limit of node capacity.");
        }
        
        this.Parent._childNodes[positionInParent] = this;
        this.BlockNumber = this.Parent._childValues[positionInParent];
        this.PositionInParent = positionInParent;
      }
    }

    #endregion

    #region Initialize

    private void Initialize()
    {
      Clear();
    }

    private void Clear()
    {
      this._childKeys = new string[this.Capacity];
      this._childValues = new long[this.Capacity + 1];
      this._childNodes = new BPlusTreeNode[this.Capacity + 1];

      for (int i = 0; i < this.Capacity; i++)
      {
        this._childKeys[i] = null;
        this._childValues[i] = StorageConstants.NullBlockNumber;
        this._childNodes[i] = null;
      }

      this._childValues[this.Capacity] = StorageConstants.NullBlockNumber;
      this._childNodes[this.Capacity] = null;

      // this is now a terminal node
      CheckIfTerminal();
    }

    #endregion

    #region Root
        
    public long MakeAsRoot()
    {
      this.Parent = null;
      this.PositionInParent = -1;
      if (this.BlockNumber == StorageConstants.NullBlockNumber)
      {
        throw new BPlusTreeException("No root seek allocated to new root.");
      }
      return this.BlockNumber;
    }
        
    public static BPlusTreeNode MakeRoot(BPlusTree tree, bool isLeaf)
    {
      return new BPlusTreeNode(tree, null, -1, isLeaf);
    }
        
    public static BPlusTreeNode BinaryRoot(
      BPlusTreeNode oldRoot, string splitFirstKey,
      BPlusTreeNode splitNode, BPlusTree tree)
    {
      if (oldRoot == null)
        throw new ArgumentNullException("oldRoot");
      if (splitNode == null)
        throw new ArgumentNullException("splitNode");
      
      BPlusTreeNode newRoot = MakeRoot(tree, false);
            
      newRoot._childKeys[0] = splitFirstKey;
            
      oldRoot.ResetParent(newRoot, 0);
      splitNode.ResetParent(newRoot, 1);

      return newRoot;
    }

    #endregion

    #region Parent
        
    private void ResetParent(BPlusTreeNode newParent, int positionInParent)
    {
      this.Parent = newParent;
      this.PositionInParent = positionInParent;
            
      newParent._childValues[positionInParent] = this.BlockNumber;
      newParent._childNodes[positionInParent] = this;
            
      this.Tree.ForgetTerminalNode(this.Parent);
    }
        
    private void ResetAllChildrenParent()
    {
      for (int i = 0; i <= this.Capacity; i++)
      {
        BPlusTreeNode node = this._childNodes[i];
        if (node != null)
        {
          node.ResetParent(this, i);
        }
      }
    }

    #endregion

    #region Dirty
        
    public BPlusTreeNode FirstChild()
    {
      BPlusTreeNode firstNode = this.LoadNodeAtPosition(0);
      if (firstNode == null)
      {
        throw new BPlusTreeException("No first child.");
      }
      return firstNode;
    }
        
    public long Invalidate(bool isDestroy)
    {
      long blockNumber = this.BlockNumber;
            
      if (!this.IsLeaf)
      {
        // need to invalidate kids
        for (int i = 0; i < this.Capacity + 1; i++)
        {
          if (this._childNodes[i] != null)
          {
            // new block numbers are recorded automatically
            this._childValues[i] = this._childNodes[i].Invalidate(true);
          }
        }
      }

      // store if dirty
      if (this.IsDirty)
      {
        blockNumber = this.DumpToNewBlock();
      }

      // remove from owner archives if present
      this.Tree.ForgetTerminalNode(this); // 我已经有了一个序列化的子节点，我不再是终端节点

      // remove from parent
      if (this.Parent != null && this.PositionInParent >= 0)
      {
        this.Parent._childNodes[this.PositionInParent] = null;
        this.Parent._childValues[this.PositionInParent] = blockNumber; // should be redundant
        this.Parent.CheckIfTerminal();
        this.PositionInParent = -1;
      }

      // render all structures useless, just in case...
      if (isDestroy)
      {
        this.Destroy();
      }

      return blockNumber;
    }
        
    private void Destroy()
    {
      // make sure the structure is useless, it should no longer be used.
      this.Tree = null;
      this.Parent = null;
      this.Capacity = -100;
      this._childValues = null;
      this._childKeys = null;
      this._childNodes = null;
      this.BlockNumber = StorageConstants.NullBlockNumber;
      this.PositionInParent = -100;
      this.IsDirty = false;
    }

    public void Free()
    {
      if (this.BlockNumber != StorageConstants.NullBlockNumber)
      {
        if (this.Tree._freeBlocksOnAbort.ContainsKey(this.BlockNumber))
        {
          // free it now
          this.Tree._freeBlocksOnAbort.Remove(this.BlockNumber);
          this.Tree.ReclaimBlock(this.BlockNumber);
        }
        else
        {
          // free on commit
          this.Tree._freeBlocksOnCommit[this.BlockNumber] = this.BlockNumber;
        }
      }
      this.BlockNumber = StorageConstants.NullBlockNumber; // don't do it twice...
    }
        
    public string SanityCheck(Hashtable visited)
    {
      string result = null;

      if (visited == null)
      {
        visited = new Hashtable();
      }
      if (visited.ContainsKey(this))
      {
        throw new BPlusTreeException(
          string.Format("Node visited twice {0}.", this.BlockNumber));
      }

      visited[this] = this.BlockNumber;
      if (this.BlockNumber != StorageConstants.NullBlockNumber)
      {
        if (visited.ContainsKey(this.BlockNumber))
        {
          throw new BPlusTreeException(
            string.Format("Block number seen twice {0}.", this.BlockNumber));
        }
        visited[this.BlockNumber] = this;
      }

      if (this.Parent != null)
      {
        if (this.Parent.IsLeaf)
        {
          throw new BPlusTreeException("Parent is leaf.");
        }

        this.Parent.LoadNodeAtPosition(this.PositionInParent);
        if (this.Parent._childNodes[this.PositionInParent] != this)
        {
          throw new BPlusTreeException("Incorrect index in parent.");
        }

        // since not at root there should be at least size/2 keys
        int limit = this.Capacity / 2;
        if (this.IsLeaf)
        {
          limit--;
        }
        for (int i = 0; i < limit; i++)
        {
          if (this._childKeys[i] == null)
          {
            throw new BPlusTreeException("Null child in first half.");
          }
        }
      }

      result = this._childKeys[0]; // for leaf
      if (!this.IsLeaf)
      {
        this.LoadNodeAtPosition(0);
        result = this._childNodes[0].SanityCheck(visited);

        for (int i = 0; i < this.Capacity; i++)
        {
          if (this._childKeys[i] == null)
          {
            break;
          }

          this.LoadNodeAtPosition(i + 1);
          string least = this._childNodes[i + 1].SanityCheck(visited);
          if (least == null)
          {
            throw new BPlusTreeException(
              string.Format("Null least in child doesn't match node entry {0}.", this._childKeys[i]));
          }
          if (!least.Equals(this._childKeys[i]))
          {
            throw new BPlusTreeException(
              string.Format("Least in child {0} doesn't match node entry {1}.",
              least, this._childKeys[i]));
          }
        }
      }
      
      string lastkey = this._childKeys[0];
      for (int i = 1; i < this.Capacity; i++)
      {
        if (this._childKeys[i] == null)
        {
          break;
        }
        if (lastkey.Equals(this._childKeys[i]))
        {
          throw new BPlusTreeException(
            string.Format("Duplicate key in node {0}.", lastkey));
        }
        lastkey = this._childKeys[i];
      }

      return result;
    }
        
    private void CheckIfTerminal()
    {
      if (!this.IsLeaf)
      {
        for (int i = 0; i < this.Capacity + 1; i++)
        {
          if (this._childNodes[i] != null)
          {
            this.Tree.ForgetTerminalNode(this);
            return;
          }
        }
      }
      
      this.Tree.RecordTerminalNode(this);
    }
        
    private void Soil()
    {
      if (!this.IsDirty)
      {
        this.IsDirty = true;
                
        if (this.Parent != null)
        {
          this.Parent.Soil();
        }
      }
    }

    #endregion

    #region ToString
        
    public string ToText(string indent)
    {
      StringBuilder sb = new StringBuilder();

      string indentPlus = indent + "\t";

      sb.AppendLine(indent + "Node{");

      sb.Append(indentPlus + "IsLeaf = " + this.IsLeaf);
      sb.Append(", Capacity = " + this.Capacity);
      sb.Append(", Count = " + this.Count);
      sb.Append(", Dirty = " + this.IsDirty);
      sb.Append(", BlockNumber = " + this.BlockNumber);
      sb.Append(", ParentBlockNumber = " + (this.Parent == null ? "NULL" : this.Parent.BlockNumber.ToString(CultureInfo.InvariantCulture)));
      sb.Append(", PositionInParent = " + this.PositionInParent);
      sb.AppendLine();

      if (this.IsLeaf)
      {
        for (int i = 0; i < this.Capacity; i++)
        {
          string key = this._childKeys[i];
          long value = this._childValues[i];
          if (key != null)
          {
            key = string.IsNullOrEmpty(key) ? "NULL" : key;
            sb.AppendLine(indentPlus + "[Key : " + key + ", Value : " + value + "]");
          }
        }
      }
      else
      {
        int count = 0;
        for (int i = 0; i < this.Capacity; i++)
        {
          string key = this._childKeys[i];
          long value = this._childValues[i];
          if (key != null)
          {
            key = string.IsNullOrEmpty(key) ? "NULL" : key;
            sb.AppendLine(indentPlus + "[Key : " + key + ", Value : " + value + "]");

            count++;
          }
        }

        for (int i = 0; i <= count; i++)
        {
          try
          {
            this.LoadNodeAtPosition(i);
            sb.Append(this._childNodes[i].ToText(indentPlus));
          }
          catch (BPlusTreeException ex)
          {
            sb.AppendLine(ex.Message);
          }
        }
      }

      sb.AppendLine(indent + "}");

      return sb.ToString();
    }

    public override string ToString()
    {
      return string.Format("PositionInParent[{0}], IsLeaf[{1}], Capacity[{2}], Count[{3}], BlockNumber[{4}], IsDirty[{5}]",
        PositionInParent, IsLeaf, Capacity, Count, BlockNumber, IsDirty);
    }

    #endregion
  }
}
