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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            Instance = this;
        }

        public void ResetMenu()
        {
            MainMenuStrip = menuStrip1;
        }

        public static FormMain Instance;

        public FormProperty fmProperty;
        public FormDrawer fmToolBox;
        public FormCanvas fmCanvas;
        public FormSolution fmSolution;
        public FormMakeTable fmMakeTable;
        public FormEditor fmEditor;
        public FormDashboard fmDashboard;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            XmlHelper.Instance.ProjectName = "Project";

            //解决方案
            fmSolution = new FormSolution();
            fmSolution.Show(MainDockPanel);
            fmSolution.DockTo(MainDockPanel, DockStyle.Left);

            //属性
            fmProperty = new FormProperty();
            fmProperty.Show(MainDockPanel);
            fmProperty.DockTo(MainDockPanel, DockStyle.Left);
            AINode.AIProperty = fmProperty.Property;
            fmSolution.MapProperty = fmProperty.Property;
            fmSolution.LoadFromArea();

            //编辑区
            fmEditor = new FormEditor();
            fmEditor.Show(MainDockPanel);



            //仪表盘
            //fmDashboard = new FormDashboard();
            //fmDashboard.Show(MainDockPanel);
            //fmDashboard.DockTo(MainDockPanel, DockStyle.Left);
            //fmDashboard.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockLeftAutoHide;

        }

        //private void 新建工程ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    FormNewProject fmNewProj = new FormNewProject();
        //    DialogResult ret = fmNewProj.ShowDialog();
        //    if(ret == DialogResult.OK)
        //    {
        //        //XmlHelper.Instance.Workspace = fmNewProj.Workspace;
        //        XmlHelper.Instance.ProjectName = fmNewProj.ProjectName;

        //        fmSolution.LoadFromArea();
        //    }
        //}

        private void 打开工程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmEditor.ClickOpenProject(null, null);
        }

        private void 保存工程CtrlSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmEditor.ClickSaveProject(null, null);
        }

        private void 制作场景元素ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmMakeTable = new FormMakeTable();
            fmMakeTable.Show(MainDockPanel);
            //fmMakeTable.DockTo(MainDockPanel, DockStyle.Fill);
        }

        private void aI编辑器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fmCanvas = new FormCanvas();
            fmCanvas.Show(MainDockPanel);
        }

        //打开地图
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            fmEditor.MenuClick_OpenMap(null, new OpenMapEventArgs() { MapScale = 100 });
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            fmEditor.MenuClick_OpenMap(null, new OpenMapEventArgs() { MapScale = 50 });
        }

        private void Help_Click(object sender, EventArgs e)
        {
            string helpstr = "说明：本工具由jwk编写，功能强大操作简单，开源免费没有广告。"
                    + "\r\n左键移动地图，右键选择网格，Shift+右键擦除，ALT+滚轮缩放，Ctrl+点击图层跳转。"
                    + "\r\n快捷键：Ctrl+E 打开 Ctrl+S保存，1 3 5 7切换画刷大小。";
            MessageBox.Show(helpstr);

        }

        private void 导入NPC表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgOpenFile.DefaultExt = "*.xml";
            DlgOpenFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            DlgOpenFile.Filter = "xml文件|*.xml";
            DialogResult r = DlgOpenFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                XmlHelper.Instance.LoadNpcs(DlgOpenFile.FileName);
                DataHelper.Instance.AllNPCData.ConfigFile = DlgOpenFile.FileName;
                MessageBox.Show("导入NPC表成功！");
            }
        }

        private void 导入怪物表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgOpenFile.DefaultExt = "*.xml";
            DlgOpenFile.Filter = "xml文件|*.xml";
            DlgOpenFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            DialogResult r = DlgOpenFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                XmlHelper.Instance.LoadMonsters(DlgOpenFile.FileName);
                DataHelper.Instance.AllMonsterZoneData.ConfigFile = DlgOpenFile.FileName;
                MessageBox.Show("导入怪物表成功！");
            }

        }

        private void 导入SceneObjectxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgOpenFile.DefaultExt = "*.xml";
            DlgOpenFile.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            DlgOpenFile.Filter = "xml文件|*.xml";
            DialogResult r = DlgOpenFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                XmlHelper.Instance.LoadSceneObjects(DlgOpenFile.FileName);
                DataHelper.Instance.AllSceneObjAreaData.ConfigFile = DlgOpenFile.FileName;
                MessageBox.Show("导入场景元素表成功！");
            }

        }


        #region 导出XML
        private void 导出安全区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "anquanqu.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportAQQ(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }
        }

        private void 导出阻挡区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "obs.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportOBJ(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出碰撞区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "collision.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportCollision(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出地图区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "maparea.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportArea(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出NPC区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "npcs.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportNPC(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出怪物区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "Monsters.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportMonster(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出路线区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "navigate.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportPath(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出传送点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "teleports.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportTeleport(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }

        }

        private void 导出巡逻线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "patrol.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportPatrol(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }
        }

        private void 导出摆放物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "sceneobjs.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportSceneObj(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }
        }

        private void 导出触发区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgSaveFile.FileName = "trigger.xml";
            DialogResult r = DlgSaveFile.ShowDialog();
            if (r == DialogResult.OK)
            {
                fmEditor.TranslateAllGrid();
                XmlHelper.Instance.ExportTrigger(DlgSaveFile.FileName);
                fmEditor.TranslateAllGrid();
            }
        }

        #endregion

    }
}
