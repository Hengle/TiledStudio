using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TiledStudio
{
    public partial class FormDashboard :Form
    {
        public FormDashboard()
        {
            InitializeComponent();
            TransparencyKey = BackColor;
            lblShowMousePoint.ForeColor = Color.IndianRed;
        }

        public void ShowCoordinate(int x, int y)
        {
            lblShowMousePoint.Text = string.Format("当前坐标：[{0},{1}]", x, y);
        }

        public void ShowImageScale(string s)
        {
            lblShowScale.Text = s;
        }

        int offsetX = 0;
        int offsetY = 0;
        bool moving = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (!moving)
            {
                moving = true;
                offsetX = e.X - Left;
                offsetY = e.Y - Top;
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (moving)
            {
                moving = false;
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (moving)
            {
                Top = e.X - offsetX;
                Left = e.Y - offsetY;
            }
        }
    }
}
