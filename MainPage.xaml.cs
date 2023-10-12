
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Maps;
using Syncfusion.Maui.Calendar;
using System.Diagnostics;
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
        private Destination selectedDestination;
        private List<Microsoft.Maui.Controls.Maps.Polyline> polyLines = new List<Microsoft.Maui.Controls.Maps.Polyline>();  

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
            border.HeightRequest = 240;
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
            dateButton.HorizontalOptions = LayoutOptions.Start;
            dateButton.Clicked += PopUpCalendar;
            dateButton.Padding = 5;
            dateButton.Margin = 2;

            var addButton = new Button { Text = "Add" };
            addButton.Margin = 2;
            addButton.HorizontalOptions = LayoutOptions.End;
            addButton.Clicked += CreateDestinationButton;
            addButton.Padding = 5;

            var cancelButton = new Button { Text = "Cancel"};
            cancelButton.Margin = 2;  
            cancelButton.HorizontalOptions = LayoutOptions.End;
            cancelButton.Clicked += CancelCreateDestinationButton;
            cancelButton.Padding = 5;

            var buttonLayout = new HorizontalStackLayout();
            buttonLayout.HorizontalOptions = LayoutOptions.End;

            border.Content = stackLayout;
            gridden.Add(border, 1,0);
            stackLayout.Children.Add(nameInputField);
            stackLayout.Children.Add(priceInputField);            
            stackLayout.Children.Add(dateButton);
            stackLayout.Children.Add(buttonLayout);
            buttonLayout.Children.Add(cancelButton);
            buttonLayout.Children.Add(addButton);


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
            newDestination.pin = currentPin;

            try
            {
                newDestination.price = Convert.ToInt32(priceInputField.Text);
            }
            catch (FormatException exception)
            {
                newDestination.price = 0;
            }

            try
            {
                DateTime selectedDateTime = ((DateTime)Calendar.SelectedDate);
                newDestination.dateString = selectedDateTime.ToString("MM/dd/yyyy");
                newDestination.date = (DateTime) Calendar.SelectedDate;
                
            }
            catch (NullReferenceException exception)
            {
                
                newDestination.dateString = DateTime.Now.ToString("MM/dd/yyyy");
                newDestination.date = DateTime.Now;
            }
            
            currentPin.Label = text;

            itinerary.AddDestination(newDestination);
            gridden.Remove(border);
            RemoveAllPolyLines();
            AddPolyLineBetweenAllDestinations();
            hasCreateDestinationPopup = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            
        }

        private void AddPolyLineBetweenAllDestinations()
        {
            for (int i = 1; i<itinerary.destinations.Count; i++)
            {
                var polyline1 = new Microsoft.Maui.Controls.Maps.Polyline
                {
                    StrokeColor = Colors.Red,
                    StrokeWidth = 10,
                    Geopath =
                {
                    itinerary.destinations[i-1].coordinates,
                    itinerary.destinations[i].coordinates
                }
                };
                map.MapElements.Add(polyline1);
                polyLines.Add(polyline1);
            }


        }
        private void RemoveAllPolyLines()
        {
            foreach (Microsoft.Maui.Controls.Maps.Polyline polyline in polyLines)
            {
                map.MapElements.Remove(polyline);
            }
        }
        void OnDestinationListClick(object sender, SelectedItemChangedEventArgs e)
        {
            selectedDestination = itinerary.destinations[e.SelectedItemIndex];
            map.MoveToRegion(MapSpan.FromCenterAndRadius(selectedDestination.coordinates, Distance.FromKilometers(100)));
        }

        void PopUpCalendar(object sender, EventArgs e)
        {
            this.Calendar = new SfCalendar();
            Calendar.Background= new SolidColorBrush(Colors.White);
            Calendar.HorizontalOptions = LayoutOptions.Fill;
            Calendar.VerticalOptions = LayoutOptions.Fill;
            Calendar.ActionButtonClicked += OnActionButtonClicked;
            gridden.Add(Calendar, 0, 0);
            gridden.SetColumnSpan(Calendar, 3);
            this.Calendar.ShowActionButtons = true;
            

        }
        private void OnActionButtonClicked(object sender, CalendarSubmittedEventArgs e)
        {
            gridden.Remove(Calendar);
        }
        private void RemoveSelectedDestination(object sender, EventArgs args)
        {
            map.Pins.Remove(selectedDestination.pin);
            itinerary.destinations.Remove(selectedDestination);
            RemoveAllPolyLines();
            AddPolyLineBetweenAllDestinations();
        }
    }
}