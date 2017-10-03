using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SucLib.Common
{
    public class XMLUtil
    {
        private static XmlDocument doc = new XmlDocument();
        public static XMLUtil Create(string path)
        {
            XMLUtil.doc.Load(path);
            return new XMLUtil();
        }
        public XmlNode GetNode(string xpath)
        {
            return XMLUtil.doc.SelectSingleNode(xpath);
        }
        public XmlNodeList GetNodeList(string xpath)
        {
            return XMLUtil.doc.SelectNodes(xpath);
        }
    }
}
