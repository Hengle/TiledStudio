namespace TiledStudio
{
    partial class FormCanvas
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
            this.DlgOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.DlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.导入xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出xmlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DlgOpenFile
            // 
            this.DlgOpenFile.Filter = "xml文件|*.xml";
            // 
            // DlgSaveFile
            // 
            this.DlgSaveFile.Filter = "xml文件|*.xml";
            // 
            // menuStrip1
            // 
            this.menuStrip1.AllowMerge = false;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导入xmlToolStripMenuItem,
            this.导出xmlToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(533, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 导入xmlToolStripMenuItem
            // 
            this.导入xmlToolStripMenuItem.Name = "导入xmlToolStripMenuItem";
            this.导入xmlToolStripMenuItem.Size = new System.Drawing.Size(79, 21);
            this.导入xmlToolStripMenuItem.Text = "导入AI.xml";
            this.导入xmlToolStripMenuItem.Click += new System.EventHandler(this.导入xmlToolStripMenuItem_Click);
            // 
            // 导出xmlToolStripMenuItem
            // 
            this.导出xmlToolStripMenuItem.Name = "导出xmlToolStripMenuItem";
            this.导出xmlToolStripMenuItem.Size = new System.Drawing.Size(79, 21);
            this.导出xmlToolStripMenuItem.Text = "导出AI.xml";
            this.导出xmlToolStripMenuItem.Click += new System.EventHandler(this.导出xmlToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            this.帮助ToolStripMenuItem.Click += new System.EventHandler(this.帮助ToolStripMenuItem_Click);
            // 
            // FormCanvas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 255);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormCanvas";
            this.ShowIcon = false;
            this.TabText = "AI编辑器";
            this.Text = "Canvas";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog DlgOpenFile;
        private System.Windows.Forms.SaveFileDialog DlgSaveFile;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导入xmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出xmlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
    }
}