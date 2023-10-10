
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Maps;
using Syncfusion.Maui.Calendar;
using Travel_Planner.Viewmodels;
using static System.Net.Mime.MediaTypeNames;

namespace Travel_Planner
{
    public partial class MainPage : ContentPage
    {
        private Itinerary itinerary;
        private Location lastClickedDestination;
        private Editor nameInputField;
        private Editor priceInputField;
        private Pin currentPin;
        private StackLayout stackLayout;
        private Border border;
        private Boolean hasCreateDestinationPopup = false;
        private SfCalendar Calendar;
        

        public MainPage()
        {
            InitializeComponent();
            itinerary= new Itinerary();
            listView.ItemsSource = itinerary.destinations;//Object list source for the items displayed in the listview. 
        }

        void OnMapClicked(object sender, MapClickedEventArgs e)
        {

            if (!hasCreateDestinationPopup)
            {
                lastClickedDestination = new Location(e.Location.Latitude, e.Location.Longitude);
                currentPin = new Pin()
                {
                    Location = lastClickedDestination,
                    Label = "Unnamed Marker", 
                    Address = "lat: " + e.Location.Latitude + " : long: " + e.Location.Longitude
                };
                map.Pins.Add(currentPin);
                CreateAddDestinationPopup();
            }

        }
        private void CreateAddDestinationPopup()
        {
            stackLayout = new StackLayout();
            stackLayout.Background = Color.Parse("white");
            stackLayout.Padding = 5;
            stackLayout.Margin = 10;


            border = new Border();
            border.HeightRequest = 250;
            border.VerticalOptions = LayoutOptions.Center;
            border.HorizontalOptions = LayoutOptions.Center;
            border.WidthRequest = 220;
            border.Padding = 2;
            border.Background = Color.Parse("white");
            border.StrokeThickness = 4;
            border.Stroke = Color.Parse("black");
            border.StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(25, 25, 25, 25)
            };

            nameInputField = new Editor { Placeholder = "Enter Destination Name" };
            nameInputField.TextChanged += OnEditorTextChanged;
            nameInputField.BackgroundColor = Color.Parse("white");
            nameInputField.Margin = 2;

            priceInputField = new Editor { Placeholder = "Enter Price" };
            priceInputField.TextChanged += OnEditorTextChanged;
            priceInputField.BackgroundColor = Color.Parse("white");
            priceInputField.Margin = 2;

            var dateButton = new Button { Text = "Date" };
            dateButton.HorizontalOptions = LayoutOptions.End;
            dateButton.Clicked += PopUpCalendar;
            dateButton.Padding = 5;

            var addButton = new Button { Text = "Add" };
            addButton.HorizontalOptions = LayoutOptions.End;
            addButton.Clicked += CreateDestinationButton;
            addButton.Padding = 5;

            var cancelButton = new Button { Text = "Cancel" };
            cancelButton.HorizontalOptions = LayoutOptions.Start;
            cancelButton.Clicked += CancelCreateDestinationButton;
            cancelButton.Padding = 5;

            border.Content = stackLayout;
            gridden.Add(border, 1,0);
            stackLayout.Children.Add(nameInputField);
            stackLayout.Children.Add(priceInputField);
            stackLayout.Children.Add(dateButton);
            stackLayout.Children.Add(cancelButton);
            stackLayout.Children.Add(addButton);


            hasCreateDestinationPopup = true;
        }
        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Editor)sender).Text;
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = nameInputField.Text;
            currentPin.Label = newText;
        }
        
        void OnEditorCompleted(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;

        }
        void CancelCreateDestinationButton(object sender, EventArgs e)
        {


            while (map.Pins.Remove(currentPin))
            {

                hasCreateDestinationPopup = false;
            }
            gridden.Remove(border);

            hasCreateDestinationPopup = false;
        }
        void CreateDestinationButton (object sender, EventArgs e)
        {
            

            string text = nameInputField.Text; 
            nameInputField.Completed += OnEditorCompleted;
            currentPin.Label = text;
            Destination newDestination = new Destination();
            newDestination.coordinates = lastClickedDestination;
            newDestination.Name = text;
            newDestination.price = Convert.ToInt32(priceInputField.Text); // TODO: add check for valid number
            currentPin.Label = text;

            itinerary.AddDestination(newDestination);
            gridden.Remove(border);
            AddPolyLineBetweenLastTwoDestinations();
            hasCreateDestinationPopup = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void AddPolyLineBetweenLastTwoDestinations()
        {
            if (itinerary.destinations.Count > 1) 
            {
                int lastDestination = itinerary.destinations.Count-2;
                int newDestination = itinerary.destinations.Count-1;
            var polyline1 = new Microsoft.Maui.Controls.Maps.Polyline
            {
                StrokeColor = Colors.Red,
                StrokeWidth = 10,
                Geopath =
                {
                    itinerary.destinations[lastDestination].coordinates,
                    itinerary.destinations[newDestination].coordinates
                }
            };
            map.MapElements.Add(polyline1);
            }
        }
        void OnDestinationListClick(object sender, SelectedItemChangedEventArgs e)
        {
            Destination selectedDestination = itinerary.destinations[e.SelectedItemIndex];
            map.MoveToRegion(MapSpan.FromCenterAndRadius(selectedDestination.coordinates, Distance.FromKilometers(100)));
        }

        void PopUpCalendar(object sender, EventArgs e)
        { 
            this.Calendar = new SfCalendar();
            this.Content = Calendar;
            this.Calendar.ShowActionButtons = true;
            

        }
    }
}