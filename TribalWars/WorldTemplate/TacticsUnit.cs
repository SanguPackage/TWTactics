using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TribalWars.Data.Units;

namespace TribalWars.WorldTemplate
{
    /// <summary>
    /// Reading fixed world Unit information from WorldTemplate\Units.xml
    /// for all unit info that is not in official TW API.
    /// </summary>
    public class TacticsUnit
    {
        public UnitTypes Type { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public bool Farmer { get; set; }
        public bool HideAttacher { get; set; }
        public bool Offense { get; set; }

        private TacticsUnit()
        {
            
        }

        public static List<TacticsUnit> GetUnitsFromXml(string xmlUri)
        {
            var units = new List<TacticsUnit>();

            var xml = XDocument.Load(xmlUri);
            foreach (var xmlUnit in xml.Root.Elements())
            {
                var unit = new TacticsUnit
                    {
                        Type = (UnitTypes)Enum.Parse(typeof(UnitTypes), xmlUnit.Element("Name").Value, true),
                        Name = xmlUnit.Element("Name").Value,
                        ShortName = xmlUnit.Element("Short").Value,

                        Farmer = Convert.ToBoolean(xmlUnit.Element("Farmer").Value),
                        HideAttacher = Convert.ToBoolean(xmlUnit.Element("HideAttacher").Value),
                        Offense = Convert.ToBoolean(xmlUnit.Element("Offense").Value)
                    };

                units.Add(unit);
            }

            return units;
        }
    }
}
