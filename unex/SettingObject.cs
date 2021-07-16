using System;
using System.IO;
using System.Windows.Forms;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class SettingObject {


        private String name;
        private String value;

        public SettingObject(string name, string value) {
            this.Name = name.Replace("=", "");
            this.Value = value;
        }

        public string Name { get => name; set => name = value; }
        public string Value { get => value; set => this.value = value; }
    }
}
