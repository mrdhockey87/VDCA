using System.Collections.Generic;

namespace VDCA.Extension
{
    internal static class ListExtensions
    {
        private static readonly System.Random rng = new();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[n], list[k]) = (list[k], list[n]);
            }
        }
        public static void ShuffleArray<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }
        public static void ShuffleValueTuple<T>(this (T item1, T item2)[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp1 = array[n].item1;
                T temp2 = array[n].item1;
                array[n].item1 = array[k].item1;
                array[n].item2 = array[k].item2;
                array[k].item1 = temp1;
                array[k].item2 = temp2;
            }
        }
    }
}
