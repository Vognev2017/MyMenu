using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace MyMenu
{
    public partial class Form1 : Form
    {
        InterfeysForm usForm = new InterfeysForm();
        static public string userGo=null;
        private SQLiteConnection connect;

        public Form1()
        {
            InitializeComponent();
            
        }

        public bool CorectEmaill(string s)
        {
            char ch = '@';
            char ch1 = '.';
            int i_ch = -1;
            int i_ch1 = -1;
            int ch_schet = 0;
            int ch1_schet = 0;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].CompareTo(ch) == 0)
                {
                    i_ch = i;

                    ch_schet++;
                }
                if (s[i].CompareTo(ch1) == 0)
                {
                    i_ch1 = i;

                    ch1_schet++;
                }
            }
            if (ch_schet == 1 && ch1_schet == 1 && i_ch > 3 && i_ch1 > i_ch && i_ch1 < s.Length - 1)
            {
                errorEmail.Clear();
                return true;
            }
            else
            {
                errorEmail.SetError(txtEmail, "Введите коректный адрес.");
                txtEmail.Clear();
                return false;
            }

           
        }

        public bool ProverckaUserToBasa(string nick)
        {
            bool control = false;
            SQLiteCommand command = connect.CreateCommand();

            command.CommandText = "SELECT login,pass FROM UserGame WHERE login=@login ";

            command.Parameters.Add("@login", DbType.String).Value =nick;
          
            SQLiteDataReader sql = command.ExecuteReader();

            if (sql.HasRows)
            {
                   control =  false;  
            }
            else { 
                 control =true ;
            }
            return control;
        }

        private void btnGo_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnGo.ForeColor = Color.Coral;
            btnGo.Height=40;
            btnGo.Width = 130;
        }

        private void btnGo_MouseLeave(object sender, EventArgs e)
        {
            this.btnGo.ForeColor = Color.White;
            btnGo.Height = 30;
            btnGo.Width = 120;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connect = new SQLiteConnection("Data Source=For_Game.db;version=3");
            connect.Open();
            panelReg.Left = 130;
        }

        private void btnReg_MouseLeave(object sender, EventArgs e)
        {
            this.btnReg.ForeColor = Color.White;
            btnReg.Height = 30;
            btnReg.Width = 120;
        }

        private void btnReg_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnReg.ForeColor = Color.Coral;
            btnReg.Height = 40;
            btnReg.Width = 130;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.ForeColor = Color.White;
            btnClose.Height = 30;
            btnClose.Width = 120;
        }

        private void btnClose_MouseMove(object sender, MouseEventArgs e)
        {this.btnClose.ForeColor= Color.Coral;
            btnClose.Height = 40;
            btnClose.Width = 130;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            panelGo.Visible = true;
            panelReg.Visible = false;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            panelGo.Visible = false;
            panelReg.Left = 130;
            panelReg.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panelReg.Visible = false;
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (ProverckaUserToBasa(txtLoginReg.Text) == true)
            {
                if (!string.IsNullOrEmpty(txtFam.Text) && !string.IsNullOrWhiteSpace(txtFam.Text) &&
                   !string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrWhiteSpace(txtName.Text) &&
                   !string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtEmail.Text) &&
                   !string.IsNullOrEmpty(txtLoginReg.Text) && !string.IsNullOrWhiteSpace(txtLoginReg.Text) &&
                   !string.IsNullOrEmpty(txtPassReg.Text) && !string.IsNullOrWhiteSpace(txtPassReg.Text) &&
                   !string.IsNullOrEmpty(txtPassReg1.Text) && !string.IsNullOrWhiteSpace(txtPassReg1.Text) &&
                   txtPassReg.Text == txtPassReg1.Text && CorectEmaill(txtEmail.Text) == true)
                   
                {
                    SQLiteCommand command = connect.CreateCommand();
                    command.CommandText = "INSERT INTO UserGame(fam,name,email,login,pass,info)values(@fam,@name,@email,@login,@pass,@info)";
                    command.Parameters.Add("@fam", DbType.String).Value = txtFam.Text.ToUpper();
                    command.Parameters.Add("@name", DbType.String).Value = txtName.Text.ToUpper();
                    command.Parameters.Add("@email", DbType.String).Value = txtEmail.Text.ToUpper();
                    command.Parameters.Add("@login", DbType.String).Value = txtLoginReg.Text;
                    command.Parameters.Add("@pass", DbType.String).Value = txtPassReg.Text;
                    command.Parameters.Add("@info", DbType.String).Value = "no";
                    command.ExecuteNonQuery();
                    panelReg.Visible = false;
                }
                else MessageBox.Show("Нужно заполнить все поля.", "Незаполненные поля.");
            }
            else
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован.", "Регистрация.");
                
                txtLoginReg.Clear();
                txtPassReg.Clear();
                txtPassReg1.Clear();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            bool openFile = false;
            if (!string.IsNullOrEmpty(txtLogin.Text) && !string.IsNullOrWhiteSpace(txtLogin.Text) &&
                !string.IsNullOrEmpty(txtPass.Text) && !string.IsNullOrWhiteSpace(txtPass.Text))
            { 
            SQLiteCommand command = connect.CreateCommand();
                
            command.CommandText = "SELECT login,pass FROM UserGame WHERE login=@login and pass=@pass"; 
                
            command.Parameters.Add("@login", DbType.String).Value = txtLogin.Text;
            command.Parameters.Add("@pass", DbType.String).Value = txtPass.Text;
                SQLiteDataReader sqlRead = command.ExecuteReader();
                if(sqlRead.HasRows)
                {
                    
                    
                    userGo= txtLogin.Text;
                    openFile = true;
                        GameForm g_frm = new GameForm();
                    g_frm.Show();
                  
                }
                else
                {
                    openFile = false;
                    panelGo.Visible = false;
                    MessageBox.Show("Вы не зарегистрированы в системе.", "Регистрация.");
                    
                    panelReg.Visible = true;
                }
            }
        else
            { MessageBox.Show("Нужно заполнить все поля.", "Незаполненные поля."); }
            if (openFile == true) panelGo.Visible = false;
            else {
                //panelGo.Visible = false;
                
            }
            txtLogin.Text = "";
            txtPass.Text = "";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            connect.Close();
        }
    }
}
