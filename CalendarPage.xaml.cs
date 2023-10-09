using Syncfusion.Maui.Calendar;

namespace Travel_Planner;

public partial class CalendarPage : ContentPage
{
    SfCalendar Calendar;
    public CalendarPage()
	{
		InitializeComponent();
        SfCalendar calendar = new SfCalendar();
        this.Content = calendar;
        this.Calendar.View = CalendarView.Month;
        this.Calendar.MonthView.FirstDayOfWeek = DayOfWeek.Monday;
        this.Calendar.ShowActionButtons = true;
        this.Calendar.ShowTodayButton = true;
    }
}