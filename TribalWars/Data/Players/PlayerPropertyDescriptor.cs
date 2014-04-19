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
        private readonly bool _showExtended;
        #endregion

        #region Properties
        public Player Player { get; private set; }
        #endregion

        #region Constructors
        public PlayerPropertyDescriptor(Player ply, bool showExtended)
            : base(ply.Name, null)
        {
            Player = ply;
            _showExtended = showExtended;
        }
        #endregion

        #region Public Methods
        public override string ToString()
        {
            return Player.Points.ToString("#,0") + " points";
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
            if (_showExtended) return new ExtendedPlayerDescriptor(Player);
            return new PlayerDescriptor(Player);
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                if (_showExtended) return typeof(ExtendedPlayerDescriptor);
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
