using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TiledStudio
{
    class ShapeHelper
    {
        #region 圆角矩形
        // C# GDI+ 绘制圆角实心矩形  
        public static void FillRoundRectangle(Graphics g, Brush b, Rectangle rectangle, int r)
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            g.FillPath(b, GetRoundRectangle(rectangle, r));
        }

        //绘制空心圆角矩形
        public static void DrawRoundRectangle(Graphics g, Pen pen, Rectangle rectangle,  int r)
        {
            rectangle = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1);
            g.DrawPath(pen, GetRoundRectangle(rectangle, r));
        }

        // 根据普通矩形得到圆角矩形的路径  
        private static GraphicsPath GetRoundRectangle(Rectangle rectangle, int r)
        {
            int l = 2 * r;
            // 把圆角矩形分成八段直线、弧的组合，依次加到路径中  
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(new Point(rectangle.X + r, rectangle.Y), new Point(rectangle.Right - r, rectangle.Y));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Y, l, l), 270F, 90F);

            gp.AddLine(new Point(rectangle.Right, rectangle.Y + r), new Point(rectangle.Right, rectangle.Bottom - r));
            gp.AddArc(new Rectangle(rectangle.Right - l, rectangle.Bottom - l, l, l), 0F, 90F);

            gp.AddLine(new Point(rectangle.Right - r, rectangle.Bottom), new Point(rectangle.X + r, rectangle.Bottom));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Bottom - l, l, l), 90F, 90F);

            gp.AddLine(new Point(rectangle.X, rectangle.Bottom - r), new Point(rectangle.X, rectangle.Y + r));
            gp.AddArc(new Rectangle(rectangle.X, rectangle.Y, l, l), 180F, 90F);
            return gp;
        }
        #endregion

        #region 菱形
        //填充菱形
        public static void FillDiamond(Graphics g, Brush b, Rectangle rect)
        {
            GraphicsPath path = CalcDiamond(rect);
            g.FillPath(b, path);
        }

        //绘制空心菱形
        public static void DrawDiamond(Graphics g, Pen p, Rectangle rect)
        {
            GraphicsPath path = CalcDiamond(rect);
            g.DrawPath(p, path);
        }

        //计算菱形路径
        private static GraphicsPath CalcDiamond(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            Point[] pts = new[]
            {
                new Point(rect.X, rect.Y + rect.Height / 2),
                new Point(rect.X + rect.Width / 2, rect.Y),
                new Point(rect.Right, rect.Y+rect.Height/2),
                new Point(rect.X+rect.Width/2, rect.Bottom),
                new Point(rect.X, rect.Y + rect.Height / 2),

            };

            path.AddLines(pts);
            return path;
        }
        #endregion

        #region 六边形
        public static void FillHexagon(Graphics g, Brush b, Rectangle rect)
        {
            GraphicsPath path = CalcHexagon(rect);
            g.FillPath(b, path);
        }

        public static void DrawHexagon(Graphics g, Pen p, Rectangle rect)
        {
            GraphicsPath path = CalcHexagon(rect);
            g.DrawPath(p, path);

        }

        //计算六边形形路径
        private static GraphicsPath CalcHexagon(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            Point[] pts = new[]
            {
                new Point(rect.X, rect.Y + rect.Height / 2),
                new Point(rect.X + 10, rect.Y),
                new Point(rect.X + rect.Width - 10, rect.Y),
                new Point(rect.Right, rect.Y + rect.Height / 2),
                new Point(rect.X+rect.Width - 10, rect.Bottom),
                new Point(rect.X + 10, rect.Bottom),
                new Point(rect.X, rect.Y + rect.Height / 2),

            };

            path.AddLines(pts);
            return path;
        }

        #endregion

        #region 连接线
        public static void DrawConnectLine(Graphics g, Pen p, Point begin, Point end)
        {
            //begin.x<end.x
            Point ctrl1 = new Point(begin.X + 50, begin.Y);
            Point ctrl2 = new Point(end.X - 50, end.Y);
            if (begin.X > end.X)
            {
                ctrl1.X = begin.X - 50;
                ctrl2.X = end.X + 50;
            }
            g.DrawBeziers(p, new[] { begin, ctrl1, ctrl2, end });

            if (begin.X < end.X)
                ShapeHelper.FillRightArrow(g, new SolidBrush(p.Color), end, 4);
            else
                ShapeHelper.FillLeftArrow(g, new SolidBrush(p.Color), end, 4);

            //DrawBeziersPoint(g);
        }

        public static void DrawBeziersPoint(Graphics g)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points for curve.
            Point start = new Point(100, 100);
            Point control1 = new Point(200, 10);
            Point control2 = new Point(350, 50);
            Point end1 = new Point(500, 100);
            Point control3 = new Point(600, 150);
            Point control4 = new Point(650, 250);
            Point end2 = new Point(500, 300);
            Point[] bezierPoints ={
                 start, control1, control2, end1,
                 control3, control4, end2
             };

            // Draw arc to screen.
            g.DrawBeziers(blackPen, bezierPoints);
            FillNodePoint(g, Brushes.Black, start, 3);
            FillNodePoint(g, Brushes.Black, control1, 3);
            FillNodePoint(g, Brushes.Black, control2, 3);
            FillNodePoint(g, Brushes.Black, end1, 3);
        }

        #endregion

        #region 关节点
        public static void FillNodePoint(Graphics g, Brush b, Point p, int r)
        {
            g.FillEllipse(b, new Rectangle(p.X - r, p.Y - r, r + r, r + r));
        }
        #endregion

        #region 水平箭头

        public static void FillLeftArrow(Graphics g, Brush b, Point p, int r)
        {
            GraphicsPath path = CalcLeftArrowPath(p, r);
            g.FillPath(b, path);
        }

        private static GraphicsPath CalcLeftArrowPath(Point p, int r)
        {
            var path = new GraphicsPath();
            int width = (int)(r * 1.6);
            Point[] pts = new[]
            {
                new Point(p.X+width, p.Y-r),
                new Point(p.X, p.Y),
                new Point(p.X+width, p.Y+r),
                new Point(p.X+width, p.Y-r),
            };

            path.AddLines(pts);
            return path;
        }
        public static void FillRightArrow(Graphics g, Brush b, Point p, int r)
        {
            GraphicsPath path = CalcRightArrowPath(p, r);
            g.FillPath(b, path);
        }

        private static GraphicsPath CalcRightArrowPath(Point p, int r)
        {
            var path = new GraphicsPath();
            int width = (int)(r * 1.6);
            Point[] pts = new[]
            {
                new Point(p.X-width, p.Y-r),
                new Point(p.X, p.Y),
                new Point(p.X-width, p.Y+r),
                new Point(p.X-width, p.Y-r),
            };

            path.AddLines(pts);
            return path;
        }
        #endregion
    }

}
