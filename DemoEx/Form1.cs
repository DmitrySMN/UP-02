using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using DB;
using System.Configuration;
using System.Threading;
using System.Text.RegularExpressions;


namespace DemoEx
{
    public partial class Form1 : Form
    {
        private Db db = new Db();
        public Form1()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            db.setConnectionStr(Connection.getConnectionString());
            pwdTb.PasswordChar = '·';

        }

        private void loginTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!char.IsNumber(ch) && !char.IsLetter(ch) && !char.IsControl(ch) && ch != '_') 
            {
                e.Handled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pwdTb.PasswordChar = '\0';
            } else
            {
                pwdTb.PasswordChar = '·';
            }
        }


        private void loginTb_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            string s = e.KeyChar.ToString();
            if (!Regex.Match(s, @"^[a-zA-Z]+$|[\b]|[_]|[0-9]").Success)
            {
                e.Handled = true;
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Вы уверенны что хотите, выйти из приложения?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (res == DialogResult.Yes)
            {
                Close();
            } else
            {
                return;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            try
            {
                if (loginTb.Text.Length == 0 || pwdTb.Text.Length == 0)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                                
                if (loginTb.Text == "admin" && pwdTb.Text == "admin")
                {
                    new AdminForm().ShowDialog();
                    return;
                } else
                {
                    string login = loginTb.Text;
                    string pwd = pwdTb.Text;
                    string pwdFromDb = db.getValuesFromColumn($"select password from employees where login='{login}';")[0];
                    if (db.getHashFromPassword(pwd) == pwdFromDb)
                    {
                        int post = Convert.ToInt32(db.getIntValuesFromColumn($"select post from employees where login='{login}';")[0]);
                        new MainForm(login, post).ShowDialog();
                        loginTb.Clear();
                        pwdTb.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                
            } catch(Exception ex)
            {
              
            }
        }
    }
}
