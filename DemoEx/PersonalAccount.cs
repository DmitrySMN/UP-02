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
using DB;

namespace DemoEx
{
    public partial class PersonalAccount : Form
    {
        private string login;
        private Db db = new Db();
        private string photo = "user.png";

        public PersonalAccount(string login)
        {
            InitializeComponent();
            this.login = login; 
        }

        private void PersonalAccount_Load(object sender, EventArgs e)
        {
            db.setConnectionStr(Connection.getConnectionString());
            this.Text = "Личный кабинет - " + db.getValuesFromColumn($"select concat(surname,' ', name, ' ', patronymic) from employees where login='{login}';")[0];
            pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\imges\\profile\\" + db.getValuesFromColumn($"select photo from employees where login='{login}';")[0]);
            //db.FillDGV(dataGridView1, $"select * from deals where employees=(select id from employees where login='{login}');");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "c:\\";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog1.FileName;
                FileInfo fi = new FileInfo(filePath);
                string[] path = filePath.Split('\\');
                photo = path[path.Length - 1];
                string newFilePath = $"\\imges\\profile\\{photo}";
                fi.CopyTo("." + newFilePath, true);
                pictureBox1.Image = Image.FromFile(Directory.GetCurrentDirectory() + "\\imges\\profile\\" + photo);
                db.updateTable($"update employees set photo='{photo}' where login='{login}';");
            }
            else
            {
                MessageBox.Show("Выберите файл!");
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();
        }
    }
}
