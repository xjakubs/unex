using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using xjakubs.Logger;

namespace UnExponat {

    public static class WebSocketClient {

        private static ClientWebSocket Socket;
        private static BlockingCollection<string> MessagesQueue = new BlockingCollection<string>();
        private static CancellationTokenSource SocketLoopTokenSource;
        private static CancellationTokenSource KeystrokeLoopTokenSource;

        private static string REQUEST = "req://";

        public static Logs mLogs;
        private const String LOG_TAG = "[WEBSOCKET]";

        public static void setLogs(Logs logs, String tag) {
            mLogs = logs;
        }

        public static async Task StartAsync(string uri) {
            await StartAsync(new Uri(uri));
        }

        public static async Task StartAsync(Uri uri) {
            mLogs.logInfo(LOG_TAG + " Connecting to server: " + uri.ToString());

            SocketLoopTokenSource = new CancellationTokenSource();
            KeystrokeLoopTokenSource = new CancellationTokenSource();

            try {
                Socket = new ClientWebSocket();
                await Socket.ConnectAsync(uri, CancellationToken.None);
                _ = Task.Run(() => SocketProcessingLoopAsync().ConfigureAwait(false));
                _ = Task.Run(() => MessageTransmitLoopAsync().ConfigureAwait(false));
            } catch (OperationCanceledException) {
                mLogs.logError(LOG_TAG + " StartAsync() catch: OperationCanceledException");
                // normal upon task/token cancellation, disregard
            }
        }

        public static async Task StopAsync() {
            mLogs.logInfo(LOG_TAG + " Closing connection");
            KeystrokeLoopTokenSource.Cancel();
            if (Socket == null || Socket.State != WebSocketState.Open) {
                return;
            }
            var timeout = new CancellationTokenSource(WebSocket.CLOSE_SOCKET_TIMEOUT_MS);
            try {
                await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Closing", timeout.Token);
                while (Socket.State != WebSocketState.Closed && !timeout.Token.IsCancellationRequested) ;
            } catch (OperationCanceledException) {
                // normal upon task/token cancellation, disregard
                mLogs.logError(LOG_TAG + " StopAsync() catch: OperationCanceledException");
            }
            SocketLoopTokenSource.Cancel();
        }

        public static WebSocketState State {
            get {
                return Socket?.State ?? WebSocketState.None;
            }
        }

        public static void QueueKeystroke(string message) {
            MessagesQueue.Add(message);
        }

        private static async Task SocketProcessingLoopAsync() {
            var cancellationToken = SocketLoopTokenSource.Token;
            try {
                var buffer = System.Net.WebSockets.WebSocket.CreateClientBuffer(4096, 4096);
                while (Socket.State != WebSocketState.Closed && !cancellationToken.IsCancellationRequested) {
                    var receiveResult = await Socket.ReceiveAsync(buffer, cancellationToken);
                    // TODO RECEIVED
                    // req://{type: "newUser", data: {data1: "Martin", data2: "trter"}}
                    string message = Encoding.UTF8.GetString(buffer.Array, 0, receiveResult.Count);
                    if (message.Contains(REQUEST) && message.IndexOf(REQUEST) == 0) {
                        message = message.Replace(REQUEST, "");

                        // if the token is cancelled while ReceiveAsync is blocking, the socket state changes to aborted and it can't be used
                        if (!cancellationToken.IsCancellationRequested) {
                            // the server is notifying us that the connection will close; send acknowledgement
                            if (Socket.State == WebSocketState.CloseReceived && receiveResult.MessageType == WebSocketMessageType.Close) {
                                mLogs.logInfo(LOG_TAG + " Acknowledging Close frame received from server");
                                KeystrokeLoopTokenSource.Cancel();
                                await Socket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, "Acknowledge Close frame", CancellationToken.None);
                            }

                            // display text or binary data
                            if (Socket.State == WebSocketState.Open && receiveResult.MessageType != WebSocketMessageType.Close) {
                                if (message.Length > 1) {
                                    filterMessage(message);
                                }
                            }
                        }
                    }
                }
                mLogs.logInfo(LOG_TAG + " Ending processing loop in state " + Socket.State);
            } catch (OperationCanceledException) {
                // normal upon task/token cancellation, disregard
                mLogs.logError(LOG_TAG + " SocketProcessingLoopAsync() catch: OperationCanceledException");
            } catch (Exception ex) {
                mLogs.logError(LOG_TAG + " SocketProcessingLoopAsync() catch: " + ex);
            } finally {
                KeystrokeLoopTokenSource.Cancel();
                Socket.Dispose();
                Socket = null;
            }
        }

        private static void filterMessage(String msg) {
            mLogs.logInfo(LOG_TAG + " Message: \n" + msg);

            //req://{ 'type': 'show_notification', 'data': { 'msg_text': 'nejaka notifikace', 'msg_long': '60'} }
            //req://{ 'type': 'presentation', 'data': { 'url': 'nejaka url adresa'} }
            //req://{ 'type': 'settings' }

            JsonMessage jsonMessage;
            try {
                jsonMessage = JsonConvert.DeserializeObject<JsonMessage>(msg);
                string type = jsonMessage.Type;
                if (type.CompareTo(JsonMessage.NOTIFICATION) == 0) {
                    string msg_text = jsonMessage.Data.Msg_text;
                    string msg_long = jsonMessage.Data.Msg_long;
                    int msg_long_i = int.Parse(msg_long);
                    if (msg_long_i <= 0) {
                        msg_long_i = 0;
                    }
                    Program.showSocketMessage(msg_text, msg_long_i);
                } else if (type.CompareTo(JsonMessage.PRESENTATION) == 0) {
                    string url = jsonMessage.Data.Url;
                    Program.loadPresentationWeb(url);
                } else if (type.CompareTo(JsonMessage.SETTINGS) == 0) {
                    sendSettings();
                }
            } catch (JsonException ex) {
                Program.logError(ex.Message);
            }

        }

        private static void sendSettings() {
            StringBuilder msg = new StringBuilder("res://{'type': 'settings', 'data': {");

            foreach(SettingObject so in Program.getSettings().lSettingObjects) {
                msg.Append("'");
                msg.Append(so.Name);
                msg.Append("': '");
                msg.Append(so.Value);
                msg.Append("', ");
            }
            msg.Append("'':''}}");
            String msgFinal = msg.ToString().Replace(", '':''", "");
            mLogs.logInfo(msgFinal.ToString());
            QueueKeystroke(msgFinal.ToString());
        }

        private static async Task MessageTransmitLoopAsync() {
            var cancellationToken = KeystrokeLoopTokenSource.Token;
            while (!cancellationToken.IsCancellationRequested) {
                try {
                    await Task.Delay(WebSocket.KEYSTROKE_TRANSMIT_INTERVAL_MS, cancellationToken);
                    if (!cancellationToken.IsCancellationRequested && MessagesQueue.TryTake(out var message)) {
                        var msgbuf = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
                        await Socket.SendAsync(msgbuf, WebSocketMessageType.Text, endOfMessage: true, CancellationToken.None);
                    }
                } catch (OperationCanceledException) {
                    // normal upon task/token cancellation, disregard
                    mLogs.logError(LOG_TAG + " MessageTransmitLoopAsync() catch: OperationCanceledException");
                } catch (Exception ex) {
                    mLogs.logError(LOG_TAG + " MessageTransmitLoopAsync() catch: KeystrokeTransmitLoopAsync: " + ex);
                }
            }
        }
    }
}

