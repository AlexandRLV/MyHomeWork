/*
 * 
 *  TigerCS - by Gil Schmidt.
 * 
 *  - this is only a translation for TigerNet by 
 *    Markus Hahn (maakus@earthlink.net) that was
 *    writtin in vb, i only converted it to c#.
 * 
 *  [ contact me at: Gil_Smdt@hotmail.com ]
 * 
 */

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using Microsoft.Win32;


namespace ThexCS
{
	public class Example : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox Input;
		private System.Windows.Forms.TextBox Output;
		private System.Windows.Forms.Button GetThexBtn;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
        private ComboBox ThexMethod;
        private Label CalcTime;
		private System.ComponentModel.IContainer components;

        public Example()
		{
			InitializeComponent();

            ThexMethod.SelectedIndex = 0;

		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
            this.Input = new System.Windows.Forms.TextBox();
            this.Output = new System.Windows.Forms.TextBox();
            this.GetThexBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ThexMethod = new System.Windows.Forms.ComboBox();
            this.CalcTime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Input
            // 
            this.Input.Location = new System.Drawing.Point(8, 24);
            this.Input.Name = "Input";
            this.Input.Size = new System.Drawing.Size(344, 20);
            this.Input.TabIndex = 1;
            this.Input.Text = "c:\\1.txt";
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(8, 124);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(344, 20);
            this.Output.TabIndex = 2;
            this.Output.TabStop = false;
            // 
            // GetThexBtn
            // 
            this.GetThexBtn.Location = new System.Drawing.Point(8, 56);
            this.GetThexBtn.Name = "GetThexBtn";
            this.GetThexBtn.Size = new System.Drawing.Size(75, 23);
            this.GetThexBtn.TabIndex = 2;
            this.GetThexBtn.Text = "Get TTH";
            this.GetThexBtn.Click += new System.EventHandler(this.GetThex_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 24);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tiger Tree Hash value:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filename:";
            // 
            // ThexMethod
            // 
            this.ThexMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ThexMethod.FormattingEnabled = true;
            this.ThexMethod.Items.AddRange(new object[] {
            "Thex Threaded",
            "Thex Optimized",
            "Thex (First version)"});
            this.ThexMethod.Location = new System.Drawing.Point(89, 56);
            this.ThexMethod.Name = "ThexMethod";
            this.ThexMethod.Size = new System.Drawing.Size(149, 21);
            this.ThexMethod.TabIndex = 6;
            // 
            // CalcTime
            // 
            this.CalcTime.AutoSize = true;
            this.CalcTime.Location = new System.Drawing.Point(12, 86);
            this.CalcTime.Name = "CalcTime";
            this.CalcTime.Size = new System.Drawing.Size(0, 13);
            this.CalcTime.TabIndex = 7;
            // 
            // Example
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(360, 159);
            this.Controls.Add(this.CalcTime);
            this.Controls.Add(this.ThexMethod);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.Input);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GetThexBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Example";
            this.Text = "Tiger Tree Hash example.";
            this.Load += new System.EventHandler(this.Example_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new Example());
		}

        const int THEX_THREADED = 0;
        const int THEX_OPTIMIZED = 1;
        const int THEX = 2;

        private void GetThex_Click(object sender, System.EventArgs e)
		{
            DateTime StartTime = DateTime.Now;
            byte[] Result = new byte[] { 0 };

            switch (ThexMethod.SelectedIndex)
            {
                case THEX_THREADED:
                    ThexThreaded TTH_Threaded = new ThexThreaded();
                    Result = TTH_Threaded.GetTTH_Value(Input.Text);
                    break;
                case THEX_OPTIMIZED:
                    ThexOptimized TTH_Optimized = new ThexOptimized();
                    Result = TTH_Optimized.GetTTH(Input.Text);
                    break;
                case THEX:
                    Thex TTH = new Thex();
                    Result = TTH.GetTTH(Input.Text);
                    break;
            }
            
			Output.Text = Base32.ToBase32String(Result);

            CalcTime.Text = "TTH resulted in: " + (DateTime.Now.Ticks - StartTime.Ticks).ToString() + " ticks.";
		}

		private void Example_Load(object sender, System.EventArgs e)
		{
			Input.Text = Application.ExecutablePath;
		}
	}
}
