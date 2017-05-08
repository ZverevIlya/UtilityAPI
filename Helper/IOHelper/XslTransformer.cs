using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Xsl;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Collections;

namespace Helper.IOHelper
{
    public class XslTransformer
    {
        /// <summary>
        /// Transforms the specified input.	
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="xslFilename">The XSL filename.</param>
        /// <param name="xsltArgs">The XSLT args.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string Transform(string input, string xslFilename, Hashtable xsltArgs)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            XsltSettings settings = new XsltSettings(true, true);
            transform.Load(xslFilename, settings, new XmlUrlResolver());
            string xmlfile = IOHelper.GetTemporaryFileName();
            File.WriteAllText(xmlfile, input);

            string htmlfile = IOHelper.GetTemporaryFileName();
            transform.Transform(xmlfile, htmlfile);

            string output = File.ReadAllText(htmlfile);

            File.Delete(xmlfile);
            File.Delete(htmlfile);

            return output;
        }

        public string Transform(string input, string xslFilename)
        {
            return Transform(input, xslFilename, null);
        }

        public string Transform(XmlDocument input, string xslFilename)
        {
            XslCompiledTransform xsldoc = new XslCompiledTransform();
            xsldoc.Load(xslFilename);

            XPathNavigator nav = input.CreateNavigator();
            nav.MoveToRoot();
            StringBuilder sb = new StringBuilder();
            using (StringWriter writer = new StringWriter(sb))
            {
                xsldoc.Transform(nav, null, writer);
                writer.Flush();
            }
            return sb.ToString();
        }

        private static XsltArgumentList CreateXsltArgs(Hashtable xsltArgs)
        {
            XsltArgumentList args = new XsltArgumentList();
            if (xsltArgs != null)
            {
                foreach (object key in xsltArgs.Keys)
                {
                    args.AddParam(key.ToString(), string.Empty, xsltArgs[key]);
                }
            }
            return args;
        }
    }
}
