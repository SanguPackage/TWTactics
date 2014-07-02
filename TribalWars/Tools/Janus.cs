using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Janus.Windows.GridEX;

namespace TribalWars.Tools
{
    /// <summary>
    /// WinForms extensions for Janus controls
    /// </summary>
    public static class Janus
    {
        public static DataRow GetDataRow(this GridEXRow row)
        {
            return ((DataRowView)row.DataRow).Row;
        }
    }
}
