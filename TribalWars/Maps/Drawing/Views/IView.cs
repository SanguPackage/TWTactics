using System.Xml.Linq;
using TribalWars.Maps.Drawing.Displays;
using TribalWars.Maps.Drawing.Drawers;
using TribalWars.Villages;

namespace TribalWars.Maps.Drawing.Views
{
    public interface IBackgroundView : IView
    {
        BackgroundDrawerData GetBackgroundDrawer(Village village);
    }

    public interface IDecoratorView : IView
    {
        DrawerBase GetDecoratorDrawer(DrawerFactoryBase drawerFactory, Village village, BackgroundDrawerData mainData);
    }

    /// <summary>
    /// Common interface for Background and Decorator views
    /// </summary>
    public interface IView
    {
        string Name { get; set; }

        /// <summary>
        /// Currently either Points or VillageType
        /// </summary>
        string Type { get; set; }

        void ReadDrawerXml(XElement drawer);

        object[] WriteDrawerXml();
    }
}