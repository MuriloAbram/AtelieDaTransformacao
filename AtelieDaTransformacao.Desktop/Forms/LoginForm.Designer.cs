using static Guna.UI2.WinForms.Suite.Descriptions;

namespace AtelieDaTransformacao.Desktop.Forms
{
    partial class LoginForm
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
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pnlBrand = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            lblAppName = new Label();
            lblSlogan = new Label();
            pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            lblCred = new Label();
            btnClose = new Guna.UI2.WinForms.Guna2ControlBox();
            btnMinimize = new Guna.UI2.WinForms.Guna2ControlBox();
            lblTitle = new Label();
            txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            btnSubmit = new Guna.UI2.WinForms.Guna2Button();
            btnToggleMode = new Guna.UI2.WinForms.Guna2Button();
            label1 = new Label();
            lblStatus = new Label();
            dragControl = new Guna.UI2.WinForms.Guna2DragControl(components);
            shadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(components);
            borderForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            pnlBrand.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlContent.SuspendLayout();
            SuspendLayout();
            // 
            // pnlBrand
            // 
            pnlBrand.Controls.Add(pictureBox1);
            pnlBrand.Controls.Add(lblAppName);
            pnlBrand.Controls.Add(lblSlogan);
            pnlBrand.CustomizableEdges = customizableEdges1;
            pnlBrand.Dock = DockStyle.Left;
            pnlBrand.FillColor = Color.FromArgb(114, 66, 31);
            pnlBrand.Location = new Point(0, 0);
            pnlBrand.Name = "pnlBrand";
            pnlBrand.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pnlBrand.Size = new Size(320, 450);
            pnlBrand.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Settings.successful_african_american_employee_manages_financial_planning;
            pictureBox1.Location = new Point(81, 70);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(159, 132);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // lblAppName
            // 
            lblAppName.BackColor = Color.Transparent;
            lblAppName.ForeColor = Color.White;
            lblAppName.Location = new Point(20, 220);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(280, 40);
            lblAppName.TabIndex = 1;
            lblAppName.Text = "Atelie Da Transformação";
            lblAppName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSlogan
            // 
            lblSlogan.BackColor = Color.Transparent;
            lblSlogan.ForeColor = Color.LightGray;
            lblSlogan.Location = new Point(20, 265);
            lblSlogan.Name = "lblSlogan";
            lblSlogan.Size = new Size(280, 40);
            lblSlogan.TabIndex = 2;
            lblSlogan.Text = "Gestão Eficiente de Artesanatos";
            lblSlogan.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.Controls.Add(lblCred);
            pnlContent.Controls.Add(btnClose);
            pnlContent.Controls.Add(btnMinimize);
            pnlContent.Controls.Add(lblTitle);
            pnlContent.Controls.Add(txtUsername);
            pnlContent.Controls.Add(txtPassword);
            pnlContent.Controls.Add(btnSubmit);
            pnlContent.Controls.Add(btnToggleMode);
            pnlContent.Controls.Add(label1);
            pnlContent.Controls.Add(lblStatus);
            pnlContent.CustomizableEdges = customizableEdges15;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(320, 0);
            pnlContent.Name = "pnlContent";
            pnlContent.ShadowDecoration.CustomizableEdges = customizableEdges16;
            pnlContent.Size = new Size(480, 450);
            pnlContent.TabIndex = 1;
            // 
            // lblCred
            // 
            lblCred.AutoSize = true;
            lblCred.BackColor = Color.Transparent;
            lblCred.ForeColor = Color.Silver;
            lblCred.Location = new Point(40, 80);
            lblCred.Name = "lblCred";
            lblCred.Size = new Size(190, 15);
            lblCred.TabIndex = 8;
            lblCred.Text = "Insira suas credenciais para acessar";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.CustomizableEdges = customizableEdges3;
            btnClose.FillColor = Color.Transparent;
            btnClose.IconColor = Color.Gray;
            btnClose.Location = new Point(435, 10);
            btnClose.Name = "btnClose";
            btnClose.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnClose.Size = new Size(35, 30);
            btnClose.TabIndex = 0;
            // 
            // btnMinimize
            // 
            btnMinimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnMinimize.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            btnMinimize.CustomizableEdges = customizableEdges5;
            btnMinimize.FillColor = Color.Transparent;
            btnMinimize.IconColor = Color.Gray;
            btnMinimize.Location = new Point(395, 10);
            btnMinimize.Name = "btnMinimize";
            btnMinimize.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnMinimize.Size = new Size(35, 30);
            btnMinimize.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.BackColor = Color.Transparent;
            lblTitle.ForeColor = Color.FromArgb(40, 40, 40);
            lblTitle.Location = new Point(40, 50);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(400, 35);
            lblTitle.TabIndex = 2;
            lblTitle.Text = "Bem-vindo de volta!";
            // 
            // txtUsername
            // 
            txtUsername.BorderRadius = 8;
            txtUsername.Cursor = Cursors.IBeam;
            txtUsername.CustomizableEdges = customizableEdges7;
            txtUsername.DefaultText = "";
            txtUsername.Font = new Font("Segoe UI", 9F);
            txtUsername.HoverState.BorderColor = Color.FromArgb(94, 23, 235);
            txtUsername.Location = new Point(40, 105);
            txtUsername.Margin = new Padding(3, 4, 3, 4);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "👨‍💼 User E-mail";
            txtUsername.SelectedText = "";
            txtUsername.ShadowDecoration.CustomizableEdges = customizableEdges8;
            txtUsername.Size = new Size(400, 42);
            txtUsername.TabIndex = 2;
            // 
            // txtPassword
            // 
            txtPassword.BorderRadius = 8;
            txtPassword.Cursor = Cursors.IBeam;
            txtPassword.CustomizableEdges = customizableEdges9;
            txtPassword.DefaultText = "";
            txtPassword.Font = new Font("Segoe UI", 9F);
            txtPassword.HoverState.BorderColor = Color.FromArgb(94, 23, 235);
            txtPassword.Location = new Point(40, 160);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "🔒 Password";
            txtPassword.SelectedText = "";
            txtPassword.ShadowDecoration.CustomizableEdges = customizableEdges10;
            txtPassword.Size = new Size(400, 42);
            txtPassword.TabIndex = 3;
            // 
            // btnSubmit
            // 
            btnSubmit.BorderRadius = 8;
            btnSubmit.CustomizableEdges = customizableEdges11;
            btnSubmit.FillColor = Color.FromArgb(114, 66, 31);
            btnSubmit.Font = new Font("Segoe UI", 9F);
            btnSubmit.ForeColor = Color.White;
            btnSubmit.Location = new Point(40, 260);
            btnSubmit.Name = "btnSubmit";
            btnSubmit.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btnSubmit.Size = new Size(400, 45);
            btnSubmit.TabIndex = 5;
            btnSubmit.Text = "Entrar";
            // 
            // btnToggleMode
            // 
            btnToggleMode.BackColor = Color.Transparent;
            btnToggleMode.CustomizableEdges = customizableEdges13;
            btnToggleMode.FillColor = Color.Transparent;
            btnToggleMode.Font = new Font("Segoe UI", 9F);
            btnToggleMode.ForeColor = Color.FromArgb(94, 23, 235);
            btnToggleMode.Location = new Point(40, 345);
            btnToggleMode.Name = "btnToggleMode";
            btnToggleMode.ShadowDecoration.CustomizableEdges = customizableEdges14;
            btnToggleMode.Size = new Size(400, 30);
            btnToggleMode.TabIndex = 6;
            btnToggleMode.Text = "Não tem uma conta? Problema seu!\r\n";
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Location = new Point(40, 378);
            label1.Name = "label1";
            label1.Size = new Size(400, 30);
            label1.TabIndex = 7;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStatus
            // 
            lblStatus.BackColor = Color.Transparent;
            lblStatus.Location = new Point(40, 389);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(400, 30);
            lblStatus.TabIndex = 7;
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // dragControl
            // 
            dragControl.DockIndicatorTransparencyValue = 0.6D;
            dragControl.TargetControl = pnlContent;
            dragControl.UseTransparentDrag = true;
            // 
            // borderForm
            // 
            borderForm.BorderRadius = 12;
            borderForm.ContainerControl = this;
            borderForm.DockIndicatorTransparencyValue = 0.6D;
            borderForm.TransparentWhileDrag = true;
            // 
            // LoginForm
            // 
            ClientSize = new Size(800, 450);
            Controls.Add(pnlContent);
            Controls.Add(pnlBrand);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login - T1B3 Library";
            pnlBrand.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlBrand;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.Label lblSlogan;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private Guna.UI2.WinForms.Guna2ControlBox btnClose;
        private Guna.UI2.WinForms.Guna2ControlBox btnMinimize;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2Button btnSubmit;
        private Guna.UI2.WinForms.Guna2Button btnToggleMode;
        private System.Windows.Forms.Label lblStatus;
        private Guna.UI2.WinForms.Guna2DragControl dragControl;
        private Guna.UI2.WinForms.Guna2ShadowForm shadowForm;
        private Guna.UI2.WinForms.Guna2BorderlessForm borderForm;
        private PictureBox pictureBox1;
        private Label lblCred;
        private Label label1;
    }
}