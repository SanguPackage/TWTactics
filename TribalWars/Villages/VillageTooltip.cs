using System.Drawing;
using System.Text;
using TribalWars.Controls;
using TribalWars.Maps.Icons;
using TribalWars.Maps.Manipulators.Implementations.Church;
using TribalWars.Tools;
using TribalWars.Worlds;

namespace TribalWars.Villages
{
    /// <summary>
    /// Provides map tooltip information for a village
    /// </summary>
    public class VillageTooltip
    {
        private readonly Village _village;
        private readonly ChurchInfo _church;

        public VillageTooltip(Village village)
        {
            _village = village;
            _church = World.Default.Map.Manipulators.ChurchManipulator.GetChurch(_village);
        }

        /// <summary>
        /// Gets the tooltip for the village
        /// </summary>
        public string Text
        {
            get
            {
                var str = new StringBuilder();

                // Calculate previous stuff
                Player prevPlayer = null;
                Tribe prevTribe = null;
                if (_village.PreviousVillageDetails != null)
                {
                    prevPlayer = _village.PreviousVillageDetails.Player;
                    if (prevPlayer != null)
                    {
                        if (prevPlayer.HasTribe) prevTribe = prevPlayer.Tribe;
                    }
                }

                // Start output
                if (_village.Type != VillageType.None && _village.Type != VillageType.Comments)
                {
					str.AppendFormat("{0}: {1}", _village.Type.GetDescription(), _village.PointsWithDiff);
                }
                else
                {
	                str.AppendFormat(ControlsRes.Tooltip_Points, _village.PointsWithDiff);
                }

                if (_village.HasPlayer)
                {
                    str.AppendLine();
					str.AppendFormat(ControlsRes.VillageTooltip_Owner, _village.Player.Name, _village.Player.Rank, Common.GetPrettyNumber(_village.Player.Points));
                    if (prevPlayer != null && !prevPlayer.Equals(_village.Player))
                    {
                        str.AppendLine();
						str.AppendFormat(ControlsRes.VillageTooltip_NobledFrom, prevPlayer.Name, prevPlayer.Rank, Common.GetPrettyNumber(prevPlayer.Points));
                    }
                    else if (prevPlayer == null && _village.PreviousVillageDetails != null)
                    {
                        str.AppendLine();
						str.Append(ControlsRes.VillageTooltip_NobledFromAbandoned);
                    }
                    str.AppendLine();
                    string conquers = _village.Player.ConquerString;
                    if (string.IsNullOrEmpty(conquers))
                    {
						str.AppendFormat(ControlsRes.Tooltip_Villages, Common.GetPrettyNumber(_village.Player.Villages.Count));
                    }
                    else
                    {
						str.AppendFormat(string.Format(ControlsRes.Tooltip_Villages, Common.GetPrettyNumber(_village.Player.Villages.Count)) + " ({0})", conquers);
                    }
                    if (_village.HasTribe)
                    {
                        str.AppendLine();
						str.AppendFormat(ControlsRes.Tooltip_TribeShort, _village.Player.Tribe);
                        if (prevTribe != null && !prevTribe.Equals(_village.Player.Tribe))
                        {
                            str.AppendLine();
							str.AppendFormat(ControlsRes.VillageTooltip_TribeChanged, prevTribe.Tag);
                        }
                    }
                }
                else
                {
                    str.AppendLine();
                    if (prevPlayer != null)
                    {
						str.AppendFormat(ControlsRes.VillageTooltip_AbandonedBy, prevPlayer.Name, Common.GetPrettyNumber(prevPlayer.Points));
                        if (prevPlayer.Villages.Count > 1)
                        {
                            str.AppendLine();
							str.AppendFormat(ControlsRes.Tooltip_Villages, Common.GetPrettyNumber(prevPlayer.Villages.Count));
                        }
                    }
                    else
                    {
						str.Append(ControlsRes.VillageTooltip_Abandoned);
                    }
                }

                return str.ToString();
            }
        }

        public string Title
        {
            get
            {
                if (_village.Player == null) return _village.ToString();
                return string.Format("{0} - {1}", _village, _village.Player.Name);
            }
        }

        public string Footer
        {
            get
            {
                var str = new StringBuilder();
                if (_church != null)
                {
					str.AppendLine(string.Format(ControlsRes.VillageTooltip_ChurchLevel, _church.ChurchLevel));
                }

                if (_village.HasComments)
                {
					str.AppendLine(ControlsRes.Tooltip_Comments);
                    str.AppendLine(_village.Comments);
                }

                return str.ToString();
            }
        }

        public Image FooterImage
        {
            get
            {
                if (_church != null)
                    return Properties.Resources.Church;

                if (_village.HasComments) 
                    return Other.Note;

                return null;
            }
        }
    }
}
