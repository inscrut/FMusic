using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FMusic
{
    public partial class GeneralForm : Form
    {
        private Panel gpanel = null;
        private ContextMenuStrip cms = null;
        private List<ItemPanel> items = null;

        private delegate void _RError(string msg);
        private event _RError ErrorEvent;

        public GeneralForm()
        {
            InitializeComponent();

            this.Size = new Size(545, 285);
            this.Resize += GeneralForm_Resize;

            gpanel = new Panel();
            gpanel.AutoScroll = true;
            gpanel.Location = new Point(0, 24);
            gpanel.Size = new Size(this.Size.Width - 15, this.Size.Height - 60);
            gpanel.Resize += Gpanel_Resize;
            gpanel.MouseClick += Gpanel_MouseClick;
            this.Controls.Add(gpanel);

            ErrorEvent += GeneralForm_ErrorEvent;

            //
            this.WindowState = FormWindowState.Minimized;
        }

        public void GeneralForm_ErrorEvent(string msg)
        {
            // Общий метод показа ошибок...
            // ...Потому что я так хочу
            MessageBox.Show(msg, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void GeneralForm_Load(object sender, EventArgs e)
        {
            initProg();
            //
            TimeLine tl = new TimeLine();
            tl.Show();
        }
        private void выйтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Item_ItemClicked(int id, EventArgs e)
        {
            try
            {
                if ((e as MouseEventArgs).Button == MouseButtons.Left)
                {
                    foreach (var i in items)
                        if (i.getID == id)
                        {
                            if(i.setPath == null)
                            {
                                using (OpenFileDialog ofd = new OpenFileDialog())
                                    if (ofd.ShowDialog() == DialogResult.OK) i.setPath = ofd.FileName;
                            }
                            else
                            {
                                //MessageBox.Show(i.setPath);
                            }
                        }
                }
                else if ((e as MouseEventArgs).Button == MouseButtons.Right)
                {
                    foreach (var i in items)
                        if (i.getID == id)
                        {
                            if (cms != null) cms.Items.Clear();
                            cms = new ContextMenuStrip();
                            cms.Items.Add("Изменить");
                            cms.Items.Add("Удалить");
                            cms.Items[0].Click += (object sender, EventArgs ex) => 
                            {
                                using (ChangeFloppySettings cfs = new ChangeFloppySettings(this, i.getID, i.getStepPin, i.getDirPin))
                                {
                                    cfs.ShowDialog();
                                    if (cfs.DialogResult == DialogResult.OK)
                                        initProg();
                                }
                            };
                            cms.Items[1].Click += (object sender, EventArgs ex) => 
                            {
                                if(MessageBox.Show(
                                    "Удалить floppy привод?", 
                                    "Подтвердите действие", 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    removeDrives(i.getID);
                                    initProg();
                                }
                            };
                            cms.Show(i.getItem, new Point((e as MouseEventArgs).X, (e as MouseEventArgs).Y));
                            break;
                        }
                }
                else { }
            }
            catch (Exception ex)
            {
                ErrorEvent(ex.Message);
            }
        }
        private void Gpanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if(cms != null) cms.Items.Clear();
                cms = new ContextMenuStrip();
                cms.Items.Add("Добавить привод");
                cms.Items[0].Click += (object ssender, EventArgs se) => 
                {
                    using (ChangeFloppySettings cfs = new ChangeFloppySettings(this))
                    {
                        cfs.ShowDialog();
                        if (cfs.DialogResult == DialogResult.OK)
                            initProg();
                    }
                };
                cms.Show(gpanel, new Point(e.X, e.Y));
            }
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
            try
            {
                if (gpanel != null) gpanel.Controls.Clear();
                if (items != null) items.Clear(); //На всякий...

                items = new List<ItemPanel>();

                if (File.Exists("settings.cfg"))
                    addItems();
                else
                {
                    if ( MessageBox.Show(
                         "Файл settings.cfg не найден или не существует!\r\nСоздать новый?", 
                         "Внимание!", 
                         MessageBoxButtons.YesNo, 
                         MessageBoxIcon.Warning) == DialogResult.Yes )
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

        //Для читаемости кода вынес в подпрограммы
        private void addItems()
        {
            using (Stream stream = new FileStream("settings.cfg", FileMode.Open))
            {
                foreach (var i in readSettings(stream))
                    items.Add(new ItemPanel(i));
                stream.Close();
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
                sr.Close();
            }

            return fs;
        }
        private void makeDefaulCfg()
        {
            using (Stream stream = new FileStream("settings.cfg", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(stream))
                {
                    sw.WriteLine("# floppy <ID> <step_pin> <direction_pin>");
                    sw.WriteLine("floppy 1 3 4");
                    sw.WriteLine("floppy 2 5 6");
                    sw.Close();
                }
                stream.Close();
            }
        }
        private void removeDrives(int id)
        {
            if (File.Exists("settings.cfg"))
            {
                using (StreamReader sr = new StreamReader("settings.cfg"))
                {
                    List<string> lines = new List<string>();
                    while (true)
                    {
                        string line = sr.ReadLine();
                        if (line == null) break;
                        lines.Add(line);
                    }
                    sr.Close();
                    lines.RemoveAll((string s) => { if (s.Contains("floppy " + id.ToString())) return true; else return false; });
                    using (StreamWriter sw = new StreamWriter("settings.cfg", false))
                    {
                        foreach (var i in lines)
                            sw.WriteLine(i);
                        sw.Close();
                    }
                }
            }
        }
    }
}
