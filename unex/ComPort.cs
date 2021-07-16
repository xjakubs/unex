using System;
using System.IO.Ports;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class ComPort {
        private const int DELAY = 15 * 1000; // 15s

        private SerialPort mSerialPort;
        private bool mRead = false;
        private String mPortDefault = "";
        private String mPort = "";
        private bool mInitialized = false;
        private Timer mTimer;

        public ComPort(String port) {
            if (port.Length > 0) {
                mPortDefault = port;
                init(true);

                mTimer = new Timer();
                mTimer.Interval = DELAY;
                mTimer.Tick += (s, e) => {
                    init(false);
                };
                mTimer.Start();
            }
        }

        private void init(bool first) {
            if (getAllPorts()) {
                try {
                    if (first == false) {
                        if (mSerialPort != null) {
                            if (String.Compare(mSerialPort.PortName, mPort) == 0) {
                                if (mSerialPort.IsOpen) {
                                    return;
                                } else {
                                    mSerialPort.Open();
                                    return;
                                }
                            } else {
                                mSerialPort.Close();
                            }
                        }
                    }

                    mSerialPort = new SerialPort(mPort, 9600, Parity.None, 8, StopBits.One);
                    //_serialPort.Handshake = Handshake.None;
                    //_serialPort.WriteTimeout = 500;
                    mSerialPort.DataReceived += new SerialDataReceivedEventHandler(dataReceived);
                    mSerialPort.Open();
                    mInitialized = true;
                } catch (Exception e) {
                    mInitialized = false;
                    Program.logError("ComPort: " + e.ToString());
                }
            } else {
                mInitialized = false;
            }
        }

        private void dataReceived(object sender, SerialDataReceivedEventArgs e) {
            //Thread.Sleep(500);
            string data = mSerialPort.ReadLine();
            if (mRead) {
                Program.sendComMessage(data.Trim());
                Program.logInfo("ComPort message: " + data.Trim());
            }
        }

        private bool getAllPorts() {
            // Get a list of serial port names.
            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0) {
                Program.logInfo("ComPort: No COM ports!");
                return false;
            }

            foreach (string port in ports) {
                // the default COM port has priority, if it's available
                if (String.Compare(port, mPortDefault) == 0) {
                    mPort = mPortDefault;
                    Program.logInfo("ComPort: Default COM port available: " + mPort);
                    return true;
                }

                // last connected COM port
                mPort = port;
            }

            Program.logInfo("ComPort: COM port for connecting: " + mPort);
            return true;
        }

        public void setRead(bool enable) {
            mRead = enable;
        }

        public void sendData(String data) {
            if (mPortDefault.Length > 0 && mInitialized) {
                try {
                    if (mSerialPort != null && !mSerialPort.IsOpen) {
                        mSerialPort.Open();
                    }
                    mSerialPort.Write(data);
                } catch (Exception e) {
                    Program.logError("ComPort-sendData(): " + e.ToString());
                }
            }
        }
    }
}
