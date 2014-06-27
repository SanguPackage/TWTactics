#region Using
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using System.IO;
using System.Xml;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Maps;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Monitoring;
using TribalWars.Data.Players;
using TribalWars.Data.Buildings;
using TribalWars.Data.Units;
using TribalWars.Data.Tribes;
using TribalWars.Tools;
using System.Globalization;
using TribalWars.Data.Maps.Displays;
#endregion

namespace TribalWars.Data
{
    /// <summary>
    /// Creates a whole bunch of classes from XML files
    /// </summary>
    public class Builder
    {
        #region Setting Files
        /// <summary>
        /// Builds the classes from a sets file 
        /// </summary>
        public static void ReadSettings(FileInfo file, Map map, Map miniMap, Monitor monitor)
        {
            Debug.Assert(file.Exists);
            
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
                    World.Default.You.Player = ply;
                }

                map.MarkerManager.YourMarker = ReadMarkerGroup(r, map);
                map.MarkerManager.YourTribeMarker = ReadMarkerGroup(r, map);
                map.MarkerManager.EnemyMarker = ReadMarkerGroup(r, map);
                map.MarkerManager.AbandonedMarker = ReadMarkerGroup(r, map);
                //MarkerGroup enemyBonusMarker = ReadMarkerGroup(r, map);
                //map.Display.ViewManager.MarkerManager.AbandonedBonusMarker = enemyBonusMarker;
                //MarkerGroup abandonedBonus = ReadMarkerGroup(r, map);
                //map.Display.ViewManager.MarkerManager.EnemyBonusMarker = abandonedBonus;
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
                if (displayType == DisplayTypes.None) displayType = DisplayTypes.Icon;
                map.ChangeDisplay(displayType, new Location(x, y, z));
                map.HomeDisplay = displayType;

                // MainMap: Display
                r.ReadStartElement();
                Color backgroundColor = XmlHelper.GetColor(r.GetAttribute("BackgroundColor"));
                map.Display.BackgroundColor = backgroundColor;

                r.ReadStartElement();
                map.Display.ContinentLines = Convert.ToBoolean(r.ReadElementString("LinesContinent"));
                map.Display.ProvinceLines = Convert.ToBoolean(r.ReadElementString("LinesProvince"));
                map.Display.HideAbandoned = Convert.ToBoolean(r.ReadElementString("HideAbandoned"));
                map.Display.MarkedOnly = Convert.ToBoolean(r.ReadElementString("MarkedOnly"));
                r.ReadEndElement();

                // MainMap: MarkerGroups
                r.ReadStartElement();
                var markers = new List<MarkerGroup>();
                while (r.IsStartElement("MarkerGroup"))
                {
                    markers.Add(ReadMarkerGroup(r, map));
                }
                map.MarkerManager.AddMarker(markers.ToArray());

                // MainMap: Manipulators
                r.ReadToFollowing("Manipulator");
                while (r.IsStartElement("Manipulator"))
                {
                    var manipulatorType = (ManipulatorManagerTypes)Enum.Parse(typeof(ManipulatorManagerTypes), r.GetAttribute("Type"));
                    Dictionary<ManipulatorManagerTypes, ManipulatorManagerBase> dict = map.Manipulators.Manipulators;
                    if (dict.ContainsKey(manipulatorType))
                    {
                        dict[manipulatorType].ReadXml(r);
                    }
                    else
                    {
                        r.Skip();
                    }
                }
                r.ReadEndElement();

                // End Main Map
                r.ReadEndElement();
            }
        }

        /// <summary>
        /// Reads a MarkerGroup from the XML node
        /// </summary>
        private static MarkerGroup ReadMarkerGroup(XmlReader r, Map map)
        {
            string name = r.GetAttribute("Name");
            bool enabled = Convert.ToBoolean(r.GetAttribute("Enabled").ToLower());
            Color color = XmlHelper.GetColor(r.GetAttribute("Color"));
            Color extraColor = XmlHelper.GetColor(r.GetAttribute("ExtraColor"));
            string view = r.GetAttribute("View");
            string decorator = r.GetAttribute("Decorator");
            var m = new MarkerGroup(map, name, enabled, color, extraColor, view, decorator);

            if (!r.IsEmptyElement)
            {
                r.ReadStartElement();
                while (r.IsStartElement("Marker"))
                {
                    string markerType = r.GetAttribute("Type");
                    string markerName = r.GetAttribute("Name");
                    CreateMarker(m, markerType, markerName);
                    r.Read();
                }
                r.ReadEndElement();
            }
            else
                r.Read();
           
            return m;
        }

        /// <summary>
        /// Writes a markergroup to the XML node
        /// </summary>
        private static void WriteMarkerGroup(XmlWriter w, MarkerGroup group)
        {
            w.WriteStartElement("MarkerGroup");
            w.WriteAttributeString("Name", group.Name);
            w.WriteAttributeString("Enabled", group.Enabled.ToString());
            w.WriteAttributeString("Color", XmlHelper.SetColor(group.Color));
            w.WriteAttributeString("ExtraColor", XmlHelper.SetColor(group.ExtraColor));
            w.WriteAttributeString("View", group.View);
            if (group.HasDecorator)
                w.WriteAttributeString("Decorator", group.Decorator);

            foreach (Player ply in group.Players)
            {
                w.WriteStartElement("Marker");
                w.WriteAttributeString("Type", "Player");
                w.WriteAttributeString("Name", ply.Name);
                w.WriteEndElement();
            }

            foreach (Tribe tribe in group.Tribes)
            {
                w.WriteStartElement("Marker");
                w.WriteAttributeString("Type", "Tribe");
                w.WriteAttributeString("Name", tribe.Tag);
                w.WriteEndElement();
            }

            w.WriteEndElement();
        }

        /// <summary>
        /// Reads a view from the XML node
        /// </summary>
        private static void ReadView(XmlReader r)
        {
            //var category = (Categories)Enum.Parse(typeof(Categories), r.GetAttribute("Category"));
            var type = (Types)Enum.Parse(typeof(Types), r.GetAttribute("Type"));
            string name = r.GetAttribute("Name");
            ViewBase d = CreateView(name, type);

            r.ReadStartElement();
            while (r.IsStartElement("Drawer"))
            {
                string drawerType = r.GetAttribute("Type");
                string drawerIcon = r.GetAttribute("Icon");
                string drawerValue = r.GetAttribute("Value");
                string drawerExtraValue = r.GetAttribute("ExtraValue");
                d.AddDrawer(drawerType, drawerIcon, Convert.ToInt32(drawerValue), drawerExtraValue);
                r.Read();
            }
            r.ReadEndElement();

            World.Default.Views.Add(d.Name, d);
        }

        /// <summary>
        /// Creates a MarkerGroup from the XML node
        /// </summary>
        private static void CreateMarker(MarkerGroup m, string type, string value)
        {
            switch (type)
            {
                /*case "Village":
                    Village village = World.Default.GetVillage(value);
                    if (village != null)
                        new VillageMarker(village);
                    break;*/

                case "Player":
                    Player ply = World.Default.GetPlayer(value);
                    if (ply != null)
                        m.Add(new PlayerMarker(ply));
                    break;

                case "Tribe":
                    Tribe tribe = World.Default.GetTribe(value);
                    if (tribe != null)
                        //return new TribeMarker(tribe);
                        m.Add(new TribeMarker(tribe));
                    break;
            }
        }

        /// <summary>
        /// Creates a view from the XML node
        /// </summary>
        private static ViewBase CreateView(string name, Types type)
        {
            switch (type)
            {
                case Types.Points:
                    return new PointsView(name);
                case Types.VillageType:
                    return new VillageTypeView(name);
            }

            Debug.Assert(false);
            return null;
        }

        /// <summary>
        /// Write the settings file
        /// </summary>
        public static void WriteSettings(FileInfo file, Map map, Monitor monitor)
        {
            var sets = new XmlWriterSettings();
            sets.Indent = true;
            sets.IndentChars = " ";
            using (XmlWriter w = XmlWriter.Create(file.FullName, sets))
            {
                w.WriteStartElement("Settings");
                w.WriteAttributeString("Date", DateTime.Now.ToLongDateString());

                w.WriteStartElement("You");
                w.WriteAttributeString("Name", World.Default.You.Player.Name);
                WriteMarkerGroup(w, map.MarkerManager.YourMarker);
                WriteMarkerGroup(w, map.MarkerManager.YourTribeMarker);
                WriteMarkerGroup(w, map.MarkerManager.EnemyMarker);
                WriteMarkerGroup(w, map.MarkerManager.AbandonedMarker);
                w.WriteEndElement();

                monitor.WriteXml(w);

                w.WriteStartElement("MainMap");
                w.WriteStartElement("Location");
                w.WriteAttributeString("Display", map.Display.DisplayManager.CurrentDisplayType.ToString());
                w.WriteAttributeString("XY", map.Location.X + "|" + map.Location.Y);
                w.WriteAttributeString("Zoom", map.Location.Zoom.ToString(CultureInfo.InvariantCulture));
                w.WriteEndElement();

                w.WriteStartElement("Display");
                w.WriteAttributeString("BackgroundColor", XmlHelper.SetColor(map.Display.BackgroundColor));
                w.WriteElementString("LinesContinent", map.Display.ContinentLines.ToString());
                w.WriteElementString("LinesProvince", map.Display.ProvinceLines.ToString());
                w.WriteElementString("HideAbandoned", map.Display.HideAbandoned.ToString());
                w.WriteElementString("MarkedOnly", map.Display.MarkedOnly.ToString());
                w.WriteEndElement();

                w.WriteStartElement("MarkerGroups");
                foreach (MarkerGroup group in map.MarkerManager.Markers)
                {
                    WriteMarkerGroup(w, group);
                }
                w.WriteEndElement();

                // Manipulators
                w.WriteStartElement("Manipulators");
                foreach (KeyValuePair<ManipulatorManagerTypes, ManipulatorManagerBase> pair in map.Manipulators.Manipulators)
                {
                    w.WriteStartElement("Manipulator");
                    w.WriteAttributeString("Type", pair.Key.ToString());
                    pair.Value.WriteXml(w);
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
        public static void ReadWorld(XmlReader r, Map map, Map miniMap)
        {
            World w = World.Default;

            r.Read();
            r.Read();
            w.Server = new Uri(r.ReadElementString("Server"));
            w.Name = r.ReadElementString("Name");
            w.ServerOffset = new TimeSpan(0, 0, Convert.ToInt32(r.ReadElementString("Offset"))); // Offset in seconds (ex: -3600 for minus one hour)
            w.Speed = Convert.ToSingle(r.ReadElementString("Speed"), CultureInfo.InvariantCulture);
            w.UnitSpeed = Convert.ToSingle(r.ReadElementString("UnitSpeed"), CultureInfo.InvariantCulture);
            w.Culture = new CultureInfo(r.ReadElementString("Culture"));

            r.ReadStartElement();
            w.Structure.DownloadVillage = r.ReadElementString("Village");
            w.Structure.DownloadPlayer = r.ReadElementString("Player");
            w.Structure.DownloadTribe = r.ReadElementString("Tribe");
            r.ReadEndElement();

            r.ReadStartElement();
            w.GameLink = r.ReadElementString("Village");
            r.ReadEndElement();

            r.ReadStartElement();
            w.TwStats.Default = new Uri(r.ReadElementString("General"));
            w.TwStats.Village = r.ReadElementString("Village");
            w.TwStats.Player = r.ReadElementString("Player");
            w.TwStats.Tribe = r.ReadElementString("Tribe");
            w.TwStats.PlayerGraph = r.ReadElementString("PlayerGraph");
            w.TwStats.TribeGraph = r.ReadElementString("TribeGraph");
            r.ReadEndElement();

            // Views
            r.Read();
            World.Default.Views.Clear();
            ReadView(r); // points
            ReadView(r); // pointsabandoned
            ReadView(r); // pointsbonus
            ReadView(r); // pointsabandonedbonus
            ReadView(r); // type
            r.ReadEndElement();

            r.ReadStartElement();
            WorldBuildings.Default.SetBuildings(ReadWorldBuildings(r));
            r.ReadEndElement();

            r.ReadStartElement();
            WorldUnits.Default.SetBuildings(ReadWorldUnits(r));
            r.ReadEndElement();
        }

        /// <summary>
        /// Loads the buildings from the World.xml stream
        /// </summary>
        private static Dictionary<BuildingTypes, Building> ReadWorldBuildings(XmlReader r)
        {
            var buildings = new Dictionary<BuildingTypes, Building>();
            while (r.IsStartElement("Building"))
            {
                r.ReadStartElement();
                string name = r.ReadElementString("Name");
                string type = r.ReadElementString("Type");
                string image = r.ReadElementString("Image");
                string points = r.ReadElementString("Points");
                string people = null;
                string production = null;
                if (r.IsStartElement("Production")) production = r.ReadElementString("Production");
                if (r.IsStartElement("People")) people = r.ReadElementString("People");
                var build = new Building(name, type, image, points, people);
                if (production != null) build.Production = production;
                r.ReadEndElement();

                buildings.Add(build.Type, build);
                
            }
            return buildings;
        }

        /// <summary>
        /// Loads the units from the World.xml stream
        /// </summary>
        private static Dictionary<UnitTypes, Unit> ReadWorldUnits(XmlReader r)
        {
            var  units = new Dictionary<UnitTypes, Unit>();
            while (r.IsStartElement("Unit"))
            {
                r.ReadStartElement();
                int pos = Convert.ToInt32(r.ReadElementString("Position"));
                string name = r.ReadElementString("Name");
                string shortname = r.ReadElementString("Short");
                string type = r.ReadElementString("Type");
                string image = r.ReadElementString("Image");
                int carry = Convert.ToInt32(r.ReadElementString("Carry"));
                bool farmer = Convert.ToBoolean(r.ReadElementString("Farmer"));
                bool hideAttacker = Convert.ToBoolean(r.ReadElementString("HideAttacker"));
                float speed = Convert.ToSingle(r.ReadElementString("Speed"), CultureInfo.InvariantCulture);
                bool offense = Convert.ToBoolean(r.ReadElementString("Offense"));

                r.ReadStartElement();
                int wood = Convert.ToInt32(r.ReadElementString("Wood"));
                int clay = Convert.ToInt32(r.ReadElementString("Clay"));
                int iron = Convert.ToInt32(r.ReadElementString("Iron"));
                int people = Convert.ToInt32(r.ReadElementString("People"));

                r.ReadEndElement();
                r.ReadEndElement();

                var unit = new Unit(pos, name, shortname, type, image, carry, farmer, hideAttacker, wood, clay, iron, people, speed, offense);
                units.Add(unit.Type, unit);
            }
            return units;
        }

        /// <summary>
        /// Write the views to the world.xml
        /// </summary>
        private static void WriteView(XmlWriter w, KeyValuePair<string, ViewBase> pair)
        {
            ViewBase view = pair.Value;

            w.WriteStartElement("View");
            w.WriteAttributeString("Category", view.Category.ToString());
            w.WriteAttributeString("Type", view.Type.ToString());
            w.WriteAttributeString("Name", view.Name);
            /*foreach (DrawerData data in view.Drawers)
            {

            }*/
            w.WriteEndElement();
        }
        #endregion
    }
}
