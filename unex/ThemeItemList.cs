using System;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {

    public class ThemeItemList {
        private Object obj;
        private int alpha = 255;
        private String typeColor = MyColor.BASE;

        public ThemeItemList(Object pn) {
            obj = pn;
        }

        public ThemeItemList(Object pn, int a) {
            obj = pn;
            alpha = a;
        }

        public ThemeItemList(Object pn, String type) {
            obj = pn;
            typeColor = type;
        }

        public ThemeItemList(Object pn, int a, String type) {
            obj = pn;
            alpha = a;
            typeColor = type;
        }

        public Object Obj { get => obj; }
        public int Alpha { get => alpha; }
        public String TypeColor { get => typeColor; }
    }
}
