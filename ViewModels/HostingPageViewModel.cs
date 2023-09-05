using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MafiaGame.Interfaces;
using MafiaGame.InternalModels;
using MafiaGame.Models;
using System.Collections.ObjectModel;

namespace MafiaGame.ViewModels
{
    public partial class HostingPageViewModel : BaseViewModel, IViewModelWithParameters
    {
        [ObservableProperty]
        private Random random;

        [ObservableProperty]
        private string lobbyTitle;

        [ObservableProperty]
        private string name;

        private PlayingGameManager _gameManager;

        public ObservableCollection<PlayerDetails> Players { get; set; } = new ObservableCollection<PlayerDetails>();

        public HostingPageViewModel(PlayingGameManager gameManager, INavigationService navigationService) : base(navigationService)
        {
            _gameManager = gameManager;
        }

        public void SetParameters(IDictionary<string, object> routeParameters)
        {
            InitVM(routeParameters["HostingGame"] as ObservableCollection<Player>);
        }

        private void InitVM(ObservableCollection<Player> item)
        {
            LobbyTitle = $"Creating Game '{_gameManager.GroupName}'";

            Players.Add(new PlayerDetails
            {
                PlayerName = item.FirstOrDefault().Name,
                GroupName = item.FirstOrDefault().GroupName,
            });

            //Name = item.PlayerName;
        }

        [RelayCommand(AllowConcurrentExecutions = false)]
        private async Task Appearing(PlayerDetails item)
        {

        }
    }
}
