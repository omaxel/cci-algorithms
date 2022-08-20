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
            // Assert.Equal(expected, IsUnique2(str));
        }

        /**
         * Solution using an HashSet.
         * T = O(N + M + C)
         * S = O(C + D)
         *
         * N = length of the first input string
         * M = length of the second input string
         *
         * C = unique characters in first input string
         * D = unique characters in second input string
         */
        private static bool CheckPermutation1(string str1, string str2)
        {
            if (str1.Length != str2.Length)
                return false;

            var longestStrDict = new Dictionary<char, int>();
            var shortestStrDict = new Dictionary<char, int>();

            var longestStr = str1.Length > str2.Length ? str1 : str2;
            var shortestStr = str1.Length > str2.Length ? str2 : str1;

            foreach (var character in shortestStr)
            {
                if (shortestStrDict.TryGetValue(character, out var count))
                    shortestStrDict[character] = count + 1;
                else
                    shortestStrDict.Add(character, 1);
            }

            foreach (var character in longestStr)
            {
                if (!shortestStrDict.TryGetValue(character, out var shortestStrCharacterCount))
                    return false;

                if (longestStrDict.TryGetValue(character, out var count))
                {
                    if (count + 1 > shortestStrCharacterCount)
                        return false;

                    longestStrDict[character] = count + 1;
                }
                else
                    longestStrDict.Add(character, 1);
            }

            // At this point, shortestStrDict and longestStrDict contain the same keys.
            // We have to check if, for each key, the longestStrDict has the same count as the shortestStrDict.

            foreach (var key in shortestStrDict.Keys)
            {
                if (longestStrDict[key] != shortestStrDict[key])
                    return false;
            }

            return true;
        }
    }
}