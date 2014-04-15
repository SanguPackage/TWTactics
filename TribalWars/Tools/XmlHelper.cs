using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Drawing;

namespace TribalWars.Tools
{
    public class XmlHelper
    {
        // Ok: put all xml reading and writing in one class together
        // put on option "IgnoreWhiteSpace"
        // and get rid of the ugly ReadXmlElement


        public static string ReadXmlElement(XmlReader r, string name)
        {
            if (r.IsStartElement(name))
            {
                //r.Read();
                if (!r.IsEmptyElement)
                {
                    r.ReadStartElement(name);
                    string s = r.ReadString();
                    r.ReadEndElement();
                    return s;
                }
                else
                {
                    r.Read();
                }
            }
            else
            {
                r.Read();
            }
            return string.Empty;
        }

        public static Color GetColor(string input)
        {
            // TODO: use a typeconverter instead?
            // or an extension method...
            Color color = Color.Transparent;
            if (string.IsNullOrEmpty(input)) return color;
            try
            {
                string[] rgb = input.Split(';');
                if (rgb.Length == 3)
                {
                    color = Color.FromArgb(Convert.ToInt32(rgb[0]), Convert.ToInt32(rgb[1]), Convert.ToInt32(rgb[2]));
                }
                else
                {
                    color = Color.FromName(input);
                }
            }
            catch
            {

            }
            return color;
        }

        public static string SetColor(Color color)
        {
            if (color.IsKnownColor) return color.Name;
            return string.Format("{0};{1};{2}", color.R.ToString(), color.G.ToString(), color.B.ToString());
        }
    }
}
