using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Svg;
using System;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class SvgPictureBox : PictureBox {

        private Color color;
        private string name;
        private SvgDocument svgDoc;
        private int height;
        private int width;
        private bool noOnClick = false;
        private bool pressed = false;
        private bool enabled = true;

        public SvgPictureBox(string name) : base() {
            this.name = name;
            this.height = 90;
            this.width = 90;

            openSvg();
        }

        public SvgPictureBox(string name, int height, int width) : base() {
            this.name = name;
            this.height = height;
            this.width = width;

            openSvg();
        }

        protected override void OnClick(EventArgs e) {
            if (enabled) {
                if (!noOnClick) {
                    colorPressed();
                    var t = new Timer();
                    t.Interval = 200;
                    t.Tick += (s, ex) => {
                        colorNormal();
                        t.Stop();
                    };
                    t.Start();
                }
                base.OnClick(e);
            }
        }

        public void setNoOnClick(bool value) {
            noOnClick = value;
        }

        public void setSize(int height, int width) {
            this.height = height;
            this.width = width;
            changeSize();
        }

        public void setColor(Color color) {
            this.color = color;

            if (noOnClick && pressed) {
                colorPressed();
            } else {
                colorNormal();
            }
        }

        private void openSvg() {
            openSvg(name);
        }

        private MemoryStream getMemoryStream(String name) {
            object resource = Resources_Image_Svg.ResourceManager.GetObject(name);

            if (resource is byte[]) {
                return new MemoryStream((byte[]) resource);
            } else {
                throw new InvalidCastException("The specified resource is not a binary resource.");
            }
        }

        private void openSvg(String name) {
            if (name == "") {
                return;
            }

            svgDoc = SvgDocument.Open<SvgDocument>((getMemoryStream(name)));
            svgDoc.Height = height;
            svgDoc.Width = width;
        }

        private void changeSize() {
            if (svgDoc != null) {
                svgDoc.Height = height;
                svgDoc.Width = width;
            }
        }

        private void replaceColor(SvgDocument doc, Color replaceColor, int opacity) {
            doc.Stroke = new SvgColourServer(replaceColor);
            doc.Fill = new SvgColourServer(replaceColor);
            doc.FillOpacity = opacity;

            Image = doc.Draw();
            Invalidate();
        }

        public void colorPressed() {
            if (enabled) {
                pressed = true;
                replaceColor(svgDoc, Color.FromArgb(color.R, color.G, color.B), 1);
            }
        }

        public void colorNormal() {
            if (enabled) {
                pressed = false;
                replaceColor(svgDoc, Color.FromArgb(color.R, color.G, color.B), 0);
            }
        }

        public void disable() {
            enabled = false;
            replaceColor(svgDoc, Color.FromArgb(50, color.R, color.G, color.B), 0);
        }

        public void enable() {
            enabled = true;
            colorNormal();
        }
    }
}
