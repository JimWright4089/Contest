
namespace TestRunner
{
  partial class TestRunner
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.lYear = new System.Windows.Forms.Label();
      this.cbYear = new System.Windows.Forms.ComboBox();
      this.bScan = new System.Windows.Forms.Button();
      this.bFix = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // lYear
      // 
      this.lYear.AutoSize = true;
      this.lYear.Location = new System.Drawing.Point(12, 15);
      this.lYear.Name = "lYear";
      this.lYear.Size = new System.Drawing.Size(51, 24);
      this.lYear.TabIndex = 0;
      this.lYear.Text = "year:";
      // 
      // cbYear
      // 
      this.cbYear.FormattingEnabled = true;
      this.cbYear.Location = new System.Drawing.Point(78, 12);
      this.cbYear.Name = "cbYear";
      this.cbYear.Size = new System.Drawing.Size(121, 30);
      this.cbYear.TabIndex = 1;
      this.cbYear.SelectedIndexChanged += new System.EventHandler(this.cbYear_SelectedIndexChanged);
      // 
      // bScan
      // 
      this.bScan.Location = new System.Drawing.Point(1246, 711);
      this.bScan.Name = "bScan";
      this.bScan.Size = new System.Drawing.Size(103, 39);
      this.bScan.TabIndex = 2;
      this.bScan.Text = "Scan";
      this.bScan.UseVisualStyleBackColor = true;
      this.bScan.Click += new System.EventHandler(this.bScan_Click);
      // 
      // bFix
      // 
      this.bFix.Location = new System.Drawing.Point(1137, 711);
      this.bFix.Name = "bFix";
      this.bFix.Size = new System.Drawing.Size(103, 39);
      this.bFix.TabIndex = 3;
      this.bFix.Text = "Fix";
      this.bFix.UseVisualStyleBackColor = true;
      this.bFix.Click += new System.EventHandler(this.bFix_Click);
      // 
      // TestRunner
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1361, 762);
      this.Controls.Add(this.bFix);
      this.Controls.Add(this.bScan);
      this.Controls.Add(this.cbYear);
      this.Controls.Add(this.lYear);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.78182F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(5);
      this.Name = "TestRunner";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Test Runner";
      this.Load += new System.EventHandler(this.TestRunner_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label lYear;
    private System.Windows.Forms.ComboBox cbYear;
    private System.Windows.Forms.Button bScan;
    private System.Windows.Forms.Button bFix;
  }
}

