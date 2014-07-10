using System.Diagnostics;
using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using TribalWars.Controls.Finders;

namespace TribalWars.Controls.Monitoring
{
    /// <summary>
    /// Contains the ActiveRectangle used by <see cref="FinderOptionsControl"/>
    /// </summary>
    public class Monitor : IXmlSerializable
    {
        #region Fields
        private Rectangle _rectangle;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the game rectangle in which villages are being monitored
        /// </summary>
        public Rectangle ActiveRectangle
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader r)
        {
            Debug.Assert(r.IsStartElement("Monitor"));

            // Read monitor rectangle
            int x = System.Convert.ToInt32(r.GetAttribute("X"));
            int y = System.Convert.ToInt32(r.GetAttribute("Y"));
            int width = System.Convert.ToInt32(r.GetAttribute("Width"));
            int height = System.Convert.ToInt32(r.GetAttribute("Height"));
            _rectangle = new Rectangle(x, y, width, height);

            r.Read();
        }

        public void WriteXml(XmlWriter w)
        {
            w.WriteStartElement("Monitor");

            // Write monitor rectangle
            w.WriteAttributeString("X", _rectangle.X.ToString(CultureInfo.InvariantCulture));
            w.WriteAttributeString("Y", _rectangle.Y.ToString(CultureInfo.InvariantCulture));
            w.WriteAttributeString("Width", _rectangle.Width.ToString(CultureInfo.InvariantCulture));
            w.WriteAttributeString("Height", _rectangle.Height.ToString(CultureInfo.InvariantCulture));

            w.WriteEndElement();
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("ActiveRectangle={0}", _rectangle);
        }
        #endregion
    }
}
