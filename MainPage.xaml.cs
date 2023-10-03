
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Travel_Planer.Viewmodels;
namespace Travel_Planer
{
    public partial class MainPage : ContentPage
    {
        private Itinerary itinerary;
        public MainPage()
        {
            InitializeComponent();
            itinerary= new Itinerary();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            map.Pins.Add(new Pin()
            {
                Location = new Location(50,6),
                Label = "doibndbvnd",
                Address = "svsvpsnv"
            });
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location (50, 6), Distance.FromKilometers(10)));
        }
        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            locationClick.Text=$"{e.Location.Latitude}, {e.Location.Longitude}";
            Location location = new Location(e.Location.Latitude, e.Location.Longitude);
            string label = "no name yet";
            if (editor.Text != null) label = editor.Text;
            map.Pins.Add(new Pin()
            {
                Location = location,
                Label = label,
                Address = "lat: "+e.Location.Latitude + " : long: "+e.Location.Longitude
            });
            Destination newDestination = new Destination();
            newDestination.coordinates = location;
            itinerary.addDestination(newDestination);

            Label label2 = new Label { Text = "lat: "+e.Location.Latitude + " : long: "+e.Location.Longitude };
            stacken.Add(label2);

        }
        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = editor.Text;
        }
        void OnEditorCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;
        }
    }
}