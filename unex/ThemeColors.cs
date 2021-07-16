using System;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {

    public enum Colors { BLUE = 0, ORANGE, RED, PURPLE, GREEN, BLACK, WHITE, END };

    public static class ThemeColors {

        private const String pathToTheme = "/common/themes/";

        private static MyColor mBlue = new MyColor("blue");
        private static MyColor mOrange = new MyColor("orange");
        private static MyColor mRed = new MyColor("red");
        private static MyColor mPurple = new MyColor("purple");
        private static MyColor mGreen = new MyColor("green");
        private static MyColor mBlack = new MyColor("black");
        private static MyColor mWhite = new MyColor("white");

        public static MyColor BLUE { get => mBlue; }
        public static MyColor ORANGE { get => mOrange; }
        public static MyColor RED { get => mRed; }
        public static MyColor PURPLE { get => mPurple; }
        public static MyColor GREEN { get => mGreen; }
        public static MyColor BLACK { get => mBlack; }
        public static MyColor WHITE { get => mWhite; }

        public static MyColor getTheme(Colors eNum) {
            MyColor theme;

            switch (eNum) {
                case Colors.BLACK: theme = BLACK; break;
                case Colors.BLUE: theme = BLUE; break;
                case Colors.GREEN: theme = GREEN; break;
                case Colors.ORANGE: theme = ORANGE; break;
                case Colors.PURPLE: theme = PURPLE; break;
                case Colors.RED: theme = RED; break;
                case Colors.WHITE: theme = WHITE; break;
                default: theme = WHITE; break;
            }
            return theme;
        }

        public static MyColor getTheme(String color) {
            MyColor theme = WHITE;

            if (String.Compare(BLACK.getName(), color) == 0) { return BLACK; }
            if (String.Compare(BLUE.getName(), color) == 0) { return BLUE; }
            if (String.Compare(GREEN.getName(), color) == 0) { return GREEN; }
            if (String.Compare(ORANGE.getName(), color) == 0) { return ORANGE; }
            if (String.Compare(PURPLE.getName(), color) == 0) { return PURPLE; }
            if (String.Compare(RED.getName(), color) == 0) { return RED; }
            if (String.Compare(WHITE.getName(), color) == 0) { return WHITE; }

            return theme;
        }

        public static String getHtmlThemePath(String basePath, MyColor color) {
            String path = basePath.Replace("/index.html", "");
            path = path.Replace("/index.php", "");
            path = path + pathToTheme + color.getName() + ".js";
            return path;
        }

    }

}
