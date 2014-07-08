using System;
using System.Diagnostics;
using TribalWars.Data;
using TribalWars.Villages.Resources;

namespace TribalWars.Villages.Units
{
    /// <summary>
    /// Representation of a unit
    /// </summary>
    public class Unit : IEquatable<Unit>
    {
        #region Fields
        private readonly UnitTypes _type;
        private readonly float _speed;
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
        public Resource Cost { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the unit amounts should be hidden for the attacker
        /// </summary>
        public bool HideAttacker { get; private set; }

        /// <summary>
        /// Gets the resource carrying capacity
        /// </summary>
        public int Carry { get; private set; }

        /// <summary>
        /// Gets the name of the unit
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this
        /// is an offensive unit
        /// </summary>
        public bool Offense { get; private set; }

        /// <summary>
        /// Gets the Unit image in BBCode
        /// </summary>
        public string BbCodeImage
        {
            get { return string.Format("[img]{0}[/img]", World.Default.Structure.GetUnitImageUrl(_type)); }
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
                        return UnitImages.Archer;
                    case UnitTypes.Axe:
                        return UnitImages.Axe;
                    case UnitTypes.Catapult:
                        return UnitImages.Catapult;
                    case UnitTypes.Heavy:
                        return UnitImages.HeavyCavalry;
                    case UnitTypes.Light:
                        return UnitImages.LightCavalry;
                    case UnitTypes.MArcher:
                        return UnitImages.MountedArcher;
                    case UnitTypes.Snob:
                        return UnitImages.Noble;
                    case UnitTypes.Knight:
                        return UnitImages.Paladin;
                    case UnitTypes.Ram:
                        return UnitImages.Ram;
                    case UnitTypes.Spy:
                        return UnitImages.Scout;
                    case UnitTypes.Spear:
                        return UnitImages.Spear;
                    case UnitTypes.Sword:
                        return UnitImages.Sword;
                    default:
                        return Properties.Resources.face;
                }
                
            }
        }

        /// <summary>
        /// Gets the short name for the unit
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the unit is typically used for farming
        /// </summary>
        public bool Farmer { get; private set; }

        /// <summary>
        /// Gets the position of the unit in a report
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the speed of the unit on the currently loaded world
        /// </summary>
        public float Speed
        {
            get { return _speed /* World.Default.UnitSpeed*/; } // TODO: World.UnitSpeed is now already calculated in the speed
        }
        #endregion

        #region Constructors
        internal Unit(int pos, string name, string shortname, string type, int carry, bool farmer, bool hideAttacker, int wood, int clay, int iron, int people, float speed, bool isOff)
        {
            Position = pos;
            Name = name;
            ShortName = shortname;
            Carry = carry;
            Farmer = farmer;
            HideAttacker = hideAttacker;
            Cost = new Resource(wood, clay, iron, people);
            _speed = speed;
            Offense = isOff;

            if (!Enum.TryParse(type, true, out _type) || _type == UnitTypes.None)
            {
                Debug.Assert(false, "Unknown unit???");
            }
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

        #region Methods
        public override string ToString()
        {
            return string.Format("{0}, Position={1}, Offense={2}, Farmer={3}, HideAtatcker={4}", Name, Position, Offense, Farmer, HideAttacker);
        }
        #endregion
    }
}
