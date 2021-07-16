using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xjakubs.Logger;

namespace UnExponat {

    public class WebSocket {

        private const String LOG_TAG = "[WEBSOCKET]";

        public const int KEYSTROKE_TRANSMIT_INTERVAL_MS = 100;
        public const int CLOSE_SOCKET_TIMEOUT_MS = 10000;
        private const int CRASH_COUNT = 5;

        private bool isRunning;
        private bool isStopping;
        public static Logs mLogs;
        private string mExponatId;
        private int mVpassId;

        private int crashCount = 0;

        public WebSocket(Logs log, string exponatId, int vpassId) {
            mLogs = log;
            mExponatId = exponatId;
            mVpassId = vpassId;
            WebSocketClient.setLogs(mLogs, LOG_TAG);            
        }

        public void Start() {
            Run();
        }

        public async Task Run() {
            if (isRunning) {
                return;
            }

            isRunning = true;

            if (isRunning) {
                while (!isStopping) {

                    try {
                        await WebSocketClient.StartAsync(@"ws://localhost:8080/");
                        bool running = true;
                        while (running && WebSocketClient.State == WebSocketState.Open) {
                            Thread.Sleep(5000);
                            StringBuilder msg = new StringBuilder("res://{'type': 'info', 'data': {");
                            msg.Append("'time': '");
                            msg.Append(getTimestamp(DateTime.Now));
                            msg.Append("', 'version': '");
                            msg.Append(Program.getVersion());
                            msg.Append("', 'exponatID': '");
                            msg.Append(mExponatId);
                            msg.Append("', 'vpassID': '");
                            msg.Append(mVpassId.ToString());
                            msg.Append("'}}");
                            WebSocketClient.QueueKeystroke(msg.ToString());
                        }
                        await WebSocketClient.StopAsync();
                        crashCount = 0;
                    } catch (OperationCanceledException) {
                        // normal upon task/token cancellation, disregard
                        mLogs.logError(LOG_TAG + " Run() catch: OperationCanceledException");
                        crashCount++;
                    } catch (Exception ex) {
                        mLogs.logError(LOG_TAG + " Run() catch: " + ex);
                        crashCount++;
                    }

                    if(crashCount >= CRASH_COUNT) {
                        mLogs.logInfo(LOG_TAG + " WebSocket.Run failed! WebSocket stops!");
                        Stop();
                        return;
                    }

                    isStopping = false;
                }
            }
        }

        public void Stop() {
            if (!isRunning || isStopping) {
                return;
            }
            isStopping = true;
            isRunning = false;
            WebSocketClient.StopAsync();
        }

        public void Send(String msg) {
            WebSocketClient.QueueKeystroke(msg);
        }

        private String getTimestamp(DateTime value) {
            return value.ToString("yyyy.MM.dd HH:mm:ss");
        }
    }
}

