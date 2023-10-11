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

        public void AddDestination(Destination add)
        {
            destinations.Add(add);

            for (int i = 0; i<destinations.Count; i++)
            {
                if (DateTime.Compare(destinations[i].date, destinations.Last().date) > 0)
                {
                    destinations.Move(destinations.Count-1, i);
                    break;
                }
            }

        }
        public void RemoveDestination(Destination remove)
        {
            destinations.Remove(remove);
        }
        public void addNavigation(Navigation add)
        {
            navigations.Add(add);
        }
        public void RemoveNavigation(Navigation remove)
        {
            navigations.Remove(remove);
        }


        public void PrintCalendar()
        {
            // TODO: implement
        }
        public void Save()
        {
            // TODO: implement
        }

        public void ShowTotalPrice()
        {
            // TODO: implement
        }
        
    }
}
