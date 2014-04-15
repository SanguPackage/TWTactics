using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

using TribalWars.Tools;
using TribalWars.Data.Reporting;

namespace TribalWars.Controls
{
    /// <summary>
    /// Monitors the clipboard and parses reports
    /// </summary>
    public partial class ParserControl : UserControl
    {
        #region Constants
        string[] formatsAll = new string[] 
		{
			DataFormats.Bitmap,
			DataFormats.CommaSeparatedValue,
			DataFormats.Dib,
			DataFormats.Dif,
			DataFormats.EnhancedMetafile,
			DataFormats.FileDrop,
			DataFormats.Html,
			DataFormats.Locale,
			DataFormats.MetafilePict,
			DataFormats.OemText,
			DataFormats.Palette,
			DataFormats.PenData,
			DataFormats.Riff,
			DataFormats.Rtf,
			DataFormats.Serializable,
			DataFormats.StringFormat,
			DataFormats.SymbolicLink,
			DataFormats.Text,
			DataFormats.Tiff,
			DataFormats.UnicodeText,
			DataFormats.WaveAudio
		};
        #endregion

        #region Fields
        private bool _Registered;
        private IntPtr _ClipboardViewerNext;
        private ReportOutputOptions ReportOptions = new ReportOutputOptions();
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether we are currently monitoring the clipboard
        /// </summary>
        public bool Registered
        {
            get { return _Registered; }
        }
        #endregion

        #region Constructors
        public ParserControl()
        {
            InitializeComponent();
            RegisterClipboardViewer();
        }
        #endregion

        #region Clipboard Hook
        /// <summary>
        /// Register this form as a Clipboard Viewer application
        /// </summary>
        public void RegisterClipboardViewer()
        {
            if (!_Registered)
            {
                //_ClipboardViewerNext = User32.SetClipboardViewer(this.Handle);
                //_Registered = true;
            }
        }

        /// <summary>
        /// Remove this form from the Clipboard Viewer list
        /// </summary>
        public void UnregisterClipboardViewer()
        {
            if (Registered)
            {
                //_Registered = false;
                //User32.ChangeClipboardChain(this.Handle, _ClipboardViewerNext);
            }
        }

        /// <summary>
        /// Show the clipboard contents in the window 
        /// and show the notification balloon if a link is found
        /// </summary>
        private void GetClipboardData()
        {
            //
            // Data on the clipboard uses the 
            // IDataObject interface
            //
            IDataObject iData = new DataObject();
            try
            {
                iData = Clipboard.GetDataObject();
            }
            catch (System.Runtime.InteropServices.ExternalException externEx)
            {
                // Copying a field definition in Access 2002 causes this sometimes?
                Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }

            // 
            // Get text if it is present
            //
            if (iData.GetDataPresent(DataFormats.Text))
            {
                if (World.Default.HasLoaded)
                {
                    string clipboard = (string)iData.GetData(DataFormats.Text);
                    if (clipboard != null)
                    {
                        Report report = ReportParser.ParseText(clipboard, ReportOptions);
                        if (report != null)
                        {
                            VillageReportCollection.Save(report);
                            string output = report.BBCode();
                            Output.Text += output + Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine;
                            try
                            {
                                Clipboard.SetText(output);
                            }
                            catch (Exception)
                            {
                                
                            }
                        }
                    }
                }
            }
        }

        /*protected override void WndProc(ref Message m)
        {
            switch ((Msgs)m.Msg)
            {
                //
                // The WM_DRAWCLIPBOARD message is sent to the first window 
                // in the clipboard viewer chain when the content of the 
                // clipboard changes. This enables a clipboard viewer 
                // window to display the new content of the clipboard. 
                //
                case Msgs.WM_DRAWCLIPBOARD:

                    Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");

                    GetClipboardData();

                    //
                    // Each window that receives the WM_DRAWCLIPBOARD message 
                    // must call the SendMessage function to pass the message 
                    // on to the next window in the clipboard viewer chain.
                    //
                    User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;


                //
                // The WM_CHANGECBCHAIN message is sent to the first window 
                // in the clipboard viewer chain when a window is being 
                // removed from the chain. 
                //
                case Msgs.WM_CHANGECBCHAIN:
                    Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");

                    // When a clipboard viewer window receives the WM_CHANGECBCHAIN message, 
                    // it should call the SendMessage function to pass the message to the 
                    // next window in the chain, unless the next window is the window 
                    // being removed. In this case, the clipboard viewer should save 
                    // the handle specified by the lParam parameter as the next window in the chain. 

                    //
                    // wParam is the Handle to the window being removed from 
                    // the clipboard viewer chain 
                    // lParam is the Handle to the next window in the chain 
                    // following the window being removed. 
                    if (m.WParam == _ClipboardViewerNext)
                    {
                        //
                        // If wParam is the next clipboard viewer then it
                        // is being removed so update pointer to the next
                        // window in the clipboard chain
                        //
                        _ClipboardViewerNext = m.LParam;
                    }
                    else
                    {
                        User32.SendMessage(_ClipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    }
                    break;

                default:
                    //
                    // Let the form process the messages that we are
                    // not interested in
                    //
                    base.WndProc(ref m);
                    break;
            }
        }*/
        #endregion

        #region Event Handlers
        private void ReportParser_Load(object sender, EventArgs e)
        {

        }
        #endregion

        #region ReportOptions
        private void CheckAttacker_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.AttackingPlayer = CheckAttacker.Checked;
        }

        private void CheckDefender_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.DefendingPlayer = CheckDefender.Checked;
        }

        private void CheckResources_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.ResourceDetails = CheckResources.Checked;
        }

        private void CheckFarming_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.FarmingDetails = CheckFarming.Checked;
        }

        private void CheckAttackTroops_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.AttackingTroops = CheckAttackTroops.Checked;
        }

        private void CheckDefenseTroops_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.DefendingTroops = CheckDefenseTroops.Checked;
        }

        private void CheckBuildings_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.Buildings = CheckBuildings.Checked;
        }

        private void CheckPeople_CheckedChanged(object sender, EventArgs e)
        {
            ReportOptions.People = CheckPeople.Checked;
        }
        #endregion
    }
}
