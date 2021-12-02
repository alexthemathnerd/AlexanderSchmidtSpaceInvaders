using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model
{
    public class User : IComparable<User>
    {

        public String Name { get; set; }
        public int Score { get; set; }
        public int CompletedLevel { get; set; }

        public int CompareTo(User other)
        {
            if (other == null)
            {
                throw new ArgumentException();
            }

            if (this.Score.CompareTo(other.Score) != 0)
            {
                return this.Score.CompareTo(other.Score);
            }

            if (this.Name.CompareTo(other.Name) != 0)
            {
                return this.Name.CompareTo(other.Name);
            }
            return this.CompletedLevel.CompareTo(other.CompletedLevel);
        }
    }
}
