using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace TiledStudio
{

    //static class UIHelper
    //{
    //    //public static UIDrawingBehaviorTree DrawBehaviorTree = new UIDrawingBehaviorTree();
    //    public static UIDrawingConnectLine DrawConnectLine = new UIDrawingConnectLine();
    //    public static UIDrawingDiamond DrawDiamond = new UIDrawingDiamond();
    //    public static UIDrawingRoundRect DrawRoundRect = new UIDrawingRoundRect();
    //    public static UIDrawingAutoLayout DrawingAutoLayout = new UIDrawingAutoLayout();
    //}

    public abstract class UIDrawing
    {
        protected static Pen drawPen = new Pen(Color.Blue, 1);
        protected static Brush drawBrush = new SolidBrush(Color.White);


        public virtual void OnMouseClick(MouseEventArgs e) { }
        public virtual void OnMouseDown(MouseEventArgs e) { }
        public virtual void OnMouseUp(MouseEventArgs e) { }
        public virtual void OnMouseMove(MouseEventArgs e) { }
        public virtual void OnPaint(PaintEventArgs e) { }
        public virtual void OnKeyDown(KeyEventArgs e) { }
        public virtual void OnSizeChanged(Size e) { }
    }


    class UIDrawingRoundRect : UIDrawing
    {
        public override void OnPaint(PaintEventArgs e)
        {
            ShapeHelper.FillRoundRectangle(e.Graphics, drawBrush, new Rectangle(150, 150, 100, 50), 10);
            ShapeHelper.DrawRoundRectangle(e.Graphics, drawPen, new Rectangle(150, 150, 100, 50), 10);
        }
    }

    class UIDrawingDiamond : UIDrawing
    {
        public override void OnPaint(PaintEventArgs e)
        {
            ShapeHelper.FillDiamond(e.Graphics, drawBrush, new Rectangle(200, 200, 200, 150));
            ShapeHelper.DrawDiamond(e.Graphics, drawPen, new Rectangle(200, 200, 200, 150));
        }
    }
    class UIDrawingConnectLine : UIDrawing
    {
        Point ConnectBegin;
        Point ConnectEnd;
        bool IsConnecting;

        public override void OnMouseClick(MouseEventArgs e)
        {
            //左键点击选择
            if (IsConnecting)
            {
                ConnectEnd.X = e.X;
                ConnectEnd.Y = e.Y;
                IsConnecting = false;
            }
            else
            {
                ConnectBegin.X = e.X;
                ConnectBegin.Y = e.Y;
                ConnectEnd.X = e.X;
                ConnectEnd.Y = e.Y;
                IsConnecting = true;
            }

        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (IsConnecting)
            {
                ConnectEnd.X = e.X;
                ConnectEnd.Y = e.Y;
            }
        }

        public override void OnPaint(PaintEventArgs e)
        {
            ShapeHelper.DrawConnectLine(e.Graphics, drawPen, ConnectBegin, ConnectEnd);

        }
    }
    class UIDrawingAutoLayout : UIDrawing
    {
        public Size ClientSize { get; set; }
        public override void OnPaint(PaintEventArgs e)
        {
            AINode rootNode = new AINode();
            rootNode.SetGraphics(e.Graphics);
            rootNode.SelfOutRect.X = ClientSize.Width / 2;
            rootNode.SelfOutRect.Y = ClientSize.Height / 2;

            rootNode.AddNode("firstOne").AddNode("node1.1").AddNode("node1.1.1");
            var node2 = rootNode.AddNode("secondOne");
            node2.AddNode("2.1");
            node2.AddNode("2.2");

            rootNode.CalcNodeHeight();
            rootNode.CalcNodeWidth();
            rootNode.SelfOutRect.X = ClientSize.Width / 2 - rootNode.NodeOutRect.Width / 2;
            rootNode.SelfOutRect.Y = ClientSize.Height / 2 - rootNode.NodeOutRect.Height / 2;
            rootNode.AutoLayout();
            rootNode.DrawNode();
        }
    }

    class UIDrawingBehaviorTree : UIDrawing
    {
        Pen outlinePen = new Pen(Color.Red, 2);
        Pen movePen = new Pen(Color.FromArgb(128, Color.Blue), 1);
        Brush moveBrush = new SolidBrush(Color.FromArgb(128, Color.White));

        AITree Tree = null;
        AINode CurrentMoveNode { get; set; }
        AINode MoveToParentNode { get; set; }
        AINode Root { get; set; }
        AINode CurrentSelectNode { get; set; }

        Point MovingPos = new Point(0, 0);
        bool IsMoving;
        bool IsInited;
        public Size ClientSize { get; set; }

        public UIDrawingBehaviorTree(AITree t)
        {
            Tree = t;
        }

        public void Init()
        {
            //if (IsInited) return;
            //IsInited = true;
            Root = Tree.Root;
            Root.SelfOutRect.X = ClientSize.Width / 2;
            Root.SelfOutRect.Y = ClientSize.Height / 2;

            //Root.AddNode("firstOne").AddNode("node1.1").AddNode("node1.1.1");
            //var node2 = Root.AddNode("secondOne");
            //node2.AddNode("2.1");
            //node2.AddNode("2.2").AddNode("2.2.1");
        }

        public override void OnKeyDown(KeyEventArgs e)
        {
            if (CurrentSelectNode != null)
            {
                if(e.KeyCode == Keys.Enter)
                {
                    if (e.Shift)
                    {
                        //孩子节点
                        CurrentSelectNode = CurrentSelectNode.AddNode("子节点");
                        return;
                    }
                    //兄弟节点
                    if (CurrentSelectNode == Root)
                        return;
                    var node = CurrentSelectNode.Parent.AddNode("子节点");
                    CurrentSelectNode = node;
                }
                else if(e.KeyCode == Keys.Delete)
                {
                    //删除节点
                    CurrentSelectNode.Parent.DelNode(CurrentSelectNode);
                    CurrentSelectNode = null;
                    CurrentMoveNode = null;
                }
                else if(e.Alt)
                {
                    if(e.KeyCode == Keys.Up)
                    {
                        AINode.SwapNode(CurrentSelectNode, true);
                    }else if(e.KeyCode == Keys.Down)
                    {
                        AINode.SwapNode(CurrentSelectNode, false);
                    }
                }
            }

        }

        public override void OnMouseDown(MouseEventArgs e)
        {
            CurrentSelectNode = Root.SelectNode(e.X, e.Y);
            if (CurrentSelectNode != null )
            {
                AINode.AIProperty.SelectedObject = CurrentSelectNode;
                if (CurrentSelectNode != Root)
                {
                    IsMoving = true;
                    CurrentMoveNode = CurrentSelectNode;
                }
            }
        }

        public override void OnMouseUp(MouseEventArgs e)
        {
            if (IsMoving)
            {
                IsMoving = false;
                if (MoveToParentNode != null)
                {
                    Root.DelNode(CurrentMoveNode);
                    MoveToParentNode.AddNode(CurrentMoveNode);
                }
                CurrentMoveNode = null;
                MoveToParentNode = null;
            }
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if (!IsMoving)
            {
                return;
            }
            MovingPos.X = e.X;
            MovingPos.Y = e.Y;
            MoveToParentNode = Root.SelectParent(CurrentMoveNode, MoveToParentNode, e.X-CurrentMoveNode.SelfOutRect.Width/2, e.Y);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            Init();

            //绘制原图
            Root.SetGraphics(e.Graphics);
            Root.CalcNodeHeight();
            Root.CalcNodeWidth();
            Root.SelfOutRect.X = ClientSize.Width / 2 - Root.NodeOutRect.Width / 2;
            Root.SelfOutRect.Y = ClientSize.Height / 2;

            Root.AutoLayout();
            Root.DrawNode();
            if(CurrentSelectNode != null)
            {
                CurrentSelectNode.DrawOutLine(outlinePen);
            }

            if(!IsMoving || CurrentMoveNode == null)
            {
                return;
            }
            //绘制移动的节点
            CurrentMoveNode.SelfOutRect.X = MovingPos.X - CurrentMoveNode.SelfOutRect.Width / 2;
            CurrentMoveNode.SelfOutRect.Y = MovingPos.Y - CurrentMoveNode.SelfOutRect.Height / 2;
            CurrentMoveNode.DrawSelf(moveBrush, movePen);

            if(MoveToParentNode == null)
            {
                return;
            }
            //绘制父节点连线
            MoveToParentNode.DrawOutLine(outlinePen);
            CurrentMoveNode.DrawOutLine(outlinePen);
            ShapeHelper.DrawConnectLine(e.Graphics, outlinePen, MoveToParentNode.ConnectOutPos, CurrentMoveNode.ConnectInPos);
        }
    }

}
