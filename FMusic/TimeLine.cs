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
        public TimeLine()
        {
            InitializeComponent();

            GPanel.BackColor = Color.Red;

            this.Resize += TimeLine_Resize;
        }

        private void TimeLine_Resize(object sender, EventArgs e)
        {
            ScrollBar_timeline.Location = new Point(0, this.Size.Height - 56);
            ScrollBar_timeline.Size = new Size(this.Size.Width - 15, 17);

            GPanel.Location = new Point(0, 27);
            GPanel.Size = new Size(this.Size.Width - 15, this.Size.Height - ScrollBar_timeline.Size.Height - 69);
        }
    }
}
