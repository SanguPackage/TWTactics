#region Using
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TribalWars.Browsers.Reporting;

#endregion

namespace TribalWars.Villages.Buildings
{
    /// <summary>
    /// Representation of a building
    /// </summary>
    public class Building : IEquatable<Building>
    {
        #region Fields

        private int[] _production;
        public readonly string ProductionImage;
        private readonly string _name;
        private int[] _points;
        private int[] _people;
        private readonly string _image;
        private readonly BuildingTypes _type;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the type of the building
        /// </summary>
        public BuildingTypes Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the name of the building
        /// </summary>
        public string Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the building image in BBCode
        /// </summary>
        public string BbCodeImage
        {
            get { return string.Format("[img]{0}[/img]", _image); }
        }

        /// <summary>
        /// Gets the building image
        /// </summary>
        public System.Drawing.Image Image
        {
            get
            {
                switch (_type)
                {
                    case BuildingTypes.Academy:
                        return BuildingImages.Academy;
                    case BuildingTypes.Barracks:
                        return BuildingImages.Barracks;
                    case BuildingTypes.ClayPit:
                        return BuildingImages.ClayPit;
                    case BuildingTypes.Farm:
                        return BuildingImages.Farm;
                    case BuildingTypes.HidingPlace:
                        return BuildingImages.HidingPlace;
                    case BuildingTypes.IronMine:
                        return BuildingImages.IronMine;
                    case BuildingTypes.Market:
                        return BuildingImages.Market;
                    case BuildingTypes.RallyPoint:
                        return BuildingImages.RallyPoint;
                    case BuildingTypes.Smithy:
                        return BuildingImages.Smithy;
                    case BuildingTypes.Stable:
                        return BuildingImages.Stable;
                    case BuildingTypes.Statue:
                        return BuildingImages.Statue;
                    case BuildingTypes.TimberCamp:
                        return BuildingImages.TimberCamp;
                    case BuildingTypes.VillageHeadquarters:
                        return BuildingImages.VillageHeadquarters;
                    case BuildingTypes.Wall:
                        return BuildingImages.Wall;
                    case BuildingTypes.Warehouse:
                        return BuildingImages.Warehouse;
                    case BuildingTypes.Workshop:
                        return BuildingImages.Workshop;
                    default:
                        return null;
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
        /// Gets or sets the comma seperated list of points per level
        /// </summary>
        public string Points
        {
            get
            {
                if (_points == null)
                    return "";

                return string.Join(",", _points.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _points = new int[] { };
                }
                else
                {
                    _points = value.Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();
                }
            }
        }

        /// <summary>
        /// Gets or sets the comma seperated list of production per level
        /// </summary>
        public string Production
        {
            get
            {
                if (_production == null)
                    return "";

                return string.Join(", ", _production.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _production = new int[] {};
                }
                else
                {
                    _production = value.Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();
                }
            }
        }

        /// <summary>
        /// Gets or sets the comma seperated list of required people per level
        /// </summary>
        public string People
        {
            get
            {
                if (_people == null)
                    return "";

                return string.Join(", ", _people.Select(x => x.ToString(CultureInfo.InvariantCulture)));
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _people = new int[] { };
                }
                else
                {
                    _people = value.Split(',').Select(x => Convert.ToInt32(x.Trim())).ToArray();
                }
            }
        }
        #endregion

        #region Constructors
        internal Building(Building build)
        {
            _image = build._image;
            _name = build._name;
            _people = build._people;
            _points = build._points;
            _production = build._production;
            _type = build._type;
            ProductionImage = build.ProductionImage;
        }

        internal Building(string name, string type, string image, string points, string people)
        {
            _name = name;
            _image = image;
            Points = points;
            if (people != null)
                People = people;

            if (Enum.IsDefined(typeof(BuildingTypes), type))
                _type = (BuildingTypes)Enum.Parse(typeof(BuildingTypes), type, true);
            else
                _type = BuildingTypes.None;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return _name;
        }

        /// <summary>
        /// Calculates required farm space for the specified level of the building
        /// </summary>
        /// <param name="level">The level of the building</param>
        public int GetTotalPeople(int level)
        {
            if (_people == null || level == 0) return 0;
            if (level > _people.Length) return _people[_people.Length - 1];
            return _people[level - 1];
        }

        /// <summary>
        /// Calculates the total points for the specified level
        /// </summary>
        /// <param name="level">The level of the building</param>
        public int GetTotalPoints(int level)
        {
            if (_points == null) return 0;
            if (level > _points.Length) return _points[_points.Length - 1];
            return _points[level - 1];
        }

        /// <summary>
        /// Production for Clay Pit, Timber Camp, Iron Mine
        /// For Warehouse and Farm it is total space
        /// For the wall it is the defense
        /// </summary>
        public int GetTotalProduction(int level)
        {
            if (_production == null) return 0;
            if (level > _production.Length) return _production[Production.Length - 1];
            return _production[level - 1];
        }

        /// <summary>
        /// Checks if the report contains info on the resource buildings
        /// </summary>
        /// <param name="buildingList">The building list for the report</param>
        public static bool HasResourceInfo(Dictionary<BuildingTypes, ReportBuilding> buildingList)
        {
            if (!buildingList.ContainsKey(BuildingTypes.Warehouse)) return false;
            if (!buildingList.ContainsKey(BuildingTypes.TimberCamp)) return false;
            if (!buildingList.ContainsKey(BuildingTypes.ClayPit)) return false;
            if (!buildingList.ContainsKey(BuildingTypes.IronMine)) return false;
            return true;
        }

        /// <summary>
        /// Checks if the list contains info on the resource buildings
        /// </summary>
        /// <param name="buildingList">The building list for the report</param>
        public static bool HasResourceInfo(IEnumerable<KeyValuePair<Building, int>> buildingList)
        {
            foreach (KeyValuePair<Building, int> building in buildingList)
            {
                if (building.Value == 0)
                switch (building.Key.Type)
                {
                    case BuildingTypes.Warehouse:
                    case BuildingTypes.ClayPit:
                    case BuildingTypes.IronMine:
                    case BuildingTypes.TimberCamp:
                        return false;
                }
            }
            return true;
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Converts a local building name to the Type
        /// </summary>
        public static BuildingTypes GetBuildingFromName(string name)
        {
            return WorldBuildings.Default[name].Type;
        }
        #endregion

        #region IEquatable<Building> Members
        public bool Equals(Building other)
        {
            if (other == null) return false;
            return _type == other._type;
        }
        #endregion
    }
}