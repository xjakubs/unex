using System;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.Linq;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    class XmlTextsReader {

        private const String mXMLFile = "UnExponat.Resources.text.texts.xml";

        private XmlReader reader;
        private XDocument doc;

        public XmlTextsReader() {
            var assembly = Assembly.GetExecutingAssembly();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            try {
                reader = XmlReader.Create(assembly.GetManifestResourceStream(mXMLFile), settings);
                doc = XDocument.Load(reader);
            } catch (XmlException ex) {
                Program.logError(ex.ToString());
            }
        }

        public String readNode(String node, String language) {
            if (doc != null) {
                var groupElements = from el in doc.Descendants().Elements(node) select el;
                foreach (XElement xe in groupElements) {
                    if (String.Compare(xe.Attribute("lang").Value, language) == 0) {
                        return xe.Value;
                    }
                }
            }

            Program.logError("NOT FOUND! Node: \"" + node + "\" language: " + language);
            return "";
        }
    }
}
