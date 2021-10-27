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
    public partial class FavouritePlayers : ContentPage
    {
        public FavouritePlayers()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            playerList.ItemsSource = await App.Database.GetPlayersAsync();
        }

        // to delete a player, slide to the right and click on the "Delete" button
        private async void OnDeleteSwipeItemInvoked(object sender, SelectedItemChangedEventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            PlayerDB tmp = item.BindingContext as PlayerDB;
            PlayerDB dbPlayer = await App.Database.GetPlayerAsync(tmp.personId);


            await App.Database.RemovePlayerAsync(dbPlayer);
            playerList.ItemsSource = await App.Database.GetPlayersAsync(); // refresh items


            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{dbPlayer.fullName} has been removed to your favourite list"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }
    }
}