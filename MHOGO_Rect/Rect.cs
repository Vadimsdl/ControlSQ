using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHOGO_Rect
{
    enum PosType { None, West, East, WE };

    abstract class Shapes
    {

        public Rectangle r;
        public bool Selected { get; set; } = false;
        int _5 = 5;
        public bool into(Point p)
        {
            return (p.X > r.X && p.X < r.X + r.Width && p.Y > r.Y && p.Y < r.Y + r.Height);
        }
        public abstract void draw(Graphics g);
        public virtual PosType inBorder(Point p)
        {
            _5 = 5;
            if (near(p.X, r.X) || near(p.X, r.X + r.Width))
            {
                return PosType.WE;
            }
            else
                return PosType.None;
        }
        public virtual PosType strongInBorder(Point p)
        {
            _5 = 10;
            if (near(p.X, r.X))
            {
                return PosType.West;
            }
            if (near(p.X, r.X + r.Width))
            {
                return PosType.East;
            }
            else
                return PosType.None;
        }

        public virtual void Loc(Point p, Point e)
        {
            r.X = e.X - p.X;
            r.Y = e.Y - p.Y;
        }

        bool near(int x, int y)
        {
            if (x > y - _5 && x < y + _5)
                return true;
            else
                return false;
        }

    }
    class Rect : Shapes
    {
        public override void draw(Graphics g)
        {
            if (Selected)
            {

                Rectangle r2d2 = new Rectangle(r.X + 2, r.Y + 2, r.Width - 4, r.Height - 4);
                g.FillRectangle(Brushes.Gray, r2d2);
                Pen pen = new Pen(Color.Black, 1);
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                g.DrawRectangle(pen, r);
                g.DrawLine(Pens.Black, (r2d2.Width / 3) + r2d2.X, r2d2.Y, (r2d2.Width / 3) + r2d2.X, (r2d2.Height / 3) + r2d2.Y-10);
                g.DrawLine(Pens.Black, (r2d2.Width / 3) * 2 + r2d2.X, r2d2.Y, (r2d2.Width / 3) * 2 + r2d2.X, (r2d2.Height / 3) + r2d2.Y-10);
                g.DrawLine(Pens.Black, r2d2.X, (r2d2.Height / 3) + r2d2.Y-10, r2d2.X + r2d2.Width, (r2d2.Height / 3) + r2d2.Y-10);
                g.DrawLine(Pens.Black, r2d2.X, (r2d2.Height / 3) * 2 + r2d2.Y + 10, r2d2.X + r2d2.Width, (r2d2.Height / 3) * 2 + r2d2.Y + 10);
                g.DrawLine(Pens.Black, (r2d2.Width / 3) + r2d2.X, r2d2.Height + r2d2.Y, (r2d2.Width / 3) + r2d2.X, r2d2.Y + 2 * (r2d2.Height / 3)+10);
                g.DrawLine(Pens.Black, (r2d2.Width / 3) * 2 + r2d2.X, r2d2.Height + r2d2.Y, (r2d2.Width / 3) * 2 + r2d2.X, r2d2.Y + 2 * (r2d2.Height / 3)+10);
            }
            else
            {


                g.FillRectangle(Brushes.Gray, r);
                g.DrawRectangle(Pens.Black, r);
                g.DrawLine(Pens.Black, (r.Width / 3) + r.X, r.Y, (r.Width / 3) + r.X, (r.Height / 3) + r.Y-10);
                g.DrawLine(Pens.Black, (r.Width / 3) * 2 + r.X, r.Y, (r.Width / 3) * 2 + r.X, (r.Height / 3) + r.Y-10);
                g.DrawLine(Pens.Black, r.X, (r.Height / 3) + r.Y-10, r.X + r.Width, (r.Height / 3) + r.Y-10);
                g.DrawLine(Pens.Black, r.X, (r.Height / 3) * 2 + r.Y + 10, r.X + r.Width, (r.Height / 3) * 2 + r.Y + 10);
                g.DrawLine(Pens.Black, (r.Width / 3) + r.X, r.Height + r.Y, (r.Width / 3) + r.X, r.Y + 2 * (r.Height / 3)+10);
                g.DrawLine(Pens.Black, (r.Width / 3) * 2 + r.X, r.Height + r.Y, (r.Width / 3) * 2 + r.X, r.Y + 2 * (r.Height / 3)+10);
            }
        }



    }
}
