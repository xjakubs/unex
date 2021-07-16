using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class Settings {
        public enum enMainScreenStyle { TEXT, WEB, VIDEO }

        public const String PATH = "C:/app/";

        public const String LOCALHOST3000 = "http://localhost:3000/";
        public const String LOCALHOST3001 = "http://localhost:3001/";

        private const String EXPONAT_ID = "EXPONAT_ID=";
        private const String VPASS_ID = "VPASS_ID=";
        private const String MOUSE_INVISIBLE = "MOUSE_INVISIBLE=";
        private const String COLOR_SCHEME = "COLOR_SCHEME=";
        private const String HOME_EXE_PATH = "HOME_EXE_PATH=";
        private const String HOME_WEB_PATH = "HOME_WEB_PATH=";
        private const String HOWTO_WEB_PATH = "HOWTO_WEB_PATH=";
        private const String INFO_WEB_PATH = "INFO_WEB_PATH=";
        private const String TIPS_WEB_PATH = "TIPS_WEB_PATH=";
        private const String LANG_EN = "LANG_EN=";
        private const String LANG_DE = "LANG_DE=";
        private const String RELOAD_AT_HOME = "RELOAD_AT_HOME=";
        private const String HEADLINE_FONT = "HEADLINE_FONT=";
        private const String TITLE_FONT = "TITLE_FONT=";
        private const String TEXT_FONT = "TEXT_FONT=";
        private const String BUTTONS_FONT = "BUTTONS_FONT=";
        private const String COMPORT_1 = "COMPORT_1=";
        private const String VIDEO_EXPOSURE = "VIDEO_EXPOSURE=";
        private const String SECOND_SCREEN_WEB_PATH = "SECOND_SCREEN_WEB_PATH=";
        private const String ONLY_PRESENTATION = "ONLY_PRESENTATION=";

        private String mExponatId = "";
        private int mVpassId = -1;
        private bool mMouseInvisible = true;
        private String mColorScheme = "GREEN";
        private String mHomeExePath = "";
        private String mHomeWebPath = "";
        private String mHowtoWebPath = "";
        private String mInfoWebPath = "";
        private String mTipsWebPath = "";
        private bool mLangEN = true;
        private bool mLangDE = true;
        private bool mReloadAtHome = false;
        private int mHeadlineFont;
        private int mTitleFont;
        private int mTextFont;
        private int mButtonsFont;
        private String mComPort1 = "";
        private int mVideoExposure = 0;
        private String mSecondScreenWebPath = "";
        private bool mOnlyPresentation = false;

        public List<SettingObject> lSettingObjects = new List<SettingObject>();

        /**
         * SECTION: [OPTICAL_RECORDING]
         */
        //private const String ARDUINO_2_PORT = "ARDUINO_2=";

        //private String mPortArduino2 = "COM0";

        String mFilePath;

        public string exponatId { get => mExponatId; }
        public int vpassId { get => mVpassId; }
        public bool mouseInvisible { get => mMouseInvisible; }
        public string colorScheme { get => mColorScheme; }
        public string homeExePath { get => mHomeExePath; }
        public string homeWebPath { get => mHomeWebPath; }
        public string howtoWebPath { get => mHowtoWebPath; }
        public string infoWebPath { get => mInfoWebPath; }
        public string tipsWebPath { get => mTipsWebPath; }
        public bool langEN { get => mLangEN; }
        public bool langDE { get => mLangDE; }
        public bool reloadAtHome { get => mReloadAtHome; }
        public int headlineFont { get => mHeadlineFont; }
        public int titleFont { get => mTitleFont; }
        public int textFont { get => mTextFont; }
        public int buttonsFont { get => mButtonsFont; }
        public string comPort1 { get => mComPort1; }
        public int videoExposure { get => mVideoExposure; }
        public string secondScreenWebPath { get => mSecondScreenWebPath; }
        public bool onlyPresentation { get => mOnlyPresentation; }

        public Settings(String fileName) {
            if (File.Exists(PATH + fileName) == true) {
                mFilePath = PATH + fileName;
            } else {
                mFilePath = fileName;
            }
            Program.logInfo("[Settings] path: " + mFilePath);

            readSettings();
        }

        private void readSettings() {

            try {
                using (StreamReader file = new StreamReader(mFilePath)) {
                    String line;

                    while ((line = file.ReadLine()) != null) {
                        // COMMENTS
                        if (line.StartsWith("#")) {
                            continue;
                        }

                        if (line.Length == 0) {
                            continue;
                        }

                        // Replace all white spaces
                        line = line.Replace(" ", "");

                        // EXPONAT_ID
                        if (checkString(line, EXPONAT_ID, ref mExponatId)) {
                            continue;
                        }

                        // VPASS_ID
                        if (checkInt(line, VPASS_ID, ref mVpassId)) {
                            continue;
                        }

                        // MOUSE_INVISIBLE
                        if (checkBool(line, MOUSE_INVISIBLE, ref mMouseInvisible)) {
                            continue;
                        }

                        // COLOR_SCHEME
                        if (checkValue(line, COLOR_SCHEME, ref mColorScheme)) {
                            continue;
                        }

                        // HOME_EXE_PATH
                        if (checkString(line, HOME_EXE_PATH, ref mHomeExePath)) {
                            continue;
                        }

                        // HOME_WEB_PATH
                        if (checkWebString(line, HOME_WEB_PATH, ref mHomeWebPath)) {
                            continue;
                        }

                        // HOWTO_WEB_PATH
                        if (checkWebString(line, HOWTO_WEB_PATH, ref mHowtoWebPath)) {
                            continue;
                        }

                        // INFO_WEB_PATH
                        if (checkWebString(line, INFO_WEB_PATH, ref mInfoWebPath)) {
                            continue;
                        }

                        // TIPS_WEB_PATH
                        if (checkWebString(line, TIPS_WEB_PATH, ref mTipsWebPath)) {
                            continue;
                        }

                        // HOME_WEB_PATH
                        if (checkWebString(line, SECOND_SCREEN_WEB_PATH, ref mSecondScreenWebPath)) {
                            continue;
                        }

                        // RELOAD_AT_HOME
                        if (checkBool(line, RELOAD_AT_HOME, ref mReloadAtHome)) {
                            continue;
                        }

                        // LANG_EN
                        if (checkBool(line, LANG_EN, ref mLangEN)) {
                            continue;
                        }

                        // LANG_DE
                        if (checkBool(line, LANG_DE, ref mLangDE)) {
                            continue;
                        }

                        // HEADLINE_FONT
                        if (checkInt(line, HEADLINE_FONT, ref mHeadlineFont)) {
                            continue;
                        }

                        // TITLE_FONT
                        if (checkInt(line, TITLE_FONT, ref mTitleFont)) {
                            continue;
                        }

                        // TEXT_FONT
                        if (checkInt(line, TEXT_FONT, ref mTextFont)) {
                            continue;
                        }

                        // BUTTONS_FONT
                        if (checkInt(line, BUTTONS_FONT, ref mButtonsFont)) {
                            continue;
                        }

                        // COMPORT_1
                        if (checkValue(line, COMPORT_1, ref mComPort1)) {
                            continue;
                        }

                        // VIDEO_EXPOSURE
                        if (checkInt(line, VIDEO_EXPOSURE, ref mVideoExposure)) {
                            continue;
                        }

                        // ONLY_PRESENTATION
                        if (checkBool(line, ONLY_PRESENTATION, ref mOnlyPresentation)) {
                            continue;
                        }

                        /**
                         *  SECTION: [OPTICAL_RECORDING]
                         */
                        /*

                       // ARDUINO 2
                       if(checkValue(line, ARDUINO_2_PORT, ref mPortArduino2)) {
                            continue;
                        }
                       */


                    }

                    file.Close();
                }
            } catch (IOException e) {
                String text = "[Settings] The file" + mFilePath + " could not be read:";
                Program.logError(text);
                Program.logError(e.Message);
                MessageBox.Show(text + e.Message, "Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(-1);
            }
        }


        private bool checkValue(String line, String constant, ref String value) {
            if (line.Contains(constant)) {
                value = line.Replace(constant, "");
                Program.logInfo("[Settings] " + constant + value);
                lSettingObjects.Add(new SettingObject(constant, value));
                return true;
            }
            return false;
        }

        private bool checkString(String line, String constant, ref String value) {
            if (line.Contains(constant)) {
                value = line.Replace("\"", "");
                value = value.Replace(constant, "");
                Program.logInfo("[Settings] " + constant + value);
                lSettingObjects.Add(new SettingObject(constant, value));
                return true;
            }
            return false;
        }

        private bool checkWebString(String line, String constant, ref String value) {
            if (line.Contains(constant)) {
                value = line.Replace("\"", "");
                value = value.Replace(constant, "");
                value = value.Replace("\\", "/");
                Program.logInfo("[Settings] " + constant + value);
                lSettingObjects.Add(new SettingObject(constant, value));
                return true;
            }
            return false;
        }

        private bool checkBool(String line, String constant, ref bool value) {
            String tmp;
            if (line.Contains(constant)) {
                tmp = line.Replace(constant, "");
                if (tmp.Contains("true")) {
                    value = true;
                } else {
                    value = false;
                }
                Program.logInfo("[Settings] " + constant + value);
                lSettingObjects.Add(new SettingObject(constant, value == true ? "1" : "0"));
                return true;
            }
            return false;
        }

        private bool checkInt(String line, String constant, ref int value) {
            String tmp;
            if (line.Contains(constant)) {
                tmp = line.Replace(constant, "");
                value = Int32.Parse(tmp);
                Program.logInfo("[Settings] " + constant + value);
                lSettingObjects.Add(new SettingObject(constant, value.ToString()));
                return true;
            }
            return false;
        }

    }
}
