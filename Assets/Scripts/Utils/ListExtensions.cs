using System.Collections.Generic;

namespace TestGame
{
    public static class ListExtensions
    {
        public static T GetNext<T>(this List<T> list, ref int currentIndex)
        {
            if (list == null || list.Count == 0)
            {
                return default;
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            currentIndex++;
            if (currentIndex >= list.Count)
            {
                currentIndex = 0;
            }

            return list[currentIndex];
        }

        public static T GetPrev<T>(this List<T> list, ref int currentIndex)
        {
            if (list == null || list.Count == 0)
            {
                return default;
            }

            if (list.Count == 1)
            {
                return list[0];
            }

            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = list.Count - 1;
            }

            return list[currentIndex];
        }
    }
}