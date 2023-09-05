using CommunityToolkit.Mvvm.ComponentModel;

namespace MafiaGame.ViewModels
{
    public partial class BaseLoadingTitleViewModel : ObservableObject
    {
        [ObservableProperty]
        string title = string.Empty;

        [ObservableProperty]
        bool isBusy = false;
    }
}
