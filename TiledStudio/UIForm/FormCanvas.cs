using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using WeifenLuo.WinFormsUI.Docking;

namespace TiledStudio
{
    public partial class FormCanvas : DockContent
    {
        public UIDrawing DoWhatDrawing { get; set; }

        AITree TheAITree;

        public FormCanvas()
        {
            InitializeComponent();
            BackColor = Color.White;
            DoubleBuffered = true;

            TheAITree = new AITree();
            var uiTree = new UIDrawingBehaviorTree(TheAITree);
            uiTree.ClientSize = ClientSize;
            DoWhatDrawing = uiTree;

            this.Activate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            var uiTree = DoWhatDrawing as UIDrawingBehaviorTree;
            if (uiTree != null)
            {
                uiTree.ClientSize = ClientSize;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DoWhatDrawing == null)
                return;

            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            DoWhatDrawing.OnPaint(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            this.Activate();

            if (DoWhatDrawing == null)
                return;

            DoWhatDrawing.OnMouseClick(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (DoWhatDrawing == null) return;
            DoWhatDrawing.OnMouseDown(e);

        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (DoWhatDrawing == null) return;
            DoWhatDrawing.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //坐标
            //coordination.Text = string.Format("[{0},{1}]", e.X, e.Y);

            if (DoWhatDrawing == null)
                return;

            DoWhatDrawing.OnMouseMove(e);
            Invalidate();

        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (DoWhatDrawing == null)
                return;

            DoWhatDrawing.OnKeyDown(e);
            Invalidate();

        }


        private void 导出xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ret = DlgSaveFile.ShowDialog();
            if (ret == DialogResult.OK)
            {
                TheAITree.Save(DlgSaveFile.FileName);
            }

        }

        private void 导入xmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult ret = DlgOpenFile.ShowDialog();
            if (ret == DialogResult.OK)
            {
                TheAITree.Load(DlgOpenFile.FileName);
                Invalidate();
            }

        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "GameJoy行为树编辑器v1.0\r\n" +
                "Shift+Enter 插入子节点\r\n" +
                "Enter 插入同级节点\r\n" +
                "Delete 删除节点\r\n" +
                "ALT+↑/↓ 调整同级节点的先后顺序" +
                "鼠标拖放修改节点树的层级顺序"
                );
        }
    }
}
