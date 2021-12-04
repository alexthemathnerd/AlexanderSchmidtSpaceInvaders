using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using SpaceInvaders.Model;
using SpaceInvaders.Model.UserComparer;

namespace SpaceInvaders.ViewModel
{
    /// <summary>
    ///     The view model for the Leader board
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class LeaderBoardViewModel : INotifyPropertyChanged
    {

        #region Properties

        /// <summary>
        ///     Gets or sets the top players.
        /// </summary>
        /// <value>
        ///     The top players.
        /// </value>
        public ObservableCollection<User> TopPlayers { get; set; }

        /// <summary>
        ///     Gets or sets the sorts.
        /// </summary>
        /// <value>
        ///     The sorts.
        /// </value>
        public ObservableCollection<UserSort> Sorts { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="LeaderBoardViewModel" /> class.
        /// </summary>
        public LeaderBoardViewModel()
        {
            this.TopPlayers = new ObservableCollection<User>(LeaderBoard.ReadTopPlayers());
            this.Sorts = new ObservableCollection<UserSort> {
                new UserSort {
                    Name = "SCORE | NAME | LEVEL",
                    Sort = new ScoreNameLevelComparer()
                },
                new UserSort {
                    Name = "NAME | SCORE | LEVEL",
                    Sort = new NameScoreLevelComparer()
                },
                new UserSort {
                    Name = "LEVEL | SCORE | NAME",
                    Sort = new LevelScoreNameComparer()
                }
            };
        }

        #endregion

        #region Methods

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Sorts by the specified sort.
        /// </summary>
        /// <param name="sort">The sort.</param>
        public void Sort(UserSort sort)
        {
            var list = this.TopPlayers.ToList();

            list.Sort(sort.Sort ?? new ScoreNameLevelComparer());
            this.TopPlayers = new ObservableCollection<User>(list);
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TopPlayers"));
        }

        /// <summary>
        ///     Determines whether [is top player] [the specified score].
        /// </summary>
        /// <param name="score">The score.</param>
        /// <returns>
        ///     <c>true</c> if [is top player] by [the specified score]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsTopPlayer(int score)
        {
            var topPlayers = this.TopPlayers.ToList();
            return topPlayers.Any(player => score.CompareTo(player.Score) > 0) || topPlayers.Count < 10;
        }

        /// <summary>
        ///     Adds the top player.
        /// </summary>
        /// <param name="user">The user.</param>
        public void AddTopPlayer(User user)
        {
            if (this.TopPlayers.Count >= 10)
            {
                this.TopPlayers.Remove(this.TopPlayers.Last());
            }

            this.TopPlayers.Add(user);
            LeaderBoard.WriteTopPlayers(this.TopPlayers.ToList());
        }

        #endregion
    }
}