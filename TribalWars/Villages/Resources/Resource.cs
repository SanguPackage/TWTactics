using System;
using System.Xml.Serialization;

namespace TribalWars.Villages.Resources
{
    /// <summary>
    /// A wrapper to contain an amount of wood, clay and iron
    /// </summary>
    /// <remarks>Possible an amount of people also</remarks>
    public class Resource : IXmlSerializable
    {
        #region Constants
        public const int NotSet = -1;
        public const string ClayBBCodeString = "[img]http://www.tribalwars.net/graphic/lehm.png[/img]";
        public const string WoodBBCodeString = "[img]http://www.tribalwars.net/graphic/holz.png[/img]";
        public const string IronBBCodeString = "[img]http://www.tribalwars.net/graphic/eisen.png[/img]";
        public const string FaceBBCodeString = "[img]http://www.tribalwars.net/graphic/face.png[/img]";
        public const string AllBBCodeString = "[img]http://www.tribalwars.net/graphic/res.png[/img]";
        #endregion

        #region Fields
        private bool _set;
        private int _people;
        private int _iron;
        private int _clay;
        private int _wood;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets a value indicating the amounts or not null
        /// </summary>
        public bool Set
        {
            get { return _set; }
            set { _set = value; }
        }

        /// <summary>
        /// Gets or sets the iron value
        /// </summary>
        public int Iron
        {
            get { return _iron; }
            set
            {
                _iron = value;
                if (_iron < 0) _iron = 0;
                _set = true;
            }
        }

        /// <summary>
        /// Gets or sets the clay value
        /// </summary>
        public int Clay
        {
            get { return _clay; }
            set
            {
                _clay = value;
                if (_clay < 0) _clay = 0;
                _set = true;
            }
        }

        /// <summary>
        /// Gets or sets the wood value
        /// </summary>
        public int Wood
        {
            get { return _wood; }
            set
            {
                _wood = value;
                if (_wood < 0) _wood = 0;
                _set = true;
            }
        }

        /// <summary>
        /// Gets or sets the people value
        /// </summary>
        public int People
        {
            get { return _people; }
            set
            {
                _people = value;
                _set = true;
            }
        }

        /// <summary>
        /// Gets the resource value based on the name
        /// </summary>
        public int this[string name]
        {
            get
            {
                switch (name)
                {
                    case ClayBBCodeString:
                        return Clay;
                    case WoodBBCodeString:
                        return Wood;
                    case IronBBCodeString:
                        return Iron;
                    case AllBBCodeString:
                        return Clay + Wood + Iron;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Gets the resource value based on the type
        /// </summary>
        public int this[ResourceTypes type]
        {
            get
            {
                switch (type)
                {
                    case ResourceTypes.Clay:
                        return Clay;
                    case ResourceTypes.Wood:
                        return Wood;
                    case ResourceTypes.Iron:
                        return Iron;
                    case ResourceTypes.All:
                        return Clay + Wood + Iron;
                    case ResourceTypes.Face:
                        return People;
                    default:
                        return 0;
                }
            }
        }

        /// <summary>
        /// Gets the pretty formatted wood amount
        /// </summary>
        public string WoodString
        {
            get { return Tools.Common.GetPrettyNumber(Wood); }
        }

        /// <summary>
        /// Gets the pretty formatted clay amount
        /// </summary>
        public string ClayString
        {
            get { return Tools.Common.GetPrettyNumber(Clay); }
        }

        /// <summary>
        /// Gets the pretty formatted iron amount
        /// </summary>
        public string IronString
        {
            get { return Tools.Common.GetPrettyNumber(Iron); }
        }
        #endregion

        #region Constructor
        public Resource()
        {

        }

        public Resource(int wood, int clay, int iron)
        {
            Wood = wood;
            Clay = clay;
            Iron = iron;
            Set = true;
        }

        public Resource(int wood, int clay, int iron, int face)
        {
            Wood = wood;
            Clay = clay;
            Iron = iron;
            People = face;
            Set = true;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            if (Set) return string.Format("W{0}, C{1}, I{2}", Wood.ToString("#,0"), Clay.ToString("#,0"), Iron.ToString("#,0"));
            return "Nothing";
        }

        /// <summary>
        /// Gets a resource image
        /// </summary>
        /// <param name="type">The resource type of the image</param>
        public static System.Drawing.Image GetImage(ResourceTypes type)
        {
            switch (type)
            {
                case ResourceTypes.Clay:
                    return ResourceImages.clay;
                case ResourceTypes.Face:
                    return ResourceImages.Face;
                case ResourceTypes.Iron:
                    return ResourceImages.iron;
                case ResourceTypes.Wood:
                    return ResourceImages.wood;
            }
            return null;
        }

        /// <summary>
        /// Calculates the total amount of resources or NotSet
        /// </summary>
        public int Total()
        {
            if (Set) return Clay + Wood + Iron;
            return NotSet;
        }
        #endregion

        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader r)
        {
            DateTime? date;
            ReadXml(r, out date);
        }

        public void ReadXml(System.Xml.XmlReader r, out DateTime? date)
        {
            r.MoveToContent();
            if (r.HasAttributes)
                date = Convert.ToDateTime(r.GetAttribute(0), System.Globalization.CultureInfo.InvariantCulture);
            else
                date = null;
            r.Read();
            r.MoveToContent();
            Wood = Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "Wood"));
            Clay = Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "Clay"));
            Iron = Convert.ToInt32(Tools.XmlHelper.ReadXmlElement(r, "Iron"));
            r.Read();
        }

        public void WriteXml(System.Xml.XmlWriter w)
        {
            WriteXml(w, null);
        }

        public void WriteXml(System.Xml.XmlWriter w, DateTime? Date)
        {
            w.WriteStartElement("Resources");
            if (Date.HasValue)
                w.WriteAttributeString("Date", Date.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
            w.WriteElementString("Wood", Wood.ToString());
            w.WriteElementString("Clay", Clay.ToString());
            w.WriteElementString("Iron", Iron.ToString());
            w.WriteEndElement();
        }
        #endregion

        #region ICloneable Members
        public Resource Clone()
        {
            return new Resource(_wood, _clay, _iron, _people);
        }
        #endregion
    }
}