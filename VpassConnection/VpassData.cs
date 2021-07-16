using System;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace VpassConnection {
    public class VpassData {

        private String code;
        private String content;
        private VpassType.TYPE type;

        public VpassData(String codeName, String content, VpassType.TYPE fileType) {
            code = codeName;
            this.content = content;
            type = fileType;
        }

        public String Code { get => code; }
        public String Content { get => content; }
        public VpassType.TYPE Type { get => type; }
    }
}
