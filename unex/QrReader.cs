using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class QrReader {

        private FilterInfoCollection videoDevicesList;
        private VideoCaptureDevice videoSource;

        private PictureBox mPictureBox;

        private const int DELAY = 200; // 1s
        private Timer mTimer;

        private const String CAM_NAME = "Microsoft® LifeCam HD-3000";
        private String mMoniker = "";
        private String mUsedCameraName = "";
        private bool mConnected = false;

        List<String> videoSourceList = new List<String>();
        String mQrTag = "";

        public bool connected { get => mConnected; }

        public QrReader(PictureBox box, int newExposure) {
            mPictureBox = box;

            // get list of video devices
            videoDevicesList = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoDevice in videoDevicesList) {
                videoSourceList.Add(videoDevice.Name);
                Program.logInfo("QRreader: video device = " + videoDevice.Name);
                if (String.Compare(videoDevice.Name, CAM_NAME) == 0) {
                    mMoniker = videoDevice.MonikerString;
                    mUsedCameraName = CAM_NAME;
                }
            }
            if (videoSourceList.Count > 0) {
                if (String.IsNullOrEmpty(mMoniker)) {
                    mMoniker = videoDevicesList[0].MonikerString;
                    mUsedCameraName = videoDevicesList[0].Name;
                }
                mConnected = true;
            } else {
                Program.logError("No video sources found");
                return;
            }

            videoSource = new VideoCaptureDevice(mMoniker);

            int exposure;
            CameraControlFlags flags;
            videoSource.GetCameraProperty(CameraControlProperty.Exposure, out exposure, out flags);
            Program.logInfo("QRreader: video property before settings is \"Exposure value\" = " + exposure + ", \"Control flag\" = " + flags.ToString());

            videoSource.SetCameraProperty(CameraControlProperty.Exposure, newExposure, CameraControlFlags.Manual);

            videoSource.GetCameraProperty(CameraControlProperty.Exposure, out exposure, out flags);
            Program.logInfo("QRreader: video property after new settings is \"Exposure value\" = " + exposure + ", \"Control flag\" = " + flags.ToString());

            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
        }


        public void stopCam() {
            if (mTimer != null) {
                mTimer.Stop();
            }

            // signal to stop
            if (videoSource != null && videoSource.IsRunning) {
                videoSource.SignalToStop();
                videoSource.Stop();
            }
        }

        public void startCam() {
            mPictureBox.Image = null;
            mPictureBox.Invalidate();
            videoSource.Start();

            mTimer = new Timer();
            mTimer.Interval = DELAY;
            mTimer.Tick += (s, e) => {
                if (mPictureBox.Image == null) {
                    Program.logError("No image!");
                    return;
                }
                decode_QRtag((Bitmap) mPictureBox.Image);
            };
            mTimer.Start();
        }

        public String getQrTag() {
            return mQrTag;
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs) {
            Bitmap bitmap = (Bitmap) eventArgs.Frame.Clone();
            mPictureBox.Image = bitmap;
        }

        private void btnStop_Click(object sender, EventArgs e) {
            videoSource.SignalToStop();
            if (videoSource != null && videoSource.IsRunning && mPictureBox.Image != null) {
                mPictureBox.Image.Dispose();
            }
        }

        private void decode_QRtag(Bitmap bitmap) {
            try {
                BarcodeReader reader = new BarcodeReader { AutoRotate = true };
                Result result = reader.Decode(bitmap);
                string decoded = result.ToString().Trim();
                //capture a snapshot if there is a match
                mPictureBox.Image = bitmap;
                Program.logInfo("new QR: " + decoded);
                stopCam();
                Program.form.successQrCam(decoded);
            } catch {
                //Program.logInfo("decode_QRtag exception");
            }
        }
    }
}