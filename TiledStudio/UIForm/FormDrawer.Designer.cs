namespace TiledStudio
{
    partial class FormDrawer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDrawer));
            this.ControlNodes = new System.Windows.Forms.ImageList(this.components);
            this.drawerpanel = new System.Windows.Forms.Panel();
            this.ConditionNode = new System.Windows.Forms.ImageList(this.components);
            this.ActionNode = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // ControlNodes
            // 
            this.ControlNodes.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ControlNodes.ImageStream")));
            this.ControlNodes.TransparentColor = System.Drawing.Color.Transparent;
            this.ControlNodes.Images.SetKeyName(0, "elipse.ico");
            this.ControlNodes.Images.SetKeyName(1, "octagon.ico");
            this.ControlNodes.Images.SetKeyName(2, "rhombus.ico");
            this.ControlNodes.Images.SetKeyName(3, "rounded_rectangle.ico");
            this.ControlNodes.Images.SetKeyName(4, "square.ico");
            this.ControlNodes.Images.SetKeyName(5, "triangle.ico");
            // 
            // drawerpanel
            // 
            this.drawerpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawerpanel.Location = new System.Drawing.Point(0, 0);
            this.drawerpanel.Name = "drawerpanel";
            this.drawerpanel.Size = new System.Drawing.Size(256, 261);
            this.drawerpanel.TabIndex = 0;
            // 
            // ConditionNode
            // 
            this.ConditionNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ConditionNode.ImageStream")));
            this.ConditionNode.TransparentColor = System.Drawing.Color.Transparent;
            this.ConditionNode.Images.SetKeyName(0, "rhombus_128px_1075237_easyicon.net.ico");
            // 
            // ActionNode
            // 
            this.ActionNode.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ActionNode.ImageStream")));
            this.ActionNode.TransparentColor = System.Drawing.Color.Transparent;
            this.ActionNode.Images.SetKeyName(0, "circle_128px_1075198_easyicon.net.ico");
            // 
            // FormDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 261);
            this.Controls.Add(this.drawerpanel);
            this.Name = "FormDrawer";
            this.ShowIcon = false;
            this.TabText = "Drawer";
            this.Text = "Drawer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList ControlNodes;
        private System.Windows.Forms.Panel drawerpanel;
        private System.Windows.Forms.ImageList ConditionNode;
        private System.Windows.Forms.ImageList ActionNode;
    }
}