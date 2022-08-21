using System;
using System.Collections.Generic;

namespace CCI.Algorithms
{
    /**
     * Given two string, write a method to decide if one is a permutation of the other.
     */
    public static class CheckPermutationTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", "", true},
                new object[] {"", "a", false},
                new object[] {"a", "a", true},
                new object[] {"abc", "abc", true},
                new object[] {"abc", "acb", true},
                new object[] {"abc", "bac", true},
                new object[] {"abc", "bca", true},
                new object[] {"abc", "cab", true},
                new object[] {"abc", "cba", true},
                new object[] {"abc", "abca", false},
                new object[] {"abc", "acba", false},
                new object[] {"abc", "aaa", false},
                new object[] {"abc", "aa", false}
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void CheckPermutation(string str1, string str2, bool expected)
        {
            Assert.Equal(expected, CheckPermutation1(str1, str2));
            Assert.Equal(expected, CheckPermutation2(str1, str2));
        }

        /**
         * Solution using an HashSet.
         * T = O(N)
         * S = O(C + D)
         *
         * N = length of the input strings. If the strings have different lengths, then T = O(1).
         *
         * C = unique characters in first input string
         * D = unique characters in second input string
         */
        private static bool CheckPermutation1(string str1, string str2)
        {
            if (str1.Length != str2.Length)
                return false;

            var dict = new Dictionary<char, int>();

            for (var i = 0; i < str1.Length; i++)
            {
                var char1 = str1[i];
                var char2 = str2[i];

                if (char1 == char2)
                    continue;

                UpdateDictCount(dict, char1, 1);
                UpdateDictCount(dict, char2, -1);
            }

            return dict.Keys.Count == 0;
        }

        private static void UpdateDictCount(IDictionary<char, int> dict, char c, int add)
        {
            if (dict.TryGetValue(c, out var count))
            {
                var newCount = count + add;
                if (newCount == 0)
                {
                    dict.Remove(c);
                }
                else
                {
                    dict[c] = newCount;
                }

                return;
            }

            dict.Add(c, add);
        }

        /**
         * Solution using an HashSet.
         * T = O(N + Nlog(N) + N + Nlog(N) + N) = O(Nlog(N))
         * S = O(N + N) = O(N) See notes below
         *
         * N = length of the input strings. If the strings have different lengths, then T = O(1).
         *
         * Notes:
         * If C# would have a method to sort strings in place, this algorithm
         *  - wouldn't need to copy the string to an array first;
         *  - wouldn't use any additional space since the strings wouldn't be converted to arrays first.
         */
        private static bool CheckPermutation2(string str1, string str2)
        {
            if (str1.Length != str2.Length)
                return false;

            var str1Array = str1.ToCharArray();
            Array.Sort(str1Array);

            var str2Array = str2.ToCharArray();
            Array.Sort(str2Array);

            for (var i = 0; i < str1.Length; i++)
            {
                if (str1Array[i] != str2Array[i])
                    return false;
            }

            return true;
        }

    }
}