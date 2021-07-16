using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class MyLabel : Label {

        private MemoryLangText mMemoryHowto;
        private MemoryLangText mMemoryInfo;
        private MemoryLangText mMemoryTips;

        public MyLabel(JsonReader jr, string id) : base() {
            mMemoryHowto = new MemoryLangText(jr.getHowto(id, Lang.CZ), jr.getHowto(id, Lang.EN), jr.getHowto(id, Lang.DE));
            mMemoryInfo = new MemoryLangText(jr.getInfo(id, Lang.CZ), jr.getInfo(id, Lang.EN), jr.getInfo(id, Lang.DE));
            mMemoryTips = new MemoryLangText(jr.getTips(id, Lang.CZ), jr.getTips(id, Lang.EN), jr.getTips(id, Lang.DE));
        }

        public void howto(Lang lang) {
            switch (lang) {
                case Lang.CZ: this.Text = mMemoryHowto.CZ; break;
                case Lang.EN: this.Text = mMemoryHowto.EN; break;
                case Lang.DE: this.Text = mMemoryHowto.DE; break;
            }
        }

        public void info(Lang lang) {
            switch (lang) {
                case Lang.CZ: this.Text = mMemoryInfo.CZ; break;
                case Lang.EN: this.Text = mMemoryInfo.EN; break;
                case Lang.DE: this.Text = mMemoryInfo.DE; break;
            }
        }

        public void tips(Lang lang) {
            switch (lang) {
                case Lang.CZ: this.Text = mMemoryTips.CZ; break;
                case Lang.EN: this.Text = mMemoryTips.EN; break;
                case Lang.DE: this.Text = mMemoryTips.DE; break;
            }
        }

    }
}
