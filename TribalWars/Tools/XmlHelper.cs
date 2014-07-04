using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Xml.Serialization;

namespace TribalWars.Tools
{
    public static class XmlHelper
    {
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
            return GetColor(input, Color.Transparent);
        }

        public static Color GetColor(string input, Color defaultColor)
        {
            Color color = defaultColor;
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
            return string.Format("{0};{1};{2}", color.R.ToString(CultureInfo.InvariantCulture), color.G.ToString(CultureInfo.InvariantCulture), color.B.ToString(CultureInfo.InvariantCulture));
        }
    }
}
