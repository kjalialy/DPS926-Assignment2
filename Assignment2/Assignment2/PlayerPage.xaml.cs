using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerPage : ContentPage
    {
        String query;
        ObservableCollection<Player> players;
        NetworkingManager manager = new NetworkingManager();
        public PlayerPage(ObservableCollection<Player> p)
        {
            InitializeComponent();
            players = p;

            playerList.ItemsSource = players;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }


        void SearchBar_TextChanged(System.Object sender,
            Xamarin.Forms.TextChangedEventArgs e)
        {
            query = e.NewTextValue;
            var dp = players.Where(c => c.fullName.ToLower().Contains(query.ToLower()));
            playerList.ItemsSource = dp;
        }

        async private void playerDetails(object sender, SelectionChangedEventArgs e)
        {
            Player p = e.CurrentSelection.FirstOrDefault() as Player;

            // get team name from GET teams API
            var listFromAPI = await manager.GetTeams();
            listFromAPI.RemoveAll(i => !i.isNBAFranchise);
            List<TeamObj> list = listFromAPI;

            ObservableCollection<TeamObj> teams = new ObservableCollection<TeamObj>(list);
            TeamObj tmp = teams.Where(c => c.teamId.Equals(p.teamId)).FirstOrDefault();


            // get playerstats
            var playerStats = await manager.GetPlayerStats(p.personId);
            PlayerStats pStats = playerStats;
            p.teamName = tmp.fullName;
            await Navigation.PushAsync(new PlayerDetails(p, pStats));
        }
    }
}