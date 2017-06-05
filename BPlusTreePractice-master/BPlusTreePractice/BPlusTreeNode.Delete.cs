using System;

namespace BPlusTreePractice
{
  public partial class BPlusTreeNode
  {
    #region Delete
        
    public string Delete(string key, out bool mergeMe)
    {
      if (this.IsLeaf)
      {
        return DeleteLeaf(key, out mergeMe);
      }
      
      mergeMe = false;
            
      int deletePosition = this.FindAtOrNextPosition(key, false);
            
      BPlusTreeNode deleteChildNode = LoadNodeAtPosition(deletePosition);
            
      bool isChildNodeNeedMerge;
      string deletedChildKey = deleteChildNode.Delete(key, out isChildNodeNeedMerge);
            
      this.Soil();
            
      string result = null;
      if (deletedChildKey != null && this.Tree.CompareKey(deletedChildKey, key) == 0)
      {
        if (this.Capacity > 3)
        {
          throw new BPlusTreeException(
            string.Format("Deletion returned delete key for too large node size: {0}.",
            this.Capacity));
        }
        
        if (deletePosition == 0)
        {
          result = this._childKeys[deletePosition];
        }
        else if (deletePosition == this.Capacity)
        {
          this._childKeys[deletePosition - 1] = null;
        }
        else
        {
          this._childKeys[deletePosition - 1] = this._childKeys[deletePosition];
        }

        if (result != null && this.Tree.CompareKey(result, key) == 0)
        {
          this.LoadNodeAtPosition(1);
          result = this._childNodes[1].LeastKey();
        }
        
        deleteChildNode.Free();
                
        OverwriteDeletePosition(deletePosition);
                
        if (this.Count < this.Capacity / 2)
        {
          mergeMe = true;
        }
        
        this.ResetAllChildrenParent();

        return result;
      }

      if (deletePosition == 0)
      {
        result = deletedChildKey;
      }
      else if (deletedChildKey != null && deletePosition > 0)
      {
        if (this.Tree.CompareKey(deletedChildKey, key) != 0)
        {
          this._childKeys[deletePosition - 1] = deletedChildKey;
        }
      }
      
      if (isChildNodeNeedMerge)
      {
        int leftIndex, rightIndex;
        BPlusTreeNode leftNode, rightNode;
        string keyBetween;

        if (deletePosition == 0)
        {
          leftIndex = deletePosition;
          rightIndex = deletePosition + 1;
          leftNode = deleteChildNode;
          rightNode = this.LoadNodeAtPosition(rightIndex);
        }
        else
        {
          leftIndex = deletePosition - 1;
          rightIndex = deletePosition;
          leftNode = this.LoadNodeAtPosition(leftIndex);
          rightNode = deleteChildNode;
        }

        keyBetween = this._childKeys[leftIndex];
                
        string rightLeastKey;
        bool isDeleteRight;
        MergeInternal(leftNode, keyBetween, rightNode, out rightLeastKey, out isDeleteRight);
                
        if (isDeleteRight)
        {
          for (int i = rightIndex; i < this.Capacity; i++)
          {
            this._childKeys[i - 1] = this._childKeys[i];
            this._childValues[i] = this._childValues[i + 1];
            this._childNodes[i] = this._childNodes[i + 1];
          }
          this._childKeys[this.Capacity - 1] = null;
          this._childValues[this.Capacity] = StorageConstants.NullBlockNumber;
          this._childNodes[this.Capacity] = null;

          this.ResetAllChildrenParent();

          rightNode.Free();
                    
          if (this.Count < this.Capacity / 2)
          {
            mergeMe = true;
          }
        }
        else
        {
          this._childKeys[rightIndex - 1] = rightLeastKey;
        }
      }

      return result;
    }
        
    public string DeleteLeaf(string key, out bool mergeMe)
    {
      mergeMe = false;
            
      if (!this.IsLeaf)
      {
        throw new BPlusTreeException("Bad call to delete leaf, this is not a leaf.");
      }

      bool isKeyFound = false;
      int deletePosition = 0;
      
      foreach (string childKey in this._childKeys)
      {
        if (childKey != null && this.Tree.CompareKey(childKey, key) == 0)
        {
          isKeyFound = true;
          break;
        }
        deletePosition++;
      }

      if (!isKeyFound)
      {
        throw new BPlusTreeKeyNotFoundException(
          string.Format("Cannot delete missing key: {0}.", key));
      }
      
      this.Soil();
            
      for (int i = deletePosition; i < this.Capacity - 1; i++)
      {
        this._childKeys[i] = this._childKeys[i + 1];
        this._childValues[i] = this._childValues[i + 1];
      }
      this._childKeys[this.Capacity - 1] = null;
            
      if (this.Count < this.Capacity / 2)
      {
        mergeMe = true;
      }
      
      string result = null;
      if (deletePosition == 0)
      {
        result = this._childKeys[0];
        if (result == null)
        {
          result = key; 
        }
      }

      return result;
    }
        
    private void OverwriteDeletePosition(int deletePosition)
    {
      for (int i = deletePosition; i < this.Capacity - 1; i++)
      {
        this._childKeys[i] = this._childKeys[i + 1];
        this._childValues[i] = this._childValues[i + 1];
        this._childNodes[i] = this._childNodes[i + 1];
      }
      this._childKeys[this.Capacity - 1] = null;

      if (deletePosition < this.Capacity)
      {
        this._childValues[this.Capacity - 1] = this._childValues[this.Capacity];
        this._childNodes[this.Capacity - 1] = this._childNodes[this.Capacity];
      }
      this._childNodes[this.Capacity] = null;
      this._childValues[this.Capacity] = StorageConstants.NullBlockNumber;
    }
        
    private string LeastKey()
    {
      string key = null;
      if (this.IsLeaf)
      {
        key = this._childKeys[0];
      }
      else
      {
        this.LoadNodeAtPosition(0);
        key = this._childNodes[0].LeastKey();
      }

      if (key == null)
      {
        throw new BPlusTreeException("No least key found.");
      }

      return key;
    }

    #endregion

    #region Merge

    public static void MergeInternal(BPlusTreeNode left, string keyBetween, BPlusTreeNode right, out string rightLeastKey, out bool canDeleteRightNode)
    {
      if (left == null)
        throw new ArgumentNullException("left");
      if (right == null)
        throw new ArgumentNullException("right");

      rightLeastKey = null; // only if DeleteRight
            
      if (left.IsLeaf || right.IsLeaf)
      {
        if (!(left.IsLeaf && right.IsLeaf))
        {
          throw new BPlusTreeException("Cannot merge leaf with non-leaf.");
        }
        
        MergeLeaves(left, right, out canDeleteRightNode);

        rightLeastKey = right._childKeys[0];

        return;
      }
      
      canDeleteRightNode = false;

      if (left._childValues[0] == StorageConstants.NullBlockNumber || right._childValues[0] == StorageConstants.NullBlockNumber)
      {
        throw new BPlusTreeException("Cannot merge empty non-leaf with non-leaf.");
      }

      string[] allKeys = new string[left.Capacity * 2 + 1];
      long[] allValues = new long[left.Capacity * 2 + 2];
      BPlusTreeNode[] allNodes = new BPlusTreeNode[left.Capacity * 2 + 2];

      int index = 0;
      allValues[0] = left._childValues[0];
      allNodes[0] = left._childNodes[0];
      for (int i = 0; i < left.Capacity; i++)
      {
        if (left._childKeys[i] == null)
        {
          break;
        }

        allKeys[index] = left._childKeys[i];
        allValues[index + 1] = left._childValues[i + 1];
        allNodes[index + 1] = left._childNodes[i + 1];

        index++;
      }
      
      allKeys[index] = keyBetween;
      index++;
            
      allValues[index] = right._childValues[0];
      allNodes[index] = right._childNodes[0];
      int rightCount = 0;
      for (int i = 0; i < right.Capacity; i++)
      {
        if (right._childKeys[i] == null)
        {
          break;
        }

        allKeys[index] = right._childKeys[i];
        allValues[index + 1] = right._childValues[i + 1];
        allNodes[index + 1] = right._childNodes[i + 1];
        index++;

        rightCount++;
      }
      
      if (index <= left.Capacity)
      {
        // it will all fit in one node
        canDeleteRightNode = true;

        for (int i = 0; i < index; i++)
        {
          left._childKeys[i] = allKeys[i];
          left._childValues[i] = allValues[i];
          left._childNodes[i] = allNodes[i];
        }

        left._childValues[index] = allValues[index];
        left._childNodes[index] = allNodes[index];

        left.ResetAllChildrenParent();
        left.Soil();

        right.Free();

        return;
      }

      // otherwise split the content between the nodes
      left.Clear();
      right.Clear();
      left.Soil();
      right.Soil();

      int leftContent = index / 2;
      int rightContent = index - leftContent - 1;

      rightLeastKey = allKeys[leftContent];

      int outputIndex = 0;
      for (int i = 0; i < leftContent; i++)
      {
        left._childKeys[i] = allKeys[outputIndex];
        left._childValues[i] = allValues[outputIndex];
        left._childNodes[i] = allNodes[outputIndex];
        outputIndex++;
      }

      rightLeastKey = allKeys[outputIndex];

      left._childValues[outputIndex] = allValues[outputIndex];
      left._childNodes[outputIndex] = allNodes[outputIndex];
      outputIndex++;

      rightCount = 0;
      for (int i = 0; i < rightContent; i++)
      {
        right._childKeys[i] = allKeys[outputIndex];
        right._childValues[i] = allValues[outputIndex];
        right._childNodes[i] = allNodes[outputIndex];
        outputIndex++;

        rightCount++;
      }

      right._childValues[rightCount] = allValues[outputIndex];
      right._childNodes[rightCount] = allNodes[outputIndex];

      left.ResetAllChildrenParent();
      right.ResetAllChildrenParent();
    }
        
    public static void MergeLeaves(BPlusTreeNode left, BPlusTreeNode right, out bool canDeleteRightNode)
    {
      if (left == null)
        throw new ArgumentNullException("left");
      if (right == null)
        throw new ArgumentNullException("right");

      canDeleteRightNode = false;

      string[] allKeys = new string[left.Capacity * 2];
      long[] allValues = new long[left.Capacity * 2];

      int index = 0;
      for (int i = 0; i < left.Capacity; i++)
      {
        if (left._childKeys[i] == null)
        {
          break;
        }
        allKeys[index] = left._childKeys[i];
        allValues[index] = left._childValues[i];
        index++;
      }

      for (int i = 0; i < right.Capacity; i++)
      {
        if (right._childKeys[i] == null)
        {
          break;
        }
        allKeys[index] = right._childKeys[i];
        allValues[index] = right._childValues[i];
        index++;
      }
      
      if (index <= left.Capacity)
      {
        canDeleteRightNode = true;

        left.Clear();

        for (int i = 0; i < index; i++)
        {
          left._childKeys[i] = allKeys[i];
          left._childValues[i] = allValues[i];
        }

        left.Soil();
        right.Free();

        return;
      }
      
      left.Clear();
      right.Clear();
      left.Soil();
      right.Soil();

      int rightContent = index / 2;
      int leftContent = index - rightContent;
      int newIndex = 0;
      for (int i = 0; i < leftContent; i++)
      {
        left._childKeys[i] = allKeys[newIndex];
        left._childValues[i] = allValues[newIndex];
        newIndex++;
      }
      for (int i = 0; i < rightContent; i++)
      {
        right._childKeys[i] = allKeys[newIndex];
        right._childValues[i] = allValues[newIndex];
        newIndex++;
      }
    }

    #endregion
  }
}
