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
            Assert.Equal(expected, CheckPermutation1(str1.ToCharArray(), str2.ToCharArray()));
            Assert.Equal(expected, CheckPermutation2(str1.ToCharArray(), str2.ToCharArray()));
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
        private static bool CheckPermutation1(char[] str1, char[] str2)
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
         * T = O(Nlog(N) + Nlog(N) + N) = O(Nlog(N))
         * S = O(1)
         *
         * N = length of the input strings. If the strings have different lengths, then T = O(1).
         */
        private static bool CheckPermutation2(char[] str1, char[] str2)
        {
            if (str1.Length != str2.Length)
                return false;

            Array.Sort(str1);
            Array.Sort(str2);

            for (var i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                    return false;
            }

            return true;
        }
    }
}