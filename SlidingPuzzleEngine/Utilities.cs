using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidingPuzzleEngine
{
    internal static class Utilities
    {
        public static void ThrowExceptionIfGreaterThan<T>(T parameter, T comparator, string paramName)
            where T : IComparable<T>
        {
            if(parameter.CompareTo(comparator) > 0) 
            {
                throw new ArgumentException(String.Format("{0} is greater than {1}", paramName, comparator));
            }
        }

        public static void ThrowExceptionIfLessThan<T>(T parameter, T comparator, string paramName)
    where T : IComparable<T>
        {
            if (parameter.CompareTo(comparator) < 0)
            {
                throw new ArgumentException(String.Format("{0} is less than {1}", paramName, comparator));
            }
        }
    }
}
