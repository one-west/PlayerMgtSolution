using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlayerMgtForm
{
    public partial class LoginForm : Form
    {
        private const String id = "root";
        private const String pw = "1234";
        public static string Id => id;

        public LoginForm()
        {
            InitializeComponent();
            btnLogin.Click += BtnLogin_Click;
            txtID.KeyPress += TxtID_KeyPress;
            txtPW.KeyPress += TxtPw_KeyPress;
            this.FormClosing += PlayerMgt_FormClosing;
        }

        private void TxtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            };
        }

        private void TxtPw_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BtnLogin_Click(sender, e);
            }
        }

        private void PlayerMgt_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (txtID.Text.Equals(id) && txtPW.Text.Equals(pw))
            {   
                this.Visible = false;
                MainForm mainForm = new MainForm();
                mainForm.ShowDialog();
                this.Close();
            }
            else if (txtID.Text.Equals(""))
            {
                lbState.Text = "아이디를 입력해주세요";
                txtID.Focus();
 
            }
            else if (txtPW.Text.Equals(""))
            {
                lbState.Text = "비밀번호를 입력해주세요";
                txtPW.Focus();
            }
            else
            {
                lbState.Text = "아이디 또는 비밀번호를 잘못 입력했습니다.\r\n입력하신 내용을 다시 확인해주세요.";
            }
        }
    }
}