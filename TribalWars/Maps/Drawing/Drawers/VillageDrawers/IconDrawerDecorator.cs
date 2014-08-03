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
        private readonly Image _image;
        //private readonly bool _comments;
        //private readonly bool _nobles;

        //private static readonly Bitmap CommentsBitmap;
        //private static readonly Bitmap NoblesBitmap;
        #endregion

        #region Constructors
        public IconDrawerDecorator(DecoratorDrawerData.IconData data)
        {
            _image = data.Icon;
            //if ((type & VillageType.Comments) == VillageType.Comments) _comments = true;
            //if ((type & VillageType.Noble) == VillageType.Noble) _nobles = true;
        }

        //static IconDrawerDecorator()
        //{
        //    CommentsBitmap = Other.Note;
        //    NoblesBitmap = Other.Noble;
        //}
        #endregion

        #region Public Methods
        /// <summary>
        /// Paints a decorator on one village bitmap
        /// </summary>
        protected override void PaintVillageCore(Graphics g, Rectangle village)
        {
            if (_image != null)
            {
                g.DrawImage(_image, new Point(village.X + 35, village.Y)); // 16x16 (farm) and 18x18
            }

            //if (_comments)
            //{
            //    g.DrawImage(CommentsBitmap, new Point(village.X + 9, village.Y + 20)); // 15x15
            //}

            //if (_nobles)
            //{
            //    g.DrawImage(NoblesBitmap, new Point(village.X + 35, village.Y + 19)); //18x18
            //}
        }
        #endregion
    }
}