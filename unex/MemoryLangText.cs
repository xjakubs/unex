using System;

/**
* Universal program for VIDA! science centrum for exhibits
*
* @author Jakub Smid (xjakubs@gmail.com)
*/
namespace UnExponat {
    class MemoryLangText {
        String csText;
        String enText;
        String deText;

        public MemoryLangText(String cs, String en, String de) {
            csText = cs;
            enText = en;
            deText = de;
        }

        public string CZ { get => csText; }
        public string EN { get => enText; }
        public string DE { get => deText; }
    }
}
