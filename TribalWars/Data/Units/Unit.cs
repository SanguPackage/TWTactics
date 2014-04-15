using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using TribalWars.Data.Resources;

namespace TribalWars.Data.Units
{
    /// <summary>
    /// Representation of a unit
    /// </summary>
    public class Unit : IEquatable<Unit>
    {
        #region Fields
        private int _position;
        private string _name;
        private string _image;
        private string _ShortName;
        private int _carry;
        private bool _farmer;
        private bool _hideAttacker;
        private Resource _cost;
        private UnitTypes _type = UnitTypes.None;
        private float _speed;
        private bool _isOffense;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the unit type
        /// </summary>
        public UnitTypes Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the cost for training the unit
        /// </summary>
        public Resource Cost
        {
            get { return _cost; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the unit amounts should be hidden for the attacker
        /// </summary>
        public bool HideAttacker
        {
            get { return _hideAttacker; }
            set { _hideAttacker = value; }
        }

        /// <summary>
        /// Gets the resource carrying capacity
        /// </summary>
        public int Carry
        {
            get { return _carry; }
        }

        /// <summary>
        /// Gets the name of the unit
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets a value indicating whether this
        /// is an offensive unit
        /// </summary>
        public bool Offense
        {
            get { return _isOffense; }
        }

        /// <summary>
        /// Gets the Unit image in BBCode
        /// </summary>
        public string BBCodeImage
        {
            get { return string.Format("[img]{0}[/img]", _image); }
        }

        /// <summary>
        /// Gets the unit image
        /// </summary>
        public System.Drawing.Image Image
        {
            get
            {
                switch (Type)
                {
                    case UnitTypes.Archer:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Archer;
                    case UnitTypes.Axeman:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Axe;
                    case UnitTypes.Catapult:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Catapult;
                    case UnitTypes.HeavyCavalry:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.HeavyCavalry;
                    case UnitTypes.LightCavalry:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.LightCavalry;
                    case UnitTypes.MountedArcher:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.MountedArcher;
                    case UnitTypes.Nobleman:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Noble;
                    case UnitTypes.Paladin:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Paladin;
                    case UnitTypes.Ram:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Ram;
                    case UnitTypes.Scout:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Scout;
                    case UnitTypes.Spearman:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Spear;
                    case UnitTypes.Swordsman:
                        return (System.Drawing.Image)TribalWars.Data.Units.Images.Sword;
                    default:
                        return (System.Drawing.Image)TribalWars.Properties.Resources.face;
                }
                
            }
        }

        /// <summary>
        /// Gets the Unit image
        /// </summary>
        public Uri ImageUri
        {
            get { return new Uri(_image); }
        }

        /// <summary>
        /// Gets the short name for the unit
        /// </summary>
        public string ShortName
        {
            get { return _ShortName; }
        }

        /// <summary>
        /// Gets a value indicating whether the unit is typically used for farming
        /// </summary>
        public bool Farmer
        {
            get { return _farmer; }
            set { _farmer = value; }
        }

        /// <summary>
        /// Gets the position of the unit in a report
        /// </summary>
        public int Position
        {
            get { return _position; }
        }

        /// <summary>
        /// Gets the basic speed of the unit
        /// </summary>
        public float BasicSpeed
        {
            get { return _speed; }
        }

        /// <summary>
        /// Gets the speed of the unit on the currently loaded world
        /// </summary>
        public float Speed
        {
            get { return _speed * World.Default.UnitSpeed; }
        }
        #endregion

        #region Constructors
        internal Unit(int pos, string name, string shortname, string type, string image, int carry, bool farmer, bool hideAttacker, int wood, int clay, int iron, int people, float speed, bool isOff)
        {
            _position = pos;
            _name = name;
            _ShortName = shortname;
            _image = image;
            _carry = carry;
            _farmer = farmer;
            _hideAttacker = hideAttacker;
            _cost = new Resource(wood, clay, iron, people);
            _speed = speed;
            _isOffense = isOff;

            if (Enum.IsDefined(typeof(UnitTypes), type))
                _type = (UnitTypes)Enum.Parse(typeof(UnitTypes), type, true);
            else
                _type = UnitTypes.None;
        }

        internal Unit(Unit unit)
        {
            _position = unit._position;
            _name = unit._name;
            _ShortName = unit._ShortName;
            _image = unit._image;
            _hideAttacker = unit._hideAttacker;
            _farmer = unit._farmer;
            _carry = unit._carry;
            _cost = unit.Cost;
            _speed = unit.Speed;
            _type = unit.Type;
            _isOffense = unit.Offense;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Converts a local building name to the Type
        /// </summary>
        public static UnitTypes GetUnitFromName(string name)
        {
            return WorldUnits.Default[name].Type;
        }
        #endregion

        #region IEquatable<Unit> Members
        public bool Equals(Unit other)
        {
            if (other == null) return false;
            return other.Type == Type;
        }

        public override int GetHashCode()
        {
            return _type.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Unit);
        }
        #endregion
    }
}
