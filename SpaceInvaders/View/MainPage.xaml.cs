using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using SpaceInvaders.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SpaceInvaders.View
{
    /// <summary>
    ///     The main page for the game.
    /// </summary>
    public sealed partial class MainPage
    {
        #region Data members

        /// <summary>
        ///     The application height
        /// </summary>
        public const double ApplicationHeight = 720;

        /// <summary>
        ///     The application width
        /// </summary>
        public const double ApplicationWidth = 1080;
        private readonly DispatcherTimer timer;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            
            this.InitializeComponent();

            ApplicationView.PreferredLaunchViewSize = new Size { Width = ApplicationWidth, Height = ApplicationHeight };
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(ApplicationWidth, ApplicationHeight));

            var gameManager = new GameManager(this.theCanvas);
            gameManager.ScoreUpdateEvent += this.onScoreUpdate;
            gameManager.GameOverEvent += this.onGameOver;
            gameManager.LevelChangeEvent += this.onLevelChange;
            gameManager.HealthUpdateEvent += this.onHealthUpdate;
            gameManager.InitializeGame();
            this.scoreSummary.Text = "Score: 0";
            this.gameSummary.Text = "SPACE INVADERS!!!";
            this.healthSummary.Text = "Health: 3";

            Window.Current.CoreWindow.KeyDown += gameManager.OnKeyDown;
            /* Window.Current.CoreWindow.KeyUp += gameManager.OnKeyUp;*/

            this.timer = new DispatcherTimer();
            this.timer.Tick += gameManager.OnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            this.timer.Start();
        }

        #endregion

        #region Methods

        private void onLevelChange(object sender, LevelChangeEventArgs e)
        {
            Debug.WriteLine("LEVEL UP!");
            if (e.NewLevel > 3)
            {
                this.timer.Stop();
                this.gameSummary.Text = "Game Over. You Win!";
                var gameManager = (GameManager)sender;
                this.Frame.Navigate(typeof(EndGamePage), new int[] { gameManager.Score, gameManager.CurrentLevel - 1});
            }
            else
            {
                this.gameSummary.Text = $"Level: {e.NewLevel}";
            }
        }
        private void onGameWin(object sender, EventArgs e)
        {
            SoundManager.Play(SoundEffectsEnum.Win);
            this.timer.Stop();
            this.gameSummary.Text = "Game Over. You Win!";
        }

        private void onHealthUpdate(object sender, HealthUpdateArgs e)
        {
            this.healthSummary.Text = $"Health: {e.NewHealth}";
        }

        private void onGameOver(object sender, EventArgs e)
        {
            this.timer.Stop();
            this.gameSummary.Text = "Game Over. You Lose!";
            var gameManager = (GameManager)sender;
            this.Frame.Navigate(typeof(EndGamePage), new int[] {gameManager.Score, gameManager.CurrentLevel - 1});

        }

        private void onScoreUpdate(object sender, ScoreUpdateArgs e)
        {
            this.scoreSummary.Text = $"Score: {e.NewScore}";
        }

        #endregion
    }
}