using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMusic
{
    public class ItemPanel
    {
        private Panel item = null;
        private int _id = 0;
        private int _step = 0;
        private int _dir = 0;

        /// <summary>
        /// Получить панель
        /// </summary>
        public Panel getItem { get { return item; } }

        /// <summary>
        /// Получить ID
        /// </summary>
        public int getID { get { return _id; } }
        /// <summary>
        /// Получить номер шагового порта
        /// </summary>
        public int getStepPin { get { return _step; } }
        /// <summary>
        /// Получить номер направляющего порта
        /// </summary>
        public int getDirPin { get { return _dir; } }

        /// <summary>
        /// Изменить цвет
        /// </summary>
        public Color changeColor { get { return item.BackColor; } set { item.BackColor = value; } }

        public delegate void _dClicked(int id, EventArgs e);
        public event _dClicked ItemClicked;

        /// <summary>
        /// Создать панель
        /// </summary>
        /// <param name="settings">Настройки устройства</param>
        public ItemPanel(FloppySettings settings)
        {
            _id = settings.getID;
            _step = settings.getStepPin;
            _dir = settings.getDirPin;

            item = new Panel();
            item.Size = new Size(256, 96);
            item.BackColor = Color.YellowGreen;

            Bitmap img = new Bitmap(Properties.Resources.floppy);
            PictureBox pb = new PictureBox();
            pb.Image = img;
            pb.Location = new Point(10, 16);
            pb.Size = img.Size;
            item.Controls.Add(pb);

            var font = new Font("Times New Roman", 10.0F);

            Label lab_id = new Label();
            lab_id.Text = "ID: " + _id.ToString();
            lab_id.Location = new Point(85, 15);
            lab_id.ForeColor = Color.Black;
            lab_id.Font = font;
            Label lab_step = new Label();
            lab_step.Text = "Step pin: " + _step.ToString();
            lab_step.Location = new Point(85, 45);
            lab_step.ForeColor = Color.Black;
            lab_step.Font = font;
            Label lab_dir = new Label();
            lab_dir.Text = "Direct pin: " + _dir.ToString();
            lab_dir.Location = new Point(85, 65);
            lab_dir.ForeColor = Color.Black;
            lab_dir.Font = font;
            item.Controls.Add(lab_id);
            item.Controls.Add(lab_step);
            item.Controls.Add(lab_dir);

            item.Click += Item_Click;
            pb.Click += Item_Click;
            lab_dir.Click += Item_Click;
            lab_step.Click += Item_Click;
            lab_id.Click += Item_Click;
        }

        private void Item_Click(object sender, EventArgs e)
        {
            ItemClicked(_id, e);
        }
    }
}
