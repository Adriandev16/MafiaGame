using MafiaGame.Interfaces;
using MafiaGame.ViewModels;
using System.Diagnostics;

namespace MafiaGame.Controls
{
    public class ActivityIndicatorExp : ContentView
    {
        #region Exposed props
        public static readonly BindableProperty IsActiveProperty = BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(ActivityIndicatorExp), false);
        public bool IsActive
        {
            get => (bool)GetValue(ActivityIndicatorExp.IsActiveProperty);
            set => SetValue(ActivityIndicatorExp.IsActiveProperty, value);
        }

        public static readonly BindableProperty LoadingTextProperty = BindableProperty.Create(nameof(LoadingText), typeof(string), typeof(ActivityIndicatorExp), string.Empty);
        public string LoadingText
        {
            get => (string)GetValue(ActivityIndicatorExp.LoadingTextProperty);
            set => SetValue(ActivityIndicatorExp.LoadingTextProperty, value);
        }
        #endregion

        #region Private props
        private BaseLoadingTitleViewModel popupViewModel;
        private ActivityIndicatorPopupPro popupPage = null;
        private INavigationService navigation => Handler.MauiContext.Services.GetService<INavigationService>();
        private bool previousIsActive = false;
        #endregion

        public ActivityIndicatorExp()
        {
            popupViewModel = new BaseLoadingTitleViewModel();
            popupViewModel.IsBusy = true;
            popupViewModel.Title = LoadingText;
        }

        protected override async void OnPropertyChanged(string? propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsActive) && previousIsActive != IsActive)
            {
                if (Handler is null || Handler.PlatformView is null)
                {
                    HandlerChanged += OnHandlerChanged;
                    return;
                }
                previousIsActive = IsActive;
                if (IsActive)
                {
                    popupPage = new ActivityIndicatorPopupPro(popupViewModel);
                    await navigation.ShowPopupAsync(popupPage);
                }
                else
                {
                    try
                    {
                        if (popupPage != null)
                            popupPage.Close("");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
            }
            else if (propertyName == nameof(LoadingText))
                popupViewModel.Title = LoadingText;
        }

        void OnHandlerChanged(object obj, EventArgs e)
        {
            OnPropertyChanged(nameof(IsActive));
            HandlerChanged -= OnHandlerChanged;
        }

    }
}
