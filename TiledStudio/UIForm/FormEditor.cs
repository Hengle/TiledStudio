using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using WeifenLuo.WinFormsUI.Docking;

namespace TiledStudio
{
    public partial class FormEditor : DockContent
    {
        #region 绘图
        //真实1米对应的100像素，1个网格大小1米
        public int kGridSize = 50;

        //地图图片
        Image TheImage;
        //显示图片
        Image ShowImage;
        //当前的绘图起点
        Point ImageStartPos = new Point(0, 0);
        //显示区域的起点
        Point DisplayStartPos = new Point(10, 10);
        //显示区域
        Rectangle DisplayArea;
        //图片裁剪区
        Rectangle srcRect = new Rectangle(0, 0, 0, 0);
        //图片显示区
        //Rectangle dstRect = new Rectangle(0, 0, 0, 0);

        //地图导入比例
        public int nImportMapScale = 100;
        //缩放比例
        public int nGridScale = 100;
        //缩放之后的网格大小[调整此参数用来修改单元格大小]
        public int nGridShowSize = 0;
        //导入比例下的网格大小
        public int nGridImportSize = 0;
        //图片在Y方向占的格子数
        public int nImageGridNumY = kGridNumY;


        //绘图笔刷
        int GridPenWidth = 0;
        //网格X方向个数(最多支持20000*20000像素大小的图片）
        const int kGridNumX = 400;
        //网格Y方向个数
        const int kGridNumY = 400;
        //网格对象
        Grid[,] AllGrids = new Grid[kGridNumX, kGridNumY];
        //图片对象
        Dictionary<int, Image> AllPickedImage = new Dictionary<int, Image>();

        //网格画笔
        Pen GridPen = new Pen(Color.FromArgb(100, Color.GhostWhite), 1);

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            FormMain.Instance.ResetMenu();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            if (ShowImage != null)
            {
                srcRect.X = ImageStartPos.X;
                srcRect.Y = ImageStartPos.Y;

                //绘制图片到显示区
                g.DrawImage(ShowImage, DisplayArea, srcRect, GraphicsUnit.Pixel);
            }

            //绘制网格
            List<Rectangle> rs = new List<Rectangle>();
            foreach (var grid in AllGrids)
            {
                if (grid.rectAngle.X + nGridShowSize > DisplayArea.X && grid.rectAngle.X < DisplayArea.X + DisplayArea.Width
                    && grid.rectAngle.Y + nGridShowSize > DisplayArea.Y && grid.rectAngle.Y < DisplayArea.Y + DisplayArea.Height)
                {
                    rs.Add(grid.rectAngle);
                    if (grid.floors.Count > 0)
                    {
                        grid.floors.Sort((a, b) => a.Depth - b.Depth);
                        foreach (var floor in grid.floors)
                        {
                            if (floor.belongArea.Show)
                            {
                                //导航网格特殊处理，画线段
                                if (floor is Path)
                                {
                                    var _path = floor as Path;
                                    var pps = (floor as Path).AllPathPoints;
                                    foreach (var pathPoint in pps)
                                    {
                                        g.FillEllipse(new SolidBrush(floor.FloorColor), new Rectangle(grid.CenterPoint.X - 3, grid.CenterPoint.Y - 3, 6, 6));
                                        foreach (var endPoint in pathPoint.neighbors)
                                        {
                                            g.DrawLine(new Pen(floor.FloorColor, 2), pathPoint.grid.CenterPoint, endPoint.grid.CenterPoint);
                                        }
                                    }
                                    if (_path.LastMousePoint.X != 0)
                                    {
                                        g.DrawLine(new Pen(floor.FloorColor, 2), _path.LastMousePoint, _path.CurrentMousePoint);
                                    }
                                }
                                else
                                {
                                    g.FillRectangle(new SolidBrush(floor.FloorColor), grid.rectAngle);
                                }

                                if (floor is SceneObjFloor)
                                {
                                    if (grid.tableX == 0 && grid.tableY == 0)
                                    {
                                        g.DrawRectangle(new Pen(Color.Red, 2), grid.rectAngle);
                                    }
                                }
                                else if (floor is MonsterFloor)
                                {
                                    var centerGrid = AllGrids[floor.X, (nImageGridNumY - floor.Y)];
                                    g.DrawString($"怪区 {floor.ID}", new Font("微软雅黑", 20), Brushes.White, centerGrid.rectAngle.X, centerGrid.rectAngle.Y);
                                }
                            }
                        }
                    }
                }
            }
            if (rs.Count > 0)
            {
                g.DrawRectangles(GridPen, rs.ToArray());
            }
            //绘制边框
            g.DrawRectangle(new Pen(Color.Black, 2), DisplayArea);
            //绘制鼠标边框（使用当前图层颜色）
            var curfloor = DataHelper.Instance.CurrentMapFloor;
            if (curfloor != null)
            {
                if (GridPenRect.X < DisplayArea.X || GridPenRect.Y < DisplayArea.Y)
                {
                    return;
                }
                g.DrawRectangle(new Pen(curfloor.FloorColor, 1), GridPenRect);

                //NPC 怪物 传送点 出生点 要加边框
                if (curfloor.belongArea.isXYRArea)
                {
                    int x = (curfloor.X - curfloor.Radius) * nGridShowSize;//绝对位置
                    int gridy = (nImageGridNumY - curfloor.Y);
                    int y = (gridy - curfloor.Radius) * nGridShowSize;//绝对位置
                    int s = (curfloor.Radius * 2 + 1) * nGridShowSize;
                    x -= ImageStartPos.X - DisplayArea.X;
                    y -= ImageStartPos.Y - DisplayArea.Y;
                    g.DrawRectangle(new Pen(Color.LightYellow, 2), new Rectangle(x, y, s, s));
                }
            }
            //显示当前坐标
            g.DrawString($"[{CurrentMouseGridXY.X},{CurrentMouseGridXY.Y}]", new Font(FontFamily.GenericSansSerif, 12), Brushes.LightGreen, DisplayArea.Left, DisplayArea.Top);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            DisplayArea.Width = ClientSize.Width - 20;
            DisplayArea.Height = ClientSize.Height - 20;
            Invalidate();
        }
        #endregion

        #region UI


        public FormEditor()
        {
            InitializeComponent();
            InitGrid();
            InitForm();
        }

        void InitForm()
        {
            //this.ClientSize = new Size(DisplayArea.X + DisplayArea.Width + 50, DisplayArea.Y + DisplayArea.Height + 50);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;

            DlgSaveFile.DefaultExt = "*.xml";
            DlgSaveFile.AddExtension = true;
            DlgSaveFile.Filter = "xml文件|*.xml";

            DlgFileOpen.InitialDirectory = Directory.GetCurrentDirectory();

            DisplayArea = new Rectangle(10, 10, ClientSize.Width-20, ClientSize.Height-20);
        }
        //初始化网格
        void InitGrid()
        {
            for (int i = 0; i < kGridNumX; i++)
            {
                for (int j = 0; j < kGridNumY; j++)
                {
                    AllGrids[i, j] = new Grid()
                    {
                        rectAngle = new Rectangle(DisplayStartPos.X + i * nGridShowSize, DisplayStartPos.Y + j * nGridShowSize, nGridShowSize, nGridShowSize),
                        gridX = i,
                        gridY = j,
                    };
                }
            }
        }

        void Reset()
        {
            foreach (var g in AllGrids)
            {
                g.floors.Clear();
            }
            TheImage = null;
            DataHelper.Instance.CurrentMapFloor = null;
            DataHelper.Instance.CurrentMapArea = null;
        }

        public Grid GetGridByXY(int x, int y)
        {
            y = nImageGridNumY - y;
            return AllGrids[x, y];
        }

        public void InvalidateUI()
        {
            Invalidate(DisplayArea);
        }

        //         public void SetNPCNum(int num)
        //         {
        //             lblNumNPC.Text = "NPC数量" + num.ToString();
        //         }
        //         public void SetTeleportNum(int num)
        //         {
        //             lblNumTeleport.Text = "传送点数量" + num.ToString();
        //         }
        //         public void SetMonsterNum(int num)
        //         {
        //             lblNumMonster.Text = "怪区数量" + num.ToString();
        //         }
        // 
        //         public void SetSceneObjNum(int num)
        //         {
        //             lblNumSceneObj.Text = "摆放物数量" + num.ToString();
        //         }


        #endregion

        #region 鼠标操作
        //鼠标笔刷外观矩形
        Rectangle GridPenRect = new Rectangle(0, 0, 0, 0);
        Point CurrentMouseGridXY = new Point(0, 0);
        Grid GetGridByMousePoint(int x, int y)
        {
            if (nGridShowSize == 0)
            {
                return null;
            }
            int gridx = (x - DisplayArea.X + ImageStartPos.X) / nGridShowSize;
            int gridy = (y - DisplayArea.Y + ImageStartPos.Y) / nGridShowSize;
            if (gridx < 0 || gridx >= kGridNumX || gridy < 0 || gridy >= kGridNumY) return null;

            //画刷矩形
            GridPenRect.Width = nGridShowSize * (GridPenWidth * 2 + 1);
            GridPenRect.Height = GridPenRect.Width;
            GridPenRect.X = x - GridPenRect.Width / 2;
            GridPenRect.Y = y - GridPenRect.Height / 2;

            //ormMain.Instance.fmDashboard.ShowCoordinate(gridx, nImageGridNumY - gridy);
            CurrentMouseGridXY.X = gridx;
            CurrentMouseGridXY.Y = nImageGridNumY - gridy;
            return AllGrids[gridx, gridy];
        }

        //鼠标拖动的相对起点
        Point MoveStartPos = new Point();
        Size MoveOffset = new Size();
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                //左键点击修改拖动起点
                MoveStartPos.X = e.X;
                MoveStartPos.Y = e.Y;
            }
            else if (e.Button == MouseButtons.Right)
            {
                var CurrentMapFloor = DataHelper.Instance.CurrentMapFloor;
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
                int x = SelectGrid.gridX;
                int y = SelectGrid.gridY;
                //选择一片区域
                if (DataHelper.Instance.CurrentMapArea is MonsterArea)
                {
                    //UIHelper.Instance.FormMonster.SetXY(x, nImageGridNumY - y, SelectGrid.areaId);
                    CurrentMapFloor.X = x;
                    CurrentMapFloor.Y = nImageGridNumY - y;
                    (CurrentMapFloor as MonsterFloor).PursuitId = SelectGrid.areaId;
                    GridPenWidth = CurrentMapFloor.Radius;
                }
                else if (DataHelper.Instance.CurrentMapArea is BornArea)
                {
                    //UIHelper.Instance.FormBorn.SetXY(x, nImageGridNumY - y);
                    CurrentMapFloor.X = x;
                    CurrentMapFloor.Y = nImageGridNumY - y;

                    GridPenWidth = (CurrentMapFloor).Radius;
                }
                else if (DataHelper.Instance.CurrentMapArea is TransportArea)
                {
                    //UIHelper.Instance.FormTeleport.SetXY(x, nImageGridNumY - y);
                    CurrentMapFloor.X = x;
                    CurrentMapFloor.Y = nImageGridNumY - y;

                    GridPenWidth = (CurrentMapFloor as TransportFloor).Radius;
                }
                else if (DataHelper.Instance.CurrentMapArea is NPCArea)
                {
                    //UIHelper.Instance.FormNPC.SetXY(x, nImageGridNumY - y);
                    CurrentMapFloor.X = x;
                    CurrentMapFloor.Y = nImageGridNumY - y;

                    GridPenWidth = 0;
                }
                else if (DataHelper.Instance.CurrentMapArea is SceneObjArea)
                {
                    //UIHelper.Instance.FormSceneobj.SetXY(x, nImageGridNumY - y);
                    CurrentMapFloor.X = x;
                    CurrentMapFloor.Y = nImageGridNumY - y;

                    GridPenWidth = 0;
                    var so = DataHelper.Instance.CurrentMapFloor as SceneObjFloor;
                    var plist = so.GetPointList();
                    so.RandXYGrids.Add(AllGrids[x, y]);

                    foreach (var pt in plist)
                    {
                        Grid grid = AllGrids[x + pt.pt.X, y + pt.pt.Y];
                        grid.tableX = pt.pt.X;
                        grid.tableY = pt.pt.Y;
                        if (Control.ModifierKeys == Keys.Shift)
                        {
                            CancelGrid(grid, DataHelper.Instance.CurrentMapFloor);
                        }else
                        {
                            OnSelectGrid(grid, DataHelper.Instance.CurrentMapFloor);
                        }
                    }
                    InvalidateUI();
                    return;
                }
                else if (DataHelper.Instance.CurrentMapArea is NavigateArea)
                {
                    GridPenWidth = 0;
                }
                //安全区、阻挡区、追击区、碰撞区不处理

                int Radius = GridPenWidth;


                for (int i = -Radius; i <= Radius; i++)
                {
                    for (int j = -Radius; j <= Radius; j++)
                    {
                        if (x + i >= 0 && x + i < kGridNumX && y + j >= 0 && y + j < kGridNumY)
                        {
                            Grid grid = AllGrids[x + i, y + j];
                            if (Control.ModifierKeys == Keys.Shift)
                            {
                                CancelGrid(grid, DataHelper.Instance.CurrentMapFloor);
                            }
                            else
                            {
                                OnSelectGrid(grid, DataHelper.Instance.CurrentMapFloor);
                            }
                        }
                    }
                }

                InvalidateUI();
            }

        }

        //从xml加载的时候专用
        public void MarkGridWithRadius(int x, int y, int Radius, object floor)
        {
            var f = floor as MapFloor;
            //xml中的y是反转后的值
            y = nImageGridNumY - y;
            for (int i = -Radius; i <= Radius; i++)
            {
                for (int j = -Radius; j <= Radius; j++)
                {
                    if (x + i >= 0 && x + i < kGridNumX && y + j >= 0 && y + j < kGridNumY)
                    {
                        Grid grid = AllGrids[x + i, y + j];
                        OnSelectGrid(grid, f);
                    }
                }
            }

        }

        //从xml加载的时候用
        public void MarkGridWithList(int x, int y, List<SceneObjPoint> plist, object floor)
        {
            var f = floor as MapFloor;
            //y = nImageGridNumY - y;

            foreach (var p in plist)
            {
                int gX = x + p.pt.X;
                int gY = y - p.pt.Y;
                if (gX >= 0 && gX < kGridNumX && gY >= 0 && gY < kGridNumY)
                {
                    Grid grid = AllGrids[gX, gY];
                    grid.tableX = p.pt.X;
                    grid.tableY = p.pt.Y;
                    OnSelectGrid(grid, f);
                }

            }
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
            if (floor.belongArea is MonsterPursuitArea)
            {
                SelectGrid.areaId = floor.ID;
                //给怪物区加上areaid
                foreach(var mz in DataHelper.Instance.AllMonsterZoneData.floors.Values)
                {
                    if(mz.X == SelectGrid.gridX && mz.Y == nImageGridNumY - SelectGrid.gridY)
                    {
                        (mz as MonsterFloor).PursuitId = floor.ID;
                    }
                }
            }

            if (floor is Path)
            {
                Path _path = floor as Path;
                _path.OnDrawKeyPoint(SelectGrid);
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

        //把当前图层移动到中心(按住ctrl)
        public void MoveToCenter(int gridX, int gridY)
        {
            if (ModifierKeys != Keys.Control)
            {
                return;
            }
            gridY = nImageGridNumY - gridY;
            int centerX = DisplayArea.Width / 2 / nGridShowSize;
            int centerY = DisplayArea.Height / 2 / nGridShowSize;

            MoveOffset.Width = (centerX - gridX) * nGridShowSize;
            MoveOffset.Height = (centerY - gridY) * nGridShowSize;

            //所有网格位移重置
            ChangeGridSize();

            ImageStartPos.X = 0;
            ImageStartPos.Y = 0;

            CalcMapMove();
            InvalidateUI();
        }

        //原地缩放，以鼠标所在格子为不动点
        void MoveToPoint(int gridX, int gridY, int pX, int pY)
        {
            int offX = pX - DisplayArea.X;
            int offY = pY - DisplayArea.Y;
            MoveOffset.Width = offX - gridX * nGridShowSize;
            MoveOffset.Height = offY - gridY * nGridShowSize;

            //所有网格位移重置
            ChangeGridSize();

            ImageStartPos.X = 0;
            ImageStartPos.Y = 0;

            CalcMapMove();
            InvalidateUI();

        }

        void CalcMapMove()
        {
            //不得拖动超过地图范围
            if (ImageStartPos.X - MoveOffset.Width + DisplayArea.Width > ShowImage.Width) MoveOffset.Width = ImageStartPos.X - ShowImage.Width + DisplayArea.Width;
            if (ImageStartPos.Y - MoveOffset.Height + DisplayArea.Height > ShowImage.Height) MoveOffset.Height = ImageStartPos.Y - ShowImage.Height + DisplayArea.Height;
            if (ImageStartPos.X - MoveOffset.Width < 0) MoveOffset.Width = ImageStartPos.X;
            if (ImageStartPos.Y - MoveOffset.Height < 0) MoveOffset.Height = ImageStartPos.Y;

            ImageStartPos.X -= MoveOffset.Width;
            ImageStartPos.Y -= MoveOffset.Height;

            //拖动最终更改的是图片起始绘图位置
            foreach (var grid in AllGrids)
            {
                grid.rectAngle.X += this.MoveOffset.Width;
                grid.rectAngle.Y += this.MoveOffset.Height;
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
            //左键拖动
            if (e.Button == MouseButtons.Left)
            {
                this.MoveOffset.Width = e.X - MoveStartPos.X;
                this.MoveOffset.Height = e.Y - MoveStartPos.Y;
                MoveStartPos.X = e.X;
                MoveStartPos.Y = e.Y;

                CalcMapMove();
            }
            else if (e.Button == MouseButtons.Right)
            {
                var CurrentMapFloor = DataHelper.Instance.CurrentMapFloor;
                if (CurrentMapFloor == null)
                {
                    MessageBox.Show("必须选择一个图层");
                    return;
                }

                Grid SelectGrid = GetGridByMousePoint(e.X, e.Y);
                //使用笔刷
                int x = SelectGrid.gridX;
                int y = SelectGrid.gridY;
                if (CurrentMapFloor is Path)
                {
                    GridPenWidth = 0;
                }

                int Radius = GridPenWidth;
                for (int i = -Radius; i <= Radius; i++)
                {
                    for (int j = -Radius; j <= Radius; j++)
                    {
                        if (x + i >= 0 && x + i < kGridNumX && y + j >= 0 && y + j < kGridNumY)
                        {
                            Grid grid = AllGrids[x + i, y + j];
                            if (ModifierKeys == Keys.Shift)
                            {
                                CancelGrid(grid, DataHelper.Instance.CurrentMapFloor);
                            }
                            else
                            {
                                OnSelectGrid(grid, DataHelper.Instance.CurrentMapFloor);
                            }
                        }
                    }
                }

            }

            if (DataHelper.Instance.CurrentMapFloor is Path)
            {
                var _path = DataHelper.Instance.CurrentMapFloor as Path;
                _path.CurrentMousePoint = new Point(e.X, e.Y);
            }
            InvalidateUI();

        }

        int nMaxScale;
        int nMinScale;
        int nStepScale;
        FormLoading fmLoading;
        void InitImageScale()
        {
            nMaxScale = 100 * 100 / nImportMapScale;
#if DEBUG
            nMaxScale = 100;
#endif
            nMinScale = 10 * 100 / nImportMapScale;
            nStepScale = nMinScale;

            fmLoading = new FormLoading();
            fmLoading.Show(this);
            BackgroundWorker loader = new BackgroundWorker();
            loader.WorkerReportsProgress = true;
            loader.DoWork += Loader_DoWork;
            loader.ProgressChanged += Loader_ProgressChanged;
            loader.RunWorkerCompleted += Loader_RunWorkerCompleted;
            loader.RunWorkerAsync(TheImage.Clone());
        }

        private void Loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            fmLoading.Close();
        }

        private void Loader_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            fmLoading.SetValue(e.ProgressPercentage);
        }

        void Loader_DoWork(object sender, DoWorkEventArgs e)
        {
            var bw = sender as BackgroundWorker;
            Image OriginImage = e.Argument as Image;
            for (int i = nMinScale; i <= nMaxScale; i += nStepScale)
            {
                if (i == 100) continue;

                //先缩放原图A得到图B，保存图像A，然后另存一份图像B到内存 拖动图B就没有误差了
                int pickedWidth = OriginImage.Width * i / 100;
                int pickedHeight = OriginImage.Height * i / 100;
                //图片处理成低像素格式
                var pickedImage = new Bitmap(pickedWidth, pickedHeight, PixelFormat.Format16bppRgb565);
                Graphics g = Graphics.FromImage(pickedImage);
                g.InterpolationMode = InterpolationMode.Default;
                g.SmoothingMode = SmoothingMode.Default;
                g.DrawImage(OriginImage, new Rectangle(0, 0, pickedWidth, pickedHeight), new Rectangle(0, 0, OriginImage.Width, OriginImage.Height), GraphicsUnit.Pixel);
                var img = pickedImage;
                g.Dispose();

                AllPickedImage.Add(i, img);

                GC.Collect();
                GC.WaitForPendingFinalizers();

                bw.ReportProgress(i * 100 / nMaxScale);
            }
            OriginImage.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            bw.ReportProgress(100);

        }
        //缩放比例(可以看，但是编辑可能精度误差)
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (ModifierKeys != Keys.Alt)
            {
                return;
            }

            //缩放之前计算grid坐标
            Grid grid = GetGridByMousePoint(e.X, e.Y);
            if (grid == null) return;
            int gx = grid.gridX;
            int gy = grid.gridY;

            nGridScale += e.Delta / 120 * nStepScale;
            if (nGridScale > nMaxScale)
            {
                nGridScale = nMaxScale;
            }
            if (nGridScale < nMinScale)
            {
                nGridScale = nMinScale;
            }


            nGridShowSize = (int)(nGridImportSize * nGridScale / 100f);
            ChangeGridSize();
            ChangeImageSize();

            //缩放完成后移动到原来的位置
            MoveToPoint(gx, gy, e.X, e.Y);


        }


        #endregion

        #region 地图比例
        private void ChangeGridSize()
        {
            //修改网格
            for (int i = 0; i < kGridNumX; i++)
            {
                for (int j = 0; j < kGridNumY; j++)
                {
                    Grid grid = AllGrids[i, j];
                    grid.rectAngle.X = DisplayStartPos.X + i * nGridShowSize;
                    grid.rectAngle.Y = DisplayStartPos.Y + j * nGridShowSize;
                    grid.rectAngle.Width = nGridShowSize;
                    grid.rectAngle.Height = nGridShowSize;
                }
            }

            //FormMain.Instance.fmDashboard.ShowImageScale(string.Format("当前缩放：{0}%[{1}%-{2}%]", nGridScale, 110 - nImportMapScale, 200 - nImportMapScale));
        }


        private void ChangeImageSize()
        {
            //图片移动到原点
            ImageStartPos.X = 0;
            ImageStartPos.Y = 0;

            if (AllPickedImage.ContainsKey(nGridScale))
            {
                ShowImage = AllPickedImage[nGridScale];

                //显示区宽高缩放
                int pickedWidth = TheImage.Width * nGridScale / 100;
                int pickedHeight = TheImage.Height * nGridScale / 100;

                if (pickedWidth < DisplayArea.Width)
                {
                    DisplayArea.Width = pickedWidth;
                }
                else if (pickedWidth > DisplayArea.Width)
                {
                    DisplayArea.Width = Math.Min(pickedWidth, ClientSize.Width-20);
                }

                if (pickedHeight < DisplayArea.Height)
                {
                    DisplayArea.Height = pickedHeight;
                }
                else if (pickedHeight > DisplayArea.Height)
                {
                    DisplayArea.Height = Math.Min(pickedHeight, ClientSize.Height-20);
                }
                srcRect.Height = DisplayArea.Height;
                srcRect.Width = DisplayArea.Width;

                Invalidate();
            }
        }

        public void InitMapScale(int scale)
        {
            //计算缩放比例和网格大小
            nImportMapScale = scale;
            nGridScale = scale;
            nGridShowSize = kGridSize * scale / 100;
            nGridImportSize = nGridShowSize;
            //lblMapScale.Text = string.Format("地图比例：{0}%", scale);
            nImageGridNumY = TheImage.Height / nGridShowSize - 1;
            //DataHelper.Instance.MapHeight = TheImage.Height * 100 / scale;
            //DataHelper.Instance.MapWidth = TheImage.Width * 100 / scale;
            ChangeGridSize();

            //初始化所有比例的图片
            InitImageScale();

            ImageStartPos.X = 0;
            ImageStartPos.Y = 0;

            InvalidateUI();

        }

        public int GetImageGridNumY()
        {
            return nImageGridNumY;
        }


        #endregion


        #region 项目
        public void ClickOpenProject(object sender, EventArgs e)
        {
            DlgFileOpen.Filter = "xml文件|*.xml";
            DialogResult r = DlgFileOpen.ShowDialog();
            if (r == DialogResult.OK)
            {
                Reset();
                XmlHelper.Instance.LoadProject(DlgFileOpen.FileName, AllGrids);
                InvalidateUI();
            }
        }

        string sProjectFile = null;
        //记录保存位置
        public void ClickSaveProject(object sender, EventArgs e)
        {
            //SaveProjectEventArgs arg = e as SaveProjectEventArgs;
            //string sProjectFile = arg.FileName;
            if (sProjectFile == null || !File.Exists(sProjectFile))
            {
                DlgSaveFile.FileName = "project.xml";
                DialogResult r = DlgSaveFile.ShowDialog();
                if (r != DialogResult.OK)
                {
                    return;
                }
                sProjectFile = DlgSaveFile.FileName;
            }

            TranslateAllGrid();
            XmlHelper.Instance.SaveProject(sProjectFile, AllGrids);
            TranslateAllGrid();
            MessageBox.Show("保存成功！");

        }

        public void MenuClick_OpenMap(object sender, EventArgs e)
        {
            OpenMapEventArgs arg = e as OpenMapEventArgs;
            DlgFileOpen.Filter = "png|*.png|jpg|*.jpg|bmp|*.bmp|所有文件|*.*";
            DialogResult r = DlgFileOpen.ShowDialog();
            if (r == DialogResult.OK)
            {
                if (File.Exists(DlgFileOpen.FileName))
                {
                    LoadImage(DlgFileOpen.FileName);
                    XmlHelper.Instance.ImageFileName = DlgFileOpen.FileName;
                    InitMapScale(arg.MapScale);
                }
            }
        }

        public void TranslateAllGrid()
        {
            //绘图坐标和地图坐标Y方向相反，需要反转一下
            foreach (var grid in AllGrids)
            {
                grid.gridY = nImageGridNumY - grid.gridY;
            }
        }


        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            //保存的快捷键
            if (e.KeyCode == Keys.S && ModifierKeys == Keys.Control)
            {
                ClickSaveProject(null, new SaveProjectEventArgs { FileName=XmlHelper.Instance.ProjectName+".xml"});
                return;
            }
            else if (e.KeyCode == Keys.E && e.Control)
            {
                ClickOpenProject(null, null);
                return;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (DataHelper.Instance.CurrentMapFloor is Path)
                {
                    var _path = DataHelper.Instance.CurrentMapFloor as Path;
                    _path.IsDrawing = false;
                    _path.LastMousePoint.X = 0;
                }
                return;
            }

            //快速切换笔刷
            if (e.KeyCode == Keys.D1)
            {
                ChangePenWidth1x1(null, null);
            }
            else if (e.KeyCode == Keys.D3)
            {
                ChangePenWidth3x3(null, null);
            }
            else if (e.KeyCode == Keys.D5)
            {
                ChangePenWidth5x5(null, null);
            }
            else if (e.KeyCode == Keys.D7)
            {
                ChangePenWidth7x7(null, null);
            }

            if (DataHelper.Instance.CurrentMapArea != null &&
                DataHelper.Instance.CurrentMapArea.isOneGridPenArea)
            {
                ChangePenWidth1x1(null, null);
            }

        }

        public void LoadImage(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                return;

            TheImage = Image.FromFile(filename);

            //图片处理成低像素格式
            var pickedImage = new Bitmap(TheImage.Width, TheImage.Height, PixelFormat.Format16bppRgb565);
            Graphics g = Graphics.FromImage(pickedImage);
            g.InterpolationMode = InterpolationMode.Low;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.DrawImage(TheImage, new Rectangle(0, 0, TheImage.Width, TheImage.Height), new Rectangle(0, 0, TheImage.Width, TheImage.Height), GraphicsUnit.Pixel);
            //释放原图
            TheImage.Dispose();
            TheImage = pickedImage;
            //释放绘图资源
            g.Dispose();
            //真正绘图图片
            ShowImage = TheImage;

            //100%缩放
            AllPickedImage.Add(100, ShowImage);

            ChangeImageSize();
            ////计算范围
            //int height = DisplayArea.Height;
            //if (height > TheImage.Height)
            //{
            //    height = TheImage.Height;
            //}
            //int width = DisplayArea.Width;
            //if (width > TheImage.Width)
            //{
            //    width = TheImage.Width;
            //}



            InvalidateUI();
        }


        #endregion

        #region 笔刷
        private void ChangePenWidth1x1(object sender, EventArgs e)
        {
            GridPenWidth = 0;
        }

        private void ChangePenWidth3x3(object sender, EventArgs e)
        {
            GridPenWidth = 1;
        }

        private void ChangePenWidth5x5(object sender, EventArgs e)
        {
            GridPenWidth = 2;
        }

        private void ChangePenWidth7x7(object sender, EventArgs e)
        {
            GridPenWidth = 3;
        }

        private void tabEditor_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(e);
        }
        #endregion

        #region 图层显隐
        private void 阻挡区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllObsData.Show = item.Checked;
            InvalidateUI();

        }

        private void 安全区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllAQQData.Show = item.Checked;
            InvalidateUI();

        }

        private void 碰撞区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllCollisionAreaData.Show = item.Checked;
            InvalidateUI();

        }

        private void 触发区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllTriggerAreaData.Show = item.Checked;
            InvalidateUI();

        }

        private void 导航线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllNavigatePathData.Show = item.Checked;
            InvalidateUI();

        }

        private void 巡逻线ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllPatrolArea.Show = item.Checked;
            InvalidateUI();

        }

        private void 怪物区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllMonsterZoneData.Show = item.Checked;
            InvalidateUI();

        }

        private void nPC点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllNPCData.Show = item.Checked;
            InvalidateUI();

        }

        private void 摆放物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllSceneObjAreaData.Show = item.Checked;
            InvalidateUI();

        }

        private void 追击区ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllPursuitData.Show = item.Checked;
            InvalidateUI();

        }

        private void 出生点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllBornData.Show = item.Checked;
            InvalidateUI();

        }

        private void 传送点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var item = sender as ToolStripMenuItem;
            DataHelper.Instance.AllTeleportData.Show = item.Checked;
            InvalidateUI();

        }
        #endregion

    }
}
