using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Maps;
namespace Travel_Planner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
            .UseMauiCommunityToolkitMaps("Ahu0VIHjQfmLEUzvYoiYzYxKJ7ajPkgF4ntGms0WQyWLjN6ze39bg5TYwSClgSHt");

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}