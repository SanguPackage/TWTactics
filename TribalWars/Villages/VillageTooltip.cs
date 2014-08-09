using System.Drawing;
using System.Text;
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
                int prevPoints = 0;
                Player prevPlayer = null;
                Tribe prevTribe = null;
                if (_village.PreviousVillageDetails != null)
                {
                    prevPoints = _village.PreviousVillageDetails.Points;
                    prevPlayer = _village.PreviousVillageDetails.Player;
                    if (prevPlayer != null)
                    {
                        if (prevPlayer.HasTribe) prevTribe = prevPlayer.Tribe;
                    }
                }

                // Start output
                string pointsPrefix;
                if (_village.Type != VillageType.None && _village.Type != VillageType.Comments)
                {
                    pointsPrefix = _village.Type.GetDescription();
                }
                else
                {
                    pointsPrefix = "Points";
                }

                str.AppendFormat("{0}: {1} points", pointsPrefix, Common.GetPrettyNumber(_village.Points));
                if (prevPoints != 0 && prevPoints != _village.Points)
                {
                    int dif = _village.Points - prevPoints;
                    if (dif < 0) str.AppendFormat(" ({0})", Common.GetPrettyNumber(dif));
                    else str.AppendFormat(" (+{0})", Common.GetPrettyNumber(dif));
                }


                if (_village.HasPlayer)
                {
                    str.AppendLine();
                    str.AppendFormat("Owner: {0} (#{1} | {2} points)", _village.Player.Name, _village.Player.Rank, Common.GetPrettyNumber(_village.Player.Points));
                    if (prevPlayer != null && !prevPlayer.Equals(_village.Player))
                    {
                        str.AppendLine();
                        str.AppendFormat("Nobled from {0} (#{1}|{2} points)", prevPlayer.Name, prevPlayer.Rank, Common.GetPrettyNumber(prevPlayer.Points));
                    }
                    else if (prevPlayer == null && _village.PreviousVillageDetails != null)
                    {
                        str.AppendLine();
                        str.Append("Nobled abandoned");
                    }
                    str.AppendLine();
                    string conquers = _village.Player.ConquerString;
                    if (string.IsNullOrEmpty(conquers))
                    {
                        str.AppendFormat("Villages: {0}", Common.GetPrettyNumber(_village.Player.Villages.Count));
                    }
                    else
                    {
                        str.AppendFormat("Villages: {0} ({1})", Common.GetPrettyNumber(_village.Player.Villages.Count), conquers);
                    }
                    if (_village.HasTribe)
                    {
                        str.AppendLine();
                        str.AppendFormat("Tribe: {0} (#{1} | {2} points)", _village.Player.Tribe.Tag, _village.Player.Tribe.Rank, Common.GetPrettyNumber(_village.Player.Tribe.AllPoints));
                        if (prevTribe != null && !prevTribe.Equals(_village.Player.Tribe))
                        {
                            str.AppendLine();
                            str.AppendFormat("Changed from {0}", prevTribe.Tag);
                        }
                    }
                }
                else
                {
                    str.AppendLine();
                    if (prevPlayer != null)
                    {
                        str.AppendFormat("Abandoned by {0} ({1})", prevPlayer.Name, Common.GetPrettyNumber(prevPlayer.Points));
                        if (prevPlayer.Villages.Count > 1)
                        {
                            str.AppendLine();
                            str.AppendFormat("Villages: {0}", Common.GetPrettyNumber(prevPlayer.Villages.Count));
                        }
                    }
                    else
                    {
                        str.Append("Abandoned");
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
                    str.AppendLine(string.Format("Church level {0}", _church.ChurchLevel));
                }

                if (_village.HasComments)
                {
                    str.AppendLine("Comments:");
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
