using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;

namespace RLM.Core.Framework.Utility
{
    public class XsltHelper
    {
        public static string Transform(string xml,string xslFile)
        {
            XslTransform transform = new XslTransform();
            XsltArgumentList args = new XsltArgumentList();
            //define the xslt rendering file
            //get the iterators for the root and context item
            XPathDocument xmlDoc = new XPathDocument(new StringReader(xml));
            XPathNavigator iter = xmlDoc.CreateNavigator();

            //define and add the xslt extension classes
            //Sitecore.Xml.Xsl.XslHelper sc = new Sitecore.Xml.Xsl.XslHelper();
            XsltHelper xslt = new XsltHelper();
            args.AddExtensionObject("http://www.rlmcore.vn/helper", xslt);

            //add parameters
            args.AddParam("item", "", iter);
            args.AddParam("currentitem", "", iter.Select("."));
            //define the stream which will contain the result of xslt transformation
            //StringBuilder sb = new StringBuilder();
            //TextWriter stream = new FileStream(new MemoryStream(Encoding.ASCII.GetBytes(sb.ToString())));
            System.IO.StringWriter stream = new System.IO.StringWriter();

            //load xslt rendering to XslTransform class
            transform.Load(xslFile);
            //perform a transformation with the rendering
            transform.Transform(iter, args, stream);

            return stream.ToString();
        }
    }
}
