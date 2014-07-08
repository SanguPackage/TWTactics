using System;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Villages;
using TribalWars.Villages.Units;

namespace TribalWars.Controls.DistanceToolStrip
{
    /// <summary>
    /// Represents a unit image and time display
    /// </summary>
    /// <remarks>Used in DistanceCollectionControl</remarks>
    public class DistanceControl : Button
    {
        #region Fields
        private readonly DistanceCollectionControl _collection;
        private readonly Unit _unit;	
        #endregion

        #region Properties
        /// <summary>
        /// Gets the FlowLayout collection parent
        /// </summary>
        public DistanceCollectionControl Collection
        {
            get { return _collection; }
        }

        /// <summary>
        /// Gets the unit for which the time should be displayed
        /// </summary>
        public Unit Unit
        {
            get { return _unit; }
        }
        #endregion

        #region Constructors
        public DistanceControl(DistanceCollectionControl col, Unit unit)
        {
            _collection = col;
            _unit = unit;
            Text = string.Empty;
            Image = unit.Image;
            ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            FlatStyle = FlatStyle.Flat;
            BackColor = System.Drawing.Color.Transparent;
            AutoSize = false;
            Size = new System.Drawing.Size(DistanceCollectionControl.TravelWidth, 21);
            Margin = new Padding(0);
            Padding = new Padding(0);
            FlatAppearance.BorderSize = 0;
            Enabled = false;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Shows the time in the Distance buttons for the specified villages and speed
        /// </summary>
        public void ShowTime(Village start, Village end, ShowDistanceEnum speed)
        {
            if (start != null && end != null)
            {
                TimeSpan time = Village.TravelTime(start, end, Unit);
                switch (speed)
                {
                    case ShowDistanceEnum.ArrivalTime:
                        Text = Tools.Common.GetShortPrettyDate(World.Default.ServerTime.Add(time));
                        break;
                    case ShowDistanceEnum.ReturnTime:
                        Text = Tools.Common.GetShortPrettyDate(World.Default.ServerTime.Add(time + time));
                        break;
                    case ShowDistanceEnum.TravelTime:
                        Text = time.ToString();
                        break;
                    case ShowDistanceEnum.TravelTime2:
                        Text = (time + time).ToString();
                        break;
                }
            }
            else
            {
                Text = "";
            }
        }
        #endregion
    }
}