#region Using

using System;
using System.Windows.Forms;
using TribalWars.Data;
using TribalWars.Data.Events;
using TribalWars.Data.Maps.Markers;

#endregion

namespace TribalWars.Controls.Accordeon
{
    public partial class MarkersContainerControl : UserControl
    {
        public MarkersContainerControl()
        {
            InitializeComponent();
        }

        private void MarkersContainerControl_Load(object sender, EventArgs e)
        {
            World.Default.EventPublisher.SettingsLoaded += World_SettingsLoaded;
            World.Default.Map.EventPublisher.MarkersChanged += World_MarkersChanged;
        }

        void World_MarkersChanged(object sender, MapMarkerEventArgs e)
        {
            MarkerGroups.Items.Clear();
            Markers.Items.Clear();

            MarkerGroups.Columns.Clear();
            var hd = new ColumnHeader();
            hd.Text = "Name";
            hd.Width = 60;
            MarkerGroups.Columns.Add(hd);
            hd = new ColumnHeader();
            hd.Text = "En.";
            hd.Width = 40;
            MarkerGroups.Columns.Add(hd);

            //World.Default.Views["Points"].

            //foreach (var filter in )
            //{
            //    hd = new ColumnHeader();
            //    hd.Text = string.Format("to {0}", filter.Value..ToString());
            //    hd.Width = 80;
            //    MarkerGroups.Columns.Add(hd);
            //}

            //foreach (MarkerGroup mg in World.Default.Map.MarkerDrawer.Markers)
            //{
            //    MarkerGroups.Items.Add(new MarkerGroupListViewItem(mg));
            //}
        }

        private void World_SettingsLoaded(object sender, EventArgs e)
        {
            //FilterGrid.SelectedObject = new MapFilter.KeyCollection(World.Default.Map.MarkerDrawer.Markers);
            //World_MarkersChanged(null, EventArgs.Empty);
        }

        private void MarkerGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            Markers.Items.Clear();
            if (MarkerGroups.SelectedItems.Count == 1)
            {
                /*MarkerGroupListViewItem itm = MarkerGroups.SelectedItems[0] as MarkerGroupListViewItem;
                if (itm != null)
                {
                    foreach (MarkerBase mark in itm.MarkerGroup)
                    {
                        Markers.Items.Add(mark.ListViewItem);
                    }
                }*/
            }
        }

        private void VillagePinPoint_Click(object sender, EventArgs e)
        {
            if (Markers.SelectedItems.Count == 1)
            {
                /*TribalWars.Descriptors.VillageListViewItem itm = Markers.SelectedItems[0] as TribalWars.Descriptors.VillageListViewItem;
                if (itm != null)
                {
                    World.Default.Map.EventPublisher.SelectVillages(null, itm.Villages, VillageTools.PinPoint);
                }*/
            }
        }

        private void VillageCenter_Click(object sender, EventArgs e)
        {
            if (Markers.SelectedItems.Count == 1)
            {
                /*TribalWars.Descriptors.VillageListViewItem itm = Markers.SelectedItems[0] as TribalWars.Descriptors.VillageListViewItem;
                if (itm != null)
                {
                    World.Default.Map.Location.SetCenter(MapController.GetSpan(itm.Villages), true);
                }*/
            }
        }

        private void VillageUnmark_Click(object sender, EventArgs e)
        {
            if (Markers.SelectedItems.Count == 1)
            {
                /*TribalWars.Descriptors.VillageListViewItem itm = Markers.SelectedItems[0] as TribalWars.Descriptors.VillageListViewItem;
                if (itm != null)
                {
                    itm.Marker.Parent.Remove(itm.Marker);
                    MarkerGroups_SelectedIndexChanged(null, EventArgs.Empty);
                }*/
            }
        }

        private void Markers_MouseClick(object sender, MouseEventArgs e)
        {
            if (Markers.SelectedItems.Count == 1 && e.Button == MouseButtons.Right)
            {
                VillagesMenu.Show(Markers, e.Location);
            }
        }

        private void StripNew_Click(object sender, EventArgs e)
        {
            //FormMarkerMaintain frm = new FormMarkerMaintain();
            //frm.ShowDialog();

            World_SettingsLoaded(null, EventArgs.Empty);
        }

        private void GroupEdit_Click(object sender, EventArgs e)
        {
            if (MarkerGroups.SelectedItems.Count == 1)
            {
                /*MarkerGroupListViewItem itm = MarkerGroups.SelectedItems[0] as MarkerGroupListViewItem;
                if (itm != null)
                {
                    FormMarkerMaintain frm = new FormMarkerMaintain(itm.MarkerGroup);
                    frm.ShowDialog();

                    World_SettingsLoaded(null, EventArgs.Empty);
                }*/
            }
        }

        private void GroupDelete_Click(object sender, EventArgs e)
        {
            /*if (MarkerGroups.SelectedItems.Count == 1)
            {
                MarkerGroupListViewItem itm = MarkerGroups.SelectedItems[0] as MarkerGroupListViewItem;
                if (itm != null)
                {
                    World.Default.Map.MarkerDrawer.Markers.Remove(itm.MarkerGroup);
                    MarkerGroups.Items.Remove(itm);
                }
            }*/
        }

        private void MarkerGroups_MouseClick(object sender, MouseEventArgs e)
        {
            if (MarkerGroups.SelectedItems.Count == 1 && e.Button == MouseButtons.Right)
            {
                MarkerGroupMenu.Show(MarkerGroups, e.Location);
            }
        }
    }
}
