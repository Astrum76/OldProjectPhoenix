using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPhoenix
{
    public class Datatypes
    {
        //todo: add newtextbox
        public class Flag : IEquatable<Flag>
        {
            public string FlagTag { get; set; }

            public int FlagValue { get; set; }

            public override string ToString()
            {
                return FlagTag;
            }
            public bool Equals(Flag other)
            {
                if (other == null) return false;
                return (this.FlagValue == other.FlagValue);
            }
            // Should also override == and != operators.
        }
    }
}
