#region Using
using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Villages;
using TribalWars.Data.Tribes;
using TribalWars.Data.Descriptors;
#endregion

namespace TribalWars.Data.Players
{
    /// <summary>
    /// Stand alone player descriptor
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter)), DefaultProperty("Villages")]
    public class ExtendedPlayerDescriptor
    {
        #region Constants
        protected const string PROPERTY_CATEGORY = "Player";
        #endregion

        #region Fields
        public Player Player;
        #endregion

        #region Constructors
        public ExtendedPlayerDescriptor(Player ply)
        {
            Player = ply;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Player.Name;
        }
        #endregion

        #region Properties
        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(10)]
        public string Name
        {
            get { return Player.Name; }
            set { }
        }

        [Category(PROPERTY_CATEGORY), TypeConverter(typeof(ExpandableObjectConverter)), Tools.PropertyOrder(15)]
        public VillageCollection Villages
        {
            get { return new VillageCollection(Player.Villages); }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(20)]
        public TribeDescriptor Tribe
        {
            get
            {
                if (Player.HasTribe) return new TribeDescriptor(Player.Tribe);
                return null;
            }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(40)]
        public string Points
        {
            get { return Player.Points.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(50)]
        public string Rank
        {
            get { return Player.Rank.ToString("#,0"); }
            set { }
        }

        [Category(PROPERTY_CATEGORY), Tools.PropertyOrder(55), Editor(typeof(ClipboardCopierUiEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public string BBCode
        {
            get { return Player.BbCode(); }
            set { }
        }
        #endregion
    }
}
