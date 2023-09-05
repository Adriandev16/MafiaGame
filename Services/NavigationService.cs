using CommunityToolkit.Maui.Views;
using MafiaGame.Interfaces;

namespace MafiaGame.Services
{
    public class NavigationService : INavigationService
    {
        public Task NavigateToAsync<TViewModel>(string route, IDictionary<string, object> routeParameters = null)
            where TViewModel : class
        {
            var currentPage = Application.Current.MainPage;
            var pageType = Initializer.GetPageTypeFromVM<TViewModel>();
            try
            {
                var page = currentPage.Handler.MauiContext.Services.GetService(pageType) as Page;
                if (routeParameters != null && routeParameters.Count > 0)
                    (page.BindingContext as IViewModelWithParameters).SetParameters(routeParameters);
                return currentPage.Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        private static Func<Task> redirectAction;
        public async Task PopAndNavigateToAsync<TViewModel>(string route, IDictionary<string, object> routeParameters = null)
            where TViewModel : class
        {
            redirectAction = async () => Application.Current.MainPage.Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(1), () =>
            {
                NavigateToAsync<TViewModel>(route, routeParameters);
            });

            await PopAsync();
        }

        public async Task<bool> RedirectToReferrer()
        {
            if (redirectAction != null)
            {
                var rdr = (Func<Task>)redirectAction.Clone();
                redirectAction = null;
                await rdr();
                return true;
            }
            else return false;
        }

        public Task PopAsync()
        {
            return Application.Current.MainPage.Navigation.PopAsync(true);
        }

        public Task PopToRootAsync()
        {
            return Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public async void ClearStackAndGoToRoot()
        {
            while (Application.Current.MainPage.Navigation.NavigationStack.Count > 1)
                Application.Current.MainPage.Navigation.RemovePage(Application.Current.MainPage.Navigation.NavigationStack[1]);

            await Application.Current.MainPage.Dispatcher.DispatchAsync(() => Application.Current.MainPage.Navigation.PopToRootAsync());
        }

        public Task<object> ShowPopupAsync(object popupPage)
        {
            return Application.Current.MainPage.ShowPopupAsync(popupPage as Popup);
        }
    }
}
