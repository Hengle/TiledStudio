namespace TiledStudio
{
    partial class FormMakeTable
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.lblShowMousePoint = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkObs = new System.Windows.Forms.CheckBox();
            this.checkTrigger = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开图片ToolStripMenuItem,
            this.导出ToolStripMenuItem,
            this.导入ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(551, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开图片ToolStripMenuItem
            // 
            this.打开图片ToolStripMenuItem.Name = "打开图片ToolStripMenuItem";
            this.打开图片ToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.打开图片ToolStripMenuItem.Text = "打开图片";
            this.打开图片ToolStripMenuItem.Click += new System.EventHandler(this.打开图片ToolStripMenuItem_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // 导入ToolStripMenuItem
            // 
            this.导入ToolStripMenuItem.Name = "导入ToolStripMenuItem";
            this.导入ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.导入ToolStripMenuItem.Text = "导入";
            this.导入ToolStripMenuItem.Click += new System.EventHandler(this.导入ToolStripMenuItem_Click);
            // 
            // DlgFileOpen
            // 
            this.DlgFileOpen.FileName = "image.png";
            // 
            // lblShowMousePoint
            // 
            this.lblShowMousePoint.AutoSize = true;
            this.lblShowMousePoint.Location = new System.Drawing.Point(12, 31);
            this.lblShowMousePoint.Name = "lblShowMousePoint";
            this.lblShowMousePoint.Size = new System.Drawing.Size(95, 12);
            this.lblShowMousePoint.TabIndex = 1;
            this.lblShowMousePoint.Text = "当前坐标：[0,0]";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "当前方向：↑",
            "当前方向：↗",
            "当前方向：→",
            "当前方向：↘",
            "当前方向：↓",
            "当前方向：↙",
            "当前方向：←",
            "当前方向：↖"});
            this.comboBox1.Location = new System.Drawing.Point(142, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(83, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(231, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(44, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "镜像";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkObs
            // 
            this.checkObs.AutoSize = true;
            this.checkObs.Checked = true;
            this.checkObs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkObs.Location = new System.Drawing.Point(306, 30);
            this.checkObs.Name = "checkObs";
            this.checkObs.Size = new System.Drawing.Size(48, 16);
            this.checkObs.TabIndex = 4;
            this.checkObs.Text = "阻挡";
            this.checkObs.UseVisualStyleBackColor = true;
            this.checkObs.CheckedChanged += new System.EventHandler(this.checkObs_CheckedChanged);
            // 
            // checkTrigger
            // 
            this.checkTrigger.AutoSize = true;
            this.checkTrigger.Location = new System.Drawing.Point(360, 30);
            this.checkTrigger.Name = "checkTrigger";
            this.checkTrigger.Size = new System.Drawing.Size(48, 16);
            this.checkTrigger.TabIndex = 4;
            this.checkTrigger.Text = "触发";
            this.checkTrigger.UseVisualStyleBackColor = true;
            this.checkTrigger.CheckedChanged += new System.EventHandler(this.checkTrigger_CheckedChanged);
            // 
            // FormMakeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 359);
            this.Controls.Add(this.checkTrigger);
            this.Controls.Add(this.checkObs);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblShowMousePoint);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMakeTable";
            this.TabText = "打表工具";
            this.Text = "MakeOffsetTable";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开图片ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog DlgFileOpen;
        private System.Windows.Forms.Label lblShowMousePoint;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkObs;
        private System.Windows.Forms.CheckBox checkTrigger;
    }
}