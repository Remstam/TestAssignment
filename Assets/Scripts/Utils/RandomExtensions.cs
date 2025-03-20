using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestGame
{
    public static class RandomExtensions
    {
        public static T GetRandom<T>(this List<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return default;
            }

            var index = Random.Range(0, list.Count);
            return list[index];
        }

        public static T GetRandom<T>(this HashSet<T> hashSet)
        {
            return GetRandom(hashSet.ToList());
        }
        
        public static T GetRandom<T>(this T[] array)
        {
            if (array == null || array.Length == 0)
            {
                return default;
            }

            var index = Random.Range(0, array.Length);
            return array[index];
        }
    }
}