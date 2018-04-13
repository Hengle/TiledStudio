using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;

namespace TiledStudio
{
    enum NodeShapeType
    {
        圆角矩形,
        直角矩形,
        椭圆形,
        菱形,//菱形
        六边形,//六边形
    }

    class AINode
    {
        //public PropertyGridManager AINodePropertyGrid = new PropertyGridManager();
        #region 属性
        public static PropertyGrid AIProperty;
        //绘图设备
        [Browsable(false)]
        Graphics nodeGraphics { get; set; }
        //定位矩形，拖动缩放自动布局都要用
        [Browsable(false)]
        public Rectangle NodeOutRect = new Rectangle();
        //节点自己的外接矩形
        [Browsable(false)]
        public Rectangle SelfOutRect = new Rectangle();
        //子节点
        [Browsable(false)]
        public LinkedList<AINode> Children = new LinkedList<AINode>();
        [Browsable(false)]
        public LinkedListNode<AINode> MyNodePtr = null;
        //父节点
        [Browsable(false)]
        public AINode Parent { get; set; }
        //同级节点的垂直间距       
        public const int VMargen = 20;
        //父子节点的水平间距
        public const int HMargen = 50;
        //接入点
        Point _ConnectInPos = new Point(0, 0);
        [Browsable(false)]
        public Point ConnectInPos
        {
            get
            {
                _ConnectInPos.X = SelfOutRect.Left;
                _ConnectInPos.Y = SelfOutRect.Top + SelfOutRect.Height / 2;
                return _ConnectInPos;
            }
        }
        //接出点
        Point _ConnectOutPos = new Point(0, 0);
        [Browsable(false)]
        public Point ConnectOutPos
        {
            get
            {
                _ConnectOutPos.X = SelfOutRect.Right;
                _ConnectOutPos.Y = SelfOutRect.Top + SelfOutRect.Height / 2;
                return _ConnectOutPos;
            }
        }

        string _nodeName;
        [Category("外观"), DisplayName("名字")]
        public string NodeName
        {
            get { return _nodeName; }
            set
            {
                _nodeName = value;
                FormMain.Instance.fmCanvas?.Invalidate();
            }
        }

        Font _textFont = SystemFonts.DefaultFont;
        [Category("外观"), DisplayName("字体")]
        public Font textFont
        {
            get { return _textFont; }
            set { _textFont = value; FormMain.Instance.fmCanvas?.Invalidate(); }
        }

        NodeShapeType _nodeShape = NodeShapeType.圆角矩形;
        [Category("外观"), DisplayName("形状")]
        public NodeShapeType NodeShape
        {
            get { return _nodeShape; }
            set { _nodeShape = value; FormMain.Instance.fmCanvas?.Invalidate(); }
        }

        Color _nodeColor = Color.White;
        [Category("外观"), DisplayName("颜色")]
        public Color NodeColor
        {
            get { return _nodeColor; }
            set { _nodeColor = value; FormMain.Instance.fmCanvas?.Invalidate(); }
        }

        string _aiNodeType = null;
        [Category("公共"), DisplayName("节点类型")]
        [TypeConverter(typeof(AINodeTypeConverter))]
        public string NodeType
        {
            get { return _aiNodeType; }
            set
            {
                _aiNodeType = value;

                string s = AITree.allNodeTypes[value];
                if (s != "con" && s != "bsel" && s != "act")
                {
                    NodeName = value;
                }
                if (s == "con"|| s == "bsel")
                {
                    AIHelper.SetPropertyVisibility(this, "ConditionType", true);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam1", true);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam2", true);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam3", true);
                }
                else
                {
                    AIHelper.SetPropertyVisibility(this, "ConditionType", false);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam1", false);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam2", false);
                    AIHelper.SetPropertyVisibility(this, "ConditionParam3", false);
                }
                if (s == "act")
                {
                    AIHelper.SetPropertyVisibility(this, "ActionType", true);
                    AIHelper.SetPropertyVisibility(this, "ActionParam1", true);
                    AIHelper.SetPropertyVisibility(this, "ActionParam2", true);
                    AIHelper.SetPropertyVisibility(this, "ActionParam3", true);
                }
                else
                {
                    AIHelper.SetPropertyVisibility(this, "ActionType", false);
                    AIHelper.SetPropertyVisibility(this, "ActionParam1", false);
                    AIHelper.SetPropertyVisibility(this, "ActionParam2", false);
                    AIHelper.SetPropertyVisibility(this, "ActionParam3", false);
                }
                if (Parent != null)
                {
                    //选中自己的时候判定一下节点类型
                    AIHelper.SetPropertyVisibility(this, "BSelectValue", Parent.NodeType == "bsel");
                    AIHelper.SetPropertyVisibility(this, "PSelectWeight", Parent.NodeType == "psel");
                }

                AIProperty.SelectedObject = this;
            }
        }

        [Browsable(false)]
        [Category("公共"), DisplayName("psel权重")]
        public int PSelectWeight { get; set; }
        [Browsable(false)]
        [Category("公共"), DisplayName("bsel值")]
        public bool BSelectValue { get; set; }

        //条件节点
        string _conditionType;
        [Browsable(false), Category("条件节点"), DisplayName(" 条件类型")]
        [TypeConverter(typeof(AINodeConditionConverter))]
        public string ConditionType
        {
            get { return _conditionType; }
            set
            {
                _conditionType = value;
                NodeName = GetDiscription(AITree.allConditions[value], _conditionParams);
                //todo 反射得到成员变量？
            }
        }

        float[] _conditionParams = new float[3];
        [Browsable(false), Category("条件节点"), DisplayName("参数1")]
        public float ConditionParam1
        {
            get { return _conditionParams[0]; }
            set
            {
                _conditionParams[0] = value;
                ConditionType = ConditionType;
            }
        }
        [Browsable(false), Category("条件节点"), DisplayName("参数2")]
        public float ConditionParam2
        {
            get { return _conditionParams[1]; }
            set
            {
                _conditionParams[1] = value;
                ConditionType = ConditionType;
            }
        }
        [Browsable(false), Category("条件节点"), DisplayName("参数3")]
        public float ConditionParam3
        {
            get { return _conditionParams[2]; }
            set
            {
                _conditionParams[2] = value;
                ConditionType = ConditionType;
            }
        }

        //动作节点
        string _actionType;
        [Browsable(false), Category("动作节点"), DisplayName(" 动作类型")]
        [TypeConverter(typeof(AINodeActionConverter))]
        public string ActionType
        {
            get { return _actionType; }
            set
            {
                _actionType = value;
                NodeName = GetDiscription(AITree.allActions[value], _actionParams);
            }
        }

        float[] _actionParams = new float[3];
        [Browsable(false), Category("动作节点"), DisplayName("参数1")]
        public float ActionParam1
        {
            get { return _actionParams[0]; }
            set
            {
                _actionParams[0] = value;
                ActionType = ActionType;
            }
        }
        [Browsable(false), Category("动作节点"), DisplayName("参数2")]
        public float ActionParam2
        {
            get { return _actionParams[1]; }
            set
            {
                _actionParams[1] = value;
                ActionType = ActionType;
            }
        }
        [Browsable(false), Category("动作节点"), DisplayName("参数3")]
        public float ActionParam3
        {
            get { return _actionParams[2]; }
            set
            {
                _actionParams[2] = value;
                ActionType = ActionType;
            }
        }


        #endregion

        public string GetXmlCondition()
        {
            var src = AITree.allConditions[ConditionType];
            if (src.Item1 == 0) return ConditionType;
            if (src.Item1 == 1) return $"{ConditionType},{_conditionParams[0]}";
            if (src.Item1 == 2) return $"{ConditionType},{_conditionParams[0]},{_conditionParams[1]}";
            if (src.Item1 == 3) return $"{ConditionType},{_conditionParams[0]},{_conditionParams[1]},{_conditionParams[2]}";
            return null;
        }
        public string GetXmlAction()
        {
            var src = AITree.allActions[ActionType];
            if (src.Item1 == 0) return ActionType;
            if (src.Item1 == 1) return $"{ActionType},{_actionParams[0]}";
            if (src.Item1 == 2) return $"{ActionType},{_actionParams[0]},{_actionParams[1]}";
            if (src.Item1 == 3) return $"{ActionType},{_actionParams[0]},{_actionParams[1]},{_actionParams[2]}";
            return null;
        }

        public string GetDiscription(Tuple<int,string> src, float[] par)
        {
            if (src.Item1 == 0) return src.Item2;
            if (src.Item1 == 1) return string.Format(src.Item2, par[0]);
            if (src.Item1 == 2) return string.Format(src.Item2, par[0], par[1]);
            if (src.Item1 == 3) return string.Format(src.Item2, par[0], par[1], par[2]);
            return null;
        }

        public AINode()
        {
            NodeType = "NONE";
        }

        void MeasureStringLength()
        {
            //根据字体计算自己的宽度
            var strSize = nodeGraphics.MeasureString(NodeName, textFont);
            SelfOutRect.Width = (int)strSize.Width + 8;
            SelfOutRect.Height = 30;
        }

        public void SetGraphics(Graphics g)
        {
            nodeGraphics = g;
            MeasureStringLength();
            foreach (var node in Children)
            {
                node.SetGraphics(g);
            }
        }

        public AINode SelectParent(AINode self, AINode last, int x, int y)
        {
            if(SelfOutRect.Left < x && x < SelfOutRect.Right + HMargen)
            {
                if(SelfOutRect.Top < y && y < SelfOutRect.Bottom)
                {
                    return this;
                }
                if(last == this && SelfOutRect.Top - VMargen < y && y< SelfOutRect.Bottom + VMargen)
                {
                    return this;
                }
            }
            foreach(var child in Children)
            {
                if(child == self)
                {
                    continue;
                }
                var n = child.SelectParent(self, last, x, y);
                if (n != null)
                {
                    return n;
                }
            }
            return null;
        }

        public AINode SelectNode(int x, int y)
        {
            if(SelfOutRect.Left<x&& x < SelfOutRect.Right && SelfOutRect.Top<y&& y<SelfOutRect.Bottom)
            {
                NodeType = NodeType;
                return this;
            }
            foreach (var node in Children)
            {
                var n = node.SelectNode(x, y);
                if (n != null)
                {
                    return n;
                }
            }

            return null;
        }
        
        public void AutoLayout()
        {
            //计算每个子节点的定位矩形
            int left = SelfOutRect.Right + HMargen;
            int top = SelfOutRect.Y + SelfOutRect.Height / 2 - NodeOutRect.Height/2;
            
            foreach (var node in Children)
            {
                top += node.NodeOutRect.Height / 2;
                node.SelfOutRect.X = left;
                node.SelfOutRect.Y = top - node.SelfOutRect.Height/2;
                node.AutoLayout();
                top += node.NodeOutRect.Height/2 + VMargen;
            }

        }

        public void AddNode(AINode node)
        {
            node.MyNodePtr = Children.AddLast(node);
        }

        public AINode AddNode(string name, NodeShapeType shape=NodeShapeType.圆角矩形)
        {
            AINode node = new AINode();
            node.NodeShape = shape;
            node.Parent = this;
            node.MyNodePtr =  Children.AddLast(node);
            return node;
        }

        public bool DelNode(AINode node)
        {
            foreach(var c in Children)
            {
                if(c == node)
                {
                    Children.Remove(c);
                    return true;
                }
                if(c.DelNode(node))
                {
                    return true;
                }
            }
            return false;
        }

        public static void SwapNode(AINode node, bool moveUp)
        {
            var prev = node.MyNodePtr.Previous;
            var next = node.MyNodePtr.Next;
            node.MyNodePtr.List.Remove(node.MyNodePtr);
            if (moveUp)
            {
                if (prev == null) return;
                node.MyNodePtr = prev.List.AddBefore(prev, node);
            }
            else
            {
                if (next == null) return;
                node.MyNodePtr = next.List.AddAfter(next, node);
            }
        }

        public void DrawSelf(Brush b, Pen p)
        {
            NodeShapeType shape = NodeShape;
            if (shape == NodeShapeType.圆角矩形)
            {
                ShapeHelper.FillRoundRectangle(nodeGraphics, b, SelfOutRect, 8);
                ShapeHelper.DrawRoundRectangle(nodeGraphics, p, SelfOutRect, 8);
            }
            else if(shape == NodeShapeType.椭圆形)
            {
                nodeGraphics.FillEllipse(b, SelfOutRect);
                nodeGraphics.DrawEllipse(p, SelfOutRect);
            }
            else if(shape == NodeShapeType.菱形)
            {
                ShapeHelper.FillDiamond(nodeGraphics, b, SelfOutRect);
                ShapeHelper.DrawDiamond(nodeGraphics, p, SelfOutRect);
            }
            else if(shape == NodeShapeType.直角矩形)
            {
                nodeGraphics.FillRectangle(b, SelfOutRect);
                nodeGraphics.DrawRectangle(p, SelfOutRect);
            }
            else if(shape == NodeShapeType.六边形)
            {
                ShapeHelper.FillHexagon(nodeGraphics, b, SelfOutRect);
                ShapeHelper.DrawHexagon(nodeGraphics, p, SelfOutRect);
            }
            nodeGraphics.DrawString(NodeName, textFont, Brushes.Black, new Point(SelfOutRect.X + 4, SelfOutRect.Y + 4));
        }

        public void DrawOutLine(Pen p)
        {
            var rect = new Rectangle(SelfOutRect.X - 1, SelfOutRect.Y - 1, SelfOutRect.Width + 2, SelfOutRect.Height + 2);
            ShapeHelper.DrawRoundRectangle(nodeGraphics, p, rect, 8);
        }

        public void DrawNode()
        {
            Brush b = new SolidBrush(NodeColor);
            Pen p = new Pen(Color.Black, 2);
            DrawSelf(b, p);

            //calc point
            Point begin = new Point(SelfOutRect.Right,SelfOutRect.Top+SelfOutRect.Height/2);
            Point end = new Point();
            foreach(var child in Children)
            {
                end.X = child.SelfOutRect.Left;
                end.Y = child.SelfOutRect.Top + child.SelfOutRect.Height / 2;
                ShapeHelper.DrawConnectLine(nodeGraphics, p, begin, end);
                child.DrawNode();
            }
        }

        public int CalcNodeHeight()
        {
            int totoalHeight = 0;
            if (Children.Count == 0)
            {
                totoalHeight = SelfOutRect.Height;
            }
            else
            {
                foreach (var node in Children)
                {
                    totoalHeight += node.CalcNodeHeight();
                }
                totoalHeight += (Children.Count - 1) * VMargen;

            }
            NodeOutRect.Height = totoalHeight;
            return totoalHeight;
        }

        public int CalcNodeWidth()
        {
            int totalWidth = 0;

            if (Children.Count == 0)
            {
                totalWidth = SelfOutRect.Width;
            }
            else
            {
                foreach (var node in Children)
                {
                    int w = node.CalcNodeWidth() + SelfOutRect.Width + HMargen;
                    if (w > totalWidth)
                    {
                        totalWidth = w;
                    }
                }
            }
            NodeOutRect.Width = totalWidth;
            return totalWidth;
        }
    }
}
