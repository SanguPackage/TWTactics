#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data.Maps.Manipulators;
using TribalWars.Data.Maps;
#endregion

namespace TribalWars.Data.Maps.Manipulators
{
    public partial class MapPolygonControl : UserControl
    {
        #region Fields
        private Polygon _polygon;
        private BBCodeManipulator _manipulator;
        #endregion

        #region Constructors
        public MapPolygonControl()
        {
            InitializeComponent();
        }

        internal MapPolygonControl(Polygon polygon, BBCodeManipulator manipulator, Point location)
        {
            InitializeComponent();

            this.Width = 149;
            this.Height = 70;
            this.Visible = true;
            this.Left = location.X + 100; 
            this.Top = location.Y + 100;

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
