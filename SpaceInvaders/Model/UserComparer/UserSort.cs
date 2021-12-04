using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders.Model.UserComparer
{
    public struct UserSort
    {
        public String Name { get; set; }
        public IComparer<User> Sort { get; set; }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
