using System;
using System.Drawing;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using MB.Web;
using xjakubs.Logger;
using CefSharp.WinForms;
using CefSharp;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    static class Program {

        private const String FILE_SETTINGS = "settings.ini";
        private const String FILE_TRANSLATE = "preklady.json";
        private const String v_num = "v0.58.1";
#if TESTING
        private const String mVersion = v_num + "debug";
#else
        private const String mVersion = v_num;
#endif
        private const String ARG_OUT_OF_ORDER = "outoforder";
        private const String ARG_NORMAL_USE = "normaluse";

        private static Logs mLogs;
        private static JsonReader mJsonReader;
        private static Settings mSettings;
        private static Theme mTheme;
        private static ComPort mComPortListener;
        private static RunExe mRunExe;
        public static Unex form;
        private static PresentationScreen formTwo;

        public static WebSocket socket;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static void Main(string[] args) {

            // Unex UI
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            mLogs = new Logs("LOG_unex_");
            logInfo(" ========= Start UNEX " + mVersion + "=========");

            if (checkProgramLoop()) {
                logInfo("UNEX is running in a different process! This process will not launched.");
                Application.Exit();
                Environment.Exit(1);
            }

            if (args.Length != 0) {
                if (args[0].CompareTo(ARG_OUT_OF_ORDER) == 0) {
                    logInfo("argument: " + ARG_OUT_OF_ORDER);
                    setOutOfOrder(true, false);
                } else if (args[0].CompareTo(ARG_NORMAL_USE) == 0) {
                    logInfo("argument: " + ARG_NORMAL_USE);
                    setOutOfOrder(false, false);
                } else {
                    logError("not valid argument!");
                }
            }
            if (File.Exists(ARG_OUT_OF_ORDER) == true) {
                logInfo("OUT OF ORDER MODE");
                Application.Run(new OutOfOrder(mVersion));
                return;
            }

            mSettings = new Settings(FILE_SETTINGS);
            mTheme = new Theme(ThemeColors.getTheme(mSettings.colorScheme.ToLower()));

            // WebSocket
            socket = new WebSocket(mLogs, mSettings.exponatId, mSettings.vpassId);
            Thread threadWebSocket = new Thread(socket.Start);
            threadWebSocket.Start();
            // WebSocket - end

            if (mSettings.onlyPresentation) {
                showPresentationScreen("web", mSettings.secondScreenWebPath + "index.html", true);
                return;
            }

            // WebServer
            var curDir = Directory.GetCurrentDirectory();
            String webDir = mSettings.homeWebPath;
            WebServer server = new WebServer(Settings.LOCALHOST3000, webDir, mLogs);
            Thread threadWebServer = new Thread(server.Start);
            threadWebServer.Start();
            // WebServer - end

            mJsonReader = new JsonReader(FILE_TRANSLATE);
            mComPortListener = new ComPort(mSettings.comPort1);
            mRunExe = new RunExe(mSettings.homeExePath);

            Application.Run(new SplashScreen(mVersion) { TopMost = true });
        }

        public static void setOutOfOrder(bool enable, bool exit) {
            if (enable) {
                using (StreamWriter w = File.AppendText(ARG_OUT_OF_ORDER)) ;
            } else {
                File.Delete(ARG_OUT_OF_ORDER);
            }

            if (exit) {
                Application.Exit();
                Environment.Exit(1);
            }
        }

        public static void changeTitle(String text) {
            form.Text = text;
        }

        public static void sendSocketMessage(String msg) {
            socket.Send(msg);
        }

        public static void showSocketMessage(String msg) {
            form.showTextMsg(msg, 0);
        }

        public static void showSocketMessage(String msg, int time) {
            form.showTextMsg(msg, time);
        }

        public static void startUnex() {
            form = new Unex();

#if !TESTING
            // ALWAYS ON TOP
            setOnTop(true);
#endif

            // FULLSCREEN
            if (isMy4Kdisplay()) {
                form.WindowState = FormWindowState.Normal;
                form.StartPosition = FormStartPosition.Manual;
                form.Left = 1920;
                form.Top = 1080;
            } else {
                // FULLSCREEN
                form.FormBorderStyle = FormBorderStyle.None;
                form.WindowState = FormWindowState.Maximized;
            }
            form.Text = "UNEX";

            if (!string.IsNullOrEmpty(mSettings.secondScreenWebPath)) {
                // WebServer
                var curDir = Directory.GetCurrentDirectory();
                String webDir = mSettings.secondScreenWebPath;
                WebServer server = new WebServer(Settings.LOCALHOST3001, webDir, mLogs);
                Thread threadWebServer = new Thread(server.Start);
                threadWebServer.Start();
                // WebServer - end
                showPresentationScreen("web", Settings.LOCALHOST3001, false);
                form.ShowDialog();
            } else {
                form.Show();
            }
        }

        public static bool checkProgramLoop() {
            int count = 0;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning) {
                if (pr.ProcessName == "UNEX") {
                    count++;
                }
            }

            return (count > 1 ? true : false);
        }

        public static void setOnTop(bool onTop) {
            form.TopMost = onTop;
        }

        public static String getVersion() {
            return mVersion;
        }

        public static Logs getLogs() {
            return mLogs;
        }

        public static JsonReader getJsonReader() {
            return mJsonReader;
        }

        public static Settings getSettings() {
            return mSettings;
        }

        public static Theme getTheme() {
            return mTheme;
        }

        public static ComPort getComPortListener() {
            return mComPortListener;
        }

        public static RunExe getRunExe() {
            return mRunExe;
        }

        public static PresentationScreen getPresentationScreen() {
            return formTwo;
        }

        public static void logInfo(String content) {
            mLogs.logInfo(content);
        }

        public static void logError(String content) {
            mLogs.logError(content);
        }

        public static void sendComMessage(String msg) {
            if (form != null) {
                form.sendMsgToBrowser(msg);
            }
        }

        public static void loadPresentationWeb(String url) {
            if (formTwo != null) {
                formTwo.loadWeb(url);
            }
        }

        public static void closeAndRestart() {
            Process.Start("shutdown", "/r /t 0");
            Application.Exit();
        }

        public static void exit() {
            Cef.Shutdown();
            mRunExe.kill();
            if (formTwo != null) {
                formTwo.Hide();
                formTwo.Close();
            }

            // remove temp folders of CefSharp
            if (Directory.Exists(mBlobStorage)) {
                Directory.Delete(mBlobStorage, true);
            }
            if (Directory.Exists(mGPUCache)) {
                Directory.Delete(mGPUCache, true);
            }
            Application.Exit();
        }

        private static bool isMy4Kdisplay() {
            // private hack for my 4K display
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            if (resolution.Height == 2160 && resolution.Width == 3840) {
                return true;
            } else {
                return false;
            }

        }


        private static void showPresentationScreen(String type, String path, bool onlyPresentation) {
            Console.WriteLine("SCREENS: count = " + Screen.AllScreens.Length);           

            if (onlyPresentation) {
                Rectangle resolution = Screen.PrimaryScreen.Bounds;
                
                if (isMy4Kdisplay()) {
                    formTwo = new PresentationScreen(type, path, new Rectangle(0,0,1920,1080));
                    formTwo.WindowState = FormWindowState.Normal;
                    formTwo.StartPosition = FormStartPosition.Manual;
                    formTwo.Left = 1920;
                    formTwo.Top = 1080;
                } else {
                    formTwo = new PresentationScreen(type, path, resolution);
                    // FULLSCREEN
                    formTwo.WindowState = FormWindowState.Maximized;                    
                    formTwo.Height = resolution.Height;
                    formTwo.Width = resolution.Width;
                }
                formTwo.FormBorderStyle = FormBorderStyle.None;
                formTwo.Text = "UNEX";
#if !TESTING
                // ALWAYS ON TOP
                formTwo.TopMost = true;
#endif
                Application.Run(formTwo);
                return;
            }

            // The HACK for my 4K: second screen on one display
            //if (isMy4Kdisplay()) {
            //    formTwo = new PresentationScreen(type, path, new Rectangle(0, 0, 1920, 1080));
            //    formTwo.StartPosition = FormStartPosition.Manual;
            //    formTwo.FormBorderStyle = FormBorderStyle.None;
            //    formTwo.Text = "UNEX_TWO";
            //    formTwo.Show();
            //    return;
            //}
            if (Screen.AllScreens.Length > 1) {
                Screen screen = GetSecondaryScreen();

                formTwo = new PresentationScreen(type, path, screen.Bounds);
                formTwo.StartPosition = FormStartPosition.Manual;
                formTwo.Location = screen.WorkingArea.Location; // set the location to the top left of the second screen
                formTwo.Size = new Size(screen.Bounds.Width, screen.Bounds.Height);   // set it fullscreen
#if !TESTING
            // ALWAYS ON TOP
            formTwo.TopMost = true;
#endif
                // FULLSCREEN
                formTwo.FormBorderStyle = FormBorderStyle.None;
                formTwo.WindowState = FormWindowState.Maximized;
                formTwo.Text = "UNEX_TWO";
                formTwo.Show();
            } else {
                mLogs.logError("No second screen!");
            }
        }

        private static Screen GetSecondaryScreen() {
            if (Screen.AllScreens.Length == 1) {
                return null;
            }

            foreach (Screen screen in Screen.AllScreens) {
                if (screen.Primary == false) {
                    return screen;
                }
            }

            return null;
        }

        // :::::::: CefSharp :::::::: 

        public static bool mCefInitialized = false;

        private const String mBlobStorage = "blob_storage";
        private const String mGPUCache = "GPUCache";

        public static void InitializeChromium() { //INFO: CEF can only be initialized once per process. 
            if (mCefInitialized == false) {
                CefSettings settings = new CefSettings();
                // Add the --enable-media-stream flag
                settings.CefCommandLineArgs.Add("enable-media-stream", "1");
                settings.CefCommandLineArgs.Add("disable-pinch", "1");
                settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
                settings.LogSeverity = LogSeverity.Disable;

                // Initialize cef with the provided settings
                Cef.Initialize(settings);
                CefSharpSettings.LegacyJavascriptBindingEnabled = true;
                mCefInitialized = true;
            }
        }

        // :::::::: ######## :::::::: 
    }
}
