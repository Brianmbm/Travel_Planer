using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace Travel_Planer
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
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
    }
}