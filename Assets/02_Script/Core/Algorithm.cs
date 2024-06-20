using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Extension
{
    public static class Algorithm
    {
        private static Random _random = new Random();

        public static T[] Shuffle<T>(this T[] toShuffleArr)
        {
            for (int i = toShuffleArr.Length - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }

        public static List<T> Shuffle<T>(this List<T> toShuffleArr)
        {
            for (int i = toShuffleArr.Count - 1; i > 0; i--)
            {
                int r = _random.Next(i + 1);

                T value = toShuffleArr[r];
                toShuffleArr[r] = toShuffleArr[i];
                toShuffleArr[i] = value;
            }

            return toShuffleArr;
        }
    }

}

