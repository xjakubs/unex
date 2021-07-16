using System;
using System.IO;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class QrVideo {

        private const String mMovie = "QR_howto.mp4";
        private const String mPath = "\\Resources\\video\\";
        private AxWMPLib.AxWindowsMediaPlayer mPlayer;
        private bool mConnected = false;

        public bool connected { get => mConnected; }

        public QrVideo(AxWMPLib.AxWindowsMediaPlayer player) {
            String path = Directory.GetCurrentDirectory() + mPath + mMovie;
            if (File.Exists(path) == false) {
                return;
            }

            mPlayer = player;
            mPlayer.uiMode = "none";
            mPlayer.settings.setMode("loop", true);

            if (File.Exists(path) == false) {
                Program.logError("The QR video is missing in path: " + path);
                return;
            }

            mConnected = true;
            mPlayer.URL = path;
        }

        public void play() {
            mPlayer.Ctlcontrols.play();
        }

        public void stop() {
            mPlayer.Ctlcontrols.stop();
        }

    }
}