using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace MyMenu
{
    public partial class GameForm : Form
    {
        List<InterfeysForm> frm = new List<InterfeysForm>();
        private static string path = Directory.GetCurrentDirectory();
        private static string f_name ="";

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            f_name = path + @"\" + Form1.userGo + ".dat";
            this.Text = Form1.userGo;
            FileInfo fileInf = new FileInfo(f_name);

            if (fileInf.Exists != false)
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    using (FileStream fs = new FileStream(f_name, FileMode.Open))
                    {
                        List<InterfeysForm> frm_t = (List<InterfeysForm>)formatter.Deserialize(fs);
                        foreach(InterfeysForm f in frm_t)
                        {
                            frm.Add(f);
                        }
                    }
                    comboBox1.SelectedIndex = frm[0].itemIndex;
                    textBox1.Text = frm[0].Mytext;
                    panel1.BackColor = Color.FromArgb(frm[0].colorPanel);
                    this.BackColor= Color.FromArgb(frm[0].colorForm);
                }
                catch (FileNotFoundException)
                {
                    fileInf.Create();
                }
            }
            else
            {
                if (fileInf.Exists == false)
                {
                    fileInf.Create().Close();
                }
            }
        }

        private void btnPanelColor_Click(object sender, EventArgs e)
        {
           
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
              
            }
           
        }

        private void btnFormColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog1.Color;
            }
         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
          
            Close();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)

            {

            frm.Clear();
            InterfeysForm f = new InterfeysForm();
            f.colorForm = this.BackColor.ToArgb(); 
            f.colorPanel = panel1.BackColor.ToArgb();
            f.Mytext = textBox1.Text;
            f.itemIndex = comboBox1.SelectedIndex;
            frm.Add(f);
            FileStream myStream;

            if ((myStream = new FileStream(f_name, FileMode.Create)) != null)
            {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(myStream, frm);

                myStream.Close();
            }
            f_name = null;
        }
        }
    }
}
