using CommunityToolkit.Mvvm.ComponentModel;
using MafiaGame.Interfaces;

namespace MafiaGame.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        bool isBusy = false;

        protected readonly INavigationService Navigation;
        public BaseViewModel(INavigationService navigationService)
        {
            Navigation = navigationService;
        }
    }
}
