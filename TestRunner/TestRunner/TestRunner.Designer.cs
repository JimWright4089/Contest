
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
      this.cbCompetition = new System.Windows.Forms.ComboBox();
      this.cbProblem = new System.Windows.Forms.ComboBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.tbRun = new System.Windows.Forms.TextBox();
      this.bBrowse = new System.Windows.Forms.Button();
      this.lvTestData = new System.Windows.Forms.ListView();
      this.chFile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.bRun = new System.Windows.Forms.Button();
      this.lbResult = new System.Windows.Forms.ListBox();
      this.SuspendLayout();
      // 
      // lYear
      // 
      this.lYear.AutoSize = true;
      this.lYear.Location = new System.Drawing.Point(12, 63);
      this.lYear.Name = "lYear";
      this.lYear.Size = new System.Drawing.Size(51, 24);
      this.lYear.TabIndex = 0;
      this.lYear.Text = "year:";
      // 
      // cbYear
      // 
      this.cbYear.FormattingEnabled = true;
      this.cbYear.Location = new System.Drawing.Point(78, 57);
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
      // cbCompetition
      // 
      this.cbCompetition.FormattingEnabled = true;
      this.cbCompetition.Location = new System.Drawing.Point(289, 57);
      this.cbCompetition.Name = "cbCompetition";
      this.cbCompetition.Size = new System.Drawing.Size(174, 30);
      this.cbCompetition.TabIndex = 4;
      this.cbCompetition.SelectedIndexChanged += new System.EventHandler(this.cbCompetition_SelectedIndexChanged);
      // 
      // cbProblem
      // 
      this.cbProblem.FormattingEnabled = true;
      this.cbProblem.Location = new System.Drawing.Point(536, 57);
      this.cbProblem.Name = "cbProblem";
      this.cbProblem.Size = new System.Drawing.Size(223, 30);
      this.cbProblem.TabIndex = 5;
      this.cbProblem.SelectedIndexChanged += new System.EventHandler(this.cbProblem_SelectedIndexChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(476, 63);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(54, 24);
      this.label1.TabIndex = 6;
      this.label1.Text = "prob:";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(220, 63);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(63, 24);
      this.label2.TabIndex = 7;
      this.label2.Text = "comp:";
      // 
      // tbRun
      // 
      this.tbRun.Location = new System.Drawing.Point(16, 12);
      this.tbRun.Name = "tbRun";
      this.tbRun.Size = new System.Drawing.Size(743, 28);
      this.tbRun.TabIndex = 8;
      // 
      // bBrowse
      // 
      this.bBrowse.Location = new System.Drawing.Point(774, 12);
      this.bBrowse.Name = "bBrowse";
      this.bBrowse.Size = new System.Drawing.Size(103, 39);
      this.bBrowse.TabIndex = 9;
      this.bBrowse.Text = "Browse";
      this.bBrowse.UseVisualStyleBackColor = true;
      this.bBrowse.Click += new System.EventHandler(this.bBrowse_Click);
      // 
      // lvTestData
      // 
      this.lvTestData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFile,
            this.chType});
      this.lvTestData.HideSelection = false;
      this.lvTestData.Location = new System.Drawing.Point(12, 106);
      this.lvTestData.Name = "lvTestData";
      this.lvTestData.Size = new System.Drawing.Size(616, 453);
      this.lvTestData.TabIndex = 10;
      this.lvTestData.UseCompatibleStateImageBehavior = false;
      this.lvTestData.View = System.Windows.Forms.View.Details;
      // 
      // chFile
      // 
      this.chFile.Text = "File";
      this.chFile.Width = 200;
      // 
      // chType
      // 
      this.chType.Text = "Type";
      this.chType.Width = 100;
      // 
      // bRun
      // 
      this.bRun.Location = new System.Drawing.Point(894, 12);
      this.bRun.Name = "bRun";
      this.bRun.Size = new System.Drawing.Size(103, 39);
      this.bRun.TabIndex = 11;
      this.bRun.Text = "Run";
      this.bRun.UseVisualStyleBackColor = true;
      this.bRun.Click += new System.EventHandler(this.bRun_Click);
      // 
      // lbResult
      // 
      this.lbResult.FormattingEnabled = true;
      this.lbResult.ItemHeight = 22;
      this.lbResult.Location = new System.Drawing.Point(12, 565);
      this.lbResult.Name = "lbResult";
      this.lbResult.Size = new System.Drawing.Size(1018, 180);
      this.lbResult.TabIndex = 12;
      // 
      // TestRunner
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1361, 762);
      this.Controls.Add(this.lbResult);
      this.Controls.Add(this.bRun);
      this.Controls.Add(this.lvTestData);
      this.Controls.Add(this.bBrowse);
      this.Controls.Add(this.tbRun);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.cbProblem);
      this.Controls.Add(this.cbCompetition);
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
    private System.Windows.Forms.ComboBox cbCompetition;
    private System.Windows.Forms.ComboBox cbProblem;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox tbRun;
    private System.Windows.Forms.Button bBrowse;
    private System.Windows.Forms.ListView lvTestData;
    private System.Windows.Forms.ColumnHeader chFile;
    private System.Windows.Forms.ColumnHeader chType;
    private System.Windows.Forms.Button bRun;
    private System.Windows.Forms.ListBox lbResult;
  }
}

