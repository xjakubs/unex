using System;
using System.Collections.Generic;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace UnExponat {
    /*
     * req://{type: "newUser", data: {data1: "Martin", data2: "trter"}}
     * res://{type: "newUser", data: {data1: "Martin", data2: "trter"}}
     * 
     * req://{type: "info"}
     * res://{type: "info", data: {version: "v0.60", exponatID: "123", vpassID: "12"}}
     * 
     * req://{type: "settings"}
     * res://{type: "settings", data: {color: "orange", home_web_path: "C:/app/homeweb/", languages: "en,de", comport: "com7"}}
     * 
     * req://{ 'type': 'show_notification', 'data': { 'msg_text': 'nejaka notifikace', 'msg_long': '60'} }
     * 
     * req://{ 'type': 'presentation', 'data': { 'url': 'nejaka url adresa'} } 
     * example: req://{ 'type': 'presentation', 'data': { 'url': 'D:/VIDA/app/212b-homeweb/index.html'} }
     */



    public class JsonMessage {
        public const string INFO = "info";
        public const string SETTINGS = "settings";
        public const string NOTIFICATION = "show_notification";
        public const string PRESENTATION = "presentation";

        private string type;
        private Data data;

        public string Type { get => type; set => type = value; }
        public Data Data { get => data; set => data = value; }
    }

    public class Data {
        private string version;
        private string exponatID;
        private string vpassID;
        private string color;
        private string home_web_path;
        private string languages;
        private string comport;
        private string msg_text;
        private string msg_long;
        private string url;

        public string Version { get => version; set => version = value; }
        public string ExponatID { get => exponatID; set => exponatID = value; }
        public string VpassID { get => vpassID; set => vpassID = value; }
        public string Color { get => color; set => color = value; }
        public string Home_web_path { get => home_web_path; set => home_web_path = value; }
        public string Languages { get => languages; set => languages = value; }
        public string Comport { get => comport; set => comport = value; }
        public string Msg_text { get => msg_text; set => msg_text = value; }
        public string Msg_long { get => msg_long; set => msg_long = value; }
        public string Url { get => url; set => url = value; }
    }

}
