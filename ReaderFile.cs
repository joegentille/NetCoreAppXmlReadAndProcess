using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Xml.Serialization;
using ReadXML.readership;
using System.IO;

namespace ReadXML
{
    public class ReaderFile
    {
        public ReaderFile()
        {

        }

        public string GetXmlFileDataWithXmlLoad()
        {
            string fileToOpen = string.Empty;
            var archivoFinal = File.CreateText(@"C:\temp\test.csv");

            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\Test\ReaderShip.xml");
            // Should use this one when reading from the API: doc.LoadXml

            XmlNodeList document = doc.DocumentElement.SelectNodes("/readership/new/document");
            string line = string.Empty;

            // Set titles for csv file
            line = GetTitles();
            archivoFinal.WriteLine(line);

            foreach (XmlNode item in document)
            {
                var hit = item.SelectNodes("hit");
                foreach (XmlNode item2 in hit)
                {
                    //line = line + ", " + GetReaderName(item2);
                    line = GetReaderName(item2);
                    line = line + ", " + GetReaderFirm(item2);
                    line = line + ", " + GetReaderEmail(item2);
                    line = line + ", " + GetHitChannel(item2);
                    
                    var id = item.Attributes["id"].Value;
                    var title = item.Attributes["title"].Value.Replace(",", "");
                    line = line + ", " + id + ", " + title;

                    var security = GetSecurityNodeValue(item);
                    line = line + ", " + security;

                    var analyst = GetAnalystNodeValue(item);
                    line = line + ", " + analyst;

                    var sector = GetSectorNodeValue(item);
                    line = line + ", " + sector;

                    var read = GetReadNodeValue(item2);
                    line = line + ", " + read;

                    var engaged = GetEngagedNodeValue(item2);
                    line = line + ", " + engaged;

                    var updateTime = GetUpdateTimeNodeValue(item2);
                    line = line + ", " + updateTime;

                    archivoFinal.WriteLine(line);
                }
            }
            archivoFinal.Close();
            string text = "";
            return text;
        }

        private string GetUpdateTimeNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("updateTime");
            var data = res?.InnerText;
            if (data != null)
            {
                DateTime date = Convert.ToDateTime(data);
                return date.ToString();
            }
            return data;
        }

        private string GetEngagedNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("engaged");
            var data = res?.InnerText;
            if (data != null)
            {
                DateTime date = Convert.ToDateTime(data);
                return date.ToString();
            }
            return data;
        }


        private string GetReadNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("read");
            var data = res?.InnerText;
            if(data != null)
            {
                DateTime date = Convert.ToDateTime(data);
                return date.ToString();
            }
            return data;
        }

        private string GetSectorNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("sector[@primary='true']");
            return res?.InnerText.Replace(",", ""); ;
        }

        private string GetAnalystNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("analyst[@primary='true']");
            return res?.InnerText.Replace(",", ""); ;
        }

        private string GetSecurityNodeValue(XmlNode xmlNode)
        {
            var res = xmlNode.SelectSingleNode("security[@primary='true']");
            return res?.InnerText.Replace(",", ""); ;
        }

        private string GetReaderName(XmlNode xmlNode)
        {
            var minodo = xmlNode.SelectSingleNode("reader/name");
            return minodo?.InnerText.Replace(",","");
        }

        private string GetReaderFirm(XmlNode xmlNode)
        {
            var minodo = xmlNode.SelectSingleNode("reader/firm");
            return minodo?.InnerText.Replace(",", "");
        }

        private string GetReaderEmail(XmlNode xmlNode)
        {
            var minodo = xmlNode.SelectSingleNode("reader/email");
            return minodo?.InnerText.Replace(",", "");
        }

        private string GetHitChannel(XmlNode xmlNode)
        {
            var minodo = xmlNode.SelectSingleNode("channel");
            return minodo?.InnerText.Replace(",", "");
        }


        private string GetTitles()
        {
            string title = "FirstName-LastName, Firm, Primary Email, Channel, DocumentID, Document Title, Primary Ticker (Security), Primary Analyst (Analyst), Sector, Read Date, Engaged, UpdateTime";
            return title;
        }

        /*
            Reader Object - (FirstName, LastName), Firm, Primary Email
            Channel
            ID - DocumentID
            Title - DocumentTitle
            Security (Type:Primary) - PrimaryTicker
            Analyst - PrimaryAnalyst
            Sector - PrimarySector
            Read - ReadDate as DateTime
            Engaged - AccessedDate as Datetime
            UpdateTime - UpdateTime as DateTime
         */

        //private string GetSecurity(XmlNode xmlNode)
        //{
        //    var minodo = xmlNode.SelectSingleNode("security");
        //    return null;
        //}

        //private string GetSecurityValue(XmlNodeList xmlNodeList)
        //{
        //    string text = string.Empty;


        //    foreach (XmlNode item in xmlNodeList)
        //    {
        //        if(item.Attributes["primary"].Value == "true")
        //        {
        //            text = item.InnerText;
        //        }

        //        //var nodito = item.SelectSingleNode("//security[@primary='true']");
        //        //if(nodito != null)
        //        //{
        //        //    text = nodito.InnerText;
        //        //}                
        //    }
        //    return text;
        //    /*
        //        XmlDocument xDoc = new XmlDocument();
        //        // Load Xml

        //        XmlNodeList nodes = xDoc.SelectNodes("//element[@name='value1']");
        //     */
        //}





        //private string GetName(XmlNode xmlNode)
        //{
        //    var nodito = xmlNode.SelectSingleNode("hit/reader/name");
        //    return nodito?.InnerText.Replace(",", ""); ;
        //}

        //private string GetFirm(XmlNode xmlNode)
        //{
        //    var nodito = xmlNode.SelectSingleNode("hit/reader/firm");
        //    return nodito?.InnerText.Replace(",","");
        //}

        public string GetXmlFileDataWithLinq()
        {
            string fileToOpen = string.Empty;

            XDocument doc = XDocument.Load(@"D:\Test\ReaderShip.xml");

            var readership = doc.Elements("readership")
                .Select(x => x).ToList();
                

            var sets = doc.Root
                .Elements("new")
                .Select(x => x).ToList();

            var document = doc.Descendants("document").Select(x => (string)x.Attribute("title"));

            //var reader = doc.Elements("hit").Select(m => m.Nodes)
            //var result = xd.Descendants(properties).Select(en => new { title = en.Element(title), body = en.Element(body) });                
            var hit1 = doc.Elements("hit");
            var hit2 = doc.Descendants("hit");

            return fileToOpen;
        }

        public string GetXmlFileDataWithSerialization()
        {
            string fileToOpen = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load(@"D:\Test\ReaderShip.xml");
            var xmlString = doc.InnerXml;

            XmlSerializer serializer = new XmlSerializer(typeof(Readership));
            Readership readership;

            using (TextReader reader = new StringReader(xmlString))
            {
                readership = (Readership)serializer.Deserialize(reader);
            }
            return fileToOpen;
        }

    }
}
