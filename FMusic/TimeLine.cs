using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMusic
{
    public partial class TimeLine : Form
    {
        private TextBox logger = null;

        public TimeLine()
        {
            InitializeComponent();

            GPanel.BackColor = Color.White;

            this.Resize += TimeLine_Resize;
            ScrollBar_timeline.ValueChanged += ScrollBar_timeline_ValueChanged;
            GPanel.Paint += GPanel_Paint;
            GPanel.MouseClick += GPanel_MouseClick;

            this.DesktopLocation = new Point(0, 0);

            showDebug();
        }

        private void GPanel_MouseClick(object sender, MouseEventArgs e)
        {
            writeLog("Location: " + e.X.ToString() + ", " + e.Y.ToString());
        }

        private void TimeLine_Resize(object sender, EventArgs e)
        {
            ScrollBar_timeline.Location = new Point(0, this.Size.Height - 56);
            ScrollBar_timeline.Size = new Size(this.Size.Width - 15, 17);

            GPanel.Location = new Point(0, 27);
            GPanel.Size = new Size(this.Size.Width - 15, this.Size.Height - ScrollBar_timeline.Size.Height - 69);

            GPanel.Refresh();
        }

        private void ScrollBar_timeline_ValueChanged(object sender, EventArgs e)
        {
            //MessageBox.Show("");
        }

        private void GPanel_Paint(object sender, PaintEventArgs e)
        {
            clearLog();
            int step = getStep(sender);
            using (var g = e.Graphics)
            {
                drawStan(sender, g, step);
                writeLog("Step: " + step.ToString());
            }
        }

        private int getStep(object _panel)
        {
            int _step = (_panel as Panel).Height / 7;
            if (_step < 1) return -1;
            return _step;
        }

        private Point getPoint(object _panel, int _step, Point _click)
        {
            return new Point(0,0);
        }

        private void showPoints(object panel, Graphics paint)
        {

        }

        private void drawStan(object _panel, Graphics _paint, int _step)
        {
            var p = _panel as Panel;

            for (int i = p.Height - _step; i > _step; i -= _step)
                if(i == p.Height - _step) _paint.DrawLine(new Pen(Color.Gray, 2.0F) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 0, i, p.Width, i);
                else _paint.DrawLine(new Pen(Color.Black, 2.0F), 0, i, p.Width, i);
        }

        private void showDebug()
        {
            try
            {
                Form debug = new Form();

                debug.Size = new Size(256, 192);
                debug.StartPosition = FormStartPosition.Manual;
                debug.DesktopLocation = new Point(this.DesktopLocation.X + this.Width, this.DesktopLocation.Y);

                TextBox log = new TextBox();
                log.Location = new Point(0, 0);
                log.Multiline = true;
                log.ScrollBars = ScrollBars.Vertical;
                log.Size = new Size(debug.Size.Width - 15, debug.Height - 25);
                log.Name = "LOG";
                debug.Controls.Add(log);

                debug.FormClosed += (object sender, FormClosedEventArgs e) => { debug.Dispose(); };

                debug.Show();

                logger = (debug.Controls["LOG"] as TextBox);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void writeLog(string s)
        {
            if (logger == null) return;
            logger.Text += s + "\r\n";
            logger.ScrollToCaret();
        }
        private void clearLog()
        {
            if (logger == null) return;
            logger.Text = "";
        }
    }
}
