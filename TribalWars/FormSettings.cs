using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace TribalWars
{
    [Serializable()]
    public class FormMainSettings
    {
        private bool _Maximized;
        private int _Height;
        private int _Top;
        private int _Left;
        private int _Width;
        private int _LeftNavigationPaneWidth;
        private bool _LeftNavigationPaneMinimized;
        private int _QuickDetailsSplitterDistance;
        private bool _BBCodeRespectMinFilter;
        private bool _MapHoverTooltip;

        //List<World.MapLocation> History;
        //List<World.MapLocation> Favorites;

        public bool Maximized
        {
            get { return _Maximized; }
            set { _Maximized = value; }
        }

        public int Top
        {
            get { return _Top; }
            set { _Top = value; }
        }

        public int Left
        {
            get { return _Left; }
            set { _Left = value; }
        }

        public int Width
        {
            get { return _Width; }
            set { _Width = value; }
        }

        public int Height
        {
            get { return _Height; }
            set { _Height = value; }
        }

        public int LeftNavigationPaneWidth
        {
            get { return _LeftNavigationPaneWidth; }
            set { _LeftNavigationPaneWidth = value; }
        }

        public bool LeftNavigationPaneMinimized
        {
            get { return _LeftNavigationPaneMinimized; }
            set { _LeftNavigationPaneMinimized = value; }
        }

        public int QuickDetailsSplitterDistance
        {
            get { return _QuickDetailsSplitterDistance; }
            set { _QuickDetailsSplitterDistance = value; }
        }

        public bool RespectMinFilter
        {
            get { return _BBCodeRespectMinFilter; }
            set { _BBCodeRespectMinFilter = value; }
        }

        public bool MapHoverTooltip
        {
            get { return _MapHoverTooltip; }
            set { _MapHoverTooltip = value; }
        }
    }

    [Serializable()]
    public class FormSettings
    {
        private FormMainSettings _FormMain;

        public FormMainSettings FormMain
        {
            get { return _FormMain; }
            set { _FormMain = value; }
        }

        private int LeftNavigationPane;

        public int MyProperty
        {
            get { return LeftNavigationPane; }
            set { LeftNavigationPane = value; }
        }
	

        public static FormSettings Load(string file)
        {
            FormSettings sets = null;
            if (File.Exists(file))
            {
                
            }

            return sets;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        public FormSettings DeserializeObject(String pXmlizedString)
        {
            XmlSerializer xs = new XmlSerializer(typeof(FormSettings));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return (FormSettings)xs.Deserialize(memoryStream);
        }
    }
}
