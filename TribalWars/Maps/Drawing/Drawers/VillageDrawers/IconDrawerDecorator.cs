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
        private readonly Point _offset;
        #endregion

        #region Constructors
        public IconDrawerDecorator(DecoratorDrawerData.IconData data)
        {
            _data = data;
            _offset = data.GetOffset();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints a decorator on one village bitmap
        /// </summary>
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            if (_data.Background.HasValue)
            {
                using (var brush = new SolidBrush(_data.Background.Value))
                {
                    // TODO: bug: village.Height is not correct!
                    g.FillRectangle(brush, village.X + _offset.X, village.Y + _offset.Y, village.Width, village.Height);
                }
            }

            g.DrawImage(_data.Icon, new Point(village.X + _offset.X, village.Y + _offset.Y));
        }
        #endregion
    }
}