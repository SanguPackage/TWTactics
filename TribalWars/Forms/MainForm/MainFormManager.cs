#region Using
using System;
using System.Collections.Generic;
using System.Text;
using TribalWars.Data.Events;
using System.Windows.Forms;
using System.IO;
#endregion

namespace TribalWars
{
    public partial class FormMain
    {
        private class MainFormManager : IDisposable
        {
            #region Fields
            private FormMain _form;

            private Dictionary<VillageTools, FormEventHandler> _handlers;
            #endregion

            #region Properties
            #endregion

            #region Constructors
            public MainFormManager(FormMain form)
            {
                _form = form;
                _handlers = new Dictionary<VillageTools, FormEventHandler>();

                // Subscribe to World events
                World.Default.EventPublisher.Loaded += new EventHandler<EventArgs>(OnWorldLoaded);
                World.Default.EventPublisher.SettingsLoaded += new EventHandler<EventArgs>(OnWorldSettingsLoaded);
                World.Default.EventPublisher.Browse += new EventHandler<BrowserEventArgs>(OnBrowse);
                World.Default.Map.EventPublisher.VillagesSelected += new EventHandler<VillagesEventArgs>(OnVillagesEvent);
            }
            #endregion

            #region Public Methods
            /// <summary>
            /// Move to the Browser tab
            /// </summary>
            private void OnBrowse(object sender, BrowserEventArgs e)
            {
                if (e.GameDestination)
                    _form.Tabs.SelectedTab = _form.Tabs.TabPages[1];
                else
                    _form.Tabs.SelectedTab = _form.Tabs.TabPages[2];
            }

            private void OnWorldLoaded(object sender, EventArgs e)
            {
                
            }

            private void OnWorldSettingsLoaded(object sender, EventArgs e)
            {
                //_form.Tool
            }

            private void OnVillagesEvent(object sender, VillagesEventArgs e)
            {
                switch (e.Tool)
                {
                    /*case VillageTools.DistanceCalculation:
                        foreach (Village vil in e.Villages)
                        {
                            if (MapDistance.ActivePlan != null)
                                MapDistance.ActivePlan.AddVillage(vil);
                        }
                        LeftNavigation.SelectNavigationPage(getNavigationPane(NavigationPanes.Attack));
                        break;*/
                    /*case VillageTools.DistanceCalculationTarget:
                        if (e.Villages != null)
                        {
                            LeftNavigation.SelectNavigationPage(getNavigationPane(NavigationPanes.Attack));
                        }
                        break;*/
                    case VillageTools.SelectVillage:
                        //Jump to quick finder pane
                        _form.LeftNavigation.SelectNavigationPage(_form.GetNavigationPane(NavigationPanes.QuickFinder));
                        break;
                }
            }
            #endregion

            #region Private Methods
            #endregion

            #region IDisposable Members
            public void Dispose()
            {
                World.Default.EventPublisher.Loaded -= OnWorldLoaded;
                World.Default.EventPublisher.Browse -= OnBrowse;
                World.Default.Map.EventPublisher.VillagesSelected -= OnVillagesEvent;
            }
            #endregion
        }
    }
}
