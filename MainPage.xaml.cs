
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Travel_Planner.Viewmodels;
namespace Travel_Planner
{
    public partial class MainPage : ContentPage
    {
        private Itinerary itinerary;
        private Location lastClickedDestination;
        private Editor editor;
        private Pin pin;
        public MainPage()
        {
            InitializeComponent();
            itinerary= new Itinerary();
            listView.ItemsSource = itinerary.destinations;//Object list source for the items displayed in the listview. 

        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            
            
            locationClick.Text=$"{e.Location.Latitude}, {e.Location.Longitude}";
            lastClickedDestination = new Location(e.Location.Latitude, e.Location.Longitude);

            string label = "no name yet";
            editor = new Editor { Placeholder = "Enter text", HeightRequest = 250 };
            editor.TextChanged += OnEditorTextChanged;
            editor.Completed += OnEditorCompleted;

            pin = (new Pin()
            {
                Location = lastClickedDestination,
                Label = label,
                Address = "lat: "+e.Location.Latitude + " : long: "+e.Location.Longitude
            });

            map.Pins.Add(pin);
            

            Label label2 = new Label { Text = "lat: "+e.Location.Latitude + " : long: "+e.Location.Longitude };
            stacken.Add(label2);
            gridden.Add(editor);
            
        }
        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Editor)sender).Text;
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = editor.Text;
            pin.Label = newText;
        }
        void OnEditorCompleted(object sender, EventArgs e)
        {

            string text = ((Editor)sender).Text;
            Destination newDestination = new Destination();
            newDestination.coordinates = lastClickedDestination;
            newDestination.Name = text;
            itinerary.addDestination(newDestination);
            pin.Label = text;
            gridden.Remove(editor);
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            map.Pins.Add(new Pin()
            {
                Location = new Location(50, 6),
                Label = "testbuttonlabel",
                Address = "testbuttonaddress"
            });
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(50, 6), Distance.FromKilometers(10)));
        }
    }
}