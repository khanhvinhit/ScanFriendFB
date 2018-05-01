using MaterialSkin;
using MaterialSkin.Controls;
using ScanFriendFB.Properties;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using xNet;

namespace ScanFriendFB
{
    public partial class FrmLogin : MaterialForm
    {
        #region Biến

        private readonly MaterialSkinManager skinManager;
        private MainFrm frm = new MainFrm();

        private string c_user;

        private string xs;

        #endregion Biến

        #region Phương thức

        public FrmLogin()
        {
            InitializeComponent();

            skinManager = MaterialSkinManager.Instance;
            skinManager.AddFormToManage(this);
            skinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SetColor(Settings.Default.skin.GetHashCode());
            ToolTip tooltip = new ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 500;
            tooltip.ShowAlways = true;
            tooltip.SetToolTip(this.txtEmail, "Nhập tài khoản facebook vào đây!");
            tooltip.SetToolTip(this.txtPassword, "Nhập mật khẩu vào đây!");
            tooltip.SetToolTip(this.checkSave, "Chọn để lưu cookie cho lần đăng nhập tiếp theo!");
            tooltip.SetToolTip(this.btnLoginPwd, "Nhấn để đăng nhập!");
            tooltip.SetToolTip(this.txtUID, "Nhập id tài khoản facebook vào đây!");
            tooltip.SetToolTip(this.txtXS, "Nhập mã cookie vào đây!");
            tooltip.SetToolTip(this.txtClear, "Nhấn để xóa cookie!");
        }

        private void SetColor(int v)
        {
            if (v > 7) v = 0;

            //These are just example color schemes
            switch (v)
            {
                case 0:
                    skinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
                    break;

                case 1:
                    skinManager.ColorScheme = new ColorScheme(Primary.Indigo500, Primary.Indigo700, Primary.Indigo100, Accent.Pink200, TextShade.WHITE);
                    break;

                case 2:
                    skinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
                    break;

                case 3:
                    skinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red700, Primary.Red200, Accent.Green100, TextShade.WHITE);
                    break;

                case 4:
                    skinManager.ColorScheme = new ColorScheme(Primary.Pink600, Primary.Pink700, Primary.Pink200, Accent.Green100, TextShade.WHITE);
                    break;

                case 5:
                    skinManager.ColorScheme = new ColorScheme(Primary.Purple600, Primary.Purple700, Primary.Purple200, Accent.Red100, TextShade.WHITE);
                    break;

                case 6:
                    skinManager.ColorScheme = new ColorScheme(Primary.Yellow600, Primary.Yellow700, Primary.Yellow200, Accent.Blue100, TextShade.WHITE);
                    break;

                case 7:
                    skinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.Yellow100, TextShade.WHITE);
                    break;
            }
        }

        public void updateResult(string str)
        {
            base.Invoke((MethodInvoker)delegate
            {
                try
                {
                    lbsttL.Text = str;
                }
                catch
                {
                }
            });
        }

        private bool LoginWithPwd()
        {
            string input = FBGraph.Instance.req.Get("https://mbasic.facebook.com/login", null).ToString();
            string action = Regex.Match(input, "form method=\"post\" action=\"(.*?)\"").Groups[1].ToString().Replace("&amp;", "&");
            string lsd = Regex.Match(input, "name=\"lsd\" value=\"(.*?)\"").Groups[1].ToString();
            string m_ts = Regex.Match(input, "name=\"m_ts\" value=\"(.*?)\"").Groups[1].ToString();
            string li = Regex.Match(input, "name=\"li\" value=\"(.*?)\"").Groups[1].ToString();
            string try_number = Regex.Match(input, "name=\"try_number\" value=\"(.*?)\"").Groups[1].ToString();
            string unrecognized_tries = Regex.Match(input, "name=\"unrecognized_tries\" value=\"(.*?)\"").Groups[1].ToString();
            string login = Regex.Match(input, "input value=\"(.*?)\" type=\"submit\" name=\"login\"").Groups[1].ToString();
            string email = txtEmail.Text;
            string pass = txtPassword.Text;
            string param = $"lsd={lsd}&m_ts={m_ts}&li={li}&try_number={try_number}&unrecognized_tries={unrecognized_tries}&email={email}&pass={pass}&login={login}";
            FBGraph.Instance.req.Post(action, param, "application/x-www-form-urlencoded").ToString();
            if (FBGraph.Instance.req.Cookies.ContainsKey("c_user"))
            {
                c_user = Regex.Match(FBGraph.Instance.req.Cookies.ToString(), "c_user=([^;]+)").Groups[1].ToString();
                xs = Regex.Match(FBGraph.Instance.req.Cookies.ToString(), "xs=(.*?);").Groups[1].ToString();
                if (checkSave.Checked)
                {
                    txtUID.Text = c_user;
                    txtXS.Text = xs;
                }
                else
                {
                    txtUID.Text = string.Empty;
                    txtXS.Text = string.Empty;
                }
                updateResult("Đăng nhập thành công!");
                return true;
            }
            if (FBGraph.Instance.req.Cookies.ContainsKey("checkpoint"))
            {
                updateResult("Lỗi, vui lòng đăng nhập lại!");
                return false;
            }
            updateResult("Lỗi, vui lòng đăng nhập lại!");
            return false;
        }

        private bool LoginFBWithCookies()
        {
            CookieDictionary admin_cookie = new CookieDictionary(false);
            admin_cookie.Add("c_user", txtUID.Text);
            admin_cookie.Add("xs", txtXS.Text);
            FBGraph.Instance.req.Cookies = admin_cookie;
            if (Regex.Match(FBGraph.Instance.req.Get("https://mbasic.facebook.com/settings/account/?name", null).ToString(), "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].ToString() != "")
            {
                updateResult("Đăng nhập thành công!");
                return true;
            }
            updateResult("Lỗi, vui lòng đăng nhập lại!");
            return false;
        }

        #endregion Phương thức

        #region sự kiện

        private void txtClear_Click(object sender, EventArgs e)
        {
            txtUID.Text = string.Empty;
            txtXS.Text = string.Empty;
            checkSave.Checked = false;
        }

        private void btnLoginPwd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    if (LoginWithPwd())
                    {
                        if (checkSave.Checked)
                        {
                            txtUID.Text = c_user;
                            txtXS.Text = xs;
                            Settings.Default.userid = txtUID.Text;
                            Settings.Default.xs = txtXS.Text;
                            Settings.Default.check = checkSave.Checked;
                            Settings.Default.Save();
                        }
                        else
                        {
                            Settings.Default.userid = "";
                            Settings.Default.xs = "";
                            Settings.Default.check = checkSave.Checked;
                            Settings.Default.Save();
                        }
                        base.Hide();
                        frm.token = FBGraph.Instance.GetToken();
                        string name = FBGraph.Instance.GetPro();
                        frm.Text = "Death Click: Xin chào: " + name;
                        frm.Show();
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txtUID.Text) && !string.IsNullOrWhiteSpace(txtXS.Text))
                {
                    if (LoginFBWithCookies())
                    {
                        if (checkSave.Checked)
                        {
                            Settings.Default.userid = txtUID.Text;
                            Settings.Default.xs = txtXS.Text;
                            Settings.Default.check = checkSave.Checked;
                            Settings.Default.Save();
                        }
                        else
                        {
                            Settings.Default.userid = "";
                            Settings.Default.xs = "";
                            Settings.Default.check = checkSave.Checked;
                            Settings.Default.Save();
                        }
                        base.Hide();
                        frm.token = FBGraph.Instance.GetToken();
                        string name = FBGraph.Instance.GetPro();
                        frm.Text = "Death Click: Xin chào: " + name;
                        frm.Show();
                    }
                }
                else
                {
                    updateResult("Vui lòng nhập thông tin!");
                }
            }
            catch
            {
                updateResult("Không thể đăng nhập!");
            }
        }

        private void txtUID_TextChanged(object sender, EventArgs e)
        {
            updateResult("");
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUID.Text = Settings.Default.userid;
            txtXS.Text = Settings.Default.xs;
            checkSave.Checked = Settings.Default.check;
        }

        private void txtXS_TextChanged(object sender, EventArgs e)
        {
            updateResult("");
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            updateResult("");
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            updateResult("");
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLoginPwd.PerformClick();
            }
        }

        private void txtXS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLoginPwd.PerformClick();
            }
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
            Application.Exit();
        }

        #endregion sự kiện
    }
}