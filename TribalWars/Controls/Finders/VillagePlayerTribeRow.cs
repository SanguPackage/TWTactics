using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TribalWars.Villages;
using TribalWars.Villages.Helpers;
using TribalWars.Worlds;

namespace TribalWars.Controls.Finders
{
    public class VillagePlayerTribeRow
    {
        private IVisible _visibilityGetter;

        public bool IsVillage { get; set; }
        public bool IsPlayer { get; set; }
        public bool IsTribe { get; set; }

        public string Value { get; set; }
        public string Text { get; set; }

        public int ImageIndex
        {
            get
            {
                if (IsVillage) return 0;
                if (IsPlayer) return 1;
                if (IsTribe) return 2;
                return -1;
            }
        }

        public Image VisibleImage
        {
            get
            {
                if (_visibilityGetter.IsVisible(World.Default.Map))
                {
                    return Properties.Resources.Visible;
                }
                return null;
            }
        }

        public VillagePlayerTribeRow(Village village)
        {
            _visibilityGetter = village;
            Value = village.LocationString;
            Text = string.Format("{0}", village.HasPlayer ? village.Player.Name : village.Name);
            IsTribe = true;
        }

        public VillagePlayerTribeRow(Tribe tribe)
        {
            _visibilityGetter = tribe;
            Value = tribe.Tag;
            Text = string.Format("#{1} ({0} points)", Tools.Common.GetPrettyNumber(tribe.AllPoints), tribe.Rank);
            IsTribe = true;
        }

        public VillagePlayerTribeRow(Player player)
        {
            _visibilityGetter = player;
            Value = player.Name;
            Text = string.Format("#{1} ({0} points)", Tools.Common.GetPrettyNumber(player.Points), player.Rank);
            IsPlayer = true;
        }
    }
}
