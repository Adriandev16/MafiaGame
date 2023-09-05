using CommunityToolkit.Maui.Views;
using MafiaGame.ViewModels;

namespace MafiaGame.Controls;

public partial class ActivityIndicatorPopupPro : Popup
{
    public ActivityIndicatorPopupPro(BaseLoadingTitleViewModel popupVM)
    {
        InitializeComponent();
        this.BindingContext = popupVM;
    }
}