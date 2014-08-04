#region Using
using System.Drawing;

#endregion

namespace TribalWars.Maps.Drawing.Drawers.VillageDrawers
{
    /// <summary>
    /// Draws an extra icon (Attack, Defense, ...)
    /// on an existing village bitmap
    /// </summary>
    public sealed class IconDrawerDecorator : DrawerBase
    {
        #region Fields
        private readonly DecoratorDrawerData.IconData _data;
        #endregion

        #region Constructors
        public IconDrawerDecorator(DecoratorDrawerData.IconData data)
        {
            _data = data;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints a decorator on one village bitmap
        /// </summary>
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            var offset = _data.GetOffset(village);

            if (_data.Background.HasValue)
            {
                using (var brush = new SolidBrush(_data.Background.Value))
                {
                    g.FillRectangle(brush, village.X + offset.X, village.Y + offset.Y, offset.Width, offset.Height);
                }
            }

            if (village.Width > 20)
            {
                g.DrawImage(_data.Icon, village.X + offset.X, village.Y + offset.Y, offset.Width, offset.Height);
            }
        }
        #endregion
    }
}