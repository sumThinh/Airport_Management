using DevExpress.XtraEditors;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            Account acc = new Account();


            if (true)
            {
                Main frmMain = new Main(acc, emp);
                this.Hide();
                frmMain.ShowDialog();
                Application.Exit();
            }
        }
    }
}