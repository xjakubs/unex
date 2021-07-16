using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
/**
* Universal program for VIDA! science centrum for exhibits
*
* @author Jakub Smid (xjakubs@gmail.com)
*/
namespace UnExponat {
    partial class Unex {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Unex));
            this.imgHomeBtn = new UnExponat.SvgPictureBox("home");
            this.imgHowtoBtn = new UnExponat.SvgPictureBox("howto");
            this.imgInfoBtn = new UnExponat.SvgPictureBox("info");
            this.imgTipsBtn = new UnExponat.SvgPictureBox("tips");
            this.imgQrBtn = new UnExponat.SvgPictureBox("circle", 30, 30);
            this.imgLangCsBtn = new UnExponat.SvgPictureBox("lang_CZ");
            this.imgLangEnBtn = new UnExponat.SvgPictureBox("lang_EN");
            this.imgLangDeBtn = new UnExponat.SvgPictureBox("lang_DE");
            this.imgMagnifierBtn = new UnExponat.SvgPictureBox("magnifier");
            this.lbHomeBtnText = new Label();
            this.lbHowtoBtnText = new Label();
            this.lbInfoBtnText = new Label();
            this.lbTipsBtnText = new Label();
            this.lbLangBtnText = new Label();
            this.lbVersion = new Label();
            this.showVersionPanel = new Panel();
            this.textPanel = new UnExponat.MyPanel();
            this.textPanelInnerHead = new Panel();
            this.textPanelInner = new Panel();
            this.imgScrollUpBtn = new UnExponat.SvgPictureBox("up", 70, 70);
            this.imgScrollDownBtn = new UnExponat.SvgPictureBox("down", 70, 70);
            this.qrPanel = new UnExponat.MyPanel();
            this.qrPanelInnerHead = new Panel();
            this.qrPanelVpass = new Panel();
            this.lbHowLogin = new Label();
            this.lbCamInput = new Label();
            this.lbQrVpass = new Label();
            this.qrCamPicBox = new System.Windows.Forms.PictureBox();
            this.qrPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.imgCloseBtn = new UnExponat.SvgPictureBox("exit", 70, 70);
            this.imgQrSuccessCloseBtn = new UnExponat.SvgPictureBox("exit", 70, 70);
            this.webPanel = new UnExponat.MyPanel();
            this.webPanelInner = new Panel();
            this.lbTextMesssage = new Label();
            this.messagePanel = new Panel();
            this.magnifierPanel = new Panel();
            this.textPanel.SuspendLayout();
            this.qrPanel.SuspendLayout();
            this.webPanel.SuspendLayout();
            this.webPanelInner.SuspendLayout();
            this.magnifierPanel.SuspendLayout();
            this.imgResetBtn = new UnExponat.SvgPictureBox("close");
            this.imgPlusBtn = new UnExponat.SvgPictureBox("font_plus");
            this.imgMinusBtn = new UnExponat.SvgPictureBox("font_minus");
            this.imgContrastBtn = new UnExponat.SvgPictureBox("contrast");
            this.SuspendLayout();
            // 
            // imgHomeBtn
            // 
            this.imgHomeBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgHomeBtn.Location = new Point(1780, 50);
            this.imgHomeBtn.Margin = new Padding(0);
            this.imgHomeBtn.Name = "imgHomeBtn";
            this.imgHomeBtn.Size = new Size(90, 90);
            this.imgHomeBtn.TabIndex = 10;
            this.imgHomeBtn.TabStop = false;
            this.imgHomeBtn.MouseDown += new MouseEventHandler(this.imgHomeBtn_MouseDown);
            this.imgHomeBtn.DoubleClick += new EventHandler(this.imgHomeBtn_DoubleClick);
            // 
            // imgHowtoBtn
            // 
            this.imgHowtoBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgHowtoBtn.Location = new Point(1780, 210);
            this.imgHowtoBtn.Margin = new Padding(0);
            this.imgHowtoBtn.Name = "imgHowtoBtn";
            this.imgHowtoBtn.Size = new Size(90, 90);
            this.imgHowtoBtn.TabIndex = 11;
            this.imgHowtoBtn.TabStop = false;
            this.imgHowtoBtn.MouseDown += new MouseEventHandler(this.imgHowtoBtn_MouseDown);
            // 
            // imgInfoBtn
            // 
            this.imgInfoBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgInfoBtn.Location = new Point(1780, 370);
            this.imgInfoBtn.Margin = new Padding(0);
            this.imgInfoBtn.Name = "imgInfoBtn";
            this.imgInfoBtn.Size = new Size(90, 90);
            this.imgInfoBtn.TabIndex = 12;
            this.imgInfoBtn.TabStop = false;
            this.imgInfoBtn.MouseDown += new MouseEventHandler(this.imgInfoBtn_MouseDown);
            // 
            // imgTipsBtn
            // 
            this.imgTipsBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgTipsBtn.Location = new Point(1780, 530);
            this.imgTipsBtn.Margin = new Padding(0);
            this.imgTipsBtn.Name = "imgTipsBtn";
            this.imgTipsBtn.Size = new Size(90, 90);
            this.imgTipsBtn.TabIndex = 13;
            this.imgTipsBtn.TabStop = false;
            this.imgTipsBtn.MouseDown += new MouseEventHandler(this.imgTipsBtn_MouseDown);
            // 
            // imgQrBtn
            // 
            this.imgQrBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Left)));
            this.imgQrBtn.Location = new Point(0, 1050);
            this.imgQrBtn.Margin = new Padding(0);
            this.imgQrBtn.Name = "imgTipsBtn";
            this.imgQrBtn.Size = new Size(30, 30);
            this.imgQrBtn.TabIndex = 13;
            this.imgQrBtn.TabStop = false;
            this.imgQrBtn.MouseDown += new MouseEventHandler(this.imgQrBtn_MouseDown);
            // 
            // imgLangBtn
            // 
            this.imgLangCsBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.imgLangCsBtn.Location = new Point(1780, 780);
            this.imgLangCsBtn.Margin = new Padding(0);
            this.imgLangCsBtn.Name = "imgLangCsBtn";
            this.imgLangCsBtn.Size = new Size(90, 90);
            this.imgLangCsBtn.TabIndex = 14;
            this.imgLangCsBtn.TabStop = false;
            this.imgLangCsBtn.MouseDown += new MouseEventHandler(this.imgLangBtn_MouseDown);
            // 
            // imgLangBtn
            // 
            this.imgLangEnBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.imgLangEnBtn.Location = new Point(1780, 780);
            this.imgLangEnBtn.Margin = new Padding(0);
            this.imgLangEnBtn.Name = "imgLangEnBtn";
            this.imgLangEnBtn.Size = new Size(90, 90);
            this.imgLangEnBtn.TabIndex = 14;
            this.imgLangEnBtn.TabStop = false;
            this.imgLangEnBtn.MouseDown += new MouseEventHandler(this.imgLangBtn_MouseDown);
            // 
            // imgLangBtn
            // 
            this.imgLangDeBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.imgLangDeBtn.Location = new Point(1780, 780);
            this.imgLangDeBtn.Margin = new Padding(0);
            this.imgLangDeBtn.Name = "imgLangDeBtn";
            this.imgLangDeBtn.Size = new Size(90, 90);
            this.imgLangDeBtn.TabIndex = 14;
            this.imgLangDeBtn.TabStop = false;
            this.imgLangDeBtn.MouseDown += new MouseEventHandler(this.imgLangBtn_MouseDown);
            // 
            // imgMagnifierBtn
            // 
            this.imgMagnifierBtn.setNoOnClick(true);
            this.imgMagnifierBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.imgMagnifierBtn.Location = new Point(1780, 940);
            this.imgMagnifierBtn.Margin = new Padding(0);
            this.imgMagnifierBtn.Name = "imgMagnifierBtn";
            this.imgMagnifierBtn.Size = new Size(90, 90);
            this.imgMagnifierBtn.TabIndex = 15;
            this.imgMagnifierBtn.TabStop = false;
            this.imgMagnifierBtn.Click += new EventHandler(this.imgMagnifierBtn_Click);
            // 
            // lbHomeBtnText
            // 
            this.lbHomeBtnText.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.lbHomeBtnText.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbHomeBtnText.Location = new Point(1747, 143);
            this.lbHomeBtnText.Name = "lbHomeBtnText";
            this.lbHomeBtnText.Size = new Size(160, 40);
            this.lbHomeBtnText.TabIndex = 17;
            this.lbHomeBtnText.Text = "Domu";
            this.lbHomeBtnText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbHowtoBtnText
            // 
            this.lbHowtoBtnText.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.lbHowtoBtnText.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbHowtoBtnText.Location = new Point(1747, 303);
            this.lbHowtoBtnText.Name = "lbHowtoBtnText";
            this.lbHowtoBtnText.Size = new Size(160, 40);
            this.lbHowtoBtnText.TabIndex = 18;
            this.lbHowtoBtnText.Text = "Jak na to?";
            this.lbHowtoBtnText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbInfoBtnText
            // 
            this.lbInfoBtnText.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.lbInfoBtnText.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbInfoBtnText.Location = new Point(1747, 463);
            this.lbInfoBtnText.Name = "lbInfoBtnText";
            this.lbInfoBtnText.Size = new Size(160, 40);
            this.lbInfoBtnText.TabIndex = 19;
            this.lbInfoBtnText.Text = "Info";
            this.lbInfoBtnText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbTipsBtnText
            // 
            this.lbTipsBtnText.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.lbTipsBtnText.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbTipsBtnText.Location = new Point(1747, 623);
            this.lbTipsBtnText.Name = "lbTipsBtnText";
            this.lbTipsBtnText.Size = new Size(160, 40);
            this.lbTipsBtnText.TabIndex = 20;
            this.lbTipsBtnText.Text = "Tipy";
            this.lbTipsBtnText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbLangBtnText
            // 
            this.lbLangBtnText.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.lbLangBtnText.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbLangBtnText.Location = new Point(1747, 873);
            this.lbLangBtnText.Name = "lbLangBtnText";
            this.lbLangBtnText.Size = new Size(160, 40);
            this.lbLangBtnText.TabIndex = 21;
            this.lbLangBtnText.Text = "cz/en/de";
            this.lbLangBtnText.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lbVersion
            // 
            this.lbVersion.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.lbVersion.Font = new Font("Dimenze RB", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbVersion.Location = new Point(1810, 1052);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new Size(100, 20);
            this.lbVersion.TabIndex = 22;
            this.lbVersion.Text = "vX.YY.ZZ";
            this.lbVersion.TextAlign = ContentAlignment.BottomRight;
            this.lbVersion.MouseDown += new MouseEventHandler(this.lbVersion_MouseDown);
            // 
            // showVersionPanel
            // 
            this.showVersionPanel.Location = new Point(0, 0);
            this.showVersionPanel.Name = "showVersionPanel";
            this.showVersionPanel.Size = new Size(40, 40);
            this.showVersionPanel.TabIndex = 23;
            this.showVersionPanel.Click += new EventHandler(this.showVersionPanel_Click);
            // 
            // textPanel
            // 
            this.textPanel.Anchor = ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.textPanel.BackColor = Color.Transparent;
            this.textPanel.BorderColor = Color.Black;
            this.textPanel.Controls.Add(this.imgScrollUpBtn);
            this.textPanel.Controls.Add(this.imgScrollDownBtn);
            this.textPanel.Controls.Add(this.textPanelInnerHead);
            this.textPanel.Controls.Add(this.textPanelInner);
            this.textPanel.Location = new Point(30, 30);
            this.textPanel.Name = "textPanel";
            this.textPanel.Size = new Size(1700, 1020);
            this.textPanel.TabIndex = 16;
            // 
            // textPanelInnerHead
            // 
            this.textPanelInnerHead.AutoScroll = true;
            this.textPanelInnerHead.BackColor = Color.Transparent;
            this.textPanelInnerHead.Location = new Point(5, 5);
            this.textPanelInnerHead.Name = "textPanelInnerHead";
            this.textPanelInnerHead.Size = new Size(1715, 150);
            this.textPanelInnerHead.TabIndex = 0;
            // 
            // textPanelInner
            // 
            this.textPanelInner.AutoScroll = true;
            this.textPanelInner.BackColor = Color.Transparent;
            this.textPanelInner.Location = new Point(5, 155);
            this.textPanelInner.Name = "textPanelInner";
            this.textPanelInner.Size = new Size(1715, 850);
            this.textPanelInner.TabIndex = 0;
            // 
            // imgScrollUpBtn
            // 
            this.imgScrollUpBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgScrollUpBtn.Location = new Point(1600, 30);
            this.imgScrollUpBtn.Margin = new Padding(0);
            this.imgScrollUpBtn.Name = "imgScrollUpBtn";
            this.imgScrollUpBtn.Size = new Size(70, 70);
            this.imgScrollUpBtn.TabIndex = 1;
            this.imgScrollUpBtn.TabStop = false;
            this.imgScrollUpBtn.MouseDown += new MouseEventHandler(this.imgScrollUpBtn_MouseDown);
            // 
            // imgScrollDownBtn
            // 
            this.imgScrollDownBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgScrollDownBtn.Location = new Point(1600, 915);
            this.imgScrollDownBtn.Margin = new Padding(0);
            this.imgScrollDownBtn.Name = "imgScrollDownBtn";
            this.imgScrollDownBtn.Size = new Size(70, 70);
            this.imgScrollDownBtn.TabIndex = 2;
            this.imgScrollDownBtn.TabStop = false;
            this.imgScrollDownBtn.MouseDown += new MouseEventHandler(this.imgScrollDownBtn_MouseDown);
            // 
            // qrPanel
            // 
            this.qrPanel.Anchor = ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.qrPanel.BackColor = Color.Transparent;
            this.qrPanel.BorderColor = Color.Black;
            this.qrPanel.Controls.Add(this.qrPanelVpass);
            this.qrPanel.Controls.Add(this.lbHowLogin);
            this.qrPanel.Controls.Add(this.lbCamInput);
            this.qrPanel.Controls.Add(this.imgCloseBtn);
            this.qrPanel.Controls.Add(this.qrPanelInnerHead);
            this.qrPanel.Controls.Add(this.qrCamPicBox);
            this.qrPanel.Controls.Add(this.qrPlayer);
            this.qrPanel.Location = new System.Drawing.Point(30, 30);
            this.qrPanel.Name = "qrPanel";
            this.qrPanel.Size = new Size(1700, 1020);
            this.qrPanel.TabIndex = 16;
            // 
            // qrPanelInnerHead
            // 
            this.qrPanelInnerHead.AutoScroll = true;
            this.qrPanelInnerHead.BackColor = Color.Transparent;
            this.qrPanelInnerHead.Location = new Point(5, 5);
            this.qrPanelInnerHead.Name = "qrPanelInnerHead";
            this.qrPanelInnerHead.Size = new Size(1690, 150);
            this.qrPanelInnerHead.TabIndex = 0;
            // 
            // qrPanelVpass
            //             
            this.qrPanelVpass.Location = new Point(5, 5);
            this.qrPanelVpass.Name = "qrPanelVpass";
            this.qrPanelVpass.Size = new Size(1690, 1010);
            this.qrPanelVpass.Controls.Add(this.lbQrVpass);
            this.qrPanelVpass.Controls.Add(this.imgQrSuccessCloseBtn);
            // 
            // lbHowLogin
            // 
            this.lbHowLogin.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbHowLogin.Location = new Point(85, 200);
            this.lbHowLogin.Name = "lbHowLogin";
            this.lbHowLogin.AutoSize = true;
            // 
            // lbCamInput
            // 
            this.lbCamInput.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbCamInput.Location = new Point(970, 200);
            this.lbCamInput.Name = "lbCamInput";
            this.lbCamInput.AutoSize = true;
            // 
            // lbQrVpass
            // 
            this.lbQrVpass.Font = new Font("Dimenze RB", 18F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbQrVpass.Location = new Point(0, 50);
            this.lbQrVpass.Size = new Size(1700, 420);
            this.lbQrVpass.Name = "lbQrVpass";
            this.lbQrVpass.AutoSize = false;
            this.lbQrVpass.TextAlign = ContentAlignment.BottomCenter;
            // 
            // imgQrSuccessCloseBtn
            // 
            this.imgQrSuccessCloseBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgQrSuccessCloseBtn.Location = new Point(807, 500);
            this.imgQrSuccessCloseBtn.Margin = new Padding(0);
            this.imgQrSuccessCloseBtn.Name = "imgQrSuccessCloseBtn";
            this.imgQrSuccessCloseBtn.Size = new Size(70, 70);
            this.imgQrSuccessCloseBtn.TabIndex = 1;
            this.imgQrSuccessCloseBtn.TabStop = false;
            this.imgQrSuccessCloseBtn.MouseDown += new MouseEventHandler(this.imgSuccessCloseBtn_MouseDown);
            // 
            // imgCloseBtn
            // 
            this.imgCloseBtn.Anchor = ((AnchorStyles) ((AnchorStyles.Top | AnchorStyles.Right)));
            this.imgCloseBtn.Location = new Point(1600, 30);
            this.imgCloseBtn.Margin = new Padding(0);
            this.imgCloseBtn.Name = "imgCloseBtn";
            this.imgCloseBtn.Size = new Size(70, 70);
            this.imgCloseBtn.TabIndex = 1;
            this.imgCloseBtn.TabStop = false;
            this.imgCloseBtn.MouseDown += new MouseEventHandler(this.imgCloseBtn_MouseDown);
            // 
            // qrCamPicBox
            // 
            this.qrCamPicBox.Location = new System.Drawing.Point(970, 350);
            this.qrCamPicBox.Name = "qrCamPicBox";
            this.qrCamPicBox.Size = new System.Drawing.Size(640, 480);
            this.qrCamPicBox.TabIndex = 1;
            this.qrCamPicBox.TabStop = false;
            // 
            // qrPlayer
            // 
            this.qrPlayer.Enabled = true;
            this.qrPlayer.Location = new System.Drawing.Point(85, 350);
            this.qrPlayer.Name = "qrPlayer";
            this.qrPlayer.Size = new System.Drawing.Size(800, 480); //450 ?
            this.qrPlayer.TabIndex = 0;
            //
            // webPanel
            // 
            this.webPanel.Anchor = ((AnchorStyles) ((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.webPanel.BackColor = Color.Transparent;
            this.webPanel.BorderColor = Color.Black;
            this.webPanel.Controls.Add(this.webPanelInner);
            this.webPanel.Location = new Point(30, 30);
            this.webPanel.Name = "webPanel";
            this.webPanel.Size = new Size(1700, 1020);
            this.webPanel.TabIndex = 25;
            // 
            // webPanelInner
            // 
            this.webPanelInner.BackColor = Color.Transparent;
            this.webPanelInner.Location = new Point(5, 5);
            this.webPanelInner.Name = "webPanelInner";
            this.webPanelInner.Size = new Size(1690, 1010);
            this.webPanelInner.TabIndex = 26;
            // 
            // lbTextMesssage
            // 
            this.lbTextMesssage.Font = new Font("Dimenze RB", 20F, FontStyle.Regular, GraphicsUnit.Point, ((byte) (0)));
            this.lbTextMesssage.Location = new Point(0, 0);
            this.lbTextMesssage.Name = "lbTextMesssage";
            this.lbTextMesssage.Size = new Size(1690, 29);
            this.lbTextMesssage.AutoSize = true;
            this.lbTextMesssage.TabIndex = 29;
            this.lbTextMesssage.Text = "";
            this.lbTextMesssage.UseMnemonic = false;
            this.lbTextMesssage.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // messagePanel
            // 
            this.messagePanel.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.messagePanel.Controls.Add(this.lbTextMesssage);
            this.messagePanel.Location = new Point(30, 1050);
            this.messagePanel.Name = "messagePanel";
            this.messagePanel.Size = new Size(1700, 29);
            this.messagePanel.TabIndex = 28;
            this.messagePanel.BackColor = Color.Transparent;
            // 
            // magnifierPanel
            // 
            this.magnifierPanel.Anchor = ((AnchorStyles) ((AnchorStyles.Bottom | AnchorStyles.Right)));
            this.magnifierPanel.Controls.Add(this.imgResetBtn);
            this.magnifierPanel.Controls.Add(this.imgPlusBtn);
            this.magnifierPanel.Controls.Add(this.imgMinusBtn);
            this.magnifierPanel.Controls.Add(this.imgContrastBtn);
            this.magnifierPanel.Location = new Point(1595, 585);
            this.magnifierPanel.Name = "magnifierPanel";
            this.magnifierPanel.Size = new Size(130, 460);
            this.magnifierPanel.TabIndex = 23;
            this.magnifierPanel.MouseDown += new MouseEventHandler(this.magnifierPanel_MouseDown);
            // 
            // imgResetBtn
            // 
            this.imgResetBtn.Location = new Point(20, 20);
            this.imgResetBtn.BackColor = Color.Transparent;
            this.imgResetBtn.Name = "imgResetBtn";
            this.imgResetBtn.Size = new Size(90, 90);
            this.imgResetBtn.TabIndex = 0;
            this.imgResetBtn.TabStop = false;
            this.imgResetBtn.Click += new EventHandler(this.imgResetBtn_Click);
            // 
            // imgPlusBtn
            // 
            this.imgPlusBtn.Location = new Point(20, 130);
            this.imgPlusBtn.BackColor = Color.Transparent;
            this.imgPlusBtn.Name = "imgPlusBtn";
            this.imgPlusBtn.Size = new Size(90, 90);
            this.imgPlusBtn.TabIndex = 1;
            this.imgPlusBtn.TabStop = false;
            this.imgPlusBtn.Click += new EventHandler(this.imgPlusBtn_Click);
            // 
            // imgMinusBtn
            // 
            this.imgMinusBtn.Location = new Point(20, 240);
            this.imgMinusBtn.BackColor = Color.Transparent;
            this.imgMinusBtn.Name = "imgMinusBtn";
            this.imgMinusBtn.Size = new Size(90, 90);
            this.imgMinusBtn.TabIndex = 2;
            this.imgMinusBtn.TabStop = false;
            this.imgMinusBtn.Click += new EventHandler(this.imgMinusBtnn_Click);
            // 
            // imgContrastBtn
            // 
            this.imgContrastBtn.Location = new Point(20, 350);
            this.imgContrastBtn.BackColor = Color.Transparent;
            this.imgContrastBtn.Name = "imgContrastBtn";
            this.imgContrastBtn.Size = new Size(90, 90);
            this.imgContrastBtn.TabIndex = 15;
            this.imgContrastBtn.TabStop = false;
            this.imgContrastBtn.Click += new EventHandler(this.imgContrastBtn_Click);
            // 
            // Unex
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.ClientSize = new Size(1920, 1080);
            this.Controls.Add(this.webPanel);
            this.Controls.Add(this.textPanel);
            this.Controls.Add(this.qrPanel);
            this.Controls.Add(this.showVersionPanel);
            this.Controls.Add(this.lbVersion);
            this.Controls.Add(this.lbLangBtnText);
            this.Controls.Add(this.lbTipsBtnText);
            this.Controls.Add(this.lbInfoBtnText);
            this.Controls.Add(this.lbHowtoBtnText);
            this.Controls.Add(this.lbHomeBtnText);
            this.Controls.Add(this.imgMagnifierBtn);
            this.Controls.Add(this.imgLangCsBtn);
            this.Controls.Add(this.imgLangEnBtn);
            this.Controls.Add(this.imgLangDeBtn);
            this.Controls.Add(this.imgTipsBtn);
            this.Controls.Add(this.imgQrBtn);
            this.Controls.Add(this.imgInfoBtn);
            this.Controls.Add(this.imgHowtoBtn);
            this.Controls.Add(this.imgHomeBtn);
            this.Controls.Add(this.magnifierPanel);
            this.Controls.Add(this.messagePanel);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon) (resources.GetObject("$this.Icon")));
            this.Name = "Unex";
            this.Text = "UNEX";
            this.KeyPreview = true;
            this.FormClosed += new FormClosedEventHandler(this.Unex_FormClosed);
            //this.Load += new System.EventHandler(this.Unex_FormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Unex_KeyDown);
            ((ISupportInitialize) (this.imgHomeBtn)).EndInit();
            ((ISupportInitialize) (this.imgHowtoBtn)).EndInit();
            ((ISupportInitialize) (this.imgInfoBtn)).EndInit();
            ((ISupportInitialize) (this.imgTipsBtn)).EndInit();
            ((ISupportInitialize) (this.imgQrBtn)).EndInit();
            ((ISupportInitialize) (this.imgLangCsBtn)).EndInit();
            ((ISupportInitialize) (this.imgLangEnBtn)).EndInit();
            ((ISupportInitialize) (this.imgLangDeBtn)).EndInit();
            ((ISupportInitialize) (this.imgMagnifierBtn)).EndInit();
            this.textPanel.ResumeLayout(false);
            this.qrPanel.ResumeLayout(false);
            ((ISupportInitialize) (this.imgScrollUpBtn)).EndInit();
            ((ISupportInitialize) (this.imgScrollDownBtn)).EndInit();
            ((ISupportInitialize) (this.imgCloseBtn)).EndInit();
            this.webPanel.ResumeLayout(false);
            this.webPanelInner.ResumeLayout(false);
            this.magnifierPanel.ResumeLayout(false);
            ((ISupportInitialize) (this.imgResetBtn)).EndInit();
            ((ISupportInitialize) (this.imgPlusBtn)).EndInit();
            ((ISupportInitialize) (this.imgMinusBtn)).EndInit();
            ((ISupportInitialize) (this.imgContrastBtn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private SvgPictureBox imgHomeBtn;
        private SvgPictureBox imgHowtoBtn;
        private SvgPictureBox imgInfoBtn;
        private SvgPictureBox imgTipsBtn;
        private SvgPictureBox imgQrBtn;
        private SvgPictureBox imgLangCsBtn;
        private SvgPictureBox imgLangEnBtn;
        private SvgPictureBox imgLangDeBtn;
        private SvgPictureBox imgMagnifierBtn;
        private Label lbHomeBtnText;
        private Label lbHowtoBtnText;
        private Label lbInfoBtnText;
        private Label lbTipsBtnText;
        private Label lbLangBtnText;
        private Label lbVersion;
        private Panel showVersionPanel;
        private MyPanel textPanel;
        private Panel textPanelInnerHead;
        private Panel textPanelInner;
        private MyPanel qrPanel;
        private Label lbHowLogin;
        private Label lbCamInput;
        private Label lbQrVpass;
        private Panel qrPanelInnerHead;
        private Panel qrPanelVpass;
        private PictureBox qrCamPicBox;
        private AxWMPLib.AxWindowsMediaPlayer qrPlayer;
        private SvgPictureBox imgScrollDownBtn;
        private SvgPictureBox imgScrollUpBtn;
        private SvgPictureBox imgCloseBtn;
        private SvgPictureBox imgQrSuccessCloseBtn;
        private MyPanel webPanel;
        private Panel webPanelInner;
        private Panel magnifierPanel;
        private SvgPictureBox imgResetBtn;
        private SvgPictureBox imgPlusBtn;
        private SvgPictureBox imgMinusBtn;
        private SvgPictureBox imgContrastBtn;
        private Label lbTextMesssage;
        private Panel messagePanel;
    }
}

