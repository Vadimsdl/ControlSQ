using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MHOGO_Rect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            labArr = new List<Label>();
            mouslast = new List<Point>();
   
        }

        Rect R = new Rect();
        int W = 0, lastX = 0, lastY = 0;
        Point lastP;

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = CreateGraphics();
            if (e.Button == MouseButtons.Right)
            {
                R.r = new Rectangle(e.X, e.Y, 150, 150);
                R.draw(g);
                StandartNW = R.r.Width;
            }

            if (e.Button == MouseButtons.Left && R.r != null && W == R.r.Width && lastX == R.r.X && lastY == R.r.Y)
            {
                if ((R.r.X <= e.X && R.r.Width + R.r.X >= e.X) && (R.r.Y <= e.Y && R.r.Height + R.r.Y >= e.Y) && mous.X != -1 && mous.Y != -1)
                {

                    if (R.r.X <= e.X && R.r.Y <= e.Y && (R.r.Width / 3) + R.r.X > e.X && ((R.r.Height / 3) + (R.r.Y - 10)) > e.Y)
                        KUBinKUB = 1;
                    else if ((R.r.Width / 3) + R.r.X <= e.X && R.r.Y <= e.Y && (R.r.Height / 3) + (R.r.Y - 10) > e.Y && (R.r.Width / 3) * 2 + R.r.X > e.X)
                        KUBinKUB = 2;
                    else if (R.r.Y <= e.Y && (R.r.Width / 3) * 2 + R.r.X <= e.X && R.r.X + R.r.Width > e.X && ((R.r.Height / 3) + (R.r.Y - 10)) > e.Y)
                        KUBinKUB = 3;
                    else if (R.r.X <= e.X && ((R.r.Height / 3) + R.r.Y - 10) <= e.Y && R.r.X + R.r.Width > e.X && ((R.r.Height / 3) * 2 + R.r.Y + 10) > e.Y)
                        KUBinKUB = 4;
                    else if (((R.r.Height / 3) * 2 + R.r.Y - 10) <= e.Y && R.r.X <= e.X && R.r.Y + R.r.Height > e.Y && ((R.r.Width / 3) + R.r.X) > e.X)
                        KUBinKUB = 5;
                    else if ((R.r.Width / 3) + R.r.X <= e.X && R.r.Y + 2 * (R.r.Height / 3) + 10 <= e.Y && (R.r.Width / 3) * 2 + R.r.X > e.X && R.r.Height + R.r.Y > e.Y)
                        KUBinKUB = 6;
                    else if ((R.r.Width / 3) * 2 + R.r.X <= e.X && R.r.Y + 2 * (R.r.Height / 3) + 10 <= e.Y && R.r.Y + R.r.Height > e.Y && R.r.X + R.r.Width > e.X)
                        KUBinKUB = 7;
                    else
                        KUBinKUB = 0;

                    TextBox textBox = new TextBox();
                    textBox.AutoSize = true;
                    textBox.Location = new System.Drawing.Point(R.r.X, R.r.Y - 19);
                    textBox.Size = new System.Drawing.Size(100, 150);
                    textBox.KeyUp += textBox_KeyPress;

                    Controls.Add(textBox);
                    lastP = new Point(e.X, e.Y);
                    mouslast.Add(new Point(e.X - R.r.X, e.Y - R.r.Y));
                    toolStripStatusLabel1.Text = KUBinKUB.ToString();
                }
            }
            else
            {
                W = R.r.Width;
                lastX = R.r.X;
                lastY = R.r.Y;
            }
            R.draw(g);
        }
        List<Label> labArr;
        int KUBinKUB = 0;
        private void textBox_KeyPress(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var Linklab = (TextBox)sender;

                if (Linklab != null)
                {
                    Label label = new Label();
                    label.AutoSize = true;

                    KubinKub(label, KUBinKUB);

                    label.Size = new System.Drawing.Size(35, 13);
                    label.Text = Linklab.Text;
                    labArr.Add(label);

                    Controls.Add(labArr.Last());

                    Controls.Remove(Linklab);

                }
                R.draw(CreateGraphics());
            }


        }

        void KubinKub(Label label, int KIK)
        {
            switch (KIK)
            {
                case 1:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 2:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 3:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 4:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 5:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 6:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                case 7:
                    label.Location = lab = new System.Drawing.Point(lastP.X, lastY);
                    break;
                default:
                    MessageBox.Show("Какая-то ошибка");
                    break;
            }

        }

        void ResizeRect(Point pt)
        {
            if (startResize)
            {
                switch (R.strongInBorder(pt))
                {
                    case PosType.West:
                        R.r.Width = -pt.X + R.r.X + R.r.Width;
                        R.r.X = pt.X;
                        break;
                    case PosType.East:
                        R.r.Width = pt.X - R.r.X;
                        break;
                }

            }
        }


        bool startResize = false;
        Point cursorPoint;
        bool isSelected = false;
        Point mous;
        List<Point> mouslast;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (R.r.X <= e.X && R.r.Width + R.r.X >= e.X && R.r.Y <= e.Y && R.r.Height + R.r.Y >= e.Y)
                mous = new Point(e.X - R.r.X, e.Y - R.r.Y);
            else mous = new Point(-1, -1);

            if (!isSelected && R.into(e.Location) && e.Button == MouseButtons.Left)
            {
                R.Selected = isSelected = true;

            }
            if (!R.into(e.Location) && e.Button == MouseButtons.Left)
            {
                R.Selected = isSelected = false;


            }
            if (e.Button == MouseButtons.Left && R.inBorder(e.Location) != PosType.None)
            {
                startResize = true;
                cursorPoint = e.Location;

                switch (R.inBorder(e.Location))
                {
                    case PosType.WE:
                        Cursor = Cursors.SizeWE;
                        break;
                    case PosType.None:
                        Cursor = Cursors.Default;
                        break;
                }
            }

        }
        int nX = 0, nY = 0, nW = 0;

        private void label_LocationChanged(object sender, EventArgs e)
        {

        }

        Point lab;
        int StandartNW = 0, resultNW = 0;
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left && startResize)
            {
                //Invalidate();  (R.r.Width / 3)
                ResizeRect(e.Location);
                R.draw(CreateGraphics());
                resultNW = R.r.Width - StandartNW;
                if (nW < (R.r.Width - 10) || nW > (R.r.Width + 10))
                {
                    Invalidate();
                    nW = R.r.Width;

                }
            }
            else
            if (e.Button == MouseButtons.Left)
            {

                if (R.r.X <= e.X && R.r.Width + R.r.X >= e.X && R.r.Y <= e.Y && R.r.Height + R.r.Y >= e.Y && mous.X != -1 && mous.Y != -1)
                {
                    R.Loc(mous, e.Location);
                    R.draw(CreateGraphics());
                    if (nX < (R.r.X - 10) || nY < (R.r.Y - 10) || nY > (R.r.Y + 10) || nX > (R.r.X + 10))
                    {
                        Invalidate();
                        nX = R.r.X;
                        nY = R.r.Y;
                    }
                    try
                    {

                        // labArr.Last().Left = R.r.X + mouslast.X;
                        // labArr.Last().Top = R.r.Y + mouslast.Y;
                        int ind = 0;
                        foreach (Label l in labArr)
                        {
                            l.Left= R.r.X + mouslast[ind].X ;
                            l.Top = R.r.Y + mouslast[ind].Y;
                            ind++;
                        }

                    }
                    catch { }
                }
            }

            switch (R.inBorder(e.Location))
            {
                case PosType.WE:
                    Cursor = Cursors.SizeWE;
                    break;
                case PosType.None:
                    Cursor = Cursors.Default;
                    break;
            }

        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            startResize = false;
            cursorPoint = e.Location;
            StandartNW = R.r.Width;
        }
    }
}
