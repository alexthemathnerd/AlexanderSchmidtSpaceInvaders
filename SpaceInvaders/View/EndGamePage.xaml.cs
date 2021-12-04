using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SpaceInvaders.Model;
using SpaceInvaders.Model.UserComparer;
using SpaceInvaders.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndGamePage : Page
    {

        private int score;
        private int level;
        private LeaderBoardViewModel viewModel;

        public EndGamePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.viewModel = (LeaderBoardViewModel)this.DataContext;
            this.score = ((int[])e.Parameter)[0];
            this.level = ((int[])e.Parameter)[1];
            if (this.viewModel.IsTopPlayer(this.score))
            {
                this.topTenForm.Visibility = Visibility.Visible;
            }
        }


        private void AddTopPlayer_OnClick(object sender, RoutedEventArgs e)
        {
            this.viewModel.AddTopPlayer(new User
            {
                Name = this.nameTextBox.Text,
                CompletedLevel = this.level,
                Score = this.score
            });
            this.topTenForm.Visibility = Visibility.Collapsed;
        }

        private void Sort_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.viewModel.Sort((UserSort) this.sorts.SelectedItem);
        }

        private void OnGoHome(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StartPage));
        }
    }
}
