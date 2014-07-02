#region Using

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Events;
using Janus.Windows.GridEX;
using TribalWars.Data.Maps.Manipulators.Managers;
using TribalWars.Data.Villages;
using TribalWars.Tools;
#endregion

namespace TribalWars.Controls.Main.Polygon
{
    /// <summary>
    /// Manages the villages inside the BBCodeArea polygons
    /// </summary>
    public partial class PolygonControl : UserControl
    {
        #region Constructors
        public PolygonControl()
        {
            InitializeComponent();
        }

        public void Initialize()
        {
            World.Default.Map.EventPublisher.PolygonActivated += EventPublisher_PolygonActivated;
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// BBCodeArea polygon(s) have been ported to this control
        /// </summary>
        private void EventPublisher_PolygonActivated(object sender, PolygonEventArgs e)
        {
            GridExPolygon.DataSource = PolygonDataSet.CreateDataSet(e.Polygons);
            GridExPolygon.CheckAllRecords();
            GridExPolygon.MoveFirst();
        }

        /// <summary>
        /// The selected villages are exported to the clipboard
        /// </summary>
        private void ButtonGenerate_Click(object sender, EventArgs e)
        {
            if (GridExPolygon.RowCount == 0)
            {
                MessageBox.Show(string.Format("There is nothing in the grid yet. Try '{0}'?", LoadPolygonData.Text), "No data!");
                return;
            }

            var str = new StringBuilder();
            int villagesExported = 0;
            foreach (GridEXRow groupRow in GridExPolygon.GetRows())
            {
                if (groupRow.RowType == RowType.GroupHeader)
                {
                    str.AppendLine();
                    str.AppendLine();
                    str.AppendLine(groupRow.GroupValue.ToString());
                    foreach (GridEXRow row in groupRow.GetChildRecords())
                    {
                        if (row.CheckState == RowCheckState.Checked)
                        {
                            villagesExported++;

                            var villageRow = (PolygonDataSet.VILLAGERow)((DataRowView)row.DataRow).Row;
                            if (!string.IsNullOrWhiteSpace(villageRow.Village.TypeString))
                            {
                                str.AppendLine(villageRow.BBCODE + " (" + villageRow.Village.TypeString + ")");
                            }
                            else
                            {
                                str.AppendLine(villageRow.BBCODE);
                            }
                        }
                    }
                }
            }

            try
            {
                Clipboard.SetText(str.ToString().Trim());
                MessageBox.Show(string.Format("BBCodes for {0} villages have been placed on the clipboard!", villagesExported), "BBCodes on clipboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                
            }
        }

        private void GridExPolygon_FormattingRow(object sender, RowLoadEventArgs e)
        {
            if (e.Row.RowType == RowType.GroupHeader)
            {
                e.Row.GroupCaption = string.Format("{0} ({1})", e.Row.GroupValue, e.Row.GetRecordCount());
            }

            if (e.Row.RowType == RowType.Record)
            {
                var record = (PolygonDataSet.VILLAGERow)e.Row.GetDataRow();

                // SetVillageVisibility()
                if (record.ISVISIBLE)
                {
                    e.Row.Cells["ISVISIBLE"].Image = VisibleImageList.Images[0];
                    e.Row.Cells["ISVISIBLE"].ToolTipText = "Currently visible on the map";
                }

                // SetVillageType()
                if (record.Village.Type != VillageType.None)
                {
                    e.Row.Cells["TYPE"].Image = record.Village.TypeImage;
                }

                // Display You and your tribe in special color
                if (record.Village.HasPlayer)
                {
                    var you = World.Default.You.Player;
                    if (record.Village.Player == you)
                    {
                        var style = new GridEXFormatStyle();
                        style.ForeColor = Color.Red;
                        style.FontBold = TriState.True;
                        e.Row.Cells["PLAYER"].FormatStyle = style;
                    }
                    else if (you != null && record.Village.Player.Tribe == you.Tribe)
                    {
                        var style = new GridEXFormatStyle();
                        style.ForeColor = Color.Blue;
                        style.FontBold = TriState.True;
                        e.Row.Cells["PLAYER"].FormatStyle = style;
                    }
                }
            }
        }
        #endregion

        private void LoadPolygonData_Click(object sender, EventArgs e)
        {
            var polygons = World.Default.Map.Manipulators.PolygonManipulator.GetAllPolygons().ToArray();
            if (polygons.Any())
            {
                World.Default.Map.EventPublisher.ActivatePolygon(this, polygons);
            }
            else
            {
                World.Default.Map.Manipulators.SetManipulator(ManipulatorManagerTypes.Polygon);
                MessageBox.Show(@"You have not yet defined any polygons.
I have activated polygon drawing for you, you can go back to the main map and create some now!

Click and hold left mouse button to start drawing!
Or... Right click on the map for more help.", "No polygons!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
