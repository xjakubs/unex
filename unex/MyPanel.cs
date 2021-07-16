using System.Drawing;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class MyPanel : Panel {

        private Color colorBorder = Color.Transparent;
        private readonly int BORDER_THICK = 5;

        Pen penBorder;
        Rectangle rectBorder;

        int thickness;
        int halfThickness;

        public MyPanel() : base() {
            this.SetStyle(ControlStyles.UserPaint, true);

            thickness = BORDER_THICK;
            halfThickness = thickness / 2;

            penBorder = new Pen(colorBorder, BORDER_THICK);
        }

        protected override void OnPaint(PaintEventArgs e) {
            rectBorder = new Rectangle(halfThickness,
                                       halfThickness,
                                       this.ClientSize.Width - thickness,
                                       this.ClientSize.Height - thickness);
            e.Graphics.DrawRectangle(penBorder, rectBorder);
            base.OnPaint(e);
        }

        public Color BorderColor {
            get {
                return colorBorder;
            }
            set {
                colorBorder = value;
                penBorder.Color = value;
                Invalidate();
            }
        }

        public void setBorderMyColor(Color color) {
            BorderColor = color;
        }

    }
}
