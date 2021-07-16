using System;
using System.Windows.Forms;
using System.Drawing;
using CefSharp;
using CefSharp.WinForms;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using CefSharp.WinForms.Internals;
using VpassConnection;
using Newtonsoft.Json;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public partial class Unex : Form {
        private Magnifier mMagnifier;

        private enum SCREENS {
            HOME, HOWTO, INFO, TIPS, QR
        }
        private SCREENS mCurrScreen;

        private MyLabel lbTextContent;
        private Label lbTextTitle;
        private Label lbQrTitle;

        private String mExponatID;
        private int mVpassID;
        private int mFontSize;
        private int mTitleSize;
        private const String FONT = "Dimenze RB";
        private const String WEB_LANG = "?lng=";
        private const String WEB_THEME = "&theme=";
        private const int TEXT_NO_SCROLL = 20;
        private const int TEXT_SCROLL = 120;

        private Theme mTheme;

        //sizes
        private Size panelInnerHeadSize;
        private Size panelInnerSize;
        private Point scrollUpBtnLocation;
        private Point scrollDownBtnLocation;

        private JsonReader mJsonReader;
        private Settings mSettings;
        private XmlTextsReader xmlTextsReader;
        private QrVideo qrVideo;
        private QrReader qrReader;
        private RunExe mRunExe;

        private Language mLanguage;

        ChromiumWebBrowser chromeBrowser;

        private int mRestartCombination = 0;
        private int mOutOfOrderCombination = 0;

        private Timer timerMessageSlide = new Timer();
        private Timer timerMessageShow = new Timer();
        private int messageSlideMoveX = 0;

        public Unex() {
            InitializeComponent();

            mJsonReader = Program.getJsonReader();
            mSettings = Program.getSettings();
            xmlTextsReader = new XmlTextsReader();
            lbVersion.Text = Program.getVersion();
            mTheme = Program.getTheme();

            mRunExe = Program.getRunExe();
            var t = new Timer();
            t.Interval = 1000;
            t.Tick += (s, e) => {
                mRunExe.run();
                t.Stop();
            };
            t.Start();

            BackColor = mTheme.currentColor.getBackground();
            mExponatID = mSettings.exponatId;
            mVpassID = mSettings.vpassId;
            mFontSize = mSettings.textFont;
            mTitleSize = mSettings.titleFont;
            mCurrScreen = SCREENS.HOME;

            mMagnifier = new Magnifier(mTheme.currentColor);
            mLanguage = new Language(mSettings.langEN, mSettings.langDE);

            if (mSettings.mouseInvisible) {
                Cursor.Hide();
            } else {
                Cursor.Show();
            }

            if (!string.IsNullOrEmpty(mSettings.homeWebPath)) {
                InitializeChromium();
            }

            lbTextTitle = new Label();
            lbTextTitle.AutoSize = true;
            lbTextTitle.Padding = new Padding(30, 30, 30, 0);
            lbTextTitle.MaximumSize = new Size(textPanelInnerHead.Width - TEXT_NO_SCROLL, 0);
            lbTextTitle.Font = new Font(FONT, mTitleSize, FontStyle.Bold);
            lbTextTitle.MouseDown += new MouseEventHandler(this.textPanel_MouseDown);
            mTheme.add(lbTextTitle);
            textPanelInnerHead.Controls.Add(lbTextTitle);

            lbTextContent = new MyLabel(mJsonReader, mExponatID);
            lbTextContent.AutoSize = true;
            lbTextContent.Padding = new Padding(30, 0, 30, 0);
            lbTextContent.MaximumSize = new Size(textPanelInner.Width - TEXT_NO_SCROLL, 0);
            lbTextContent.Font = new Font(FONT, mFontSize);
            lbTextContent.MouseDown += new MouseEventHandler(this.textPanel_MouseDown);
            mTheme.add(lbTextContent);
            textPanelInner.Controls.Add(lbTextContent);

            lbQrTitle = new Label();
            lbQrTitle.AutoSize = true;
            lbQrTitle.Padding = new Padding(30, 30, 30, 0);
            lbQrTitle.MaximumSize = new Size(qrPanelInnerHead.Width - TEXT_NO_SCROLL, 0);
            lbQrTitle.Font = new Font(FONT, mTitleSize, FontStyle.Bold);
            lbQrTitle.MouseDown += new MouseEventHandler(this.textPanel_MouseDown);
            mTheme.add(lbQrTitle);
            qrPanelInnerHead.Controls.Add(lbQrTitle);

            lbHowLogin.Font = new Font(FONT, mFontSize);
            lbCamInput.Font = new Font(FONT, mFontSize);
            lbQrVpass.Font = new Font(FONT, mFontSize);

            //save sizes
            panelInnerHeadSize = textPanelInnerHead.Size;
            panelInnerSize = textPanelInner.Size;
            scrollUpBtnLocation = imgScrollUpBtn.Location;
            scrollDownBtnLocation = imgScrollDownBtn.Location;

            showVersion(10000);

#if TESTING
            imgQrBtn.Visible = true;
#else
            imgQrBtn.Visible = false;
#endif

            mTheme.add(textPanel);
            mTheme.add(webPanel);
            mTheme.add(qrPanel);
            mTheme.add(messagePanel);
            mTheme.add(magnifierPanel, 20, MyColor.BACKGROUND);

            mTheme.add(lbTextTitle);
            mTheme.add(lbTextContent);
            mTheme.add(lbVersion);
            mTheme.add(lbTextMesssage);
            mTheme.add(lbHowLogin);
            mTheme.add(lbCamInput);
            mTheme.add(lbQrVpass);

            mTheme.add(lbHomeBtnText);
            mTheme.add(lbHowtoBtnText);
            mTheme.add(lbInfoBtnText);
            mTheme.add(lbTipsBtnText);
            mTheme.add(lbLangBtnText);

            mTheme.add(imgHomeBtn, MyColor.MONODARKER3);
            mTheme.add(imgHowtoBtn, MyColor.MONODARKER3);
            mTheme.add(imgInfoBtn, MyColor.MONODARKER3);
            mTheme.add(imgTipsBtn, MyColor.MONODARKER3);
            mTheme.add(imgQrBtn, MyColor.MONODARKER3);
            mTheme.add(imgLangCsBtn, MyColor.MONODARKER3);
            mTheme.add(imgLangEnBtn, MyColor.MONODARKER3);
            mTheme.add(imgLangDeBtn, MyColor.MONODARKER3);
            mTheme.add(imgMagnifierBtn, MyColor.MONODARKER3);
            mTheme.add(imgScrollUpBtn, MyColor.MONODARKER3);
            mTheme.add(imgScrollDownBtn, MyColor.MONODARKER3);
            mTheme.add(imgCloseBtn, MyColor.MONODARKER3);
            mTheme.add(imgResetBtn, MyColor.MONODARKER3);
            mTheme.add(imgPlusBtn, MyColor.MONODARKER3);
            mTheme.add(imgMinusBtn, MyColor.MONODARKER3);
            mTheme.add(imgContrastBtn, MyColor.MONODARKER3);
            mTheme.add(imgQrSuccessCloseBtn, MyColor.MONODARKER3);

            mTheme.changeColor(mTheme.currentColor);
            imgLangCsBtn.Visible = true;
            imgLangEnBtn.Visible = false;
            imgLangDeBtn.Visible = false;

            if (mLanguage.isEnabled() == false) {
                hideLanguages();
            } else {
                lbLangBtnText.Text = mLanguage.getTitle();
            }

            refreshButtons();
            refreshScreens();

            this.timerMessageSlide.Tick += new EventHandler(this.timerMessageTick);
        }

        public void showTextMsg(String msg, int time) {
            String message = "";
            if (msg.Length > 0) {
                message = msg;
                lbTextMesssage.Visible = true;
            }
            if (lbTextMesssage.InvokeRequired) {
                lbTextMesssage.Invoke(new Action(() => {
                    lbTextMesssage.Text = msg;
                    //sliding
                    messageSlideMoveX = messagePanel.Width;
                    timerMessageSlide.Interval = 1;
                    timerMessageSlide.Start();

                    if (time > 0) {
                        timerMessageShow.Interval = time * 1000;
                        timerMessageShow.Tick += (s, er) => {
                            Program.logError("TIMER SHOW STOPPED");
                            timerMessageSlide.Stop();
                            lbTextMesssage.Visible = false;
                            timerMessageShow.Stop();
                        };
                        Program.logError("NEW TIMER SHOW");
                        timerMessageShow.Stop();
                        timerMessageShow.Start();
                    }
                }));
            } else {
                lbTextMesssage.Text = msg;
            }
        }



        private void updateTextsSize() {
            int titleSize = mMagnifier.getTitleFontSizeChange();
            int textSize = mMagnifier.getFontSizeChange();

            lbTextTitle.Font = new Font(FONT, mTitleSize + titleSize, FontStyle.Bold);
            lbTextContent.Font = new Font(FONT, mFontSize + textSize);
            lbQrTitle.Font = new Font(FONT, mTitleSize + titleSize, FontStyle.Bold);

            // max sizes
            if (textSize > 40) {
                textSize = 40;
            }
            lbHowLogin.Font = new Font(FONT, mFontSize + textSize);
            lbCamInput.Font = new Font(FONT, mFontSize + textSize);
            lbQrVpass.Font = new Font(FONT, mFontSize + textSize);
        }

        private void updateWebPanel() {
            if (!string.IsNullOrEmpty(mSettings.homeWebPath)) {
                try {
                    var task = chromeBrowser.GetZoomLevelAsync();
                    task.ContinueWith(previous => {
                        if (previous.Status == TaskStatus.RanToCompletion) {
                            chromeBrowser.SetZoomLevel(mMagnifier.getWebSizeChange());
                        } else {
                            throw new InvalidOperationException("Unexpected failure of calling CEF->GetZoomLevelAsync", previous.Exception);
                        }
                    }, TaskContinuationOptions.ExecuteSynchronously);
                } catch (Exception ex) {
                    Program.logError(ex.Message);
                }
            }
        }

        private void updateTextScrolls(bool reset) {
            if (reset) {
                textPanelInner.VerticalScroll.Value = 0;
                textPanelInner.VerticalScroll.Value = 0;
            }

            imgScrollUpBtn.Visible = textPanelInner.VerticalScroll.Visible;
            imgScrollDownBtn.Visible = textPanelInner.VerticalScroll.Visible;

            imgScrollUpBtn.Location = new Point(scrollUpBtnLocation.X, scrollUpBtnLocation.Y);
            imgScrollDownBtn.Location = new Point(scrollDownBtnLocation.X, scrollDownBtnLocation.Y);

            if (textPanelInner.VerticalScroll.Visible) {
                lbTextTitle.MaximumSize = new Size(panelInnerHeadSize.Width - TEXT_SCROLL, 0);
                lbTextContent.MaximumSize = new Size(panelInnerSize.Width - TEXT_SCROLL, 0);
            } else {
                lbTextTitle.MaximumSize = new Size(panelInnerHeadSize.Width - TEXT_NO_SCROLL, 0);
                lbTextContent.MaximumSize = new Size(panelInnerSize.Width - TEXT_NO_SCROLL, 0);
            }
        }

        private void InitializeChromium() {
            Program.InitializeChromium();

            // Create a browser component
            String web = Settings.LOCALHOST3000 + WEB_LANG + mLanguage.getLangText() + WEB_THEME + mTheme.currentColor.getName();
            chromeBrowser = new ChromiumWebBrowser();

            Program.logInfo("web size: " + webPanelInner.Width + ":" + webPanelInner.Height);
            chromeBrowser.MaximumSize = new Size(webPanelInner.Width, webPanelInner.Height);

            chromeBrowser.Load(web);
            // Add it to the form and fill it to the form window.
            webPanelInner.Controls.Add(chromeBrowser);
            chromeBrowser.Dock = DockStyle.Fill;
            chromeBrowser.FrameLoadEnd += MyBrowserOnFrameLoadEnd;

            var callb = new BrowserCallback(chromeBrowser);
            callb.dataIncomming += Callb_DataIncomming;
            chromeBrowser.RegisterJsObject("callbackUnex", callb);

            var obj = new BrowserBoundObject(chromeBrowser);
            obj.HtmlItemClicked += Obj_HtmlItemClicked;
            chromeBrowser.RegisterJsObject("bound", obj);
            chromeBrowser.FrameLoadEnd += obj.OnFrameLoadEnd;
            chromeBrowser.KeyboardHandler = new WebKeyboardHandler();
        }

        private void Callb_DataIncomming(object sender, EventArgs e) {
            this.InvokeOnUiThreadIfRequired(() => {
                openQRScreen();
            });
        }

        private void Obj_HtmlItemClicked(object sender, HtmlItemClickedEventArgs e) {
            this.InvokeOnUiThreadIfRequired(() => {
                hideMagnifier();
            });
        }

        private void MyBrowserOnFrameLoadEnd(object sender, FrameLoadEndEventArgs frameLoadEndEventArgs) {
            ChromiumWebBrowser browser = (ChromiumWebBrowser) sender;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() => {
                browser.SetZoomLevel(mMagnifier.getWebSizeChange());
            });
        }

        public void sendMsgToBrowser(String msg) {
            String script = "dataFromComPort(\'" + msg + "\')";
            try {
                chromeBrowser.ExecuteScriptAsync(script);
            } catch (Exception ex) {
                Program.logError(ex.Message);
            }
        }

        private void imgHomeBtn_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
            mCurrScreen = SCREENS.HOME;
            refreshScreens();

            if (mSettings.reloadAtHome) {
                reloadWeb(true);
            }
        }

        private void imgHomeBtn_DoubleClick(object sender, EventArgs e) {
            mCurrScreen = SCREENS.HOME;
            refreshScreens();
            reloadWeb(true);
        }

        private void imgHowtoBtn_MouseDown(object sender, MouseEventArgs e) {
            mCurrScreen = SCREENS.HOWTO;
            menuMouseDown();
            if (mRestartCombination == 3) {
                mRestartCombination++;
                Program.logInfo("Restart combination +3");
            } else {
                mRestartCombination = 0;
            }
            if (mOutOfOrderCombination == 5 || mOutOfOrderCombination == 8) {
                mOutOfOrderCombination++;
                Program.logInfo("OutOfOrder combination +5/8");
            } else {
                mOutOfOrderCombination = 0;
            }
        }

        private void imgInfoBtn_MouseDown(object sender, MouseEventArgs e) {
            mCurrScreen = SCREENS.INFO;
            menuMouseDown();
            if (mRestartCombination == 2) {
                mRestartCombination++;
                Program.logInfo("Restart combination +2");
            } else {
                mRestartCombination = 0;
            }
            if (mOutOfOrderCombination == 6 || mOutOfOrderCombination == 9) {
                mOutOfOrderCombination++;
                Program.logInfo("OutOfOrder combination +6/9");
            } else {
                mOutOfOrderCombination = 0;
            }
        }

        private void imgTipsBtn_MouseDown(object sender, MouseEventArgs e) {
            mCurrScreen = SCREENS.TIPS;
            menuMouseDown();
            if (mRestartCombination == 1) {
                mRestartCombination++;
                Program.logInfo("Restart combination +1");
            } else {
                mRestartCombination = 0;
            }
            if (mOutOfOrderCombination == 7) {
                mOutOfOrderCombination++;
                Program.logInfo("OutOfOrder combination +7");
            } else if (mOutOfOrderCombination == 10) {
                Program.logInfo("OutOfOrder (enable): Restart manually!");
                lbVersion.Text = "RESTARTING...";
                mRestartCombination = 0;
                Program.setOutOfOrder(true, true);
                return;
            } else {
                mOutOfOrderCombination = 0;
            }
        }

        private void lbVersion_MouseDown(object sender, MouseEventArgs e) {
            mOutOfOrderCombination++;
            Program.logInfo("OutOfOrder combination: " + mOutOfOrderCombination);
        }

        private void imgQrBtn_MouseDown(object sender, MouseEventArgs e) {
            openQRScreen();
        }

        public void openQRScreen() {
            mCurrScreen = SCREENS.QR;
            menuMouseDown();
        }

        private void menuMouseDown() {
            hideMagnifier();
            refreshScreens();
            mMagnifier.refreshTimer();
        }

        private void offQrPanel() {
            if (qrVideo != null) {
                qrVideo.stop();
            }
            if (qrReader != null) {
                qrReader.stopCam();
            }
        }

        private void imgLangBtn_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
            changeLanguage();
            mMagnifier.refreshTimer();
        }

        private void imgMagnifierBtn_Click(object sender, EventArgs e) {
            if (mMagnifier.visible) {
                hideMagnifier();
            } else {
                showMagnifier();
            }
        }

        private void magnifierPanel_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
        }

        private void textPanel_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
        }

        private void imgResetBtn_Click(object sender, EventArgs e) {
            disableMagnifier();
        }

        private void imgPlusBtn_Click(object sender, EventArgs e) {
            mMagnifier.plusFontSize();
            updateUI();
        }

        private void imgMinusBtnn_Click(object sender, EventArgs e) {
            mMagnifier.minusFontSize();
            updateUI();
        }

        private void imgContrastBtn_Click(object sender, EventArgs e) {
            mMagnifier.changeContrast();
            changeTheme();
        }

        private void showMagnifier() {
            magnifierPanel.Show();
            magnifierPanel.BringToFront();
            mMagnifier.visible = true;
            imgMagnifierBtn.colorPressed();
            mMagnifier.refreshTimer();
            updateUI();
        }

        private void hideMagnifier() {
            if (mMagnifier.visible) {
                magnifierPanel.Hide();
                mMagnifier.visible = false;
            }
            if (!mMagnifier.getEnable()) {
                imgMagnifierBtn.colorNormal();
            }
        }

        public void disableMagnifier() {
            hideMagnifier();
            imgMagnifierBtn.colorNormal();
            mMagnifier.reset();
            changeTheme();
            updateUI();
        }

        public void hideLanguages() {
            imgLangCsBtn.Visible = false;
            imgLangEnBtn.Visible = false;
            imgLangDeBtn.Visible = false;
            lbLangBtnText.Visible = false;
        }

        private void changeTheme() {
            mTheme.currentColor = mMagnifier.getContrast();

            String scriptFileName = ThemeColors.getHtmlThemePath(mSettings.homeWebPath, mTheme.currentColor);
            try {
                string script = File.ReadAllText(scriptFileName);
                chromeBrowser.ExecuteScriptAsync(script);
                if (Program.getPresentationScreen() != null) {
                    Program.getPresentationScreen().useWebScript(script);
                }
            } catch (Exception ex) {
                Program.logError(ex.Message);
            }

            BackColor = mTheme.currentColor.getBackground();
            mTheme.changeColor(mTheme.currentColor);
            Program.changeTitle("UNEX:" + mLanguage.getLangText() + ":" + mTheme.currentColor.getBaseColorHex());
        }

        private void showVersionPanel_Click(object sender, EventArgs e) {
            var t = new Timer();

            if (mRestartCombination == 4) {
                Program.logInfo("Restart manually!");
                lbVersion.Text = "RESTARTING...";
                lbVersion.Visible = true;
                mRestartCombination = 0;

                t.Interval = 3000;
                t.Tick += (s, er) => {
                    Program.closeAndRestart();
                    t.Stop();
                };
                t.Start();
                return;
            }

            int delay = 10000;
            showVersion(delay);
            t.Interval = delay;
            t.Tick += (s, er) => {
                mRestartCombination = 0;
                t.Stop();
            };
            t.Start();
            mRestartCombination = 1;
        }

        private void showVersion(int delay) {
            lbVersion.Visible = true;
#if !TESTING
            var t = new Timer();
            t.Interval = delay;
            t.Tick += (s, e) => {
                lbVersion.Visible = false;
                t.Stop();
            };
            t.Start();
#endif
        }


        private void refreshButtons() {
            int btnSize = mSettings.buttonsFont;

            lbHomeBtnText.Text = xmlTextsReader.readNode("homeBtn", mLanguage.getLangText());
            lbHomeBtnText.Font = new Font(FONT, btnSize);
            lbHowtoBtnText.Text = xmlTextsReader.readNode("howtoBtn", mLanguage.getLangText());
            lbHowtoBtnText.Font = new Font(FONT, btnSize);
            lbInfoBtnText.Text = xmlTextsReader.readNode("infoBtn", mLanguage.getLangText());
            lbInfoBtnText.Font = new Font(FONT, btnSize);
            lbTipsBtnText.Text = xmlTextsReader.readNode("tipsBtn", mLanguage.getLangText());
            lbTipsBtnText.Font = new Font(FONT, btnSize);
            // we don't have a node for languages...
            lbLangBtnText.Font = new Font(FONT, btnSize);
        }

        private void refreshScreens() {
            switch (mCurrScreen) {
                case SCREENS.HOME:
                    Program.getComPortListener().setRead(true);
                    webPanel.BringToFront();
                    textPanel.Visible = false;
                    qrPanel.Visible = false;
                    if (!string.IsNullOrEmpty(mSettings.homeWebPath)) {
                        chromeBrowser.Focus();
                    }
                    mRunExe.showWindow();
                    if (mRunExe.check(true)) {
                        imgMagnifierBtn.disable();
                    }
                    updateLang();
                    break;
                case SCREENS.HOWTO:
                case SCREENS.INFO:
                case SCREENS.TIPS:
                    Program.getComPortListener().setRead(false);
                    webPanel.SendToBack();
                    textPanel.Visible = true;
                    qrPanel.Visible = false;
                    webPanel.Focus();
                    updateTextLanguage();
                    updateTextScrolls(true);
                    mRunExe.hideWindow();
                    imgMagnifierBtn.enable();
                    updateLang();
                    break;
                case SCREENS.QR:
                    Program.getComPortListener().setRead(false);
                    webPanel.SendToBack();
                    textPanel.Visible = false;
                    qrPanel.Visible = true;
                    webPanel.Focus();
                    updateTextLanguage();
                    playVideo();
                    startQrCam();
                    qrPanelVpass.Visible = false;
                    mRunExe.hideWindow();
                    imgMagnifierBtn.enable();
                    updateLang();
                    break;
            }

            if (mMagnifier.getEnable()) {
                updateUI();
            }
        }

        private void updateLang() {
            if (mSettings.langEN == true || mSettings.langDE == true) {
                lbLangBtnText.Visible = true;
            }
        }

        private void updateUI() {   // because of magnifier
            switch (mCurrScreen) {
                case SCREENS.HOME:
                    updateWebPanel();
                    break;
                case SCREENS.HOWTO:
                case SCREENS.INFO:
                case SCREENS.TIPS:
                    updateTextLanguage();
                    updateTextsSize();
                    updateTextScrolls(false);
                    break;
                case SCREENS.QR:
                    updateTextLanguage();
                    updateTextsSize();
                    break;
            }
        }

        private void updateTextLanguage() {
            String lngTxt = mLanguage.getLangText();
            Lang lng = mLanguage.getLang();

            switch (mCurrScreen) {
                case SCREENS.HOME:
                    //updateWebPanel();
                    break;
                case SCREENS.HOWTO:
                    lbTextTitle.Text = xmlTextsReader.readNode("howtoTitle", lngTxt);
                    lbTextContent.howto(lng);
                    break;
                case SCREENS.INFO:
                    lbTextTitle.Text = xmlTextsReader.readNode("infoTitle", lngTxt);
                    lbTextContent.info(lng);
                    break;
                case SCREENS.TIPS:
                    lbTextTitle.Text = xmlTextsReader.readNode("tipsTitle", lngTxt);
                    lbTextContent.tips(lng);
                    break;
                case SCREENS.QR:
                    lbQrTitle.Text = xmlTextsReader.readNode("qrTitle", lngTxt);
                    lbHowLogin.Text = xmlTextsReader.readNode("qrHowLogin", lngTxt);
                    lbCamInput.Text = xmlTextsReader.readNode("qrCamInput", lngTxt);
                    lbQrVpass.Text = xmlTextsReader.readNode("qrSending", lngTxt);
                    break;
            }

            if (mMagnifier.getEnable()) {
                magnifierPanel.BringToFront();
            }
        }

        private void reloadWeb(bool refresh) {
            reloadWeb(refresh, "");
        }

        private void reloadWeb(bool refresh, string script) {
            if (!string.IsNullOrEmpty(mSettings.homeWebPath)) {
                if (refresh) {
                    String web = Settings.LOCALHOST3000 + WEB_LANG + mLanguage.getLangText() + WEB_THEME + mTheme.currentColor.getName();
                    chromeBrowser.Load(web);
                } else {
                    try {
                        chromeBrowser.ExecuteScriptAsync(script);
                    } catch (Exception ex) {
                        Program.logError(ex.Message);
                    }
                }
            }
        }

        private void imgScrollUpBtn_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
            scrolling(false);
        }

        private void imgScrollDownBtn_MouseDown(object sender, MouseEventArgs e) {
            hideMagnifier();
            scrolling(true);
        }

        private void scrolling(bool plus) {
            //TODO vypocitat podle aktualniho okna a zvetseni...!!!magnifier
            long change = textPanelInner.VerticalScroll.LargeChange / textPanelInner.VerticalScroll.SmallChange;
            long newValue;

            if (plus) {
                newValue = textPanelInner.VerticalScroll.Value + change;
                if (newValue > textPanelInner.VerticalScroll.Maximum) {
                    newValue = textPanelInner.VerticalScroll.Maximum;
                }
            } else {
                newValue = textPanelInner.VerticalScroll.Value - change;
                if (newValue < textPanelInner.VerticalScroll.Minimum) {
                    newValue = textPanelInner.VerticalScroll.Minimum;
                }
            }

            textPanelInner.VerticalScroll.Value = (int) newValue;
            textPanelInner.VerticalScroll.Value = (int) newValue;
        }

        private void imgCloseBtn_MouseDown(object sender, MouseEventArgs e) {
            offQrPanel();
            VpassBuffer.getInstance().reset();
            hideMagnifier();
            mCurrScreen = SCREENS.HOME;
            refreshScreens();

            if (mSettings.reloadAtHome) {
                reloadWeb(true);
            }
        }

        private void imgSuccessCloseBtn_MouseDown(object sender, MouseEventArgs e) {
            offQrPanel();
            hideMagnifier();
            mCurrScreen = SCREENS.HOME;
            refreshScreens();
            reloadWeb(true);
        }

        public void changeLanguage() {
            Lang newLang = mLanguage.changeLang();
            switch (newLang) {
                case Lang.CZ:
                    imgLangCsBtn.Visible = true;
                    imgLangEnBtn.Visible = false;
                    imgLangDeBtn.Visible = false;
                    break;
                case Lang.EN:
                    imgLangCsBtn.Visible = false;
                    imgLangEnBtn.Visible = true;
                    imgLangDeBtn.Visible = false;
                    break;
                case Lang.DE:
                    imgLangCsBtn.Visible = false;
                    imgLangEnBtn.Visible = false;
                    imgLangDeBtn.Visible = true;
                    break;
            }
            StringBuilder builder = new StringBuilder("");
            builder.Append("currentLng = \'" + mLanguage.getLangText() + "\';");
            builder.Append("translatePage();");
            builder.Append("var lngChangeEvent = new CustomEvent(\"lngChange\",{detail:{lng: currentLng},bubbles: true,cancelable: true});");
            builder.Append("document.dispatchEvent(lngChangeEvent);");
            String script = builder.ToString();

            reloadWeb(false, script);
            refreshButtons();
            updateTextLanguage();
            mRunExe.sendMessage(mLanguage.getLangText());
            Program.changeTitle("UNEX:" + mLanguage.getLangText() + ":" + mTheme.currentColor.getBaseColorHex());

            if (Program.getPresentationScreen() != null) {
                Program.getPresentationScreen().useWebScript(script);
            }
        }

        private void Unex_FormClosed(object sender, FormClosedEventArgs e) {
            Program.exit();
            offQrPanel();
        }

        private void playVideo() {
            if (qrVideo == null) {
                qrVideo = new QrVideo(qrPlayer);
            }

            if (qrVideo.connected) {
                qrVideo.play();
            } else {
                lbHowLogin.Text = "No video...";
            }
        }

        private void startQrCam() {
            if (qrReader == null) {
                qrReader = new QrReader(qrCamPicBox, mSettings.videoExposure);
            }

            if (qrReader.connected) {
                qrReader.startCam();
            } else {
                lbCamInput.Text = "No webcam...";
            }
        }

        public void successQrCam(String ticketNr) {
            String lngTxt = mLanguage.getLangText();
            qrPanelVpass.Visible = true;
            lbQrVpass.Text = xmlTextsReader.readNode("qrSending", lngTxt);
            imgQrSuccessCloseBtn.Visible = false;

            VpassBuffer vbuf = VpassBuffer.getInstance();
            Vpass vpass = new Vpass(Program.getLogs(), mSettings.vpassId, ticketNr);
            vpass.addData(vbuf.getData());

            Timer timer = new Timer();
            timer.Interval = 500;
            timer.Tick += (s, e) => {
                if (vpass.sendData() == false) {
                    lbQrVpass.Text = xmlTextsReader.readNode("qrSendingTwo", lngTxt);
                    imgQrSuccessCloseBtn.Visible = false;
                    Timer timerTwo = new Timer();
                    timerTwo.Interval = 500;
                    timerTwo.Tick += (s2, e2) => {
                        if (vpass.sendData() == false) {
                            lbQrVpass.Text = xmlTextsReader.readNode("qrSendFailed", lngTxt);
                            imgQrSuccessCloseBtn.Visible = true;
                        } else {
                            lbQrVpass.Text = xmlTextsReader.readNode("qrSuccess", lngTxt);
                            imgQrSuccessCloseBtn.Visible = true;
                        }
                        timerTwo.Stop();
                    };
                    timerTwo.Start();
                } else {
                    lbQrVpass.Text = xmlTextsReader.readNode("qrSuccess", lngTxt);
                    imgQrSuccessCloseBtn.Visible = true;
                }
                timer.Stop();
            };
            timer.Start();
        }

        private void Unex_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.F2) {
                Program.setOnTop(false);
            }
        }

        private void timerMessageTick(object sender, EventArgs e) {
            Size size = TextRenderer.MeasureText(lbTextMesssage.Text, lbTextMesssage.Font);
            int width = size.Width;

            lbTextMesssage.SetBounds(messageSlideMoveX, 0, width, lbTextMesssage.Height);
            messageSlideMoveX--;
            if (messageSlideMoveX <= -width) {
                messageSlideMoveX = messagePanel.Width;
            }
        }
    }
}
