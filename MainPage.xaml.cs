
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Travel_Planner.Viewmodels;
using static System.Net.Mime.MediaTypeNames;

namespace Travel_Planner
{
    public partial class MainPage : ContentPage
    {
        private Itinerary itinerary;
        private Location lastClickedDestination;
        private Editor editor;
        private Pin pin;
        private StackLayout stackLayout;
        public MainPage()
        {
            InitializeComponent();
            itinerary= new Itinerary();
            listView.ItemsSource = itinerary.destinations;//Object list source for the items displayed in the listview. 

        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {
            lastClickedDestination = new Location(e.Location.Latitude, e.Location.Longitude);


            pin = (new Pin()
            {
                Location = lastClickedDestination,
                Label = "Unnamed Marker",
                Address = "lat: "+e.Location.Latitude + " : long: "+e.Location.Longitude
            });

            map.Pins.Add(pin);
            stackLayout = new StackLayout();

            editor = new Editor { Placeholder = "Enter text", HeightRequest = 100};
            editor.TextChanged += OnEditorTextChanged;

            var button = new Button { Text = "Submit" };
            button.HorizontalOptions = LayoutOptions.End;
            button.Clicked += EditorButton;

            stacken.Children.Add(stackLayout);
            stackLayout.Children.Add(editor);
            stackLayout.Children.Add(button);

        }

        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            map.Pins.Remove(pin);
            map.Pins.Add(pin);
            string text = ((Editor)sender).Text;
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = editor.Text;
            pin.Label = newText;
        }
        
        void OnEditorCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;

        }
        void EditorButton (object sender, EventArgs e)
        {
            map.Pins.Remove(pin);
            map.Pins.Add(pin);
            string text = editor.Text; 
            editor.Completed += OnEditorCompleted;
            pin.Label = text;
            Destination newDestination = new Destination();
            newDestination.coordinates = lastClickedDestination;
            newDestination.Name = text;
            itinerary.AddDestination(newDestination);
            stackLayout.Children.Clear();


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