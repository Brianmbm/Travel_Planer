using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Planner.Viewmodels
{
    internal class Itinerary
    {
        public ObservableCollection<Destination> destinations;
        public List<Navigation> navigations;

        public Itinerary()
        {
            destinations = new ObservableCollection<Destination>();
            navigations = new List<Navigation>();
        }

        public void addDestination(Destination add)
        {
            destinations.Add(add);
        }
        public void addNavigation(Navigation add)
        {
            navigations.Add(add);
        }
    }
}
