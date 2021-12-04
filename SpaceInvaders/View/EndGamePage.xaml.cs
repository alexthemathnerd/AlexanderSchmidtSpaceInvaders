using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using SpaceInvaders.Model;
using SpaceInvaders.Model.UserComparer;
using SpaceInvaders.ViewModel;

namespace SpaceInvaders.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EndGamePage
    {

        private int score;
        private int level;
        private LeaderBoardViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndGamePage"/> class.
        /// </summary>
        public EndGamePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.viewModel = (LeaderBoardViewModel)this.DataContext;
            var array = (int[])e.Parameter ?? throw new ArgumentException();
            this.score = array[0];
            this.level = array[1];
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

        private void PlayAgain_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(StartPage));
        }
    }
}
