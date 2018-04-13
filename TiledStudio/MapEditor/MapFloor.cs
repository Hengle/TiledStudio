using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace TiledStudio
{
    #region 图层
    public class MapFloor
    {
        public MapFloor(MapArea parent)
        {
            belongArea = parent;
        }
        [Browsable(false)]
        public TreeNode TreeViewNode;
        [Browsable(false)]
        public MapArea belongArea;
        [Category("常规"), DisplayName("图层ID")]
        public int ID { get; set; }

        [Category("常规"), DisplayName("图层深度")]
        public int Depth { get; set; }

        private Color floorcolor;
        [Category("常规"), DisplayName("图层颜色")]
        public Color FloorColor
        {
            get { return floorcolor; }
            set { floorcolor = Color.FromArgb(128, value); if(TreeViewNode!=null) TreeViewNode.ForeColor = value; }
        }

        private string text;
        [Category("常规"), DisplayName("图层名字")]
        public string Text
        {
            get { return text; }
            set { text = value; if (TreeViewNode != null) TreeViewNode.Text = value; }
        }
        [Category("常规"), DisplayName("X坐标")]
        public int X { get; set; }
        [Category("常规"), DisplayName("Y坐标")]
        public int Y { get; set; }
        [Category("常规"), DisplayName("区域半径")]
        public int Radius { get; set; }
        [Category("常规"), DisplayName("摆放方向")]
        public int Dir { get; set; }

        public List<Grid> grids = new List<Grid>();

        public virtual void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }
    }

    class NPCFloor : MapFloor
    {
        public NPCFloor(MapArea parent) : base(parent) { }

        [Category("NPC"), DisplayName("NPCID")]
        [TypeConverter(typeof(NPCListConverter))]
        public string SelectNPC
        {
            get
            {
                if (NPCID == 0) return null;
                return XmlHelper.Instance.AllNpcsConfig[NPCID].ToString();
            }

            set
            {
                string[] x = value.Split(' ');
                NPCID = int.Parse(x[0]);
                Text = belongArea.Text + ID.ToString() + " " + x[1];
            }
        }

        [Browsable(false)]
        public int NPCID { get; set; }

        public override void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }

    }

    class MonsterFloor : MapFloor
    {
        public MonsterFloor(MapArea parent) : base(parent) { }

        [Category("怪物"), DisplayName("怪物ID")]
        [TypeConverter(typeof(MonsterListConverter))]
        public string SelectMonster
        {
            get
            {
                if (MonsterId == 0) return null;
                return XmlHelper.Instance.AllMonstersConfig[MonsterId].ToString();
            }

            set
            {
                string[] x = value.Split(' ');
                MonsterId = int.Parse(x[0]);
                Text = belongArea.Text + ID.ToString() + " " + x[1];
            }
        }

        [Browsable(false)]
        public int MonsterId { get; set; }
        [Category("怪物"), DisplayName("怪物数量")]
        public int MonsterNum { get; set; }
        [Category("怪物"), DisplayName("追击区域")]
        public int PursuitId { get; set; }

        public override void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }

    }

    class TransportFloor : MapFloor
    {
        public TransportFloor(MapArea parent) : base(parent) { }
        public int toMapId { get; set; }
        public int tox { get; set; }
        public int toy { get; set; }
        public int todir { get; set; }
        public string tip { get; set; }

        public override void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }

    }


    class PathPoint
    {
        public int PathPointId;
        public Grid grid;
        public List<PathPoint> neighbors;
    }
    class PathEdge
    {
        public Grid StartGrid;
        public Grid EndGrid;
    }
    class Path : MapFloor
    {
        public Path(MapArea parent) : base(parent) { }

        public override void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }

        private int _autoId = 0;

        private int AutoPathPointId
        {
            get { return _autoId++; }
        }

        public List<PathPoint> AllPathPoints = new List<PathPoint>();
        public List<PathEdge> AllPathEdges = new List<PathEdge>();
        private PathEdge CurrentEdge = new PathEdge();
        public bool IsDrawing = false;
        public Point LastMousePoint = new Point(0, 0);
        public Point CurrentMousePoint;

        public bool IsClosePath { get; set; }

        private PathPoint GetPathPointByGrid(Grid grid)
        {
            foreach (var qq in AllPathPoints)
            {
                if (qq.grid == grid)
                {
                    return qq;
                }
            }
            var pp = new PathPoint()
            {
                PathPointId = AutoPathPointId,
                grid = grid,
                neighbors = new List<PathPoint>()
            };

            AllPathPoints.Add(pp);
            return pp;

        }

        private void AddCurrentEdge()
        {
            var pstart = GetPathPointByGrid(CurrentEdge.StartGrid);
            var pend = GetPathPointByGrid(CurrentEdge.EndGrid);
            pstart.neighbors.Add(pend);
            pend.neighbors.Add(pstart);

            AllPathEdges.Add(new PathEdge()
            {
                StartGrid = CurrentEdge.StartGrid,
                EndGrid = CurrentEdge.EndGrid
            });
        }

        public void OnDrawKeyPoint(Grid grid)
        {
            if (IsDrawing)
            {
                CurrentEdge.EndGrid = grid;
                AddCurrentEdge();
                CurrentEdge = new PathEdge();
                CurrentEdge.StartGrid = grid;
            }
            else
            {
                CurrentEdge.StartGrid = grid;
                IsDrawing = true;
            }

            LastMousePoint = grid.CenterPoint;

        }

    }


    public class SceneObjPoint
    {
        public Point pt;
        public bool isTrigger;

        public SceneObjPoint(int x, int y, bool b)
        {
            pt.X = x;
            pt.Y = y;
            isTrigger = b;
        }
    }

    class SceneObjFloor : MapFloor
    {
        public SceneObjFloor(MapArea parent) : base(parent) { }
        public SceneObjConfigItem SelectItem;

        [Category("场景元素"), DisplayName("场景元素ID")]
        [TypeConverter(typeof(SceneObjListConverter))]
        public string SelectMonster
        {
            get
            {
                if (objID == 0) return null;
                return XmlHelper.Instance.AllSceneObjsConfig[objID].ToString();
            }

            set
            {
                string[] x = value.Split(' ');
                objID = int.Parse(x[0]);
                Text = belongArea.Text + ID.ToString() + " " + x[1];
            }
        }

        [Browsable(false)]
        public int objID { get; set; }
        [Category("场景元素"), DisplayName("随机总数")]
        public int num { get; set; }
        [Category("场景元素"), DisplayName("是否随机")]
        public bool isRandObj { get; set; }
        public List<Grid> RandXYGrids = new List<Grid>();


        public List<SceneObjPoint> GetPointList()
        {
            return SelectItem.plist[Dir];
        }

        public override void SetPropertyObject(PropertyGrid p)
        {
            p.SelectedObject = this;
        }


    }

    #endregion
}
