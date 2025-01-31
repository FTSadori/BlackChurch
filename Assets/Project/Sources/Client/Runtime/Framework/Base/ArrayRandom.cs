using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Runtime.Framework.Base
{
    public static class ArrayRandom
    {
        public static List<T> Shuffle<T>(List<T> list)
        {
            Random rand = new();

            List<T> result = list.ToList();
            for (int i = 0; i < result.Count; ++i)
            {
                int r = rand.Next(i, result.Count);
                (result[r], result[i]) = (result[i], result[r]);
            }

            return result;
        }
    }
}