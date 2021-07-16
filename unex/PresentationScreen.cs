using CefSharp;
using CefSharp.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace UnExponat {
    public partial class PresentationScreen : Form {

        private Panel mWebPanel;

        ChromiumWebBrowser chromeBrowser;

        private const String WEB_THEME = "?theme=";

        private String mCurrentWeb;

        public PresentationScreen(String type, String path, Rectangle resolution) {
            InitializeComponent();

            //This could be used for 2 windows but it doesnt work 100%
            //this.SetStyle(
            //    ControlStyles.AllPaintingInWmPaint |
            //    ControlStyles.UserPaint |
            //    ControlStyles.DoubleBuffer,
            //    true);

            mWebPanel = new Panel();
            mWebPanel.Size = new Size(resolution.Width, resolution.Height);

            if (string.IsNullOrEmpty(path)) {
                Program.logError("[PresentationScreen] path is empty!");
                return;
            }

            if (String.Compare(type, "web") == 0) {
                mCurrentWeb = path + WEB_THEME + Program.getTheme().currentColor.getName();
                webPanel.Visible = true;
                Program.InitializeChromium();
                chromeBrowser = new ChromiumWebBrowser();

                Program.logInfo("web two size: " + resolution.Width + ":" + resolution.Height);
                chromeBrowser.MaximumSize = new Size(resolution.Width, resolution.Height);

                chromeBrowser.Load(mCurrentWeb);
                // Add it to the form and fill it to the form window.
                webPanel.Controls.Add(chromeBrowser);
                chromeBrowser.Dock = DockStyle.Fill;
            } else if (String.Compare(type, "exe") == 0) {
                //TODO in future (not needed now)
            }
        }

        public void loadWeb(String web) {
            mCurrentWeb = web;
            chromeBrowser.Load(web);
        }

        public void useWebScript(String script) {
            try {
                chromeBrowser.ExecuteScriptAsync(script);
            } catch (Exception ex) {
                Program.logError("[PresentationScreen]: " + ex.Message);
            }
        }
    }
}
