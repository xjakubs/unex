using CefSharp.WinForms;
using System;
using System.IO;
using System.Text.RegularExpressions;
using VpassConnection;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class BrowserCallback {

        public delegate void EventHandler(object sender, EventArgs e);
        public event EventHandler dataIncomming;

        public ChromiumWebBrowser browser;

        private int mCountSavedData = 0;

        public BrowserCallback(ChromiumWebBrowser br) {
            browser = br;
        }

        private const string IMAGE = "ImageBase64";
        private const string VIDEO = "Video";
        private const string TEXT = "String";
        private const string NUMBER = "Int";

        private const string TMP_PATH = "tmp";
        private const string JPG = ".jpg";

        public void dataCount(string count) {
            Program.logInfo("[BrowserCallback] dataCount: " + count);
            if (string.IsNullOrEmpty(count)) {
                Program.logError("[BrowserCallback] dataCount is null/empty");
                return;
            }
            int cnt = int.Parse(count);
            if (cnt <= 0) {
                Program.logError("[BrowserCallback] dataCount is not bigger than 0");
                return;
            }

            // reset  Vpass data + buffer
            mCountSavedData = 0;
            VpassBuffer.getInstance().reset();
            VpassBuffer.getInstance().setCount(cnt);

            // Not needed for "canvas.toDataURL('image/jpeg', 1.0);"
            //Directory.CreateDirectory(TMP_PATH);
        }

        public void sendData(String codename, String type, String content) {
            Program.logInfo("[BrowserCallback] sendData( codename: " + codename + ", type: " + type + " )");

            VpassBuffer vbuf = VpassBuffer.getInstance();
            mCountSavedData++;
            if (mCountSavedData > vbuf.getCount()) {
                return;
            }

            if (type.Equals(IMAGE)) {
                //string tmpPicture = Directory.GetCurrentDirectory() + "\\" + TMP_PATH + "\\" + codename + JPG;

                //canvas.toDataURL('image/jpeg', 1.0);
                vbuf.addData(new VpassData(codename, GetBase64FromJavaScriptImage(content), VpassType.TYPE.picture));

                //canvas.toDataURL('image/png');
                // convert back from base64 to picture
                //using (Image image = Image.FromStream(new MemoryStream(Convert.FromBase64String(GetBase64FromJavaScriptImage(content))))) {
                //    image.Save(tmpPicture, System.Drawing.Imaging.ImageFormat.Jpeg);
                //}

                //string tmpPictureEncoded;
                //using (Image image = Image.FromFile(tmpPicture)) {
                //    using (MemoryStream ms = new MemoryStream()) {
                //        image.Save(ms, image.RawFormat);
                //        byte[] imageBytes = ms.ToArray();
                //        tmpPictureEncoded = Convert.ToBase64String(imageBytes);
                //        vbuf.addData(new VpassData(codename, tmpPictureEncoded, VpassType.TYPE.picture));
                //    }
                //}
            } else if (type.Equals(VIDEO)) {
                vbuf.addData(new VpassData(codename, content, VpassType.TYPE.video));
            } else if (type.Equals(TEXT)) {
                vbuf.addData(new VpassData(codename, content, VpassType.TYPE.text));
            } else if (type.Equals(NUMBER)) {
                vbuf.addData(new VpassData(codename, content, VpassType.TYPE.number));
            }

            // open QR and send data
            if (mCountSavedData == vbuf.getCount()) {
                if (dataIncomming != null) {
                    dataIncomming(this, new EventArgs());
                }
            }
        }

        public static String GetBase64FromJavaScriptImage(String imgData) {
            return Regex.Match(imgData, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
        }

        public void dataToComPort(string data) {
            Program.logInfo("[BrowserCallback] dataToComPort: " + data);
            Program.getComPortListener().sendData(data);
        }

    }
}