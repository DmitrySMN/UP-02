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
    public partial class ChangePassword : Form
    {
        private Db db = new Db();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            db.setConnectionStr(Connection.getConnectionString());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
