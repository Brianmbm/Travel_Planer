using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel_Planer.Viewmodels
{
    internal class Itinerary
    {
        public List<Destination> destinations;
        public List<Navigation> navigations;

        public Itinerary()
        {
            destinations = new List<Destination>();
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
