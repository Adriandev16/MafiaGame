using CommunityToolkit.Maui;
using MafiaGame.Enums;
using MafiaGame.Interfaces;
using MafiaGame.Services;
using MafiaGame.ViewModels;
using MafiaGame.Views;
using Microsoft.Maui.Controls.Compatibility.Hosting;

namespace MafiaGame;

public static class MauiProgram
{
    public static TNavigationType NavigationType = TNavigationType.NavigationPage;
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder()
                                 .UseMauiApp<App>()
                                 .UseMauiCompatibility()
                                 .UseMauiCommunityToolkit()
                                 .ConfigureFonts(fonts =>
                                 {
                                     fonts.AddFont("FontAwesome6Brands.otf", "FontAwesomeBrands");
                                     fonts.AddFont("FontAwesome6Duotone.otf", "FontAwesomeDuotone");
                                     fonts.AddFont("FontAwesome6Light.otf", "FontAwesomeLight");
                                     fonts.AddFont("FontAwesome6Regular.otf", "FontAwesomeRegular");
                                     fonts.AddFont("FontAwesome6Solid.otf", "FontAwesomeSolid");
                                     fonts.AddFont("FontAwesome6Thin.otf", "FontAwesomeThin");
                                 })

                                 .UseNavigation(NavigationType)
                                 .MapViewMoldelsToPages()

                                 .RegisterAppServices()
                                 .RegisterViewModels()
                                 .RegisterViews();

        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<PlayingGameManager>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<HostingPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        if (NavigationType == TNavigationType.NavigationPage)
        {
            mauiAppBuilder.Services.AddTransient<MainPage>();
            mauiAppBuilder.Services.AddTransient<HostingPage>();
        }

        return mauiAppBuilder;
    }

    public static MauiAppBuilder MapViewMoldelsToPages(this MauiAppBuilder mauiAppBuilder)
    {
        Initializer.RegisterViewModelToPage<MainPage, MainPageViewModel>();
        Initializer.RegisterViewModelToPage<HostingPage, HostingPageViewModel>();

        return mauiAppBuilder;
    }
}
