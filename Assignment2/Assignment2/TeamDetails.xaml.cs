using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Assignment2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetails : ContentPage
    {
        public TeamObj team;
        public List<Player> players;
        public TeamObj dbTeam;
        public TeamDetails(TeamObj t, List<Player> p)
        {
            team = t;
            players = p;
            InitializeComponent();
            image.Source = team.imageURL;
            teamName.Text = team.fullName;
            conference.Text = team.confName;
            division.Text = team.divName;

            playersList.ItemsSource = players;
        }

        public void changeBtnViewIfNotExist()
        {
            btnAdd.IsVisible = false;
            btnRemove.IsVisible = true;
        }

        public void changeBtnViewIfExist()
        {
            btnAdd.IsVisible = true;
            btnRemove.IsVisible = false;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            dbTeam = await App.Database.GetTeamAsync(team.teamId);

            if (dbTeam != null)
            {
                changeBtnViewIfNotExist();
            }
            else
            {
                changeBtnViewIfExist();
            }
        }

        private async void addTeamToDB(object sender, EventArgs e)
        {
            await App.Database.AddTeamAsync(team);
            changeBtnViewIfNotExist();
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{team.fullName} has been added to your favourite list!"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }

        private async void removeTeamFromDB(object sender, EventArgs e)
        {
            dbTeam = await App.Database.GetTeamAsync(team.teamId);
            await App.Database.RemoveTeamAsync(dbTeam);
            changeBtnViewIfExist();
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{team.fullName} has been removed to your favourite list"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }
    }
}