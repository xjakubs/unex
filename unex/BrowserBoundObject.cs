using System;
using CefSharp;
using CefSharp.WinForms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class BrowserBoundObject {

        public delegate void ItemClickedEventHandler(object sender, HtmlItemClickedEventArgs e);
        public event ItemClickedEventHandler HtmlItemClicked;

        public ChromiumWebBrowser browser;

        public BrowserBoundObject(ChromiumWebBrowser br) {
            browser = br;
        }

        public void OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e) {
            if (e.Frame.IsMain) {
                try {
                    browser.EvaluateScriptAsync(@"window.onclick = function(e) { e.preventDefault(); bound.onClicked(e.target.outerHTML); }");
                } catch (Exception ex) {
                    Program.logError("BrowserBoundObject: " + ex);
                }
            }
        }

        public void OnClicked(string id) {
            if (HtmlItemClicked != null) {
                HtmlItemClicked(this, new HtmlItemClickedEventArgs() { Id = id });
            }
        }
    }

    public class HtmlItemClickedEventArgs : EventArgs {
        public string Id { get; set; }
    }
}