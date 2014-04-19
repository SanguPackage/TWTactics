#region Using
using System;
using System.Drawing;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators.Helpers;
using TribalWars.Data.Maps.Manipulators.Implementations;

#endregion

namespace TribalWars.Data.Maps.Manipulators.Controls
{
    public partial class MapPolygonControl : UserControl
    {
        #region Fields
        private readonly Polygon _polygon;
        private readonly BbCodeManipulator _manipulator;
        #endregion

        #region Constructors
        public MapPolygonControl()
        {
            InitializeComponent();
        }

        internal MapPolygonControl(Polygon polygon, BbCodeManipulator manipulator, Point location)
        {
            InitializeComponent();

            Width = 149;
            Height = 70;
            Visible = true;
            Left = location.X + 100;
            Top = location.Y + 100;

            _polygon = polygon;
            _manipulator = manipulator;

            PolygonName.Focus();
        }
        #endregion

        #region Event Handlers
        private void PolygonName_Leave(object sender, EventArgs e)
        {
            _polygon.Name = PolygonName.Text;
            _manipulator.RemoveControl();
        }
        #endregion
    }
}