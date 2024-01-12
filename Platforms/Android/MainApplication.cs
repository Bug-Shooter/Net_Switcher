using Android.App;
using Android.Runtime;

namespace SG_Net_Switcher
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });
            builder.Services.AddMauiBlazorWebView();

            // Регистрация сервисов
            builder.Services.AddSingleton<Services.NetworkService>();
            builder.Services.AddSingleton<Services.IDHCP_Service, Platforms.Android.DHCP_Service>();
            return builder.Build();
        }
    }
}
