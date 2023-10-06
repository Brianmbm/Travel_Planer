﻿
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Controls.Shapes;
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
        private Pin currentPin;
        private StackLayout stackLayout;
        private Border border;
        private Boolean hasCreateDestinationPopup = false;
        public MainPage()
        {
            InitializeComponent();
            itinerary= new Itinerary();
            listView.ItemsSource = itinerary.destinations;//Object list source for the items displayed in the listview. 
            PolyLine();
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(47.6368678, -122.137305), Distance.FromKilometers(10)));
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
                CreateEnterNamePopup();
            }

        }
        private void CreateEnterNamePopup()
        {
            stackLayout = new StackLayout();
            stackLayout.Background = Color.Parse("white");
            stackLayout.Padding = 5;
            stackLayout.Margin = 10;


            border = new Border();
            border.HeightRequest = 180;
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

            editor = new Editor { Placeholder = "Enter Destination Name" };
            editor.TextChanged += OnEditorTextChanged;
            editor.BackgroundColor = Color.Parse("white");
            editor.Margin = 2;

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
            stackLayout.Children.Add(editor);
            stackLayout.Children.Add(cancelButton);
            stackLayout.Children.Add(addButton);

            hasCreateDestinationPopup = true;
        }
        void OnEditorTextChanged(object sender, TextChangedEventArgs e)
        {
            string text = ((Editor)sender).Text;
            string oldText = e.OldTextValue;
            string newText = e.NewTextValue;
            string myText = editor.Text;
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
            

            string text = editor.Text; 
            editor.Completed += OnEditorCompleted;
            currentPin.Label = text;
            Destination newDestination = new Destination();
            newDestination.coordinates = lastClickedDestination;
            newDestination.Name = text;
            currentPin.Label = text;

            itinerary.AddDestination(newDestination);
            gridden.Remove(border);

            hasCreateDestinationPopup = false;
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void PolyLine()
        {
            var polyline1 = new Microsoft.Maui.Controls.Maps.Polyline
            {
                StrokeColor = Colors.Red,
                StrokeWidth = 10,
                Geopath =
                {
                    new Location(47.6368678, -122.137305),
                    new Location(47.6368894, -122.134655)
                }
            };
            map.MapElements.Add(polyline1);
        }
    }
}