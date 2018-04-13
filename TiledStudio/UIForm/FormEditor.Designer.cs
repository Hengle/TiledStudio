namespace TiledStudio
{
    partial class FormEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEditor));
            this.DlgFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.DlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.DlgColor = new System.Windows.Forms.ColorDialog();
            this.SuspendLayout();
            // 
            // FormEditor
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 334);
            this.CloseButton = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormEditor";
            this.TabText = "地图选点工具";
            this.Text = "地图选点工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog DlgFileOpen;
        private System.Windows.Forms.SaveFileDialog DlgSaveFile;
        private System.Windows.Forms.ColorDialog DlgColor;
    }
}

