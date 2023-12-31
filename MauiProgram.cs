﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Maps;
using Syncfusion.Maui.Core.Hosting;

namespace Travel_Planner;


#if WINDOWS
    public static class MauiProgram
    {

    public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
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
#elif ANDROID

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureSyncfusionCore();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiMaps();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
#endif