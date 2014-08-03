#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using TribalWars.Maps;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Manipulators.Managers;
using TribalWars.Maps.Monitoring;
using TribalWars.Maps.Views;
using TribalWars.Tools;
using System.Globalization;
using TribalWars.Villages;
using TribalWars.Villages.Buildings;
using TribalWars.Villages.Units;
using TribalWars.WorldTemplate;
using World = TribalWars.Worlds.World;

#endregion

namespace TribalWars.Worlds
{
    /// <summary>
    /// Creates a whole bunch of classes from XML files
    /// </summary>
    public static class Builder
    {
        #region Setting Files
        /// <summary>
        /// Builds the classes from a sets file 
        /// </summary>
        public static DisplaySettings ReadSettings(FileInfo file, Map map, Monitor monitor)
        {
            Debug.Assert(file.Exists);

            var newReader = XDocument.Load(file.FullName);

            var sets = new XmlReaderSettings();
            sets.IgnoreWhitespace = true;
            sets.CloseInput = true;
            using (XmlReader r = XmlReader.Create(File.Open(file.FullName, FileMode.Open, FileAccess.Read), sets))
            {
                r.ReadStartElement();
                //DateTime date = XmlConvert.ToDateTime(r.GetAttribute("Date"));

                // You
                string youString = r.GetAttribute("Name");
                r.ReadStartElement();
                Player ply = World.Default.GetPlayer(youString);
                if (ply != null)
                {
                    World.Default.You = ply;
                }
                else
                {
                    World.Default.You = new Player();
                }

                map.MarkerManager.ReadDefaultMarkers(r);

                r.ReadEndElement();

                // Monitor
                monitor.ReadXml(r);

                // MainMap
                r.ReadStartElement();

                // MainMap: Location
                Point? location = World.Default.GetCoordinates(r.GetAttribute("XY"));
                int x = 500;
                int y = 500;
                if (location.HasValue)
                {
                    x = location.Value.X;
                    y = location.Value.Y;
                }
                int z = Convert.ToInt32(r.GetAttribute("Zoom"));
                var displayType = (DisplayTypes)Enum.Parse(typeof(DisplayTypes), r.GetAttribute("Display"), true);
                map.HomeLocation = new Location(displayType, x, y, z);

                // MainMap: Display
                r.ReadStartElement();
                Color backgroundColor = XmlHelper.GetColor(r.GetAttribute("BackgroundColor"));

                r.ReadStartElement();
                bool continentLines = Convert.ToBoolean(r.ReadElementString("LinesContinent"));
                bool provinceLines = Convert.ToBoolean(r.ReadElementString("LinesProvince"));
                bool hideAbandoned = Convert.ToBoolean(r.ReadElementString("HideAbandoned"));
                bool markedOnly = Convert.ToBoolean(r.ReadElementString("MarkedOnly"));
                r.ReadEndElement();

                var displaySettings = new DisplaySettings(backgroundColor, continentLines, provinceLines, hideAbandoned, markedOnly);

                // MainMap: Markers
                r.ReadStartElement();
                map.MarkerManager.ReadUserDefinedMarkers(r);

                // MainMap: Manipulators
                Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase> dict = map.Manipulators.Manipulators;
                dict.ForEach(d => d.Value.CleanUp());

                r.ReadToFollowing("Manipulator");
                while (r.IsStartElement("Manipulator"))
                {
                    var manipulatorType = (ManipulatorManagerTypes)Enum.Parse(typeof(ManipulatorManagerTypes), r.GetAttribute("Type"));
                    if (dict.ContainsKey(manipulatorType))
                    {
                        if (dict[manipulatorType].UseLegacyXmlWriter)
                        {
                            dict[manipulatorType].ReadXml(r);
                        }
                        else
                        {
                            r.Skip();
                            dict[manipulatorType].ReadXml(newReader);
                        }
                    }
                    else
                    {
                        r.Skip();
                    }
                }
                r.ReadEndElement();

                // End Main Map
                r.ReadEndElement();

                return displaySettings;
            }
        }

        /// <summary>
        /// Write the settings file
        /// </summary>
        public static void WriteSettings(FileInfo file, Map map, Monitor monitor)
        {
            // There is the old and the new 'way'
            // old = XmlReader/Writer: error prone, difficult maintenance
            // new = XDocument/Linq: easier...

            var sets = new XmlWriterSettings();
            sets.Indent = true;
            sets.IndentChars = " ";
            using (XmlWriter w = XmlWriter.Create(file.FullName, sets))
            {
                w.WriteStartElement("Settings");
                w.WriteAttributeString("Date", DateTime.Now.ToLongDateString());

                w.WriteStartElement("You");
                w.WriteAttributeString("Name", World.Default.You.Name);
                map.MarkerManager.WriteDefaultMarkers(w);
                w.WriteEndElement();

                monitor.WriteXml(w);

                w.WriteStartElement("MainMap");
                w.WriteStartElement("Location");
                w.WriteAttributeString("Display", map.HomeLocation.Display.ToString());
                w.WriteAttributeString("XY", map.HomeLocation.X + "|" + map.HomeLocation.Y);
                w.WriteAttributeString("Zoom", map.HomeLocation.Zoom.ToString(CultureInfo.InvariantCulture));
                w.WriteEndElement();

                w.WriteStartElement("Display");
                w.WriteAttributeString("BackgroundColor", XmlHelper.SetColor(map.Display.Settings.BackgroundColor));
                w.WriteElementString("LinesContinent", map.Display.Settings.ContinentLines.ToString());
                w.WriteElementString("LinesProvince", map.Display.Settings.ProvinceLines.ToString());
                w.WriteElementString("HideAbandoned", map.Display.Settings.HideAbandoned.ToString());
                w.WriteElementString("MarkedOnly", map.Display.Settings.MarkedOnly.ToString());
                w.WriteEndElement();

                w.WriteStartElement("Markers");
                map.MarkerManager.WriteUserDefinedMarkers(w);
                w.WriteEndElement();

                // Manipulators
                w.WriteStartElement("Manipulators");
                foreach (KeyValuePair<ManipulatorManagerTypes, ManipulatorManagerBase> pair in map.Manipulators.Manipulators)
                {
                    w.WriteStartElement("Manipulator");
                    w.WriteAttributeString("Type", pair.Key.ToString());

                    if (pair.Value.UseLegacyXmlWriter)
                    {
                        pair.Value.WriteXml(w);
                    }
                    else
                    {
                        string resultingXml = pair.Value.WriteXml();
                        w.WriteRaw(resultingXml);
                    }
                    w.WriteEndElement();
                }
                w.WriteEndElement();

                // end MainMap
                w.WriteEndElement();

                // end Settings
                w.WriteEndElement();
            }
        }
        #endregion

        #region World Files
        /// <summary>
        /// Reads the world settings
        /// </summary>
        public static void ReadWorld(string worldXmlPath)
        {
            World w = World.Default;
            var info = WorldTemplate.WorldConfiguration.LoadFromFile(worldXmlPath);

            w.Settings.Server = new Uri(info.Server);
            w.Settings.Name = info.Name;
            w.Settings.ServerOffset = new TimeSpan(Convert.ToInt32(info.Offset), 0, 0);
            w.Settings.Speed = Convert.ToSingle(info.Speed, CultureInfo.InvariantCulture);
            w.Settings.UnitSpeed = Convert.ToSingle(info.UnitSpeed, CultureInfo.InvariantCulture);

            w.Structure.DownloadVillage = info.DataVillage;
            w.Structure.DownloadPlayer = info.DataPlayer;
            w.Structure.DownloadTribe = info.DataTribe;

            w.Settings.GameLink = info.GameVillage;
            w.Settings.GuestPlayerLink = info.GuestPlayer;
            w.Settings.GuestTribeLink = info.GuestTribe;

            w.Settings.TwStats.Default = new Uri(info.TWStatsGeneral);
            w.Settings.TwStats.Village = info.TWStatsVillage;
            w.Settings.TwStats.Player = info.TWStatsPlayer;
            w.Settings.TwStats.Tribe = info.TWStatsTribe;
            w.Settings.TwStats.PlayerGraph = info.TWStatsPlayerGraph;
            w.Settings.TwStats.TribeGraph = info.TWStatsTribeGraph;

            w.Settings.IconScenery = (IconDrawerFactory.Scenery)Convert.ToInt32(info.WorldDatScenery);

            var views = new List<ViewBase>();
            foreach (var view in info.Views)
            {
                ViewBase viewToAdd = CreateView(view.Name, view.Type);
                foreach (WorldConfigurationViewsViewDrawersDrawer drawer in view.Drawers)
                {
                    viewToAdd.AddDrawer(drawer);
                }
                views.Add(viewToAdd);
            }
            World.Default.SetViews(views);

            WorldBuildings.Default.SetBuildings(ReadWorldBuildings(info.Buildings));
            WorldUnits.Default.SetUnits(ReadWorldUnits(info.Units));
        }

        /// <summary>
        /// Creates a view from the XML node
        /// </summary>
        private static ViewBase CreateView(string name, string type)
        {
            switch (type)
            {
                case "Points":
                    return new PointsView(name);
                case "VillageType":
                    return new VillageTypeView(name);
            }

            Debug.Assert(false);
            return null;
        }

        /// <summary>
        /// Loads the buildings from the World.xml stream
        /// </summary>
        private static Dictionary<BuildingTypes, Building> ReadWorldBuildings(IEnumerable<WorldConfigurationBuildingsBuilding> buildingsIn)
        {
            var buildingsOut = new Dictionary<BuildingTypes, Building>();
            foreach (var building in buildingsIn)
            {
                var build = new Building(building.Name, building.Type, building.Image, building.Points, building.People);
                build.Production = building.Production;
                buildingsOut.Add(build.Type, build);
            }
            return buildingsOut;
        }

        /// <summary>
        /// Loads the units from the World.xml stream
        /// </summary>
        private static Dictionary<UnitTypes, Unit> ReadWorldUnits(IEnumerable<WorldConfigurationUnitsUnit> unitsIn)
        {
            var  unitsOut = new Dictionary<UnitTypes, Unit>();
            foreach (var unit in unitsIn)
            {
                int carry = Convert.ToInt32(unit.Carry);
                float speed = Convert.ToSingle(unit.Speed, CultureInfo.InvariantCulture);

                bool farmer = Convert.ToBoolean(unit.Farmer);
                bool hideAttacker = Convert.ToBoolean(unit.HideAttacker);
                bool offense = Convert.ToBoolean(unit.Offense);

                int people = Convert.ToInt32(unit.CostPeople);
                int wood = Convert.ToInt32(unit.CostWood);
                int clay = Convert.ToInt32(unit.CostClay);
                int iron = Convert.ToInt32(unit.CostIron);

                var u = new Unit(Convert.ToInt32(unit.Position), unit.Name, unit.Short, unit.Type, carry, farmer, hideAttacker, wood, clay, iron, people, speed, offense);

                unitsOut.Add(u.Type, u);
            }
            return unitsOut;
        }
        #endregion
    }
}
