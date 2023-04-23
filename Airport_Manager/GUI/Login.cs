using DTO;

namespace GUI
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login() => InitializeComponent();

        void btnLogin_Click(object sender, EventArgs e)
        {
            var emp = new Employee();
            var acc = new Account();

            if (true)
            {
                var frmMain = new Main(acc, emp);
                Hide();
                frmMain.ShowDialog();
                Show();
            }
        }
    }
}