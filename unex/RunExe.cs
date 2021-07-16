using System;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Timer = System.Windows.Forms.Timer;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class RunExe {

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(int hWnd, int Msg, int wParam, ref COPYDATASTRUCT lParam);

        private const int WM_COPYDATA = 0x4A;

        public struct COPYDATASTRUCT {
            public int cbData;
            public IntPtr dwData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }

        private enum WindowBehavior {
            FORCEMINIMIZE = 11,
            HIDE = 0,
            MAXIMIZE = 3,
            MINIMIZE = 6,
            RESTORE = 9,
            SHOW = 5,
            SHOWDEFAULT = 10,
            SHOWMAXIMIZED = 3,
            SHOWMINIMIZED = 2,
            SHOWMINNOACTIVE = 7,
            SHOWNA = 8,
            SHOWNOACTIVATE = 4,
            SHOWNORMAL = 1,
        }

        private enum SpecialWindowHandles {
            ///     Places the window at the top of the Z order.
            HWND_TOP = 0,
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            HWND_BOTTOM = 1,
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            HWND_TOPMOST = -1,
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            HWND_NOTOPMOST = -2
        }

        [Flags]
        private enum SetWindowPosFlags : uint {
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            SWP_ASYNCWINDOWPOS = 0x4000,
            ///     Prevents generation of the WM_SYNCPAINT message.
            SWP_DEFERERASE = 0x2000,
            ///     Draws a frame (defined in the window's class description) around the window.
            SWP_DRAWFRAME = 0x0020,
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            SWP_FRAMECHANGED = 0x0020,
            ///     Hides the window.
            SWP_HIDEWINDOW = 0x0080,
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            SWP_NOACTIVATE = 0x0010,
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            SWP_NOCOPYBITS = 0x0100,
            ///     Retains the current position (ignores X and Y parameters).
            SWP_NOMOVE = 0x0002,
            ///     Does not change the owner window's position in the Z order.
            SWP_NOOWNERZORDER = 0x0200,
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            SWP_NOREDRAW = 0x0008,
            ///     Same as the SWP_NOOWNERZORDER flag.
            SWP_NOREPOSITION = 0x0200,
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            SWP_NOSENDCHANGING = 0x0400,
            ///     Retains the current size (ignores the cx and cy parameters).
            SWP_NOSIZE = 0x0001,
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            SWP_NOZORDER = 0x0004,
            ///     Displays the window.
            SWP_SHOWWINDOW = 0x0040,
        }

        private String mProcessName = "";
        private String mExePath = "";
        private Process mProcess = null;
        IntPtr hWndProcess = IntPtr.Zero;
        private bool initialized = false;
        private Timer mTimer;
        private const int DELAY = 250;

        public RunExe(String path) {
            if (path.Length > 0) {
                mExePath = path;
                mProcessName = Regex.Replace(path, @".*\\", "");
                mProcessName = mProcessName.Replace(".exe", "");
                initialized = true;

                Program.logInfo("RunExe: mProcessName = " + mProcessName);

                mTimer = new Timer();
                mTimer.Interval = DELAY;
                mTimer.Tick += (s, e) => {
                    checkForeground();
                };
            }
        }

        // RUN
        public void run() {
            if(!initialized) {
                return;
            }
            if ((mProcess == null || mProcess.HasExited) && File.Exists(mExePath)) {
                mProcess = new Process();
                mProcess.StartInfo.FileName = mExePath;
                mProcess.StartInfo.CreateNoWindow = true;
                mProcess.Start();

                hWndProcess = mProcess.MainWindowHandle;
                while (hWndProcess == IntPtr.Zero) {
                    Thread.Sleep(500);
                    hWndProcess = mProcess.MainWindowHandle;
                    SetWindowPos(mProcess.MainWindowHandle, (IntPtr) SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);
                }
                Program.logInfo("RunExe: run() => hWndProcess = " + hWndProcess.ToString());
            }
        }

        // KILL 
        public void kill() {
            if (!initialized) {
                return;
            }
            if (mProcess != null && !mProcess.HasExited) {
                mProcess.Kill();
            }
            mTimer.Stop();
        }

        // CHECK PROCESS
        public bool check(bool init) {
            if(init) {
                return initialized;
            } else {
                return check();
            }
        }

        public bool check() {
            if (!initialized) {
                return false;
            }
            Process[] pname = Process.GetProcessesByName(mProcessName);
            if (pname.Length == 0) {
                Program.logInfo("RunExe: check() => Process doesn't exist!");
                return false;
            } else {
                Program.logInfo("RunExe: check() => Process exists!");
                return true;
            }
        }

        // CHECK FOREGROUND PROCESS
        public static Process getForegroundProcess() {
            uint processID = 0;
            IntPtr hWnd = GetForegroundWindow();
            uint threadID = GetWindowThreadProcessId(hWnd, out processID);
            Process fgProc = Process.GetProcessById(Convert.ToInt32(processID));
            return fgProc;
        }

        private void checkForeground() {
            Process procForeground = getForegroundProcess();
            if (procForeground != null && mProcess != null && !mProcess.HasExited) {
                if (String.Compare(procForeground.ProcessName, mProcessName) != 0) {
                    SetForegroundWindow(mProcess.MainWindowHandle);
                }
            }
        }

        // MOVE WINDOW
        public void showWindow() {
            if (!initialized) {
                return;
            }
            Program.logInfo("RunExe: shoWindow()");
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning) {
                if (pr.ProcessName == mProcessName) {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, (int) WindowBehavior.RESTORE);
                }
            }
            mTimer.Start();
        }

        public void hideWindow() {
            if (!initialized) {
                return;
            }
            Program.logInfo("RunExe: hideWindow()");
            int hWnd;
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning) {
                if (pr.ProcessName == mProcessName) {
                    hWnd = pr.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, (int) WindowBehavior.MINIMIZE);
                }
            }
            mTimer.Stop();
        }

        // SEND MESSAGE
        public void sendMessage(string text) {
            if (!initialized) {
                return;
            }
            Program.logInfo("RunExe: sendMessage() - msg = " + text);
            Process[] processRunning = Process.GetProcesses();
            foreach (Process pr in processRunning) {
                if (pr.ProcessName == mProcessName) {
                    byte[] sarr = System.Text.Encoding.Default.GetBytes(text);
                    int len = sarr.Length;
                    COPYDATASTRUCT cds;
                    cds.dwData = (IntPtr) 100;
                    cds.lpData = text;
                    cds.cbData = len + 1;
                    SendMessage(pr.MainWindowHandle.ToInt32(), WM_COPYDATA, 0, ref cds);
                }
            }
        }
    }
}
