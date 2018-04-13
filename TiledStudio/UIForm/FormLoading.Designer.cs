namespace TiledStudio
{
    partial class FormLoading
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
            this.progLoad = new System.Windows.Forms.ProgressBar();
            this.lblShow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progLoad
            // 
            this.progLoad.Location = new System.Drawing.Point(30, 51);
            this.progLoad.Name = "progLoad";
            this.progLoad.Size = new System.Drawing.Size(252, 23);
            this.progLoad.TabIndex = 19;
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(28, 25);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(215, 12);
            this.lblShow.TabIndex = 20;
            this.lblShow.Text = "你打开的地图略大，玩命加载中...(0%)";
            // 
            // FM_LOAD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 88);
            this.Controls.Add(this.lblShow);
            this.Controls.Add(this.progLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FM_LOAD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FM_LOAD";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progLoad;
        private System.Windows.Forms.Label lblShow;
    }
}