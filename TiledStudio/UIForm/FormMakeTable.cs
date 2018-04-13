using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace TiledStudio
{
    public partial class FormMakeTable : DockContent
    {
        public FormMakeTable()
        {
            InitializeComponent();
            InitGrid();
            ClientSize = new Size(kGridNumX * kGridSize + 20, kGridNumY * kGridSize + 60);
            DoubleBuffered = true;
            StartPosition = FormStartPosition.CenterScreen;

        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
        }

        //地图图片
        Image TheImage;
        //显示区域的起点
        Point DisplayStartPos = new Point(10, 50);
        //显示区域
        Rectangle DisplayArea = new Rectangle(10, 50, kGridNumX * kGridSize, kGridNumY * kGridSize);

        //真实1米对应的100像素，1个网格大小1米
        const int kGridSize = 100;
        //网格X方向个数(最多支持20000*20000像素大小的图片）
        const int kGridNumX = 9;
        //网格Y方向个数
        const int kGridNumY = 9;
        //图片在Y方向占的格子数
        public int nImageGridNumY = kGridNumY;

        //网格对象
        Grid[,] AllGrids = new Grid[kGridNumX, kGridNumY];
        //网格画笔
        Pen GridPen = new Pen(Color.FromArgb(100, Color.GhostWhite), 1);

        private static MapArea CurrentMapArea = new MapArea();
        private MapFloor ObstructionFloor = new MapFloor(CurrentMapArea);
        private MapFloor TriggerFloor = new MapFloor(CurrentMapArea);
        private MapFloor CurrentMapFloor = null;

        //初始化网格
        void InitGrid()
        {
            for (int i = 0; i < kGridNumX; i++)
            {
                for (int j = 0; j < kGridNumY; j++)
                {
                    AllGrids[i, j] = new Grid()
                    {
                        rectAngle = new Rectangle(DisplayStartPos.X + i * kGridSize, DisplayStartPos.Y + j * kGridSize, kGridSize, kGridSize),
                        gridX = i,
                        gridY = j,
                        tableX = -kGridNumX/2+i,
                        tableY = kGridNumY/2-j
                    };
                }
            }
        }

        private void 打开图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DlgFileOpen.Filter = "png|*.png|jpg|*.jpg|bmp|*.bmp|所有文件|*.*";
            DialogResult r = DlgFileOpen.ShowDialog();
            if (r == DialogResult.OK)
            {
                if (File.Exists(DlgFileOpen.FileName))
                {
                    TheImage = Image.FromFile(DlgFileOpen.FileName);
                    if (TheImage.Size != DisplayArea.Size)
                    {
                        MessageBox.Show($"导入图片必须是[{DisplayArea.Width}x{DisplayArea.Height}]像素大小！");
                        return;
                    }
                    //CurrentMapFloor = new MapFloor(CurrentMapArea);
                    CurrentMapFloor = ObstructionFloor;
                    XmlHelper.Instance.ImageFileName = DlgFileOpen.FileName;
                }
            }
            Invalidate();
        }

        Brush obsBrush = new SolidBrush(Color.FromArgb(100, Color.BlueViolet));
        private Brush tgBrush = new SolidBrush(Color.FromArgb(100, Color.Aqua));
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            if (TheImage != null)
            {
                //绘制图片
                g.DrawImage(TheImage, DisplayStartPos.X, DisplayStartPos.Y);
            }

            //绘制网格
            List<Rectangle> rs = new List<Rectangle>();
            foreach (var grid in AllGrids)
            {
                rs.Add(grid.rectAngle);

                if (grid.floors.Count > 0)
                {
                    foreach (var floor in grid.floors)
                    {
                        if (floor == ObstructionFloor)
                        {
                            g.FillRectangle(obsBrush, grid.rectAngle);
                        }
                        else
                        {
                            g.FillRectangle(tgBrush, grid.rectAngle);
                        }
                    }
                }

                if (grid.tableX == 0 && grid.tableY == 0)
                {
                    g.DrawRectangle(new Pen(Color.Red, 2), grid.rectAngle);
                }
            }
            if (rs.Count > 0)
            {
                g.DrawRectangles(GridPen, rs.ToArray());
            }
            //绘制边框
            g.DrawRectangle(new Pen(Color.Black, 2), DisplayArea);

        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Right)
            {
                if (CurrentMapFloor == null)
                {
                    MessageBox.Show("必须选择一个图层");
                    return;
                }
                //右键点击选择网格
                Grid SelectGrid = GetGridByMousePoint(e.X, e.Y);
                if (SelectGrid == null)
                {
                    return;
                }

                if (Control.ModifierKeys == Keys.Shift)
                {
                    CancelGrid(SelectGrid, CurrentMapFloor);
                }
                else
                {
                    OnSelectGrid(SelectGrid, CurrentMapFloor);
                }

                Invalidate();
            }

        }

        Grid GetGridByMousePoint(int x, int y)
        {
            int gridx = (x - DisplayArea.X) / kGridSize;
            int gridy = (y - DisplayArea.Y) / kGridSize;
            if (gridx < 0 || gridx >= kGridNumX || gridy < 0 || gridy >= kGridNumY) return null;

            var grid = AllGrids[gridx, gridy];

            lblShowMousePoint.Text = string.Format("坐标偏移：[{0},{1}]", grid.tableX, grid.tableY);
            lblShowMousePoint.ForeColor = Color.IndianRed;
            return grid;
        }

        //选择或变更图层
        private void OnSelectGrid(Grid SelectGrid, MapFloor floor)
        {
            if (!SelectGrid.floors.Contains(floor))
            {
                //添加到图层
                SelectGrid.floors.Add(floor);
                floor.grids.Add(SelectGrid);
            }
        }

        //取消选择图层
        private void CancelGrid(Grid SelectGrid, MapFloor floor)
        {
            if (SelectGrid.floors.Contains(floor))
            {
                //取消选择
                floor.grids.Remove(SelectGrid);
                SelectGrid.floors.Remove(floor);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            //内有实时更新坐标的功能。。
            GetGridByMousePoint(e.X, e.Y);
            if (TheImage == null) return;
            if (e.X < DisplayArea.X || e.X > DisplayArea.X + DisplayArea.Width || e.Y < DisplayArea.Y || e.Y > DisplayArea.Y + DisplayArea.Height)
            {
                return;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (CurrentMapFloor == null)
                {
                    MessageBox.Show("必须选择一个图层");
                    return;
                }

                Grid grid = GetGridByMousePoint(e.X, e.Y);
                if (ModifierKeys == Keys.Shift)
                {
                    CancelGrid(grid, CurrentMapFloor);
                }
                else
                {
                    OnSelectGrid(grid, CurrentMapFloor);
                }

            }

            Invalidate();

        }

        private int currentDir = 0;//默认方向↑
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dirBox = sender as ComboBox;
            currentDir = dirBox.SelectedIndex;
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMapFloor == null)
            {
                return;
            }
            StringBuilder sb = new StringBuilder();

            if (ObstructionFloor.grids.Count > 0)
            {
                foreach (var grid in ObstructionFloor.grids)
                {
                    sb.AppendFormat("{0}_{1},", grid.tableX, grid.tableY);
                }
                sb.Length--;
            }

            sb.Append('|');
            if (TriggerFloor.grids.Count > 0)
            {
                foreach (var grid in TriggerFloor.grids)
                {
                    sb.AppendFormat("{0}_{1},", grid.tableX, grid.tableY);
                }
                sb.Length--;
            }

            var dlgTxt = new ShowTextDialog();
            dlgTxt.ShowText = sb.ToString();
            dlgTxt.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("此功能非常复杂，你还是自己涂吧，懒得做了");
        }

        public void LoadText(string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return;
            }

            var floors = txt.Split('|');
            if (!string.IsNullOrEmpty(floors[0]))
            {
                var pts = floors[0].Split(',');
                foreach (var s in pts)
                {
                    var p = s.Split('_');
                    int tableX = Convert.ToInt32(p[0]);
                    int tableY = Convert.ToInt32(p[1]);

                    int gridX = tableX + (kGridNumX / 2);
                    int gridY = tableY + (kGridNumY / 2);
                    OnSelectGrid(AllGrids[gridX, gridY], ObstructionFloor);
                }
            }

            if (!string.IsNullOrEmpty(floors[1]))
            {
                var pts = floors[1].Split(',');
                foreach (var s in pts)
                {
                    var p = s.Split('_');
                    int tableX = Convert.ToInt32(p[0]);
                    int tableY = Convert.ToInt32(p[1]);

                    int gridX = tableX + (kGridNumX / 2);
                    int gridY = tableY + (kGridNumY / 2);
                    OnSelectGrid(AllGrids[gridX, gridY], TriggerFloor);
                }
            }
        }

        private void 导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentMapFloor == null)
            {
                MessageBox.Show("先打开图片");
                return;
            }

            var dlgTxt = new ShowTextDialog();
            //dlgTxt.EnterEndCallback += LoadText;
            var ret = dlgTxt.ShowDialog(this);
            if(ret == DialogResult.OK)
            {
                LoadText(dlgTxt.Text);
            }
            Invalidate();
        }

        private void checkObs_CheckedChanged(object sender, EventArgs e)
        {
            if (checkObs.Checked)
            {
                checkTrigger.Checked = false;
                CurrentMapFloor = ObstructionFloor;
            }
            else
            {
                checkTrigger.Checked = true;
                CurrentMapFloor = TriggerFloor;
            }
        }

        private void checkTrigger_CheckedChanged(object sender, EventArgs e)
        {
            if (checkTrigger.Checked)
            {
                checkObs.Checked = false;
                CurrentMapFloor = TriggerFloor;
            }
            else
            {
                checkObs.Checked = true;
                CurrentMapFloor = ObstructionFloor;
            }

        }
    }
}
