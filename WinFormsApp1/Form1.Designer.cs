namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lbDangNhap = new Label();
            tbUsername = new Guna.UI2.WinForms.Guna2TextBox();
            tbPassword = new Guna.UI2.WinForms.Guna2TextBox();
            pictureBox1 = new PictureBox();
            btnLogin = new Guna.UI2.WinForms.Guna2Button();
            lbLoginError = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lbDangNhap
            // 
            lbDangNhap.AutoSize = true;
            lbDangNhap.Font = new Font("Shopee Display", 16.1999989F, FontStyle.Regular, GraphicsUnit.Point);
            lbDangNhap.Location = new Point(260, 30);
            lbDangNhap.Name = "lbDangNhap";
            lbDangNhap.Size = new Size(148, 39);
            lbDangNhap.TabIndex = 1;
            lbDangNhap.Text = "Đăng nhập";
            // 
            // tbUsername
            // 
            tbUsername.BorderRadius = 18;
            tbUsername.CustomizableEdges = customizableEdges1;
            tbUsername.DefaultText = "";
            tbUsername.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbUsername.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbUsername.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbUsername.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbUsername.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbUsername.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbUsername.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbUsername.IconLeft = Properties.Resources.username;
            tbUsername.Location = new Point(260, 87);
            tbUsername.Name = "tbUsername";
            tbUsername.PasswordChar = '\0';
            tbUsername.PlaceholderText = "Tên đăng nhập";
            tbUsername.SelectedText = "";
            tbUsername.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tbUsername.Size = new Size(250, 45);
            tbUsername.TabIndex = 4;
            tbUsername.WordWrap = false;
            tbUsername.KeyPress += tbUsername_KeyPress;
            // 
            // tbPassword
            // 
            tbPassword.BorderRadius = 18;
            tbPassword.CustomizableEdges = customizableEdges3;
            tbPassword.DefaultText = "";
            tbPassword.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tbPassword.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tbPassword.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tbPassword.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tbPassword.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tbPassword.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tbPassword.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tbPassword.IconLeft = Properties.Resources.password;
            tbPassword.Location = new Point(260, 138);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.PlaceholderText = "Mật khẩu";
            tbPassword.SelectedText = "";
            tbPassword.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tbPassword.Size = new Size(250, 45);
            tbPassword.TabIndex = 5;
            tbPassword.WordWrap = false;
            tbPassword.KeyPress += tbPassword_KeyPress;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.reception;
            pictureBox1.Location = new Point(12, 30);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(218, 229);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // btnLogin
            // 
            btnLogin.BorderRadius = 18;
            btnLogin.CustomizableEdges = customizableEdges5;
            btnLogin.DisabledState.BorderColor = Color.DarkGray;
            btnLogin.DisabledState.CustomBorderColor = Color.DarkGray;
            btnLogin.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnLogin.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnLogin.Font = new Font("Shopee Display", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(260, 189);
            btnLogin.Name = "btnLogin";
            btnLogin.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnLogin.Size = new Size(250, 45);
            btnLogin.TabIndex = 7;
            btnLogin.Text = "Đăng nhập";
            btnLogin.Click += btnLogin_Click;
            // 
            // lbLoginError
            // 
            lbLoginError.AutoSize = true;
            lbLoginError.Font = new Font("Shopee Display", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lbLoginError.ForeColor = Color.Red;
            lbLoginError.Location = new Point(260, 237);
            lbLoginError.Name = "lbLoginError";
            lbLoginError.Size = new Size(255, 22);
            lbLoginError.TabIndex = 8;
            lbLoginError.Text = "Thông tin đăng nhập không chính xác!";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(559, 285);
            Controls.Add(lbLoginError);
            Controls.Add(btnLogin);
            Controls.Add(pictureBox1);
            Controls.Add(tbPassword);
            Controls.Add(tbUsername);
            Controls.Add(lbDangNhap);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimumSize = new Size(577, 332);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập : Nhóm 6 : Hotel Management";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbDangNhap;
        private Guna.UI2.WinForms.Guna2TextBox tbUsername;
        private Guna.UI2.WinForms.Guna2TextBox tbPassword;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnLogin;
        private Label lbLoginError;
    }
}