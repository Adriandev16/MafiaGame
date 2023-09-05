namespace MafiaGame.Interfaces
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(string route, IDictionary<string, object> routeParameters = null)
            where TViewModel : class;
        Task PopAndNavigateToAsync<TViewModel>(string route, IDictionary<string, object> routeParameters = null)
            where TViewModel : class;
        Task<bool> RedirectToReferrer();
        Task PopAsync();
        Task PopToRootAsync();
        void ClearStackAndGoToRoot();
        Task<object> ShowPopupAsync(object popupPage);
    }
}
