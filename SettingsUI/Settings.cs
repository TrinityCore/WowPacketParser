using System;
using System.Windows.Forms;
using System.Xml;

namespace SettingsUI
{
    public class Settings // By circumpunct
    {
        readonly XmlDocument _xmlDocument = new XmlDocument();
        readonly string _documentPath = Application.StartupPath + "//settings.xml";

        public Settings()
        {
            try
            {
                ExistingFile = true;
                _xmlDocument.Load(_documentPath);
            }
            catch
            {
                ExistingFile = false;
                _xmlDocument.LoadXml("<settings></settings>");
            }
        }

        public bool ExistingFile;

        public bool GetSetting(string xPath, bool defaultValue)
        {
            return Convert.ToBoolean(GetSetting(xPath, Convert.ToString(defaultValue)));
        }

        public void PutSetting(string xPath, bool value)
        {
            PutSetting(xPath, Convert.ToString(value));
        }

        public int GetSetting(string xPath, int defaultValue)
        {
            return Convert.ToInt16(GetSetting(xPath, Convert.ToString(defaultValue)));
        }

        public void PutSetting(string xPath, int value)
        {
            PutSetting(xPath, Convert.ToString(value));
        }

        public string GetSetting(string xPath, string defaultValue)
        {
            var xmlNode = _xmlDocument.SelectSingleNode("settings/" + xPath);
            return xmlNode != null ? xmlNode.InnerText : defaultValue;
        }

        public void PutSetting(string xPath, string value)
        {
            var xmlNode = _xmlDocument.SelectSingleNode("settings/" + xPath) ?? CreateMissingNode("settings/" + xPath);
            xmlNode.InnerText = value;
            try
            {
                _xmlDocument.Save(_documentPath);
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { } // Silently error, program can still work
// ReSharper restore EmptyGeneralCatchClause
        }

        private XmlNode CreateMissingNode(string xPath)
        {
            var xPathSections = xPath.Split('/');
            var currentXPath = "";
            var currentNode = _xmlDocument.SelectSingleNode("settings");
            foreach (var xPathSection in xPathSections)
            {
                currentXPath += xPathSection;
                var testNode = _xmlDocument.SelectSingleNode(currentXPath);
                if (testNode == null)
                {
                    if (currentNode != null)
                        currentNode.InnerXml += "<" +
                                                xPathSection + "></" +
                                                xPathSection + ">";
                }
                currentNode = _xmlDocument.SelectSingleNode(currentXPath);
                currentXPath += "/";
            }
            return currentNode;
        }
    }
}
