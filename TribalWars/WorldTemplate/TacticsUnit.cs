using System;
using System.Collections.Generic;
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
        public UnitTypes Type { get; private set; }
        public string Name { get; private set; }
        public string ShortName { get; private set; }
        public bool Farmer { get; private set; }
        public bool HideAttacher { get; private set; }
        public bool Offense { get; private set; }

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
                        Type = (UnitTypes)Enum.Parse(typeof(UnitTypes), xmlUnit.Element("Type").Value, true),
                        Name = xmlUnit.Element("Name").Value,
                        ShortName = xmlUnit.Element("Short").Value,

                        Farmer = Convert.ToBoolean(xmlUnit.Element("Farmer").Value),
                        HideAttacher = Convert.ToBoolean(xmlUnit.Element("HideAttacker").Value),
                        Offense = Convert.ToBoolean(xmlUnit.Element("Offense").Value)
                    };

                units.Add(unit);
            }

            return units;
        }

        public override string ToString()
        {
            return string.Format("Type={0}, Name={1}", Type, Name);
        }
    }
}
