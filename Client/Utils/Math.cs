using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outbreak.Utils
{
    public class Math
    {
        public static int Random(int Min, int Max)
        {
            Random RandomNumber = new Random();
            return RandomNumber.Next(Min, Max);
        }
    }
}
