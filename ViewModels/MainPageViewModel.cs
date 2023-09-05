using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MafiaGame.Interfaces;
using MafiaGame.InternalModels;
using MafiaGame.Models;
using MafiaGame.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MafiaGame.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        string _playerName = string.Empty;

        [ObservableProperty]
        string _gameName = string.Empty;

        [ObservableProperty]
        bool _isWarningLabelVisible = false;

        public ObservableCollection<Player> Players = new();

        private readonly PlayingGameManager playingGameManager;

        public MainPageViewModel(INavigationService navigationService, PlayingGameManager playingGameManager) : base(navigationService)
        {
            this.playingGameManager = playingGameManager;
        }

        private void GetPlayersDetails()
        {
            Player item = new()
            {
                Name = PlayerName,
                GroupName = GameName
            };
            Players.Add(item);
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task HostGame(Player player)
        {
            IsBusy = true;
            try
            {
                if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(GameName))
                {
                    await playingGameManager.StartGame(PlayerName, GameName, true);

                    GetPlayersDetails();

                    var paramsToPass = new Dictionary<string, object> { };
                    paramsToPass.Add("HostingGame", Players);
                    await Navigation.NavigateToAsync<HostingPageViewModel>(nameof(HostingPageViewModel), paramsToPass);
                }
                else
                    IsWarningLabelVisible = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        public async Task JoinGame(PlayerDetails player)
        {
            IsBusy = true;
            try
            {
                if (!string.IsNullOrWhiteSpace(PlayerName) && !string.IsNullOrWhiteSpace(GameName))
                {
                    await playingGameManager.StartGame(PlayerName, GameName, false);

                    GetPlayersDetails();

                    var paramsToPass = new Dictionary<string, object> { };
                    paramsToPass.Add("HostingGame", Players);

                    await Navigation.NavigateToAsync<HostingPageViewModel>(nameof(HostingPageViewModel), paramsToPass);
                }
                else
                    IsWarningLabelVisible = true;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            IsBusy = false;
        }
    }
}
