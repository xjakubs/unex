using System;
using System.Drawing;
using System.IO;
using System.Reflection;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {

    public class MyColor {

        private const String mPathToLocalTheme = "UnExponat.Resources.themes.";
        private const String mSuffix = ".js";
        private const String mPathToWebTheme = "/common/themes/";

        public const String NAME = "name";
        public const String BACKGROUND = "background";
        public const String BASE = "base";
        public const String COMPLEMENTARY = "complementary";
        public const String ANALOGOUS1 = "analogous1";
        public const String ANALOGOUS2 = "analogous2";
        public const String MONODARKER1 = "monoDarker1";
        public const String MONODARKER2 = "monoDarker2";
        public const String MONODARKER3 = "monoDarker3";
        public const String MONOLIGHTER1 = "monoLighter1";
        public const String MONOLIGHTER2 = "monoLighter2";
        public const String MONOLIGHTER3 = "monoLighter3";

        private String name;
        private String background;
        private String baseColor;
        private String complementary;
        private String analogous1;
        private String analogous2;
        private String monoDarker1;
        private String monoDarker2;
        private String monoDarker3;
        private String monoLighter1;
        private String monoLighter2;
        private String monoLighter3;

        private String mColorName;

        public MyColor(String sourceName) {
            mColorName = sourceName;
            readLocalFile();
        }

        private bool readLocalFile() {
            var assembly = Assembly.GetExecutingAssembly();
            Stream stream = assembly.GetManifestResourceStream(mPathToLocalTheme + mColorName + mSuffix);
            String line;

            try {
                StreamReader reader = new StreamReader(stream);
                while ((line = reader.ReadLine()) != null) {
                    if (checkString(line, NAME, ref name)) { continue; }
                    if (checkString(line, BACKGROUND, ref background)) { continue; }
                    if (checkString(line, BASE, ref baseColor)) { continue; }
                    if (checkString(line, COMPLEMENTARY, ref complementary)) { continue; }
                    if (checkString(line, ANALOGOUS1, ref analogous1)) { continue; }
                    if (checkString(line, ANALOGOUS2, ref analogous2)) { continue; }
                    if (checkString(line, MONODARKER1, ref monoDarker1)) { continue; }
                    if (checkString(line, MONODARKER2, ref monoDarker2)) { continue; }
                    if (checkString(line, MONODARKER3, ref monoDarker3)) { continue; }
                    if (checkString(line, MONOLIGHTER1, ref monoLighter1)) { continue; }
                    if (checkString(line, MONOLIGHTER2, ref monoLighter2)) { continue; }
                    if (checkString(line, MONOLIGHTER3, ref monoLighter3)) { continue; }
                }
            } catch (Exception ex) {
                Program.logError("readLocalFile() exception: " + ex);
                return false;
            }

            return checkValues();
        }

        private bool checkValues() {
            if (string.IsNullOrEmpty(name)) { return false; }
            if (string.IsNullOrEmpty(background)) { return false; }
            if (string.IsNullOrEmpty(baseColor)) { return false; }
            if (string.IsNullOrEmpty(complementary)) { return false; }
            if (string.IsNullOrEmpty(analogous1)) { return false; }
            if (string.IsNullOrEmpty(analogous2)) { return false; }
            if (string.IsNullOrEmpty(monoDarker1)) { return false; }
            if (string.IsNullOrEmpty(monoDarker2)) { return false; }
            if (string.IsNullOrEmpty(monoDarker3)) { return false; }
            if (string.IsNullOrEmpty(monoLighter1)) { return false; }
            if (string.IsNullOrEmpty(monoLighter2)) { return false; }
            if (string.IsNullOrEmpty(monoLighter3)) { return false; }

            return true;
        }

        private bool checkString(String line, String constant, ref String value) {
            if (line.Contains(constant)) {
                value = line.Replace(" ", "");
                value = value.Replace(constant, "");
                value = value.Replace("\"", "");
                value = value.Replace("=", "");
                value = value.Replace(",", "");
                return true;
            }
            return false;
        }

        public Color findColor(String type) {
            if (String.Compare(type, BACKGROUND) == 0) return getBackground();
            if (String.Compare(type, BASE) == 0) return getBaseColor();
            if (String.Compare(type, COMPLEMENTARY) == 0) return getComplementary();
            if (String.Compare(type, ANALOGOUS1) == 0) return getAnalogous1();
            if (String.Compare(type, ANALOGOUS2) == 0) return getAnalogous2();
            if (String.Compare(type, MONODARKER1) == 0) return getMonoDarker1();
            if (String.Compare(type, MONODARKER2) == 0) return getMonoDarker2();
            if (String.Compare(type, MONODARKER3) == 0) return getMonoDarker3();
            if (String.Compare(type, MONOLIGHTER1) == 0) return getMonoLighter1();
            if (String.Compare(type, MONOLIGHTER2) == 0) return getMonoLighter2();
            if (String.Compare(type, MONOLIGHTER3) == 0) return getMonoLighter3();

            Program.logError("getColor() no match with: " + type);
            return Color.White;
        }

        public String getName() { return name; }
        public String getBackgroundHex() { return background; }
        public Color getBackground() { return ColorTranslator.FromHtml(background); }
        public String getBaseColorHex() { return baseColor; }
        public Color getBaseColor() { return ColorTranslator.FromHtml(baseColor); }
        public String getComplementaryHex() { return complementary; }
        public Color getComplementary() { return ColorTranslator.FromHtml(complementary); }
        public String getAnalogous1Hex() { return analogous1; }
        public Color getAnalogous1() { return ColorTranslator.FromHtml(analogous1); }
        public String getAnalogous2Hex() { return analogous2; }
        public Color getAnalogous2() { return ColorTranslator.FromHtml(analogous2); }
        public String getMonoDarker1Hex() { return monoDarker1; }
        public Color getMonoDarker1() { return ColorTranslator.FromHtml(monoDarker1); }
        public String getMonoDarker2Hex() { return monoDarker2; }
        public Color getMonoDarker2() { return ColorTranslator.FromHtml(monoDarker2); }
        public String getMonoDarker3Hex() { return monoDarker3; }
        public Color getMonoDarker3() { return ColorTranslator.FromHtml(monoDarker3); }
        public String getMonoLighter1Hex() { return monoLighter1; }
        public Color getMonoLighter1() { return ColorTranslator.FromHtml(monoLighter1); }
        public String getMonoLighter2Hex() { return monoLighter2; }
        public Color getMonoLighter2() { return ColorTranslator.FromHtml(monoLighter2); }
        public String getMonoLighter3Hex() { return monoLighter3; }
        public Color getMonoLighter3() { return ColorTranslator.FromHtml(monoLighter3); }

    }


}
