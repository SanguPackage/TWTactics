#region Imports
using System;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

using System.Drawing;
using TribalWars.Data.Maps.Views;
using TribalWars.Data.Events;
using TribalWars.Data.Villages;
using TribalWars.Data.Tribes;
using TribalWars.Data.Players;
#endregion

namespace TribalWars.Data.Maps.Markers
{
    /// <summary>
    /// Represents a named collection of Player, Tribe and VillageMarkers
    /// </summary>
    public class MarkerGroup : IEquatable<MarkerGroup>
    {
        #region Fields
        private string _name;
        private bool _enabled;
        private Color _color;
        private Color _extraColor;

        private string _view;
        private string _decorator;

        private MarkerManager _markerManager;

        private List<Player> _players;
        private List<Tribe> _tribes;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the tribes marked by the group
        /// </summary>
        public List<Tribe> Tribes
        {
            get { return _tribes; }
        }

        /// <summary>
        /// Gets the players marked by the group
        /// </summary>
        public List<Player> Players
        {
            get { return _players; }
        }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the markers are to be drawn
        /// </summary>
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        /*/// <summary>
        /// Gets the list of markers
        /// </summary>
        public IList<MarkerBase> Markers
        {
            get { return _collection; }
        }*/

        /// <summary>
        /// Gets or sets the secundary color for the marker
        /// </summary>
        public Color ExtraColor
        {
            get { return _extraColor; }
            set { _extraColor = value; }
        }

        /// <summary>
        /// Gets or sets the primary color for the marker
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Gets or sets how to represent the marked
        /// villages on the map
        /// </summary>
        public string View
        {
            get { return _view; }
            set { _view = value; }
        }

        /// <summary>
        /// Gets or sets how to extra decorate the marked
        /// villages on the map
        /// </summary>
        public string Decorator
        {
            get { return _decorator; }
            set { _decorator = value; }
        }

        /// <summary>
        /// Gets a value indicating whether 
        /// the markergroup has specified a view
        /// </summary>
        public bool HasView
        {
            get { return _view != null; }
        }

        /// <summary>
        /// Gets a value indicating whether 
        /// the markergroup has a decorator
        /// </summary>
        public bool HasDecorator
        {
            get { return _decorator != null; }
        }

        /// <summary>
        /// Gets the marker manager this group belongs to
        /// </summary>
        public MarkerManager MarkerManager
        {
            get { return _markerManager; }
        }
        #endregion

        #region Constructor
        public MarkerGroup()
            : this(World.Default.Map, "New Marker", true, Color.Transparent, Color.Transparent, null, null)
        {

        }

        public MarkerGroup(Map map, string name)
            : this(map, name, true, Color.Transparent, Color.Transparent, null, null)
        {
            
        }

        public MarkerGroup(Map map, string name, bool enabled, Color color, Color extraColor, string view, string decorator)
        {
            _markerManager = map.MarkerManager;
            _name = name;
            _enabled = enabled;
            _color = color;
            _extraColor = extraColor;
            _view = view;
            _decorator = decorator;
            //_collection = new MarkerGroupCollection(_markerManager, this);

            _players = new List<Player>();
            _tribes = new List<Tribe>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a PlayerMarker to the collection
        /// </summary>
        public void Add(PlayerMarker itm)
        {
            if (itm != null && itm.Player != null)
            {
                //_collection.Add((MarkerBase)itm);
                if (!_players.Contains(itm.Player))
                    _players.Add(itm.Player);
            }
        }

        /// <summary>
        /// Adds a TribeMarker to the collection
        /// </summary>
        public void Add(TribeMarker itm)
        {
            if (itm != null && itm.Tribe != null)
            {
                //_collection.Add((MarkerBase)itm);
                if (!_tribes.Contains(itm.Tribe))
                    _tribes.Add(itm.Tribe);
            }
        }

        /// <summary>
        /// Removes a PlayerMarker from the collection
        /// </summary>
        public void Remove(PlayerMarker itm)
        {
            if (itm != null && itm.Player != null)
            {
                //_collection.Remove((MarkerBase)itm);
                if (_players.Contains(itm.Player))
                    _players.Remove(itm.Player);
            }
        }

        /// <summary>
        /// Removes a TribeMarker from the collection
        /// </summary>
        public void Remove(TribeMarker itm)
        {
            if (itm != null && itm.Tribe != null)
            {
                //_collection.Remove((MarkerBase)itm);
                if (_tribes.Contains(itm.Tribe))
                    _tribes.Remove(itm.Tribe);
            }
        }
        #endregion

        #region Overriden Methods
        public override string ToString()
        {
            string views = string.Empty;
            if (_view != null) views = _view;
            if (_decorator != null) views += (_view == null ? "" : ", ") + _decorator;
            return string.Format("{0} ({1} - {2})", _name, views, _color.ToKnownColor()); 
        }
        #endregion

        #region IEquatable<MarkerGroup> Members
        public bool Equals(MarkerGroup other)
        {
            if (other == null) return false;
            return this._view == other.View && _decorator == other.Decorator
                && _color == other.Color && _extraColor == other.ExtraColor;
        }
        #endregion
    }
}



/*
 * #region MarkerGroupCollection
/// <summary>
/// Handles the different markers
/// </summary>
private class MarkerGroupCollection : Collection<MarkerBase>
{
    #region Fields
    private MarkerManager _markerManager;
    private MarkerGroup _parent;
    private Dictionary<Village, int> _villages;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the counter how many times a village
    /// is included in one of the MarkerBases
    /// </summary>
    public Dictionary<Village, int> Villages
    {
        get { return _villages; }
    }
    #endregion

    #region Constructors
    public MarkerGroupCollection(MarkerManager markerManager, MarkerGroup parent)
    {
        _villages = new Dictionary<Village, int>();
        _markerManager = markerManager;
        _parent = parent;
    }
    #endregion

    #region Public Methods
    protected override void InsertItem(int index, MarkerBase item)
    {
        item.Parent = _parent;
        foreach (Village village in item)
        {
            if (!_villages.ContainsKey(village)) _villages.Add(village, 0);
            _villages[village] += 1;
        }
        base.InsertItem(index, item);
        _markerManager.Map.EventPublisher.InformMarkersChanged(this, new MapMarkerEventArgs(item, true));
    }

    protected override void RemoveItem(int index)
    {
        MarkerBase item = this.Items[index];
        foreach (Village village in item)
        {
            _villages[village] -= 1;
            if (_villages[village] <= 0) _villages.Remove(village);
        }
        _markerManager.Map.EventPublisher.InformMarkersChanged(this, new MapMarkerEventArgs(item, false));
        base.RemoveItem(index);
    }
    #endregion
}

/*private class MarkerGroupCollection : Collection<MarkerBase>
{
    #region Fields
    private MarkerManager _markerManager;
    private MarkerGroup _parent;
    private List<string> _players;
    private List<string> _tribes;
    #endregion

    #region Properties
    /// <summary>
    /// Gets the counter how many times a village
    /// is included in one of the MarkerBases
    /// </summary>
    public Dictionary<Village, int> Villages
    {
        get { return _villages; }
    }
    #endregion

    #region Constructors
    public MarkerGroupCollection(MarkerManager markerManager, MarkerGroup parent)
    {
        _villages = new Dictionary<Village, int>();
        _markerManager = markerManager;
        _parent = parent;
    }
    #endregion

    #region Public Methods
    protected override void InsertItem(int index, MarkerBase item)
    {
        item.Parent = _parent;
        foreach (Village village in item)
        {
            if (!_villages.ContainsKey(village)) _villages.Add(village, 0);
            _villages[village] += 1;
        }
        base.InsertItem(index, item);
        _markerManager.Map.EventPublisher.InformMarkersChanged(this, new MapMarkerEventArgs(item, true));
    }

    protected override void RemoveItem(int index)
    {
        //MarkerBase item = this.Items[index];
        //foreach (Village village in item)
        //{
        //    _villages[village] -= 1;
        //    if (_villages[village] <= 0) _villages.Remove(village);
        //}
        //_markerManager.Map.EventPublisher.InformMarkersChanged(this, new MapMarkerEventArgs(item, false));
        
        //base.RemoveItem(index);
    }
    #endregion
}*/