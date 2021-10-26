using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PlayerDetails : ContentPage
    {
        public Player player;
        public PlayerStats playerStats;
        public PlayerDB dbPlayer;
        public bool btn;
        public PlayerDetails(Player p, PlayerStats pStats)
        {
            InitializeComponent();
            this.player = p;
            this.playerStats = pStats;
            //playerName.Text = this.player.fullName;
            image.Source = this.player.imageURL;
            playerName.Text = this.player.fullName;
            teamName.Text = this.player.teamName;
            lastAffliation.Text = this.player.lastAffiliation;
            height.Text = this.player.heightFeet + "'" + this.player.heightInches;
            weight.Text = this.player.weightPounds + "lbs";
            jersey.Text = this.player.jersey;
            position.Text = this.player.teamSitesOnly.posFull;

            pts.Text = this.playerStats.ppg;
            rbs.Text = this.playerStats.rpg;
            asts.Text = this.playerStats.apg;
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
            dbPlayer = await App.Database.GetPlayerAsync(player.personId);

            if (dbPlayer != null)
            {
                changeBtnViewIfNotExist();
            }
            else 
            {
                changeBtnViewIfExist();
            }
        }

        private async void addPlayerToDB(object sender, EventArgs e)
        {
            await App.Database.AddPlayerAsync(new PlayerDB
            {
                firstName = player.firstName,
                lastName = player.lastName,
                personId = player.personId,
                teamId = player.teamId,
                yearsPro = player.yearsPro,
                fullName = player.fullName,
                imageURL = player.imageURL,
                posFull = player.teamSitesOnly.posFull
            });
            changeBtnViewIfNotExist();
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{player.fullName} has been added to your favourite list!"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }

        private async void removePlayerFromDB(object sender, EventArgs e)
        {
            dbPlayer = await App.Database.GetPlayerAsync(player.personId);
            await App.Database.RemovePlayerAsync(dbPlayer);
            changeBtnViewIfExist();
            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{player.fullName} has been removed to your favourite list"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }
    }
}