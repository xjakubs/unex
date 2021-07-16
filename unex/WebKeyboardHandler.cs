using CefSharp;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class WebKeyboardHandler : IKeyboardHandler {

        public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut) {
            return false;
        }

        public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey) {
            const int WM_KEY_F2 = 113;
            const int WM_KEY_F5 = 116;

            switch (type) {
                case KeyType.RawKeyDown:
                    if (windowsKeyCode == WM_KEY_F5) {
                        browser.ShowDevTools();
                        return true;
                    }
                    if (windowsKeyCode == WM_KEY_F2) {
                        Program.setOnTop(false);
                        return true;
                    }
                    break;
            }

            return false;
        }
    }
}
