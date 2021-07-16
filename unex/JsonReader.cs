using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class JsonReader {

        String mFilePath;
        JsonMap mJson;

        enum Node { NAME, HOWTO, INFO, TIPS }

        public JsonReader(String fileName) {
            mFilePath = fileName;
            Program.logInfo("[JsonReader] path: " + mFilePath);

            try {
                String jsonString = File.ReadAllText(mFilePath);
                mJson = JsonConvert.DeserializeObject<JsonMap>(jsonString);
            } catch (IOException e) {
                String text = "[JsonReader] The file" + mFilePath + " could not be read:";
                Program.logError(text);
                Program.logError(e.Message);
                MessageBox.Show(text + e.Message, "Translation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                Environment.Exit(-1);
            }
        }

        public String getName(String id, Lang lang) {
            return searchId(id, Node.NAME, lang);
        }

        public String getName(String id, Lang lang, bool tags) {
            return searchId(id, Node.NAME, lang, tags);
        }

        public String getHowto(String id, Lang lang) {
            return searchId(id, Node.HOWTO, lang);
        }

        public String getHowto(String id, Lang lang, bool tags) {
            return searchId(id, Node.HOWTO, lang, tags);
        }

        public String getInfo(String id, Lang lang) {
            return searchId(id, Node.INFO, lang);
        }

        public String getInfo(String id, Lang lang, bool tags) {
            return searchId(id, Node.INFO, lang, tags);
        }

        public String getTips(String id, Lang lang) {
            return searchId(id, Node.TIPS, lang);
        }

        public String getTips(String id, Lang lang, bool tags) {
            return searchId(id, Node.TIPS, lang, tags);
        }

        private String searchId(String id, Node node, Lang lang) {
            return searchId(id, node, lang, false);
        }

        private String searchId(String id, Node node, Lang language, bool tags) {
            String text = null;
            if (mJson.Id.ContainsKey(id)) {

                switch (node) {
                    case Node.NAME:
                        switch (language) {
                            case Lang.CZ: text = mJson.Id[id].Name.CS; break;
                            case Lang.DE: text = mJson.Id[id].Name.DE; break;
                            case Lang.EN: text = mJson.Id[id].Name.EN; break;
                        };
                        break;
                    case Node.HOWTO:
                        switch (language) {
                            case Lang.CZ: text = mJson.Id[id].Howto.CS; break;
                            case Lang.DE: text = mJson.Id[id].Howto.DE; break;
                            case Lang.EN: text = mJson.Id[id].Howto.EN; break;
                        };
                        break;
                    case Node.INFO:
                        switch (language) {
                            case Lang.CZ: text = mJson.Id[id].Info.CS; break;
                            case Lang.DE: text = mJson.Id[id].Info.DE; break;
                            case Lang.EN: text = mJson.Id[id].Info.EN; break;
                        };
                        break;
                    case Node.TIPS:
                        switch (language) {
                            case Lang.CZ: text = mJson.Id[id].Tips.CS; break;
                            case Lang.DE: text = mJson.Id[id].Tips.DE; break;
                            case Lang.EN: text = mJson.Id[id].Tips.EN; break;
                        };
                        break;
                }

                if (!tags) {
                    text = removeTags(text);
                }
            }

            return text;
        }

        private String removeTags(String input) {
            String output;

            // new paragraph block
            output = input.Replace("</p><p>", "\r\n\r\n");
            // new line
            output = output.Replace("<br />", "\r\n");
            // remove others tags
            output = Regex.Replace(output, @"<.*?>", "");

            return output;
        }
    }
}
