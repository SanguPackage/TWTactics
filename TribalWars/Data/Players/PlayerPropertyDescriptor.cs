using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using TribalWars.Data.Tribes;

namespace TribalWars.Data.Players
{
    /// <summary>
    /// Used in the PlayerCollection
    /// </summary>
    [TypeConverter(typeof(Tools.PropertySorter))]
    public class PlayerPropertyDescriptor : PropertyDescriptor
    {
        #region Fields
        private Player player = null;
        bool ShowExtended;
        #endregion

        #region Properties
        public Player Player
        {
            get { return player; }
        }
        #endregion

        #region Constructors
        public PlayerPropertyDescriptor(Player ply, bool showExtended)
            : base(ply.Name, null)
        {
            player = ply;
            ShowExtended = showExtended;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return player.Points.ToString("#,0") + " points";
        }
        #endregion

        #region PropertyDescriptor Methods
        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return typeof(Tribe); }
        }

        public override object GetValue(object component)
        {
            if (ShowExtended) return new ExtendedPlayerDescriptor(player);
            return new PlayerDescriptor(player);
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                if (ShowExtended) return typeof(ExtendedPlayerDescriptor);
                return typeof(PlayerDescriptor);
            }
        }

        public override void ResetValue(object component)
        {

        }

        public override void SetValue(object component, object value)
        {

        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }
        #endregion
    }
}
