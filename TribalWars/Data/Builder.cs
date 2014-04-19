#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Data.Maps;
using TribalWars.Data.Monitoring;
using TribalWars.Data.Events;
using TribalWars.Controls.Maps;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Maps.Drawers;
using TribalWars.Data.Maps.Markers;
using TribalWars.Data.Players;
using TribalWars.Data.Buildings;
using TribalWars.Data.Units;
using TribalWars.Data.Tribes;
using TribalWars.Tools;
using System.Globalization;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators;
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
        public static void ReadSettings(FileInfo file, Map map, Map miniMap)
        {
            if (file.Exists)
            {
                XmlReaderSettings sets = new XmlReaderSettings();
                sets.IgnoreWhitespace = true;
                sets.CloseInput = true;
                using (XmlReader r = XmlReader.Create(System.IO.File.Open(file.FullName, FileMode.Open, FileAccess.Read), sets))
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
                    r.Skip();

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
                    int z = System.Convert.ToInt32(r.GetAttribute("Zoom"));
                    DisplayTypes displayType = (DisplayTypes)Enum.Parse(typeof(DisplayTypes), r.GetAttribute("Display"), true);
                    if (displayType == DisplayTypes.None) displayType = DisplayTypes.Icon;
                    map.ChangeDisplay(displayType, new Location(x, y, z));

                    // MainMap: Display
                    r.ReadStartElement();
                    Color backgroundColor = XmlHelper.GetColor(r.GetAttribute("BackgroundColor"));
                    map.Display.BackgroundColor = backgroundColor;

                    r.ReadStartElement();
                    map.Display.ContinentLines = System.Convert.ToBoolean(r.ReadElementString("LinesContinent"));
                    map.Display.ProvinceLines = System.Convert.ToBoolean(r.ReadElementString("LinesProvince"));
                    map.Display.HideAbandoned = System.Convert.ToBoolean(r.ReadElementString("HideAbandoned"));
                    map.Display.MarkedOnly = System.Convert.ToBoolean(r.ReadElementString("MarkedOnly"));
                    r.ReadEndElement();

                    // MainMap: MarkerGroups
                    r.ReadStartElement();
                    List<MarkerGroup> markers = new List<MarkerGroup>();
                    while (r.IsStartElement("MarkerGroup"))
                    {
                        markers.Add(ReadMarkerGroup(r, map));
                    }
                    r.ReadEndElement();
                    map.MarkerManager.AddMarker(markers.ToArray());

                    // MainMap: Manipulators
                    r.ReadStartElement();
                    while (r.IsStartElement("Manipulator"))
                    {
                        ManipulatorManagerTypes manipulatorType = (ManipulatorManagerTypes)Enum.Parse(typeof(ManipulatorManagerTypes), r.GetAttribute("Type"));
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
            MarkerGroup m = new MarkerGroup(map, name, enabled, color, extraColor, view, decorator);

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
        private static void WriteMarkerGroup(XmlWriter w, MarkerGroup group, Map map)
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
        private static ViewBase ReadView(XmlReader r)
        {
            var category = (Categories)Enum.Parse(typeof(Categories), r.GetAttribute("Category"));
            var type = (Types)Enum.Parse(typeof(Types), r.GetAttribute("Type"));
            string name = r.GetAttribute("Name");
            ViewBase d = CreateView(name, type, category);

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

            return d;
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
        private static ViewBase CreateView(string name, Types type, Categories category)
        {
            switch (type)
            {
                case Types.Points:
                    return new PointsView(name);
                case Types.VillageType:
                    return new VillageTypeView(name);
                case Types.Defense:
                    return new DefenseView(name);
                case Types.Marked:
                    return new MarkView(name);
            }

            return null;
        }

        /// <summary>
        /// Write the settings file
        /// </summary>
        public static void WriteSettings(FileInfo file, Map map)
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
                WriteMarkerGroup(w, map.MarkerManager.YourMarker, map);
                WriteMarkerGroup(w, map.MarkerManager.YourTribeMarker, map);
                WriteMarkerGroup(w, map.MarkerManager.EnemyMarker, map);
                WriteMarkerGroup(w, map.MarkerManager.AbandonedMarker, map);
                w.WriteEndElement();

                w.WriteStartElement("Monitor");
                w.WriteEndElement();

                w.WriteStartElement("MainMap");
                w.WriteStartElement("Location");
                w.WriteAttributeString("Display", map.Display.DisplayManager.CurrentDisplayType.ToString());
                w.WriteAttributeString("XY", map.Location.X.ToString() + "|" + map.Location.Y.ToString());
                w.WriteAttributeString("Zoom", map.Location.Zoom.ToString());
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
                    WriteMarkerGroup(w, group, map);
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
            w.ServerOffset = new TimeSpan(0, 0, System.Convert.ToInt32(r.ReadElementString("Offset")));
            w.Speed = System.Convert.ToSingle(r.ReadElementString("Speed"), System.Globalization.CultureInfo.InvariantCulture);
            w.UnitSpeed = System.Convert.ToSingle(r.ReadElementString("UnitSpeed"), System.Globalization.CultureInfo.InvariantCulture);
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
            //ReadView(r); // comment
            //ReadView(r); // noble
            ReadView(r); // defense
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
                int pos = System.Convert.ToInt32(r.ReadElementString("Position"));
                string name = r.ReadElementString("Name");
                string shortname = r.ReadElementString("Short");
                string type = r.ReadElementString("Type");
                string image = r.ReadElementString("Image");
                int carry = System.Convert.ToInt32(r.ReadElementString("Carry"));
                bool farmer = System.Convert.ToBoolean(r.ReadElementString("Farmer"));
                bool hideAttacker = System.Convert.ToBoolean(r.ReadElementString("HideAttacker"));
                float speed = System.Convert.ToSingle(r.ReadElementString("Speed"), System.Globalization.CultureInfo.InvariantCulture);
                bool offense = System.Convert.ToBoolean(r.ReadElementString("Offense"));

                r.ReadStartElement();
                int wood = System.Convert.ToInt32(r.ReadElementString("Wood"));
                int clay = System.Convert.ToInt32(r.ReadElementString("Clay"));
                int iron = System.Convert.ToInt32(r.ReadElementString("Iron"));
                int people = System.Convert.ToInt32(r.ReadElementString("People"));

                r.ReadEndElement();
                r.ReadEndElement();

                Unit unit = new Unit(pos, name, shortname, type, image, carry, farmer, hideAttacker, wood, clay, iron, people, speed, offense);
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
