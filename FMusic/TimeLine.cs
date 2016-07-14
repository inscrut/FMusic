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
        private int step = 0;

        private List<Note> nlist = new List<Note>()
        {
            new Note(new Point(1, 6), 1),
            new Note(new Point(2, 6), 1),
            new Note(new Point(3, 5), 1)
        };

        public TimeLine()
        {
            InitializeComponent();

            GPanel.BackColor = Color.White;

            this.Resize += TimeLine_Resize;
            ScrollBar_timeline.ValueChanged += ScrollBar_timeline_ValueChanged;
            GPanel.Paint += GPanel_Paint;
            GPanel.MouseClick += GPanel_MouseClick;

            this.DesktopLocation = new Point(0, 0);

            //showDebug();
        }

        private void GPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (step <= 0) return;
            if(e.Button == MouseButtons.Left)
            {
                addNote(getPoint(step, e.Location), 1, ref nlist);
            }
            else if (e.Button == MouseButtons.Right)
            {
                removeNote(getPoint(step, e.Location), ref nlist);
            }
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
            //
        }

        private void GPanel_Paint(object sender, PaintEventArgs e)
        {
            clearLog();
            step = getStep(sender);
            if (step <= 0) return;
            using (var g = e.Graphics)
            {
                drawStan(sender, g, step);
                showPoints(g, step, getAllPoints(step, sender));
                drawNotes(g, step, nlist);
            }
        }

        private int getStep(object _panel)
        {
            int _step = (_panel as Panel).Height / 7;
            if (_step < 1) return -1;
            return _step;
        }

        private Point getPoint(int _step, Point _loc)
        {
            Point p = new Point(0, 0);
            int buf = 0;

            for (int i = _step / 2; i < _loc.X; i += _step)
                buf++;
            p.X = buf;
            buf = 0;
            for (int i = _step / 2; i < _loc.Y; i += _step)
                buf++;
            p.Y = buf;

            return p;
        }

        private Point getLoc(int _step, Point _point)
        {
            Point p = new Point(0, 0);

            p.X = _step * _point.X;
            p.Y = _step * _point.Y;

            return p;
        }

        private List<Point> getAllPoints(int _step, object _panel)
        {
            List<Point> res = new List<Point>();
            var p = _panel as Panel;

            for(int i = 1; i < (p.Width / _step) + 1; i++)
            {
                if (getLoc(_step, new Point(i, 1)).X + _step / 2 > p.Width) break;
                for (int j = 1; j < 7; j++)
                    res.Add(new Point(i, j));
            }

            return res;
        }

        private void showPoints(Graphics _paint, int _step, List<Point> _points)
        {
            foreach (var point in _points)
                drawBox(_paint, getLoc(_step, point));
        }

        private void addNote(Point _point, int _type, ref List<Note> _notes)
        {
            foreach (var note in _notes)
            {
                if(note.getPointNote.X == _point.X)
                {
                    _notes.Remove(note);
                    break;
                }
            }

            _notes.Add(new Note(_point, _type));

            GPanel.Refresh();
        }

        private void removeNote(Point _point, ref List<Note> _notes)
        {
            foreach (var note in _notes)
            {
                if(note.getPointNote == _point)
                {
                    _notes.Remove(note);
                    break;
                }
            }

            sortNote(ref _notes);
            advSort(ref _notes);

            GPanel.Refresh();
        }

        private void sortNote(ref List<Note> _notes)
        {
            List<Note> newlist = new List<Note>();
            int max = _notes.Count;

            Note buff;
            for (int i = 0; i < max; i++)
            {
                buff = _notes[0];
                foreach (var note in _notes)
                    if (note.getPointNote.X > buff.getPointNote.X) buff = note;
                newlist.Add(buff);
                _notes.Remove(buff);
            }

            newlist.Reverse();
            _notes = newlist;
        }

        private void advSort(ref List<Note> _notes)
        {
            int val = 1;
            foreach (var item in _notes)
            {
                item.setPointX = val;
                val++;
            }
        }

        private void drawBox(Graphics _paint, Point _loc)
        {
            _paint.DrawRectangle(new Pen(Color.Red), _loc.X - 5, _loc.Y - 5, 10, 10);
        }

        private void drawStan(object _panel, Graphics _paint, int _step)
        {
            var p = _panel as Panel;

            for (int i = p.Height - _step; i > _step; i -= _step)
                if(i == p.Height - _step) _paint.DrawLine(new Pen(Color.Gray, 2.0F) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash }, 0, i, p.Width, i);
                else _paint.DrawLine(new Pen(Color.Black, 2.0F), 0, i, p.Width, i);
        }

        private void drawNotes(Graphics _paint, int _step, List<Note> _notes)
        {
            foreach (var note in _notes)
                _paint.DrawImage(note.getImage(), getLoc(_step, note.getPointNote));
        }

        //Debug
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
