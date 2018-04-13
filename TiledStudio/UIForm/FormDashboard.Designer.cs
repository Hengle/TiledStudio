namespace TiledStudio
{
    partial class FormDashboard
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
            this.lblCurFloor = new System.Windows.Forms.Label();
            this.lblShowMousePoint = new System.Windows.Forms.Label();
            this.lblShowScale = new System.Windows.Forms.Label();
            this.lblMapScale = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurFloor
            // 
            this.lblCurFloor.AutoSize = true;
            this.lblCurFloor.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCurFloor.Location = new System.Drawing.Point(12, 81);
            this.lblCurFloor.Name = "lblCurFloor";
            this.lblCurFloor.Size = new System.Drawing.Size(77, 12);
            this.lblCurFloor.TabIndex = 12;
            this.lblCurFloor.Text = "当前区域：无";
            // 
            // lblShowMousePoint
            // 
            this.lblShowMousePoint.AutoSize = true;
            this.lblShowMousePoint.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShowMousePoint.Location = new System.Drawing.Point(12, 56);
            this.lblShowMousePoint.Name = "lblShowMousePoint";
            this.lblShowMousePoint.Size = new System.Drawing.Size(95, 12);
            this.lblShowMousePoint.TabIndex = 13;
            this.lblShowMousePoint.Text = "实时坐标：[0,0]";
            // 
            // lblShowScale
            // 
            this.lblShowScale.AutoSize = true;
            this.lblShowScale.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShowScale.Location = new System.Drawing.Point(12, 33);
            this.lblShowScale.Name = "lblShowScale";
            this.lblShowScale.Size = new System.Drawing.Size(89, 12);
            this.lblShowScale.TabIndex = 14;
            this.lblShowScale.Text = "缩放比例：100%";
            // 
            // lblMapScale
            // 
            this.lblMapScale.AutoSize = true;
            this.lblMapScale.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMapScale.Location = new System.Drawing.Point(12, 9);
            this.lblMapScale.Name = "lblMapScale";
            this.lblMapScale.Size = new System.Drawing.Size(89, 12);
            this.lblMapScale.TabIndex = 15;
            this.lblMapScale.Text = "地图比例：100%";
            // 
            // FormDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(109, 103);
            this.ControlBox = false;
            this.Controls.Add(this.lblCurFloor);
            this.Controls.Add(this.lblShowMousePoint);
            this.Controls.Add(this.lblShowScale);
            this.Controls.Add(this.lblMapScale);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDashboard";
            this.Opacity = 0.5D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "FormDashboard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurFloor;
        private System.Windows.Forms.Label lblShowMousePoint;
        private System.Windows.Forms.Label lblShowScale;
        private System.Windows.Forms.Label lblMapScale;
    }
}