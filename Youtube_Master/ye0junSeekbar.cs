using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Master
{
    delegate void valueChangeCallback(int value);
    class Ye0junSeekbar
    {
        private Panel parent;
        private TransparentPanel touch;
        private PictureBox pb_bar;
        private PictureBox pb_color_bar;
        private PictureBox pb_circle;
        private int width;
        private int maxValue;
        private int value;
        private int margin;
        private bool bClick;
        private Point circleLocation;
        private valueChangeCallback callback;

        public Ye0junSeekbar(Panel root,int width,int maxValue)
        {
            callback = null;
            this.margin = 5;
            this.width = width;
            this.maxValue = maxValue;
            this.value = 0;
            bClick = false;

            parent = new Panel();
            parent.Width = width + margin * 2;
            parent.Height = 9;
            root.Controls.Add(parent);
            root.Controls.SetChildIndex(parent, 0);

            touch = new TransparentPanel();
            touch.Width = width + margin * 2;
            touch.Height = 9;
            touch.Visible = false;
            parent.Controls.Add(touch);
            parent.Controls.SetChildIndex(touch, 0);

            circleLocation = new Point(0, 0);
            pb_circle = new PictureBox();
            pb_circle.Width = 9;
            pb_circle.Height = 9;
            pb_circle.Location = circleLocation;
            pb_circle.Image = Properties.Resources.circle;
            pb_circle.BackColor = Color.Transparent;
            parent.Controls.Add(pb_circle);

            pb_color_bar = new PictureBox();
            pb_color_bar.Width = 50;
            pb_color_bar.Height = 1;
            pb_color_bar.Location = new Point(4, 4);
            pb_color_bar.BackColor = Color.FromArgb(255, 255, 255, 255);
            parent.Controls.Add(pb_color_bar);

            pb_bar = new PictureBox();
            pb_bar.Width = width;
            pb_bar.Height = 1;
            pb_bar.Location = new Point(4, 4);
            pb_bar.BackColor = Color.FromArgb(100, 255, 255, 255);
            parent.Controls.Add(pb_bar);

            touch.MouseDown += new MouseEventHandler((o, e) =>
            {
                bClick = true;
            });

            touch.MouseUp += new MouseEventHandler((o, e) =>
            {
                bClick = false;
            });

            touch.MouseMove += new MouseEventHandler(SeekbarMoveEvent);

            void SeekbarMoveEvent(object o, MouseEventArgs e)
            {
                if(bClick)
                    SetValueByPoint(e.X);
            }
        }

        public void AddValueChangeEvent(valueChangeCallback c)
        {
            callback = c;
        }

        public void SetMaxValue(int maxValue)
        {
            this.maxValue = maxValue;
        }

        public void SetPosition(Point p)
        {
            parent.Location = p;
        }

        public Point GetPosition()
        {
            return parent.Location;
        }

        private void RunCallback()
        {
            if (callback != null)
                callback(value);
        }

        private void SetValueByPoint(int pointX)
        {
            if (pointX >= 0 && pointX <= width)
            {
                pb_circle.Location = new Point(pointX, pb_circle.Location.Y);
                pb_color_bar.Width = pointX - 4;
                double perLength = (double)maxValue / (double)width;
                value = (int)(perLength * pointX);
                RunCallback();
            }
        }

        public void SetValue(int value)
        {
            if (pb_color_bar.InvokeRequired)
            {
                valueChangeCallback a = new valueChangeCallback(SetValue);
                pb_color_bar.Invoke(a, value);
            }
            else
            {
                this.value = value;
                double perLength = (double)width / (double)maxValue;
                int location = (int)(perLength * value);
                circleLocation.X = location;
                pb_color_bar.Width = location;
                pb_circle.Location = circleLocation;
                RunCallback();
            }
        }
    }

    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        }
    }
}
