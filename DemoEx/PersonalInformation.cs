using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DB;

namespace DemoEx
{
    public partial class PersonalInformation : Form
    {
        private Db db = new Db();
        private int id;
        public PersonalInformation(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void PersonalInformation_Load(object sender, EventArgs e)
        {
            db.setConnectionStr(Connection.getConnectionString());
            surname.Text = db.getValuesFromColumn($"select surname from clients where id={id};")[0];
            name.Text = db.getValuesFromColumn($"select name from clients where id={id};")[0];
            pat.Text = db.getValuesFromColumn($"select Patronymic from clients where id={id};")[0];
            passport.Text = db.getValuesFromColumn($"select passport from clients where id={id};")[0];
            dateTimePicker1.Value = db.getDateValuesFromColumn($"select birth from clients where id={id};")[0];
            phone.Text = db.getValuesFromColumn($"select phone_number from clients where id={id};")[0];
            type.Text = db.getValuesFromColumn($"select type from clients where id={id};")[0];
            address.Text = db.getValuesFromColumn($"select address from clients where id={id};")[0];
        }
    }
}
