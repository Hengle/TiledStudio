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
    public partial class FormNewProject : Form
    {
        public FormNewProject()
        {
            InitializeComponent();
        }

        //public string Workspace { get; set; }
        public string ProjectName { get; set; }


        private void btnOK_Click(object sender, EventArgs e)
        {
            ProjectName = txtProjectName.Text;
            if (//string.IsNullOrEmpty(Workspace)||
                string.IsNullOrEmpty(ProjectName))
            {
                MessageBox.Show("配置不全");
                return;
            }
            DialogResult = DialogResult.OK;
        }

        //private void btnWorkspace_Click(object sender, EventArgs e)
        //{
        //    DlgBrowseFloder.RootFolder = Environment.SpecialFolder.Desktop;
        //    DlgBrowseFloder.ShowNewFolderButton = true;
        //    var ret = DlgBrowseFloder.ShowDialog();
        //    if(ret == DialogResult.OK)
        //    {
        //        Workspace = DlgBrowseFloder.SelectedPath;
        //        txtWorkspace.Text = Workspace;
        //    }
        //}
    }
}
