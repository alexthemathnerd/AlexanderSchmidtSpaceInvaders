using System;
using System.Collections.Generic;

namespace SpaceInvaders.Model.UserComparer
{
    /// <summary>
    /// A Comparer for Users base off of Score then Name then Level.
    /// </summary>
    public class ScoreNameLevelComparer : IComparer<User>
    {
        public int Compare(User user1, User user2)
        {
            if (user1 == null)
            {
                throw new ArgumentException();
            }
            if (user2 == null)
            {
                throw new ArgumentException();
            }
            if (user1.Score.CompareTo(user2.Score) != 0)
            {
                return -1 * user1.Score.CompareTo(user2.Score);
            }

            if (String.Compare(user1.Name, user2.Name, StringComparison.Ordinal) != 0)
            {
                return String.Compare(user1.Name, user2.Name, StringComparison.Ordinal);
            }
            return -1 * user1.CompletedLevel.CompareTo(user2.CompletedLevel);
        }
    }
}
