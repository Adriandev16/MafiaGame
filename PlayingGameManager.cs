using CommunityToolkit.Mvvm.ComponentModel;
using MafiaGame.Interfaces;
using MafiaGame.Models;
using MafiaGame.ViewModels;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace MafiaGame
{
    public partial class PlayingGameManager : ObservableObject
    {
        protected readonly INavigationService Navigation;

        public PlayingGameManager(INavigationService navigationService)
        {
            Navigation = navigationService;
        }

        private HubConnection hubConnection;

        public string GroupName { get; set; }
        public string PlayerName { get; set; }
        public string Card { get; set; }
        public string HostPlayerName { get; set; }

        private const string PlayerConnectedName = "PlayerConnected";
        private const string SessionStartedName = "SessionStarted";

        [ObservableProperty]
        private TimeSpan timeRemaining;



        public ObservableCollection<Player> Players = new ObservableCollection<Player>();

        public async Task StartGame(string name, string groupName, bool isCreatingGame)
        {
            var httpClientHandler = new HttpClient
            {
                Timeout = TimeSpan.FromMinutes(5)
            };

            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://10.11.1.104:5106/mafiagame", options =>
                {
                    options.HttpMessageHandlerFactory = _ => new HttpClientHandler
                    {

                    };
                })
                .ConfigureLogging(logging =>
                {
                    logging.SetMinimumLevel(LogLevel.Debug);
                })
                .Build();

            this.GroupName = groupName;
            this.PlayerName = name;

            hubConnection.On<Player>(PlayerConnectedName, player =>
            {
                Players.Add(player);
            });

            hubConnection.On<SessionStarted>(SessionStartedName, async session =>
            {
                Card = session.Card;
                HostPlayerName = session.HostPlayerName;

                //await MainThread.InvokeOnMainThreadAsync(async () =>
                //{
                //    await Navigation.NavigateToAsync<HostingPageViewModel>(nameof(HostingPageViewModel));
                //});
            });

            try
            {
                //await Task.Run(async () =>
                //{
                //    await hubConnection.StartAsync();
                //});

                await hubConnection.SendAsync(PlayerConnectedName,
                    new Player
                    {
                        GroupName = groupName,
                        Name = name
                    });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.StackTrace);
            }
        }
    }
}
