using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using TribalWars.Maps.Displays;
using TribalWars.Maps.Drawing.Drawers;
using TribalWars.Maps.Markers;
using TribalWars.Villages;

namespace TribalWars.Maps.Drawing.Views
{
    /// <summary>
    /// The background views and decorators that can be used by a map
    /// to draw certain shapes/icons
    /// </summary>
    public class ViewsCollection
    {
        private readonly Dictionary<string, IBackgroundView> _backgroundViews;
        private readonly List<IDecoratorView> _decorators;

        public ViewsCollection()
        {
            _backgroundViews = new Dictionary<string, IBackgroundView>();
            _decorators = new List<IDecoratorView>();
        }

        public ViewsCollection(IEnumerable<IBackgroundView> backgroundViews, IEnumerable<IDecoratorView> decorators)
        {
            _backgroundViews = backgroundViews.ToDictionary(x => x.Name);
            _decorators = decorators.ToList();
        }

        public BackgroundDrawerData GetBackgroundDrawerData(Village village, Marker marker)
        {
            BackgroundDrawerData data = _backgroundViews[marker.Settings.View].GetBackgroundDrawer(village);
            return data;
        }

        public IEnumerable<DrawerBase> GetDecoratorDrawers(DrawerFactoryBase drawerFactory, Village village, BackgroundDrawerData mainData)
        {
            return _decorators
                .Select(decorator => decorator.GetDecoratorDrawer(drawerFactory, village, mainData))
                .Where(drawer => drawer != null);
        }

        public IEnumerable<string> GetBackgroundViews(bool alsoGetBarbarianViews)
        {
            var views = _backgroundViews.Select(x => x.Value);
            if (!alsoGetBarbarianViews)
            {
                views = views.Where(x => x.Name != "Abandoned");
            }
            return views.Select(x => x.Name);
        }

        public string WriteViews()
        {
            IEnumerable<IView> views = _backgroundViews.Values.OfType<IView>().Union(_decorators);

            var output = new XDocument(
                new XElement("Views",
                    views.Select(view => new XElement("View",
                            new XAttribute("Type", view.Type),
                            new XAttribute("Name", view.Name),
                            new XElement("Drawers", view.WriteDrawerXml())))));

            return output.ToString();
        }
    }
}
