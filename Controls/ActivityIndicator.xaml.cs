using MafiaGame.ViewModels;

namespace MafiaGame.Controls;

public partial class ActivityIndicator : ContentView
{
    public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(ActivityIndicator), false);
    public bool IsActive
    {
        get => (bool)GetValue(ActivityIndicator.IsActiveProperty);
        set => SetValue(ActivityIndicator.IsActiveProperty, value);
    }

    public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(nameof(LoadingText), typeof(string), typeof(ActivityIndicator), string.Empty);
    public string LoadingText
    {
        get => (string)GetValue(ActivityIndicator.LoadingTextProperty);
        set => SetValue(ActivityIndicator.LoadingTextProperty, value);
    }


    public ActivityIndicator()
    {
        InitializeComponent();
    }

}