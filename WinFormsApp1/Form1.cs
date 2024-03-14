namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        function fn = new function();
        public Form1()
        {
            InitializeComponent();
            lbLoginError.Visible = false;
            tbUsername.Focus();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUsername.Text))
            {
                lbLoginError.Text = "Vui lòng nhập username!";
                lbLoginError.Visible = true;
                tbUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                lbLoginError.Text = "Vui lòng nhập password!";
                lbLoginError.Visible = true;
                tbPassword.Focus();
                return;
            }

            bool isValidLogin = authentication(tbUsername.Text, tbPassword.Text);
            if (isValidLogin)
            {
                string role = fn.getUserRole(tbUsername.Text);
                Dashboard ds = new Dashboard(tbUsername.Text, role);
                this.Hide();
                ds.Show();
                ds.FormClosed += (s, args) => this.Close();
            }
            else
            {
                lbLoginError.Text = "Thông tin đăng nhập không chính xác!";
                lbLoginError.Visible = true;
                tbUsername.Focus();
            }
        }

        private void tbPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(tbPassword.Text))
                {
                    lbLoginError.Text = "Vui lòng nhập password!";
                    lbLoginError.Visible = true;
                } else if (string.IsNullOrEmpty(tbUsername.Text))
                {
                    tbUsername.Focus();
                } else
                {
                    btnLogin.PerformClick();
                }
            }
        }

        private void tbUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (string.IsNullOrEmpty(tbUsername.Text))
                {
                    lbLoginError.Text = "Vui lòng nhập username!";
                    lbLoginError.Visible = true;
                } else if (string.IsNullOrEmpty(tbPassword.Text))
                {
                    tbPassword.Focus();
                } else
                {
                    btnLogin.PerformClick();
                }
            }
        }

        private bool authentication(string username, string password)
        {
            return(fn.validateLogin(username, password));
        }
    }
}