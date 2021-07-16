using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {

    public class Theme {

        private List<ThemeItemList> itemList = new List<ThemeItemList>();

        private MyColor mCurrentColor;
        public MyColor currentColor { get => mCurrentColor; set => mCurrentColor = value; }

        public Theme(MyColor theme) {
            mCurrentColor = theme;
        }

        public bool add(Object pn) {
            itemList.Add(new ThemeItemList(pn, 0));
            return true;
        }

        public bool add(Object pn, String typeColor) {
            itemList.Add(new ThemeItemList(pn, typeColor));
            return true;
        }

        public bool add(Object pn, int alpha, String typeColor) {
            itemList.Add(new ThemeItemList(pn, alpha, typeColor));
            return true;
        }

        public void changeColor(MyColor color) {
            foreach (var item in itemList) {
                if (item.Obj is Label l) {
                    l.ForeColor = color.getBaseColor();
                } else if (item.Obj is SvgPictureBox spb) {
                    spb.setColor(color.findColor(item.TypeColor));
                } else if (item.Obj is MyPanel mp) {
                    if (String.Compare(item.TypeColor, MyColor.BASE) == 0) {
                        mp.setBorderMyColor(color.getBaseColor());
                    }
                } else if (item.Obj is Panel p) {
                    if (String.Compare(item.TypeColor, MyColor.BACKGROUND) == 0) {
                        p.BackColor = Color.FromArgb(item.Alpha, color.getBaseColor());
                    }
                }
            }
        }

    }

}
