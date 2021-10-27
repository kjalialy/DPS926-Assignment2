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
    public partial class FavouriteTeams : ContentPage
    {
        public FavouriteTeams()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            teamList.ItemsSource = await App.Database.GetTeamsAsync();
        }

        // to delete a team, slide to the right and click on the "Delete" button
        private async void OnDeleteSwipeItemInvoked(object sender, SelectedItemChangedEventArgs e)
        {
            SwipeItem item = sender as SwipeItem;
            TeamObj tmp = item.BindingContext as TeamObj;
            TeamObj dbTeam = await App.Database.GetTeamAsync(tmp.teamId);


            await App.Database.RemoveTeamAsync(dbTeam);
            teamList.ItemsSource = await App.Database.GetTeamsAsync(); // refresh items


            var options = new SnackBarOptions
            {
                MessageOptions = new MessageOptions
                {
                    Foreground = Color.White,
                    Message = $"{dbTeam.fullName} has been removed to your favourite list"
                },
                BackgroundColor = Color.FromHex("#E18B6D"),
                Duration = TimeSpan.FromSeconds(2)
            };

            await this.DisplaySnackBarAsync(options);
        }
    }
}