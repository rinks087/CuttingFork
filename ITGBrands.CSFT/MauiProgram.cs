using Microsoft.Extensions.Logging;
using ITGBrands.CSFT.Services;
using ITGBrands.CSFT.ViewModels;

namespace ITGBrands.CSFT
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddSingleton<ISaratogaService, SaratogaService>();
            builder.Services.AddSingleton<SaratogaViewModel>();
            builder.Services.AddSingleton<MainPage>();



            builder
                .UseMauiApp<App>()


                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });


        builder.Services.AddSingleton<IRFIDService, WindowsRFIDService>();


            // Register services and view models

#if DEBUG
            //  builder.Logging.AddDebug();
#endif

            return builder.Build();


        }
    }
}

