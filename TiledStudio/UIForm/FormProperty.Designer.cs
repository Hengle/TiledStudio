namespace TiledStudio
{
    partial class FormProperty
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
            this.ppgAI = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // ppgAI
            // 
            this.ppgAI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ppgAI.Location = new System.Drawing.Point(0, 0);
            this.ppgAI.Name = "ppgAI";
            this.ppgAI.Size = new System.Drawing.Size(310, 369);
            this.ppgAI.TabIndex = 3;
            // 
            // FormProperty
            // 
            this.AutoHidePortion = 300D;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 369);
            this.CloseButton = false;
            this.ControlBox = false;
            this.Controls.Add(this.ppgAI);
            this.HideOnClose = true;
            this.Name = "FormProperty";
            this.ShowIcon = false;
            this.TabText = "属性";
            this.Text = "Property";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid ppgAI;
    }
}