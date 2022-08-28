namespace CCI.Algorithms
{
    /**
     * Given a string, write a function to check if it is a permutation of a palindrome. A palindrome is a word or phrase
     * that is the same forwards and backwards. A permutation is a rearrangement of letters. The palindrome does not need
     * to be limited to just dictionary words. You can ignore casing and non-letter characters.
     */
    public static class PalindromePermutationTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", true},
                new object[] {" ", true},
                new object[] {"  ", true},
                new object[] {"Tact Coa", true},
                new object[] {"Tacc Coa", false},
                new object[] {"Aa bb", true},
                new object[] {"Aa bbb", true},
                new object[] {" A a b b b ", true},
                new object[] {"B Ab Abc", false},
                new object[] {"Bc Ab Abc", true},
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void PalindromePermutation(string str, bool expected)
        {
            Assert.Equal(expected, PalindromePermutation1(str));
            Assert.Equal(expected, PalindromePermutation2(str));
        }

        /**
         * T = O(N)
         * S = O(N)
         *
         * N = length of the input string.
         */
        private static bool PalindromePermutation1(string str)
        {
            var realLength = 0;
            var hashSet = new HashSet<char>();

            foreach (var character in str)
            {
                if (!char.IsLetter(character))
                    continue;

                var charLower = char.ToLowerInvariant(character);
                realLength++;

                if (hashSet.Contains(charLower))
                    hashSet.Remove(charLower);
                else
                    hashSet.Add(charLower);
            }

            if (realLength % 2 == 0)
                return hashSet.Count == 0;

            return hashSet.Count == 1;
        }

        /**
         * If only ASCII letters are allowed, we can use a bit vector to store odds or evens appearing of a letter.
         * If the input string has an even number of a specific letter, the bit will set to 1 in the index position for that letter.
         * Otherwise it will be set to 0.
         * 
         * T = O(N)
         * S = O(1)
         *
         * N = length of the input strings. If the strings have different lengths, then T = O(1).
         */
        private static bool PalindromePermutation2(string str)
        {
            var bitVector = 0;

            foreach (var character in str)
            {
                if (!IsAsciiLetter(character))
                    continue;

                var bitIndex = char.ToLowerInvariant(character) - 97;
                bitVector = ToggleBit(bitVector, bitIndex);
            }

            return HasAtMostOneBitSet(bitVector);
        }

        private static int ToggleBit(int bitVector, int index)
        {
            bitVector ^= 1 << index;
            return bitVector;
        }

        private static bool IsAsciiLetter(char character)
        {
            var number = (int) character;
            return number is >= 97 and <= 122 or >= 65 and <= 90;
        }

        /// <summary>
        /// If <param name="bitVector"></param> has zero or one bit set, it returns true; otherwise false. 
        /// </summary>
        /// <param name="bitVector"></param>
        /// <returns></returns>
        private static bool HasAtMostOneBitSet(int bitVector)
        {
            return (bitVector & (bitVector - 1)) == 0;
        }
    }
}