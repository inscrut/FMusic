using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FMusic
{
    public partial class GeneralForm : Form
    {
        private Panel gpanel = null;
        private List<ItemPanel> items = null;

        private delegate void _RError(string msg);
        private event _RError ErrorEvent;
        private delegate void _RWarning(string msg);
        private event _RError WarningEvent;

        public GeneralForm()
        {
            InitializeComponent();

            this.Size = new Size(545, 270);
            this.Resize += GeneralForm_Resize;

            ErrorEvent += GeneralForm_ErrorEvent;
            WarningEvent += GeneralForm_WarningEvent;
        }

        private void GeneralForm_WarningEvent(string msg)
        {
            MessageBox.Show(msg, "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void GeneralForm_ErrorEvent(string msg)
        {
            MessageBox.Show(msg, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GeneralForm_Load(object sender, EventArgs e)
        {
            initProg();
        }

        private void Item_ItemClicked(int id, EventArgs e)
        {
            if ((e as MouseEventArgs).Button == MouseButtons.Left)
            {
                foreach (var i in items)
                    if (i.getID == id)
                        i.changeColor = Color.Green;
            }
            else if ((e as MouseEventArgs).Button == MouseButtons.Right)
            {
                foreach (var i in items)
                    if (i.getID == id)
                        i.changeColor = Color.Red;
            }
            else { }
        }

        private void Gpanel_Resize(object sender, EventArgs e)
        {
            showItems();
        }
        private void GeneralForm_Resize(object sender, EventArgs e)
        {
            gpanel.Size = new Size(this.Size.Width - 15, this.Size.Height - 60);
        }

        private void initProg()
        {
            if (gpanel != null) this.Controls.Remove(gpanel);
            try
            {
                gpanel = new Panel();
                gpanel.AutoScroll = true;
                gpanel.Location = new Point(0, 24);
                gpanel.Size = new Size(this.Size.Width - 15, this.Size.Height - 60);
                gpanel.Resize += Gpanel_Resize;
                this.Controls.Add(gpanel);

                items = new List<ItemPanel>();

                if (File.Exists("settings.cfg"))
                    addItems();
                else
                {
                    if (MessageBox.Show(
                        "Файл settings.cfg не найден или не существует!\r\nСоздать новый?", 
                        "Внимание!", 
                        MessageBoxButtons.YesNo, 
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        makeDefaulCfg();
                        addItems();
                    }
                }

                foreach (var item in items)
                {
                    gpanel.Controls.Add(item.getItem);
                    item.ItemClicked += Item_ItemClicked;
                }

                showItems();
            }
            catch (Exception e)
            {
                ErrorEvent(e.Message);
            }
        }

        private void addItems()
        {
            try
            {
                using (Stream stream = new FileStream("settings.cfg", FileMode.Open))
                    foreach (var i in readSettings(stream))
                        items.Add(new ItemPanel(i));
            }
            catch (Exception e)
            {
                //ErrorEvent(e.Message);
            }
        }
        private void showItems()
        {
            int x = 5;
            int y = 5;
            int offset = 5;

            foreach (var i in gpanel.Controls)
            {
                if (i is Panel)
                {
                    (i as Panel).Location = new Point(x, y);
                    x += (i as Panel).Width + offset;
                    if (x + (i as Panel).Width + offset > gpanel.Width) { x = 5; y += (i as Panel).Height + offset; }
                }
            }
        }
        private List<FloppySettings> readSettings(Stream stream)
        {
            List<FloppySettings> fs = new List<FloppySettings>();

            try
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        switch (line.Split()[0])
                        {
                            case "#":
                                continue;
                            case "floppy":
                                fs.Add(new FloppySettings(Convert.ToInt32(line.Split()[1]), Convert.ToInt32(line.Split()[2]), Convert.ToInt32(line.Split()[3])));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //ErrorEvent(e.Message);
            }            

            return fs;
        }
        private void makeDefaulCfg()
        {
            try
            {
                using (Stream stream = new FileStream("settings.cfg", FileMode.Create))
                {
                    using(StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine("# floppy <ID> <step_pin> <direction_pin>");
                        sw.WriteLine("floppy 1 3 4");
                        sw.WriteLine("floppy 2 5 6");
                    }
                }
            }
            catch (Exception e)
            {
                //ErrorEvent(e.Message);
            }
        }
    }
}
