namespace ScanFriendFB
{
    partial class FrmLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkSave = new MaterialSkin.Controls.MaterialCheckBox();
            this.btnLoginPwd = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtPassword = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtEmail = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtClear = new MaterialSkin.Controls.MaterialRaisedButton();
            this.txtXS = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.txtUID = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.lbsttL = new System.Windows.Forms.Label();
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabSelector1.BaseTabControl = this.materialTabControl1;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(0, 64);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(354, 29);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(6, 93);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(342, 95);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.txtEmail);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(334, 96);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tài khoản";
            // 
            // checkSave
            // 
            this.checkSave.AutoSize = true;
            this.checkSave.Depth = 0;
            this.checkSave.Font = new System.Drawing.Font("Roboto", 10F);
            this.checkSave.Location = new System.Drawing.Point(6, 196);
            this.checkSave.Margin = new System.Windows.Forms.Padding(0);
            this.checkSave.MouseLocation = new System.Drawing.Point(-1, -1);
            this.checkSave.MouseState = MaterialSkin.MouseState.HOVER;
            this.checkSave.Name = "checkSave";
            this.checkSave.Ripple = true;
            this.checkSave.Size = new System.Drawing.Size(60, 30);
            this.checkSave.TabIndex = 2;
            this.checkSave.Text = "Save";
            this.checkSave.UseVisualStyleBackColor = true;
            // 
            // btnLoginPwd
            // 
            this.btnLoginPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoginPwd.AutoSize = true;
            this.btnLoginPwd.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnLoginPwd.Depth = 0;
            this.btnLoginPwd.Icon = null;
            this.btnLoginPwd.Location = new System.Drawing.Point(249, 196);
            this.btnLoginPwd.MouseState = MaterialSkin.MouseState.HOVER;
            this.btnLoginPwd.Name = "btnLoginPwd";
            this.btnLoginPwd.Primary = true;
            this.btnLoginPwd.Size = new System.Drawing.Size(99, 36);
            this.btnLoginPwd.TabIndex = 3;
            this.btnLoginPwd.Text = "Đăng nhập";
            this.btnLoginPwd.UseVisualStyleBackColor = true;
            this.btnLoginPwd.Click += new System.EventHandler(this.btnLoginPwd_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Depth = 0;
            this.txtPassword.Hint = "Mật khẩu";
            this.txtPassword.Location = new System.Drawing.Point(6, 40);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '\0';
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.Size = new System.Drawing.Size(322, 23);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.TabStop = false;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // txtEmail
            // 
            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.Depth = 0;
            this.txtEmail.Hint = "Tài khoản";
            this.txtEmail.Location = new System.Drawing.Point(6, 11);
            this.txtEmail.MaxLength = 32767;
            this.txtEmail.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.SelectedText = "";
            this.txtEmail.SelectionLength = 0;
            this.txtEmail.SelectionStart = 0;
            this.txtEmail.Size = new System.Drawing.Size(322, 23);
            this.txtEmail.TabIndex = 0;
            this.txtEmail.TabStop = false;
            this.txtEmail.UseSystemPasswordChar = false;
            this.txtEmail.TextChanged += new System.EventHandler(this.txtEmail_TextChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.txtXS);
            this.tabPage2.Controls.Add(this.txtUID);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(334, 69);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Cookie";
            // 
            // txtClear
            // 
            this.txtClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClear.AutoSize = true;
            this.txtClear.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.txtClear.Depth = 0;
            this.txtClear.Icon = null;
            this.txtClear.Location = new System.Drawing.Point(195, 196);
            this.txtClear.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtClear.Name = "txtClear";
            this.txtClear.Primary = true;
            this.txtClear.Size = new System.Drawing.Size(48, 36);
            this.txtClear.TabIndex = 3;
            this.txtClear.Text = "Xóa";
            this.txtClear.UseVisualStyleBackColor = true;
            this.txtClear.Click += new System.EventHandler(this.txtClear_Click);
            // 
            // txtXS
            // 
            this.txtXS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXS.Depth = 0;
            this.txtXS.Hint = "XS";
            this.txtXS.Location = new System.Drawing.Point(6, 40);
            this.txtXS.MaxLength = 32767;
            this.txtXS.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtXS.Name = "txtXS";
            this.txtXS.PasswordChar = '\0';
            this.txtXS.SelectedText = "";
            this.txtXS.SelectionLength = 0;
            this.txtXS.SelectionStart = 0;
            this.txtXS.Size = new System.Drawing.Size(322, 23);
            this.txtXS.TabIndex = 1;
            this.txtXS.TabStop = false;
            this.txtXS.UseSystemPasswordChar = true;
            this.txtXS.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtXS_KeyDown);
            this.txtXS.TextChanged += new System.EventHandler(this.txtXS_TextChanged);
            // 
            // txtUID
            // 
            this.txtUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUID.Depth = 0;
            this.txtUID.Hint = "Tài khoản";
            this.txtUID.Location = new System.Drawing.Point(6, 11);
            this.txtUID.MaxLength = 32767;
            this.txtUID.MouseState = MaterialSkin.MouseState.HOVER;
            this.txtUID.Name = "txtUID";
            this.txtUID.PasswordChar = '\0';
            this.txtUID.SelectedText = "";
            this.txtUID.SelectionLength = 0;
            this.txtUID.SelectionStart = 0;
            this.txtUID.Size = new System.Drawing.Size(322, 23);
            this.txtUID.TabIndex = 0;
            this.txtUID.TabStop = false;
            this.txtUID.UseSystemPasswordChar = false;
            this.txtUID.TextChanged += new System.EventHandler(this.txtUID_TextChanged);
            // 
            // lbsttL
            // 
            this.lbsttL.AutoSize = true;
            this.lbsttL.BackColor = System.Drawing.Color.Transparent;
            this.lbsttL.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbsttL.ForeColor = System.Drawing.Color.Red;
            this.lbsttL.Location = new System.Drawing.Point(13, 230);
            this.lbsttL.Name = "lbsttL";
            this.lbsttL.Size = new System.Drawing.Size(0, 18);
            this.lbsttL.TabIndex = 2;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 263);
            this.Controls.Add(this.checkSave);
            this.Controls.Add(this.lbsttL);
            this.Controls.Add(this.btnLoginPwd);
            this.Controls.Add(this.txtClear);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.materialTabSelector1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmLogin";
            this.Sizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmLogin_FormClosed);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtPassword;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtEmail;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialRaisedButton btnLoginPwd;
        private MaterialSkin.Controls.MaterialRaisedButton txtClear;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtXS;
        private MaterialSkin.Controls.MaterialSingleLineTextField txtUID;
        private MaterialSkin.Controls.MaterialCheckBox checkSave;
        private System.Windows.Forms.Label lbsttL;
    }
}

