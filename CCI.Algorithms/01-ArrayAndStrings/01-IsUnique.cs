using System.Collections.Generic;

namespace CCI.Algorithms
{
    /**
     * Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional
     * data structures?
     */
    public static class IsUniqueTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", true},
                new object[] {"a", true},
                new object[] {"abc", true},
                new object[] {"abbc", false},
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void IsUnique(string str, bool expected)
        {
            Assert.Equal(expected, IsUnique1(str));
            Assert.Equal(expected, IsUnique2(str));
        }

        /**
         * Solution using an HashSet.
         * T = O(N)
         * S = O(N)
         *
         * N = length of input string
         */
        private static bool IsUnique1(string str)
        {
            var alreadyThereCharacters = new HashSet<char>();

            foreach (var character in str)
            {
                if (alreadyThereCharacters.Contains(character)) return false;
                alreadyThereCharacters.Add(character);
            }

            return true;
        }

        /**
         * Solution without any data structure.
         * T = O(N^2)
         * S = O(1)
         *
         * N = length of input string
         */
        private static bool IsUnique2(string str)
        {
            for (var i = 0; i < str.Length - 1; i++)
            {
                for (var j = i + 1; j < str.Length; j++)
                {
                    if (str[i] == str[j])
                        return false;
                }
            }

            return true;
        }
    }
}