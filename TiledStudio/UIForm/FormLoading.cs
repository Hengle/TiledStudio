using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TiledStudio
{
    public partial class FormLoading : Form
    {
        public FormLoading()
        {
            InitializeComponent();
        }

        public void SetValue(int value)
        {
            progLoad.Value = value;
            lblShow.Text = string.Format("你打开的地图略大，玩命加载中...({0}%)", value);
        }
    }
}
