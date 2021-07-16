using System;
using System.Collections.Generic;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    public class JsonMap {
        /**
        * ID : {"exponat_id":{"name":{"CS":"text","EN":"text","DE":"text"},"howto":{"CS":"text","EN":"text","DE":"text"},"info":{"CS":"text","EN":"text","DE":"text"},"tips":{"CS":"text","EN":"text","DE":"text"}}}
        */

        private Dictionary<String, IdBean> id;

        public Dictionary<string, IdBean> Id { get => id; set => id = value; }
    }

    public class IdBean {
        /**
         * name : {"CS":"text1","EN":"text2","DE":"text3"}
         * howto : {"CS":"text1","EN":"text2","DE":"text3"}
         * info : {"CS":"text1","EN":"text2","DE":"text3"}
         * tips : {"CS":"text1","EN":"text2","DE":"text3"}
         */

        private langBean name;
        private langBean howto;
        private langBean info;
        private langBean tips;

        public langBean Name { get => name; set => name = value; }
        public langBean Howto { get => howto; set => howto = value; }
        public langBean Info { get => info; set => info = value; }
        public langBean Tips { get => tips; set => tips = value; }
    }

    public class langBean {
        /**
         * CS : "text"
         * EN : "text"
         * DE : "text"
         */

        private String cs;
        private String en;
        private String de;

        public string CS { get => cs; set => cs = value; }
        public string EN { get => en; set => en = value; }
        public string DE { get => de; set => de = value; }
    }
}
