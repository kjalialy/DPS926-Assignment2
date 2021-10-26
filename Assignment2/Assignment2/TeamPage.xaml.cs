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
    public partial class TeamPage : ContentPage
    {
        String query;
        ObservableCollection<TeamObj> teams;
        NetworkingManager manager = new NetworkingManager();
        public TeamPage(ObservableCollection<TeamObj> t)
        {
            InitializeComponent();

            teams = t;
            teamList.ItemsSource = teams;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }


        void SearchBar_TextChanged(System.Object sender,
            Xamarin.Forms.TextChangedEventArgs e)
        {
            query = e.NewTextValue;
            var dp = teams.Where(c => c.fullName.ToLower().Contains(query.ToLower()));
            teamList.ItemsSource = dp;
        }

        async private void teamDetails(object sender, SelectionChangedEventArgs e)
        {
            TeamObj t = e.CurrentSelection.FirstOrDefault() as TeamObj;

            // get team name from GET teams API
            var listFromAPI = await manager.GetPlayers();
            listFromAPI.RemoveAll(i => i.teamId != t.teamId);
            List<Player> list = listFromAPI;

            foreach (Player i in listFromAPI)
            {
                i.imageURL = "https://cdn.nba.com/headshots/nba/latest/1040x760/" + i.personId + ".png";
                i.fullName = i.firstName + " " + i.lastName;
            }

            await Navigation.PushAsync(new TeamDetails(t, list));
        }
    }
}