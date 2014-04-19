using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Villages;
using TribalWars.Data.Units;
using TribalWars.Data.Events;

namespace TribalWars.Controls.DistanceToolStrip
{
    #region Enum
    /// <summary>
    /// 
    /// </summary>
    public enum ShowDistanceEnum
    {
        TravelTime = 0,
        TravelTime2,
        ArrivalTime,
        ReturnTime
    }
    #endregion

    /// <summary>
    /// Holder type for DistanceControls
    /// </summary>
    public class DistanceCollectionControl : FlowLayoutPanel
    {
        #region Constants
        public const int TravelWidth = 77;
        public const int TimeWidth = 100;
        #endregion

        #region Fields
        private List<DistanceControl> _items;
        private Village _villageStart;
        private Village _villageEnd;
        //private Button _moraleButton;
        private readonly Button _speedButton;
        private readonly ContextMenuStrip _speedStrip;
        private ShowDistanceEnum _speed = ShowDistanceEnum.TravelTime;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the current speed display type
        /// </summary>
        public ShowDistanceEnum CurrentSpeed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// Gets the list of Distance buttons
        /// </summary>
        public List<DistanceControl> Items
        {
            get { return _items; }
        }

        /// <summary>
        /// Gets or sets the bullseye village
        /// </summary>
        public Village VillageStart
        {
            get { return _villageStart; }
            set { _villageStart = value; }
        }

        /// <summary>
        /// Gets or sets the target village
        /// </summary>
        public Village VillageEnd
        {
            get { return _villageEnd; }
            set { _villageEnd = value; }
        }

        /// <summary>
        /// Gets the ContextMenuStrip containing the different speed options
        /// </summary>
        public ContextMenuStrip SpeedStrip
        {
            get { return _speedStrip; }
        }
        #endregion

        #region Constructors
        public DistanceCollectionControl()
        {
            AutoSize = false;
            BackColor = System.Drawing.Color.Transparent;
            Margin = new Padding(0);
            Padding = new Padding(0);
            

            World.Default.EventPublisher.Loaded += World_Loaded;
            World.Default.Map.EventPublisher.VillagesSelected += World_VillagesSelected;

            // Loads the speed strip & items
            _speedStrip = new ContextMenuStrip();
            _speedStrip.Items.Add(new DistanceContextMenuItem("Travel time", ShowDistanceEnum.TravelTime, this));
            _speedStrip.Items.Add(new DistanceContextMenuItem("Travel time * 2", ShowDistanceEnum.TravelTime2, this));
            _speedStrip.Items.Add(new DistanceContextMenuItem("Arrival time", ShowDistanceEnum.ArrivalTime, this));
            _speedStrip.Items.Add(new DistanceContextMenuItem("Return time", ShowDistanceEnum.ReturnTime, this));
            ((ToolStripMenuItem)_speedStrip.Items[0]).Checked = true;

            _speedButton = new Button();
            _speedButton.AutoSize = false;
            _speedButton.Image = Properties.Resources.speed;
            _speedButton.ContextMenuStrip = _speedStrip;
            _speedButton.Size = new System.Drawing.Size(21, 21);
            _speedButton.Text = string.Empty;
            _speedButton.FlatStyle = FlatStyle.Flat;
            _speedButton.FlatAppearance.BorderSize = 0;
            _speedButton.Margin = new Padding(0);
            _speedButton.Padding = new Padding(0);
            _speedButton.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 15, System.Drawing.FontStyle.Bold);
            _speedButton.MouseClick += SpeedButton_MouseClick;
            Controls.Add(_speedButton);

            #region Commented Morale
            // morale
            /*_moraleButton = new Button();
            _moraleButton.AutoSize = false;
            _moraleButton.Size = new System.Drawing.Size(70, 21);
            _moraleButton.Text = string.Empty;
            _moraleButton.FlatStyle = FlatStyle.Flat;
            _moraleButton.FlatAppearance.BorderSize = 0;
            _moraleButton.Font = new System.Drawing.Font(System.Drawing.FontFamily.GenericSansSerif, 15, System.Drawing.FontStyle.Bold);
            Controls.Add(_moraleButton);*/
            #endregion
        }
        #endregion

        #region Event Handlers
        private void SpeedButton_MouseClick(object sender, MouseEventArgs e)
        {
            // Show the context menu
            _speedStrip.Show(_speedButton, e.Location);
        }

        private void World_VillagesSelected(object sender, VillagesEventArgs e)
        {
            switch (e.Tool)
            {
                case VillageTools.Default:
                    VillageEnd = e.FirstVillage;
                    //SetMorale();
                    SetTimes();
                    break;
                case VillageTools.Distance:
                    VillageStart = e.FirstVillage;
                    SetTimes();
                    break;
            }
        }

        private void World_Loaded(object sender, EventArgs e)
        {
            // dispose existing Distance buttons
            if (_items != null)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    var ctl = _items[i];
                    if (ctl != null)
                    {
                        ctl.Dispose();
                    }
                }
            }

            // Add all units
            _items = new List<DistanceControl>();
            float lastSpeed = 0;
            foreach (Unit unit in WorldUnits.Default)
            {
                if (unit.Speed != lastSpeed)
                {
                    var ctl = new DistanceControl(this, unit);
                    Controls.Add(ctl);
                    _items.Add(ctl);
                }
                lastSpeed = unit.Speed;
            }
            SetSize();

            // load default bullseye from settings
            /*if (World.Default.Settings.BullsEye != null)
            {
                _villageStart = World.Default.Settings.BullsEye;
            }*/
        }
        #endregion

        #region Commented Morale
        /*private void SetMorale()
        {
            int morale = 0;
            if (!VillageEnd.HasPlayer)
                morale = 100;
            else if (World.Default.Settings.You.PlayerSelected)
            {
                if (VillageEnd.Player != World.Default.Settings.You.Player)
                {

                }
            }

            if (morale == 0)
            {
                _moraleButton.Text = "???";
                _moraleButton.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                _moraleButton.Text = morale.ToString();
                if (morale < 60) _moraleButton.ForeColor = System.Drawing.Color.Red;
                else _moraleButton.ForeColor = System.Drawing.Color.Green;
            }
        }*/
        #endregion

        #region Private Implementation
        /// <summary>
        /// Sets the time for all Distance buttons
        /// </summary>
        private void SetTimes()
        {
            foreach (DistanceControl distance in _items)
            {
                distance.ShowTime(_villageStart, _villageEnd, _speed);
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the size of the collection to fit the Distance buttons
        /// </summary>
        public void SetSize()
        {
            int width = TravelWidth;
            if (CurrentSpeed == ShowDistanceEnum.TravelTime || CurrentSpeed == ShowDistanceEnum.TravelTime2) width = TimeWidth;
            Size = new System.Drawing.Size((width + 3) * _items.Count + _speedButton.Width + 2, 21);
        }
        #endregion
    }
}
