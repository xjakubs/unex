using System;
using System.Text;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {

    public enum Lang { CZ, DE, EN, LAST };

    class Language {
        private const int MAX = 4;
        private Lang[] aLangs = new Lang[MAX];

        private bool mEnable = false;
        private String mLangText;
        private int mCurrentLang;
        private String mTitle;

        public bool isEnabled () {
            return mEnable;
        }

        public Language(bool english, bool german) {
            mEnable = false;

            StringBuilder title = new StringBuilder("");
            int i = 0;
            aLangs[i++] = Lang.CZ;
            title.Append("cz");

            if (english) {
                aLangs[i++] = Lang.EN;
                title.Append("/en");
                mEnable = true;
            }

            if (german) {
                aLangs[i++] = Lang.DE;
                mEnable = true;
                title.Append("/de");
            }

            for (; i < MAX; ) {
                aLangs[i++] = Lang.LAST;
            }

            mTitle = title.ToString();

            mCurrentLang = 0;
            mLangText = "cs";
        }

        public Lang getLang() {
            return aLangs[mCurrentLang];
        }

        public String getLangText() {
            return mLangText;
        }


        public Lang changeLang() {
            mCurrentLang++;
            if(aLangs[mCurrentLang] == Lang.LAST) {
                mCurrentLang = 0;
            }

            if (aLangs[mCurrentLang] == Lang.CZ) {
                mLangText = "cs";
            } else if (aLangs[mCurrentLang] == Lang.EN) {
                mLangText = "en";
            } else if (aLangs[mCurrentLang] == Lang.DE) {
                mLangText = "de";
            }

            return aLangs[mCurrentLang];
        }

        public String getTitle() {
            return mTitle;
        }


    }
}
