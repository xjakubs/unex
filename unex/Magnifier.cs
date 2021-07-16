using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class Magnifier {

        private const int DELAY = 3 * 60 * 1000; // 3min
        private const int MAX_THEME = 7;


        // There should be first item 0 - it means, magnifier is disabled
        private int[] aFontSize = { 0, 5, 25, 50, 120, 250, 350 };
        private double[] aWebSize = { 0, 0.3, 0.8, 2, 4, 6.5, 10 };
        private const int SIZE_MAX = 6;    // lenght of field - 1
        private const int TITLE_SIZE_MAX = 3;

        private bool mVisible;
        private int mZoom;
        private int mContrastIdx;
        private int mContrastDefaultIdx;

        private MyColor[] aThemes = new MyColor[MAX_THEME];

        private Timer mTimer;

        public bool visible { get => mVisible; set => mVisible = value; }

        public Magnifier(MyColor theme) {
            mContrastDefaultIdx = 0;

            //themes for magnifier + default theme
            aThemes[0] = theme;
            aThemes[1] = ThemeColors.BLACK;
            aThemes[2] = ThemeColors.WHITE;
            aThemes[3] = ThemeColors.GREEN;

            aThemes[4] = ThemeColors.ORANGE;
            aThemes[5] = ThemeColors.RED;
            aThemes[6] = ThemeColors.BLUE;

            reset();
        }

        public void reset() {
            mZoom = 0;
            mContrastIdx = mContrastDefaultIdx;
        }

        public bool getEnable() {
            if (mZoom == 0 && mContrastIdx == mContrastDefaultIdx) {
                return false;
            } else {
                return true;
            }
        }

        // ZOOM
        public int getFontSizeChange() {
            if (getEnable()) {
                return aFontSize[mZoom];
            } else {
                return 0;
            }
        }

        public int getTitleFontSizeChange() {
            if (getEnable()) {
                if (mZoom > TITLE_SIZE_MAX) {
                    return aFontSize[TITLE_SIZE_MAX];
                } else {
                    return aFontSize[mZoom];
                }
            } else {
                return 0;
            }
        }

        public double getWebSizeChange() {
            if (getEnable()) {
                return aWebSize[mZoom];
            } else {
                return 0;
            }
        }

        public void plusFontSize() {
            mZoom++;
            if (mZoom > SIZE_MAX) {
                mZoom = SIZE_MAX;
            }
            Program.logInfo("Size change: " + mZoom + " - Font: " + aFontSize[mZoom]);
            refreshTimer();
        }

        public void minusFontSize() {
            mZoom--;
            if (mZoom < 0) {
                mZoom = 0;
            }
            Program.logInfo("Size change: " + mZoom + " - Font: " + aFontSize[mZoom]);
            refreshTimer();
        }

        // CONTRAST
        public MyColor getContrast() {
            if (getEnable()) {
                return aThemes[mContrastIdx];
            } else {
                return aThemes[mContrastDefaultIdx];
            }
        }

        public void changeContrast() {
            mContrastIdx++;

            //check if theme is the same
            if (mContrastIdx > 0 && mContrastIdx < MAX_THEME) {
                if (aThemes[0] == aThemes[mContrastIdx]) {
                    mContrastIdx++;
                }
            }

            if (mContrastIdx >= MAX_THEME) {
                mContrastIdx = 0;
            }
            refreshTimer();
        }

        // TIMER
        public void refreshTimer() {
            if (mTimer != null) {
                mTimer.Stop();
            }

            if (getEnable()) {
                mTimer = new Timer();
                mTimer.Interval = DELAY;
                mTimer.Tick += (s, e) => {
                    Program.form.disableMagnifier();
                    mTimer.Stop();
                };
                mTimer.Start();
            }
        }
    }
}
