namespace ScreenTask
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.gbLog = new System.Windows.Forms.GroupBox();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.imgPreview = new System.Windows.Forms.PictureBox();
            this.pnlOptions = new System.Windows.Forms.Panel();
            this.comboIPs = new System.Windows.Forms.ComboBox();
            this.cbCaptureMouse = new System.Windows.Forms.CheckBox();
            this.cbPreview = new System.Windows.Forms.CheckBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPrivate = new System.Windows.Forms.CheckBox();
            this.numPort = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbScreenshotEvery = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numShotEvery = new System.Windows.Forms.NumericUpDown();
            this.lblMe = new System.Windows.Forms.Label();
            this.lblWebsite = new System.Windows.Forms.Label();
            this.isStoppingTranslation = new System.Windows.Forms.CheckBox();
            this.Tree = new System.Windows.Forms.NotifyIcon(this.components);
            this.PutUpMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.startServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopTranslationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ImageChoosing = new System.Windows.Forms.ComboBox();
            this.ImageWay = new System.Windows.Forms.TextBox();
            this.Rooting = new System.Windows.Forms.Button();
            this.OnceTranslation = new System.Windows.Forms.CheckBox();
            this.LanguageChoosing = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).BeginInit();
            this.PutUpMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOptions
            // 
            resources.ApplyResources(this.gbOptions, "gbOptions");
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.TabStop = false;
            // 
            // gbLog
            // 
            resources.ApplyResources(this.gbLog, "gbLog");
            this.gbLog.Name = "gbLog";
            this.gbLog.TabStop = false;
            // 
            // gbPreview
            // 
            resources.ApplyResources(this.gbPreview, "gbPreview");
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.TabStop = false;
            // 
            // txtLog
            // 
            resources.ApplyResources(this.txtLog, "txtLog");
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // imgPreview
            // 
            resources.ApplyResources(this.imgPreview, "imgPreview");
            this.imgPreview.Image = global::ScreenTask.Properties.Resources.imgPrev;
            this.imgPreview.InitialImage = global::ScreenTask.Properties.Resources.imgPrev;
            this.imgPreview.Name = "imgPreview";
            this.imgPreview.TabStop = false;
            this.imgPreview.Click += new System.EventHandler(this.imgPreview_Click);
            // 
            // pnlOptions
            // 
            resources.ApplyResources(this.pnlOptions, "pnlOptions");
            this.pnlOptions.BackColor = System.Drawing.Color.SteelBlue;
            this.pnlOptions.Name = "pnlOptions";
            // 
            // comboIPs
            // 
            resources.ApplyResources(this.comboIPs, "comboIPs");
            this.comboIPs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPs.FormattingEnabled = true;
            this.comboIPs.Name = "comboIPs";
            // 
            // cbCaptureMouse
            // 
            resources.ApplyResources(this.cbCaptureMouse, "cbCaptureMouse");
            this.cbCaptureMouse.BackColor = System.Drawing.Color.Transparent;
            this.cbCaptureMouse.Name = "cbCaptureMouse";
            this.cbCaptureMouse.UseVisualStyleBackColor = false;
            this.cbCaptureMouse.CheckedChanged += new System.EventHandler(this.cbCaptureMouse_CheckedChanged);
            // 
            // cbPreview
            // 
            resources.ApplyResources(this.cbPreview, "cbPreview");
            this.cbPreview.BackColor = System.Drawing.Color.Transparent;
            this.cbPreview.Name = "cbPreview";
            this.cbPreview.UseVisualStyleBackColor = false;
            this.cbPreview.CheckedChanged += new System.EventHandler(this.cbPreview_CheckedChanged);
            // 
            // btnStopServer
            // 
            resources.ApplyResources(this.btnStopServer, "btnStopServer");
            this.btnStopServer.BackColor = System.Drawing.Color.Maroon;
            this.btnStopServer.ForeColor = System.Drawing.Color.White;
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.UseVisualStyleBackColor = false;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // btnStartServer
            // 
            resources.ApplyResources(this.btnStartServer, "btnStartServer");
            this.btnStartServer.BackColor = System.Drawing.Color.Gray;
            this.btnStartServer.ForeColor = System.Drawing.Color.White;
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Tag = "start";
            this.btnStartServer.UseVisualStyleBackColor = false;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            // 
            // txtUser
            // 
            resources.ApplyResources(this.txtUser, "txtUser");
            this.txtUser.Name = "txtUser";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Name = "label3";
            // 
            // cbPrivate
            // 
            resources.ApplyResources(this.cbPrivate, "cbPrivate");
            this.cbPrivate.BackColor = System.Drawing.Color.Transparent;
            this.cbPrivate.Name = "cbPrivate";
            this.cbPrivate.UseVisualStyleBackColor = false;
            this.cbPrivate.CheckedChanged += new System.EventHandler(this.cbPrivate_CheckedChanged);
            // 
            // numPort
            // 
            resources.ApplyResources(this.numPort, "numPort");
            this.numPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPort.Name = "numPort";
            this.numPort.Value = new decimal(new int[] {
            7070,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            // 
            // txtURL
            // 
            resources.ApplyResources(this.txtURL, "txtURL");
            this.txtURL.Name = "txtURL";
            this.txtURL.ReadOnly = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Name = "label5";
            // 
            // cbScreenshotEvery
            // 
            resources.ApplyResources(this.cbScreenshotEvery, "cbScreenshotEvery");
            this.cbScreenshotEvery.BackColor = System.Drawing.Color.Transparent;
            this.cbScreenshotEvery.Checked = true;
            this.cbScreenshotEvery.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScreenshotEvery.Name = "cbScreenshotEvery";
            this.cbScreenshotEvery.UseVisualStyleBackColor = false;
            this.cbScreenshotEvery.CheckedChanged += new System.EventHandler(this.cbScreenshotEvery_CheckedChanged);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Name = "label6";
            // 
            // numShotEvery
            // 
            resources.ApplyResources(this.numShotEvery, "numShotEvery");
            this.numShotEvery.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numShotEvery.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numShotEvery.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numShotEvery.Name = "numShotEvery";
            this.numShotEvery.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // lblMe
            // 
            resources.ApplyResources(this.lblMe, "lblMe");
            this.lblMe.BackColor = System.Drawing.Color.Transparent;
            this.lblMe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblMe.Name = "lblMe";
            this.lblMe.Click += new System.EventHandler(this.lblMe_Click);
            // 
            // lblWebsite
            // 
            resources.ApplyResources(this.lblWebsite, "lblWebsite");
            this.lblWebsite.BackColor = System.Drawing.Color.Transparent;
            this.lblWebsite.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblWebsite.Name = "lblWebsite";
            this.lblWebsite.Click += new System.EventHandler(this.lblWebsite_Click);
            // 
            // isStoppingTranslation
            // 
            resources.ApplyResources(this.isStoppingTranslation, "isStoppingTranslation");
            this.isStoppingTranslation.Name = "isStoppingTranslation";
            this.isStoppingTranslation.UseVisualStyleBackColor = true;
            this.isStoppingTranslation.CheckedChanged += new System.EventHandler(this.isStoppingTranslation_CheckedChanged);
            // 
            // Tree
            // 
            this.Tree.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            resources.ApplyResources(this.Tree, "Tree");
            this.Tree.ContextMenuStrip = this.PutUpMenu;
            this.Tree.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // PutUpMenu
            // 
            resources.ApplyResources(this.PutUpMenu, "PutUpMenu");
            this.PutUpMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PutUpMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startServerToolStripMenuItem,
            this.stopServerToolStripMenuItem,
            this.stopTranslationToolStripMenuItem});
            this.PutUpMenu.Name = "PutUpMenu";
            // 
            // startServerToolStripMenuItem
            // 
            resources.ApplyResources(this.startServerToolStripMenuItem, "startServerToolStripMenuItem");
            this.startServerToolStripMenuItem.Name = "startServerToolStripMenuItem";
            this.startServerToolStripMenuItem.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // stopServerToolStripMenuItem
            // 
            resources.ApplyResources(this.stopServerToolStripMenuItem, "stopServerToolStripMenuItem");
            this.stopServerToolStripMenuItem.Name = "stopServerToolStripMenuItem";
            this.stopServerToolStripMenuItem.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // stopTranslationToolStripMenuItem
            // 
            resources.ApplyResources(this.stopTranslationToolStripMenuItem, "stopTranslationToolStripMenuItem");
            this.stopTranslationToolStripMenuItem.CheckOnClick = true;
            this.stopTranslationToolStripMenuItem.DoubleClickEnabled = true;
            this.stopTranslationToolStripMenuItem.Name = "stopTranslationToolStripMenuItem";
            this.stopTranslationToolStripMenuItem.Click += new System.EventHandler(this.stopTranslationToolStripMenuItem_Click);
            // 
            // ImageChoosing
            // 
            resources.ApplyResources(this.ImageChoosing, "ImageChoosing");
            this.ImageChoosing.FormattingEnabled = true;
            this.ImageChoosing.Items.AddRange(new object[] {
            resources.GetString("ImageChoosing.Items"),
            resources.GetString("ImageChoosing.Items1"),
            resources.GetString("ImageChoosing.Items2")});
            this.ImageChoosing.Name = "ImageChoosing";
            this.ImageChoosing.SelectedIndexChanged += new System.EventHandler(this.ImageChoosing_SelectedIndexChanged);
            // 
            // ImageWay
            // 
            resources.ApplyResources(this.ImageWay, "ImageWay");
            this.ImageWay.Name = "ImageWay";
            // 
            // Rooting
            // 
            resources.ApplyResources(this.Rooting, "Rooting");
            this.Rooting.Name = "Rooting";
            this.Rooting.Tag = "choose way";
            this.Rooting.UseVisualStyleBackColor = true;
            this.Rooting.Click += new System.EventHandler(this.Rooting_Click);
            // 
            // OnceTranslation
            // 
            resources.ApplyResources(this.OnceTranslation, "OnceTranslation");
            this.OnceTranslation.Name = "OnceTranslation";
            this.OnceTranslation.UseVisualStyleBackColor = true;
            // 
            // LanguageChoosing
            // 
            resources.ApplyResources(this.LanguageChoosing, "LanguageChoosing");
            this.LanguageChoosing.FormattingEnabled = true;
            this.LanguageChoosing.Name = "LanguageChoosing";
            // 
            // frmMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::ScreenTask.Properties.Resources.ScreenTaskBackground;
            this.Controls.Add(this.LanguageChoosing);
            this.Controls.Add(this.OnceTranslation);
            this.Controls.Add(this.Rooting);
            this.Controls.Add(this.ImageWay);
            this.Controls.Add(this.ImageChoosing);
            this.Controls.Add(this.isStoppingTranslation);
            this.Controls.Add(this.imgPreview);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.gbPreview);
            this.Controls.Add(this.gbLog);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pnlOptions);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbPrivate);
            this.Controls.Add(this.txtURL);
            this.Controls.Add(this.numPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboIPs);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numShotEvery);
            this.Controls.Add(this.cbScreenshotEvery);
            this.Controls.Add(this.cbCaptureMouse);
            this.Controls.Add(this.cbPreview);
            this.Controls.Add(this.btnStopServer);
            this.Controls.Add(this.lblWebsite);
            this.Controls.Add(this.lblMe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Resize += new System.EventHandler(this.frmMain_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.imgPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numShotEvery)).EndInit();
            this.PutUpMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.GroupBox gbLog;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.PictureBox imgPreview;
        private System.Windows.Forms.Panel pnlOptions;
        private System.Windows.Forms.ComboBox comboIPs;
        private System.Windows.Forms.CheckBox cbCaptureMouse;
        private System.Windows.Forms.CheckBox cbPreview;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbPrivate;
        private System.Windows.Forms.NumericUpDown numPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbScreenshotEvery;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numShotEvery;
        private System.Windows.Forms.Label lblMe;
        private System.Windows.Forms.Label lblWebsite;
        private System.Windows.Forms.CheckBox isStoppingTranslation;
        private System.Windows.Forms.ContextMenuStrip PutUpMenu;
        private System.Windows.Forms.ToolStripMenuItem startServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopTranslationToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon Tree;
        private System.Windows.Forms.ComboBox ImageChoosing;
        private System.Windows.Forms.TextBox ImageWay;
        private System.Windows.Forms.Button Rooting;
        private System.Windows.Forms.CheckBox OnceTranslation;
        private System.Windows.Forms.ComboBox LanguageChoosing;
    }
}
