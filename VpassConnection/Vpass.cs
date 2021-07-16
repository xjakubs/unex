using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using xjakubs.Logger;

/**
 * Universal program for VIDA! science centrum for exhibits
 *
 * @author Jakub Smid (xjakubs@gmail.com)
 */
namespace VpassConnection {
    public class Vpass {
        private Logs mLogs;
        private const String LOG_TAG = "[VPASS]";

        private int mExhibitID;
        private string mTicketID = ""; 
        private List<VpassData> aDataList;

        private const string connUrl = "http://10.0.11.250/Services/ClientSystemService.svc";
        private const string connAction = "http://tempuri.org/IClientSystemService/TestConnection";

        private const string appUrl = "http://10.0.11.250/Services/VisitorService.svc";
        private const string appAction = "http://tempuri.org/IVisitorService/SetApplicationData";

        private const string IMAGE64 = "ImageBase64";
        private const string VIDEO = "Video";
        private const string INTEGER = "Int";
        private const string STRING = "String";

        public Vpass(Logs log, int exhibitId, string ticketId) {
            mExhibitID = exhibitId;
            mTicketID = ticketId;
            mLogs = log;
        }

        public Vpass(Logs log, int exhibitId) {
            mExhibitID = exhibitId;
            mLogs = log;
        }

        public void addData(List<VpassData> list) {
            aDataList = list;
        }

        public bool sendTestConnection() {
            mLogs.logInfo(LOG_TAG + " sendTestConnection()");
            XmlDocument soapEnvelopeXml = createSoapEnvelope(VpassType.TYPE.test);
            HttpWebRequest webRequest = createWebRequest(connUrl, connAction);
            if (insertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest) == false) {
                return false;
            }
            if (sendCommandAsync(webRequest) == false) {
                return false;
            }
            return true;
        }

        public bool sendData() {
            if (aDataList == null) {
                mLogs.logError(LOG_TAG + " sendData() datalist is null");
                return false;
            }
            if (aDataList.Count > 0) {
                foreach (VpassData data in aDataList) {
                    if (send(data) == false) {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool send(VpassData data) {
            mLogs.logInfo(LOG_TAG + " send()");
            XmlDocument soapEnvelopeXml = createSoapEnvelope(data.Type, data.Content, data.Code);
            if (soapEnvelopeXml == null) {
                return false;
            }
            HttpWebRequest webRequest = createWebRequest(appUrl, appAction);
            if (insertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest) == false) {
                return false;
            }
            if (sendCommandAsync(webRequest) == false) {
                return false;
            }
            return true;
        }

        private static HttpWebRequest createWebRequest(string url, string action) {
            HttpWebRequest webRequest = (HttpWebRequest) WebRequest.Create(url);
            webRequest.Method = "POST";
            webRequest.ContentType = "text/xml; charset=utf-8";
            webRequest.Headers.Add("SOAPAction", "\"" + action + "\"");
            webRequest.ServicePoint.Expect100Continue = true;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            return webRequest;
        }

        private XmlDocument createSoapEnvelope(VpassType.TYPE type) {
            return createSoapEnvelope(type, "", "");
        }

        private XmlDocument createSoapEnvelope(VpassType.TYPE type, String content, String codename) {
            mLogs.logInfo(LOG_TAG + " createSoapEnvelope()");
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            StringBuilder message = new StringBuilder();

            switch (type) {
                case VpassType.TYPE.picture:
                    mLogs.logInfo(LOG_TAG + " createSoapEnvelope(picture)");
                    message.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                    message.Append("<s:Body>");
                    message.Append("<SetApplicationData xmlns=\"http://tempuri.org/\">");
                    message.Append("<data xmlns:a=\"http://schemas.datacontract.org/2004/07/VPass.Communication.DataContracts\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    message.Append("<a:Data>");
                    message.Append("<a:DataItem>");
                    message.Append("<a:Code>");
                    message.Append(codename);
                    message.Append("</a:Code>");
                    message.Append("<a:Data i:type=\"b:string\" xmlns:b=\"http://www.w3.org/2001/XMLSchema\">");
                    message.Append(content);
                    message.Append("</a:Data>");
                    message.Append("<a:Type>" + IMAGE64 + "</a:Type>");
                    message.Append("</a:DataItem>");
                    message.Append("</a:Data>");
                    message.Append("</data>");
                    message.Append("<ticketCode>" + mTicketID + "</ticketCode>");
                    message.Append("<exhbitId>" + mExhibitID.ToString() + "</exhbitId>");
                    message.Append("</SetApplicationData>");
                    message.Append("</s:Body>");
                    message.Append("</s:Envelope>");
                    break;
                case VpassType.TYPE.video:
                    mLogs.logInfo(LOG_TAG + " createSoapEnvelope(video)");
                    message.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                    message.Append("<s:Body>");
                    message.Append("<SetApplicationData xmlns=\"http://tempuri.org/\">");
                    message.Append("<data xmlns:a=\"http://schemas.datacontract.org/2004/07/VPass.Communication.DataContracts\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    message.Append("<a:Data>");
                    message.Append("<a:DataItem>");
                    message.Append("<a:Code>");
                    message.Append(codename);
                    message.Append("</a:Code>");
                    message.Append("<a:Data i:type=\"b:string\" xmlns:b=\"http://www.w3.org/2001/XMLSchema\">");
                    message.Append(content);
                    message.Append("</a:Data>");
                    message.Append("<a:Type>" + VIDEO + "</a:Type>");
                    message.Append("</a:DataItem>");
                    message.Append("</a:Data>");
                    message.Append("</data>");
                    message.Append("<ticketCode>" + mTicketID + "</ticketCode>");
                    message.Append("<exhbitId>" + mExhibitID.ToString() + "</exhbitId>");
                    message.Append("</SetApplicationData>");
                    message.Append("</s:Body>");
                    message.Append("</s:Envelope>");
                    break;
                case VpassType.TYPE.number:
                    mLogs.logInfo(LOG_TAG + " createSoapEnvelope(number)");
                    message.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                    message.Append("<s:Body>");
                    message.Append("<SetApplicationData xmlns=\"http://tempuri.org/\">");
                    message.Append("<data xmlns:a=\"http://schemas.datacontract.org/2004/07/VPass.Communication.DataContracts\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    message.Append("<a:Data>");
                    message.Append("<a:DataItem>");
                    message.Append("<a:Code>");
                    message.Append(codename);
                    message.Append("</a:Code>");
                    message.Append("<a:Data i:type=\"b:int\" xmlns:b=\"http://www.w3.org/2001/XMLSchema\">");
                    message.Append(content);
                    message.Append("</a:Data>");
                    message.Append("<a:Type>" + INTEGER + "</a:Type>");
                    message.Append("</a:DataItem>");
                    message.Append("</a:Data>");
                    message.Append("</data>");
                    message.Append("<ticketCode>" + mTicketID + "</ticketCode>");
                    message.Append("<exhbitId>" + mExhibitID.ToString() + "</exhbitId>");
                    message.Append("</SetApplicationData>");
                    message.Append("</s:Body>");
                    message.Append("</s:Envelope>");
                    break;
                case VpassType.TYPE.text:
                    mLogs.logInfo(LOG_TAG + " createSoapEnvelope(text)");
                    message.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                    message.Append("<s:Body>");
                    message.Append("<SetApplicationData xmlns=\"http://tempuri.org/\">");
                    message.Append("<data xmlns:a=\"http://schemas.datacontract.org/2004/07/VPass.Communication.DataContracts\" xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    message.Append("<a:Data>");
                    message.Append("<a:DataItem>");
                    message.Append("<a:Code>");
                    message.Append(codename);
                    message.Append("</a:Code>");
                    message.Append("<a:Data i:type=\"b:string\" xmlns:b=\"http://www.w3.org/2001/XMLSchema\">");
                    message.Append(content);
                    message.Append("</a:Data>");
                    message.Append("<a:Type>" + STRING + "</a:Type>");
                    message.Append("</a:DataItem>");
                    message.Append("</a:Data>");
                    message.Append("</data>");
                    message.Append("<ticketCode>" + mTicketID + "</ticketCode>");
                    message.Append("<exhbitId>" + mExhibitID.ToString() + "</exhbitId>");
                    message.Append("</SetApplicationData>");
                    message.Append("</s:Body>");
                    message.Append("</s:Envelope>");
                    break;
                case VpassType.TYPE.test:
                default:
                    mLogs.logInfo(LOG_TAG + " createSoapEnvelope(test)");
                    message.Append("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\">");
                    message.Append("<s:Body>");
                    message.Append("<TestConnection xmlns=\"http://tempuri.org/\">");
                    message.Append("<exhbitId>" + mExhibitID.ToString() + "</exhbitId>");
                    message.Append("</TestConnection>");
                    message.Append("</s:Body>");
                    message.Append("</s:Envelope>");
                    break;
            }

            soapEnvelopeDocument.LoadXml(message.ToString());
            mLogs.logInfo(LOG_TAG + " LoadXml - message length: " + message.Length);
            return soapEnvelopeDocument;
        }

        private bool insertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest) {
            mLogs.logInfo(LOG_TAG + " insertSoapEnvelopeIntoWebRequest()");
            try {
                using (Stream stream = webRequest.GetRequestStream()) {
                    soapEnvelopeXml.Save(stream);
                    return true;
                }
            } catch (WebException ex) {
                mLogs.logError(LOG_TAG + " Problem with webRequest.GetRequestStream()! Error: " + ex.Message);
                return false;
            }
        }

        private bool sendCommandAsync(HttpWebRequest webRequest) {
            mLogs.logInfo(LOG_TAG + " createWebRequest()");
            // begin async call to web request.
            IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);
            //webRequest.BeginGetResponse(new AsyncCallback(OnResponse), webRequest);
            mLogs.logInfo(LOG_TAG + " sendCommandAsync asyncResult= " + asyncResult);
            // suspend this thread until call is complete. You might want to
            // do something usefull here like update your UI.
            asyncResult.AsyncWaitHandle.WaitOne();

            // get the response from the completed web request.
            string soapResult;
            try {
                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult)) {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream())) {
                        soapResult = rd.ReadToEnd();
                    }
                    mLogs.logInfo(LOG_TAG + " soapResult: " + soapResult);
                    XmlDocument xmlResult = new XmlDocument();
                    xmlResult.LoadXml(soapResult);

                    //PICTURE
                    //soapResult: <s:Envelope xmlns:s="http://schemas.xmlsoap.org/soap/envelope/"><s:Body><SetApplicationDataResponse xmlns="http://tempuri.org/"><SetApplicationDataResult>true</SetApplicationDataResult></SetApplicationDataResponse></s:Body></s:Envelope>
                    //TESTCONNECTION
                    //soapResult: < s:Envelope xmlns:s = "http://schemas.xmlsoap.org/soap/envelope/" >< s:Body >< TestConnectionResponse xmlns = "http://tempuri.org/" >< TestConnectionResult > true </ TestConnectionResult ></ TestConnectionResponse ></ s:Body ></ s:Envelope >
                    XmlNodeList nodeList = xmlResult.GetElementsByTagName("SetApplicationDataResult");
                    if (nodeList.Count > 0) {
                        if ((String.Compare(nodeList[0].InnerText, "true") == 0)) {
                            return true;
                        }
                    }
                    nodeList = xmlResult.GetElementsByTagName("TestConnectionResult");
                    if (nodeList.Count > 0) {
                        if ((String.Compare(nodeList[0].InnerText, "true") == 0)) {
                            return true;
                        }
                    }
                    return false;
                }
            } catch (WebException e) {
                mLogs.logError(LOG_TAG + " Error:");
                mLogs.logError(LOG_TAG + " " + e.Message);
                mLogs.logError(LOG_TAG + " " + e.Status.ToString());
                return false;
            }
        }
    }
}
