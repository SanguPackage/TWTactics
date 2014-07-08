using System;
using System.Text;
using TribalWars.Worlds;

namespace TribalWars.Browsers.Reporting
{
    // Currently not in use!

    /// <summary>
    /// Generates a Html report
    /// </summary>
    public class HtmlReport
    {
        private const string CustomDateFormat = "d-MM H:mm";
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        private HtmlReport()
        {

        }
        #endregion

        #region Public Methods
        public static string BuildReport(VillageReportCollection col)
        {
            string path = Environment.CurrentDirectory + "\\Html\\";

            string player = "unknown";            
            string village = "";
            if (World.Default.Villages.ContainsKey(col.VillageLocation))
            {
                village = World.Default.Villages[col.VillageLocation].ToString();
                if (World.Default.Villages[col.VillageLocation].HasPlayer)
                    player = World.Default.Villages[col.VillageLocation].Player.Name;
            }

            StringBuilder str = new StringBuilder();
            str.AppendFormat("<html><head><link rel='stylesheet' type='text/css' href='{0}Markup.css' /></head><body>", path);
            str.Append("<table width='100%' border=1 style='border: 1px solid #DED3B9'>");
            str.AppendFormat("<tr><td nowrap>Player:</td><td>{0}</td></tr>", player);
            str.AppendFormat("<tr><td>Village:</td><td>{0}</td></tr>", village);
            str.Append("<tr><td colspan='2'>");
	        str.Append("<table class='vis'>");
	        str.Append("<tr class='center'>");
            /*str.AppendFormat("<td>{0}</td>", col.DefenseDate.ToString(CustomDateFormat));
            foreach (ReportUnit unit in col.Defense.Values) if (unit.AmountTotal != 0)
            {
                str.AppendFormat("<td><img url='{0}{1}.png' alt='{2}'></td>", path, unit.Unit.Type.ToString(), unit.Unit.Name);
            }
            str.Append("</tr><tr class='center'><td>Quantity:</td>");
            foreach (ReportUnit unit in col.Defense.Values) if (unit.AmountTotal != 0)
            {
                str.AppendFormat("<td>{0}</td>", unit.AmountStart.ToString("#,0"));
            }
            str.Append("</tr><tr class='center'><td>Out:</td>");
            foreach (ReportUnit unit in col.Defense.Values) if (unit.AmountTotal != 0)
            {
                str.AppendFormat("<td>{0}</td>", unit.AmountOut.ToString("#,0"));
            }*/
            str.Append("</tr>");
            str.Append("</table>");
            str.Append("</td></tr></table>");
            str.Append("</body></html>");

            //System.IO.File.WriteAllText("c:\\temp\\test.html", str.ToString());
	/*
</td></tr>
</table><br />
<table width="100%" style="border: 1px solid #DED3B9">
<tr><th width="100">Defender:</th><th><a href="game.php?village=95233&amp;screen=info_player&amp;id=1431637">XmasterchiefX</a></th></tr>
<tr><td>Village:</td><td><a href="game.php?village=95233&amp;screen=info_village&amp;id=98922">Danktopia! (780|365) K37</a></td></tr>
<tr><td colspan="2">
	<table class="vis">
	<tr class="center"><td></td><td width="35"><img src="graphic/unit/unit_spear.png" title="Spear fighter" alt="" /></td><td width="35"><img src="graphic/unit/unit_sword.png" title="Swordsman" alt="" /></td><td width="35"><img src="graphic/unit/unit_axe.png" title="Axeman" alt="" /></td><td width="35"><img src="graphic/unit/unit_spy.png" title="Scout" alt="" /></td><td width="35"><img src="graphic/unit/unit_light.png" title="Light cavalry" alt="" /></td><td width="35"><img src="graphic/unit/unit_heavy.png" title="Heavy cavalry" alt="" /></td><td width="35"><img src="graphic/unit/unit_ram.png" title="Ram" alt="" /></td><td width="35"><img src="graphic/unit/unit_catapult.png" title="Catapult" alt="" /></td><td width="35"><img src="graphic/unit/unit_snob.png" title="Nobleman" alt="" /></td></tr>
	<tr class="center"><td>Quantity:</td><td>192</td><td>97</td><td>48</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td></tr>
	<tr class="center"><td>Losses:</td><td>192</td><td>97</td><td>48</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td><td class="hidden">0</td></tr>
	</table>
	
</td></tr>
</table><br /><br />

<h4>Espionage</h4>
<table style="border: 1px solid #DED3B9">
<tr><th>Resources scouted:</th><td><img src="graphic/holz.png" title="Wood" alt="" />0 <img src="graphic/lehm.png" title="Clay" alt="" />0 <img src="graphic/eisen.png" title="Iron" alt="" />0 </td></tr>
	<tr><th>Buildings:</th><td>
		Village Headquarters <b>(Level 10)</b><br />
		Barracks <b>(Level 5)</b><br />
		Stable <b>(Level 3)</b><br />
		Smithy <b>(Level 5)</b><br />
		Rally point <b>(Level 1)</b><br />
		Market <b>(Level 2)</b><br />
		Timber camp <b>(Level 12)</b><br />
		Clay pit <b>(Level 8)</b><br />
		Iron mine <b>(Level 6)</b><br />
		Farm <b>(Level 8)</b><br />
		Warehouse <b>(Level 10)</b><br />
		Hiding place <b>(Level 2)</b><br />
		</td></tr>
</table>
<br />




<table width="100%" style="border: 1px solid #DED3B9">
	<tr><th>Haul:</th>
	<td width="220"><img src="graphic/holz.png" title="Wood" alt="" />4<span class="grey">.</span>265 <img src="graphic/lehm.png" title="Clay" alt="" />2<span class="grey">.</span>583 <img src="graphic/eisen.png" title="Iron" alt="" />1<span class="grey">.</span>837 </td>
	<td>8685/21070</td>
	</tr>*/

            return str.ToString();
        }
        #endregion

        #region Private Implementation
        #endregion
    }
}
