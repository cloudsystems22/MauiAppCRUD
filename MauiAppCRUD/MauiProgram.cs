using CommunityToolkit.Maui;
using MauiAppCRUD.Data;
using MauiAppCRUD.View;
using MauiAppCRUD.ViewModel;
using Microsoft.Extensions.Logging;

namespace MauiAppCRUD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
            builder.Services.AddSingleton<CustomersPage>();
            builder.Services.AddSingleton<CustomerDetailPage>();
            builder.Services.AddSingleton<CustomerSavePage>();
            builder.Services.AddSingleton<CustomerViewModel>();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
