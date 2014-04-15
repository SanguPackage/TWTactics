#region Using
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Globalization;
#endregion

namespace TribalWars.Data.Players
{
    /// <summary>
    /// Representation of your player in the world
    /// </summary>
    public class PlayerYou
    {
        #region Fields
        private Player _player;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the active player
        /// </summary>
        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }
        #endregion

        #region Constructors
        private static readonly PlayerYou _instance = new PlayerYou();
        private PlayerYou()
        {

        }

        /// <summary>
        /// Gets the currently active player
        /// </summary>
        public static PlayerYou Default
        {
            get { return _instance; }
        }
        #endregion

        #region Public Methods
        public void ReadXml(XmlReader r)
        {
            string name = r.ReadElementString("Name").ToUpper(CultureInfo.InvariantCulture);
            if (World.Default.Players.ContainsKey(name))
                _player = World.Default.Players[name];


        }

   //   <MarkerGroup>
   //      <Name>You</Name>
   //      <Enabled>True</Enabled>
   //      <Markers>
   //         <Marker Type="XMarker">
   //            <Color>Black</Color>
   //            <Enabled>True</Enabled>
   //            <ExtraMarkColor>Blue</ExtraMarkColor>
   //         </Marker>
   //      </Markers>
   //      <Markers>
   //         <PlayerMarker>
   //            <Name>Grote Smurf</Name>
   //            <Group />
   //            <Marker Type="EmptyMarker" />
   //         </PlayerMarker>
   //      </Markers>
   //   </MarkerGroup>
   //   <MarkerGroup>
   //      <Name>Your Tribe</Name>
   //      <Enabled>True</Enabled>
   //      <Markers>
   //         <Marker Type="EllipseMarker">
   //            <Color>Black</Color>
   //            <Enabled>True</Enabled>
   //            <ExtraMarkColor>0</ExtraMarkColor>
   //         </Marker>
   //         <Marker Type="RectangleMarker">
   //            <Color>Black</Color>
   //            <Enabled>True</Enabled>
   //            <ExtraMarkColor>0</ExtraMarkColor>
   //         </Marker>
   //         <Marker Type="Ellipse2Marker">
   //            <Color>Black</Color>
   //            <Enabled>True</Enabled>
   //            <ExtraMarkColor>White</ExtraMarkColor>
   //         </Marker>
   //         <Marker Type="Rectangle2Marker">
   //            <Color>Black</Color>
   //            <Enabled>True</Enabled>
   //            <ExtraMarkColor>White</ExtraMarkColor>
   //         </Marker>
   //      </Markers>
   //      <Markers>
   //         <TribeMarker>
   //            <Tag>.XI.</Tag>
   //            <Group />
   //            <Marker Type="EmptyMarker" />
   //         </TribeMarker>
   //         <PlayerMarker>
   //            <Name>dwnrivertown</Name>
   //            <Group>C 777</Group>
   //            <Marker Type="XMarker">
   //               <Color>Yellow</Color>
   //               <Enabled>True</Enabled>
   //               <ExtraMarkColor>Yellow</ExtraMarkColor>
   //            </Marker>
   //         </PlayerMarker>
   //         <PlayerMarker>
   //            <Name>Grote Smurf</Name>
   //            <Group>C 777</Group>
   //            <Marker Type="XMarker">
   //               <Color>Pink</Color>
   //               <Enabled>True</Enabled>
   //               <ExtraMarkColor>Pink</ExtraMarkColor>
   //            </Marker>
   //         </PlayerMarker>
   //         <PlayerMarker>
   //            <Name>hoendroe</Name>
   //            <Group>C 777</Group>
   //            <Marker Type="XMarker">
   //               <Color>Blue</Color>
   //               <Enabled>True</Enabled>
   //               <ExtraMarkColor>Blue</ExtraMarkColor>
   //            </Marker>
   //         </PlayerMarker>
   //         <PlayerMarker>
   //            <Name>sjarlowitsky</Name>
   //            <Group>C 777</Group>
   //            <Marker Type="XMarker">
   //               <Color>Black</Color>
   //               <Enabled>True</Enabled>
   //               <ExtraMarkColor>Black</ExtraMarkColor>
   //            </Marker>
   //         </PlayerMarker>
   //         <PlayerMarker>
   //            <Name>We Are Enforced</Name>
   //            <Group>C 777</Group>
   //            <Marker Type="XMarker">
   //               <Color>Gray</Color>
   //               <Enabled>True</Enabled>
   //               <ExtraMarkColor>Gray</ExtraMarkColor>
   //            </Marker>
   //         </PlayerMarker>
   //      </Markers>
   //   </MarkerGroup>
   //</You>
        #endregion
    }
}
