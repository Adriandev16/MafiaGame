using MafiaGame.ViewModels;

namespace MafiaGame.Views;

public partial class HostingPage : ContentPage
{
    public HostingPage(HostingPageViewModel hostingPageViewModel)
    {
        InitializeComponent();
        BindingContext = hostingPageViewModel;
    }

}