using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Assignment2
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Player> players;
        ObservableCollection<TeamObj> teams;
        ObservableCollection<TeamClass> listOfTeams = new ObservableCollection<TeamClass>();
        NetworkingManager manager = new NetworkingManager();
        public MainPage()
        {
            InitializeComponent();
        }

        async private void MoveToPlayersPage(object sender, EventArgs e)
        {
            // fetch players
            var listFromAPI = await manager.GetPlayers();
            List<Player> list = listFromAPI;
            players = new ObservableCollection<Player>(list);

            foreach (Player i in listFromAPI)
            {
                i.imageURL = "https://cdn.nba.com/headshots/nba/latest/1040x760/" + i.personId + ".png";
                i.fullName = i.firstName + " " + i.lastName;
            }
            await Navigation.PushAsync(new PlayerPage(players));
        }

        async private void MoveToTeamsPage(object sender, EventArgs e)
        {
            var listFromAPI = await manager.GetTeams();
            listFromAPI.RemoveAll(i => !i.isNBAFranchise);
            List<TeamObj> list = listFromAPI;
            teams = new ObservableCollection<TeamObj>(list);

            foreach (TeamObj i in listFromAPI)
            {
                if (i.isNBAFranchise)
                    i.imageURL = "https://www.nba.com/.element/img/1.0/teamsites/logos/teamlogos_500x500/" + i.tricode.ToLower() + ".png"; ;
            }
            await Navigation.PushAsync(new TeamPage(teams));
        }

        async private void MoveToFavouritePlayersPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouritePlayers());
        }

        async private void MoveToFavouriteTeamsPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FavouriteTeams());
        }
    }
}
