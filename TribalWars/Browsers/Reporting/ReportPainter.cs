using System;
using System.Collections.Generic;
using System.Drawing;
using TribalWars.Villages;
using TribalWars.Villages.Units;

namespace TribalWars.Browsers.Reporting
{
    /// <summary>
    /// Paints a report on a canvas
    /// </summary>
    public static class ReportPainter
    {
        // player & village name color: #804000
        // grey display color: #ded3b9
        // background color: #f7f3e8    // 247; 243; 232
        // dark background color: #e1d7bc
        // background color with player & village name foreground: #e0d6bb

        //private Rectangle _playerRectangle;
        //private Rectangle _villageRectangle;

        private static Brush DarkBackgroundBrush = new SolidBrush(Color.FromArgb(225, 215, 188));
        private static Brush PlayerBrush = new SolidBrush(Color.FromArgb(128, 64, 0));
        private static Font PlayerFont = new Font(SystemFonts.DefaultFont.FontFamily, 10, FontStyle.Bold);

        public static void Paint(Graphics g, Report report)
        {
            g.TranslateTransform(10, 10);
            PaintPlayer(g, report.Attacker.Village);
            g.TranslateTransform(0, 27);
            PaintTroops(g, report.Attack);

            g.TranslateTransform(0, 70);
            PaintPlayer(g, report.Defender.Village);
            g.TranslateTransform(0, 27);
            PaintTroops(g, report.Defense);
        }

        private static void PaintTroops(Graphics g, Dictionary<UnitTypes, ReportUnit> troops)
        {
            int x = 5;
            int yImage = 5;
            int yQuantity = 25;
            int yLosses = 45;
            foreach (ReportUnit unit in troops.Values)
            {
                if (unit.AmountStart > 0)
                {
                    g.DrawImageUnscaled(unit.Unit.Image, x, yImage);
                    g.DrawString(Tools.Common.GetPrettyNumber(unit.AmountStart), SystemFonts.DefaultFont, Brushes.Black, x, yQuantity);
                    g.DrawString(Tools.Common.GetPrettyNumber(unit.AmountLost), SystemFonts.DefaultFont, Brushes.Black, x, yLosses);

                    x += (int)Math.Max(25, g.MeasureString(Tools.Common.GetPrettyNumber(unit.AmountStart), SystemFonts.DefaultFont).Width);
                }
            }
            g.DrawRectangle(Pens.Black, 0, 0, g.ClipBounds.Width - 20, 65);
        }

        private static void PaintPlayer(Graphics g, Village village)
        {
            PaintPlayer(g, village, 0, 0);
        }

        private static void PaintPlayer(Graphics g, Village village, int x, int y)
        {
            g.FillRectangle(DarkBackgroundBrush, x, y, g.ClipBounds.Width - 20, 25);
            float widthPlayer = 0;
            if (village.HasPlayer)
            {
                g.DrawString(village.Player.Name, PlayerFont, PlayerBrush, x + 5, y + 4);
                widthPlayer = g.MeasureString(village.Player.Name, PlayerFont).Width + 10;
            }

            g.DrawString(village.LocationString + " " + village.Name, PlayerFont, PlayerBrush, x + widthPlayer, y + 4);
        }
    }
}
