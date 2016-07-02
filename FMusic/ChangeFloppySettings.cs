using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FMusic
{
    public partial class ChangeFloppySettings : Form
    {
        private FloppySettings fs = null;

        private delegate void _refError(string msg);
        private _refError ErrorEvent = null;

        private int _id_obj = 0;

        public ChangeFloppySettings(GeneralForm gf, int id, int step, int direct)
        {
            InitializeComponent();
            _id_obj = id;

            ErrorEvent = new _refError(gf.GeneralForm_ErrorEvent);

            button_accept.Click += button_accept_Click;
            button_cancel.Click += button_cancel_Click;

            numericUpDown_id.Value = id;
            numericUpDown_step.Value = step;
            numericUpDown_dir.Value = direct;

            numericUpDown_id.ValueChanged += NumericUpDown_ValueChanged;
            numericUpDown_step.ValueChanged += NumericUpDown_ValueChanged;
            numericUpDown_dir.ValueChanged += NumericUpDown_ValueChanged;
        }

        public ChangeFloppySettings(GeneralForm gf)
        {
            InitializeComponent();

            ErrorEvent = new _refError(gf.GeneralForm_ErrorEvent);

            button_accept.Click += button_CreatePanel;
            button_accept.Text = "Create";
            button_cancel.Click += button_cancel_Click;

            this.Text = "Добавить новый floppy привод";

            numericUpDown_id.ValueChanged += NumericUpDown_ValueChanged;
            numericUpDown_step.ValueChanged += NumericUpDown_ValueChanged;
            numericUpDown_dir.ValueChanged += NumericUpDown_ValueChanged;
        }

        private void button_RemovePanel(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if((sender as NumericUpDown).Value >= (sender as NumericUpDown).Maximum)
                (sender as NumericUpDown).Maximum = (sender as NumericUpDown).Value + 1;
            else if((sender as NumericUpDown).Value < 0)
                (sender as NumericUpDown).Value = 0;
        }

        private void button_accept_Click(object sender, EventArgs e)
        {
            fs = new FloppySettings(
                Convert.ToInt32(numericUpDown_id.Value), 
                Convert.ToInt32(numericUpDown_step.Value), 
                Convert.ToInt32(numericUpDown_dir.Value));
            changeConf(_id_obj, false, fs);
            result(DialogResult.OK);
        }
        private void button_CreatePanel(object sender, EventArgs e)
        {
            fs = new FloppySettings(
                Convert.ToInt32(numericUpDown_id.Value),
                Convert.ToInt32(numericUpDown_step.Value),
                Convert.ToInt32(numericUpDown_dir.Value));
            addConf(fs);
            result(DialogResult.OK);
        }
        private void button_cancel_Click(object sender, EventArgs e)
        {
            result(DialogResult.Cancel);
        }

        private void changeConf(int id, bool rem, FloppySettings settings)
        {
            try
            {
                if (File.Exists("settings.cfg"))
                {
                    using (StreamReader sr = new StreamReader("settings.cfg"))
                    {
                        List<string> lines = new List<string>();
                        while (true)
                        {
                            string str = sr.ReadLine();
                            if (str == null) break;
                            lines.Add(str);
                        }
                        int val = 0;
                        foreach (var i in lines)
                        {
                            if(i.Contains("floppy " + id.ToString()))
                            {
                                if (!rem)
                                {
                                    lines.Remove(i);
                                    lines.Insert(val,
                                    "floppy " + settings.getID.ToString() + " "
                                    + settings.getStepPin.ToString() + " "
                                    + settings.getDirPin.ToString());
                                    break;
                                }
                                else
                                {
                                    lines.RemoveAll((string s) => { if (s.Equals(i)) return true; else return false; });
                                    break;
                                }
                            }
                            
                            val++;
                        }
                        sr.Close();

                        using(StreamWriter sw = new StreamWriter("settings.cfg", false))
                        {
                            foreach (var i in lines)
                                sw.WriteLine(i);
                            sw.Close();
                        }
                    }
                }
                else
                {
                    ErrorEvent("Не найден файл settings.cfg !");
                    result(DialogResult.Abort);
                }
            }
            catch (Exception e)
            {
                ErrorEvent(e.Message);
            }
        }
        private void addConf(FloppySettings settings)
        {
            try
            {
                if (File.Exists("settings.cfg"))
                {
                    using (StreamWriter sw = new StreamWriter("settings.cfg", true))
                    {
                        sw.WriteLine(
                            "floppy " + settings.getID + " "
                            + settings.getStepPin + " "
                            + settings.getDirPin);
                        sw.Close();
                    }
                }
                else
                {
                    ErrorEvent("Не найден файл settings.cfg !");
                    result(DialogResult.Abort);
                }
            }
            catch (Exception e)
            {
                ErrorEvent(e.Message);
            }
        }
        private void result(DialogResult dr) { DialogResult = dr; }
    }
}
