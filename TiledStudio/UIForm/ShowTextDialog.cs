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
    //public delegate void OnEnterTextEnd(string txt);
    public partial class ShowTextDialog : Form
    {
        //public event OnEnterTextEnd EnterEndCallback;
        public ShowTextDialog()
        {
            InitializeComponent();
        }

        public string ShowText
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }


        private void OK_Click(object sender, EventArgs e)
        {
            //EnterEndCallback?.Invoke(ShowText);
            if (ShowText.Length > 1)
            {
                DialogResult = DialogResult.OK;
            }
            //Close();
        }

    }
}
