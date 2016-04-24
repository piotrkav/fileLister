using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PTLAB1
{
    [Serializable]
    class MyComparer : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            if (x.Length < y.Length)
            {
                return -1;
            }
            else if (x.Length == y.Length)
            {
                return x.CompareTo(y);
            }
            else
            {
                return 1;
            }
        }
    }
}
