using MafiaGame.Enums;
using MafiaGame.Interfaces;

namespace MafiaGame.Services
{
    public static class Initializer
    {
        internal static Dictionary<Type, Type> ViewModelsMappedToPages { get; set; }

        public static void RegisterViewModelToPage<TPage, TViewModel>()
            where TPage : Page
            where TViewModel : class
        {
            if (ViewModelsMappedToPages == null)
                ViewModelsMappedToPages = new Dictionary<Type, Type>();

            if (ViewModelsMappedToPages.ContainsKey(typeof(TViewModel)))
                ViewModelsMappedToPages[typeof(TViewModel)] = typeof(TPage);
            else
                ViewModelsMappedToPages.Add(typeof(TViewModel), typeof(TPage));
        }

        internal static Type GetPageTypeFromVM<TViewModel>()
        {
            if (ViewModelsMappedToPages == null)
                throw new Exception("You must register the pages");

            if (ViewModelsMappedToPages.ContainsKey(typeof(TViewModel)))
                return ViewModelsMappedToPages[typeof(TViewModel)];
            else
                throw new Exception("You must register the pages");
        }

        public static MauiAppBuilder UseNavigation(this MauiAppBuilder mauiAppBuilder, TNavigationType navigationType)
        {
            if (navigationType == TNavigationType.Shell)
                mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();
            else if (navigationType == TNavigationType.NavigationPage)
                mauiAppBuilder.Services.AddSingleton<INavigationService, NavigationService>();

            return mauiAppBuilder;
        }
    }
}
