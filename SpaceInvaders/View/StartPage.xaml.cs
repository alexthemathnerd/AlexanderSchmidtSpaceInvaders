using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SpaceInvaders.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage
    {
        public StartPage()
        {
            this.InitializeComponent();
        }

        private void OnClickStartGame(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }

        private void OnClickViewLeaderBoard(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LeaderBoardControl));
        }

        private void ResetLeaderBoard_OnClick(object sender, RoutedEventArgs e)
        {
            LeaderBoard.WriteTopPlayers(new List<User>());

        }
    }
}
