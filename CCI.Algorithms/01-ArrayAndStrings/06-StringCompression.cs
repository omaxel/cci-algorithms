using System.Text;

namespace CCI.Algorithms
{
    /**
     * Implement a method to perform basic string compression using the counts of a repeated characters.
     * For example, the string aabcccccaaa would become a2b1c5a3. If the "compressed" string would not become smaller than the original string,
     * your method should return the original string. You can assume the string has only uppercase and lowercase letters (a-z).
     */
    public static class StringCompressionTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", ""},
                new object[] {"a", "a"},
                new object[] {"aa", "aa"},
                new object[] {"aaa", "a3"},
                new object[] {"aaab", "aaab"},
                new object[] {"aaabb", "a3b2"},
                new object[] {"abcabc", "abcabc"},
                new object[] {"aabcccccaaa", "a2b1c5a3"},
                new object[] {"A", "A"},
                new object[] {"aA", "aA"},
                new object[] {"aaAA", "aaAA"},
                new object[] {"aaAAA", "a2A3"},
                new object[] {"abcCaAbc", "abcCaAbc"},
                new object[] {"abcCCCaAbc", "abcCCCaAbc"},
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void StringCompression(string str, string expected)
        {
            Assert.Equal(expected, StringCompression1(str));
            Assert.Equal(expected, StringCompression2(str));
        }

        /**
         * T = O(N)
         * S = O(N*2)
         *
         * N = length of the input string.
         */
        private static string StringCompression1(string str)
        {
            if (str.Length <= 1)
                return str;

            var compressed = Compress(str);

            return compressed.Length >= str.Length ? str : compressed;
        }


        /**
         * This algorithm calculates the gain obtained by compressing the string before computing the compressed string.
         * This uses less space than the previous algorithm when the total gain is 0 or negative (1 or 2 consecutive characters).
         *
         * When then gain is 0, the input string and the compressed string have the same length.
         * When the gain is negative, the compressed string is longer than the input string.
         * 
         * T = O(2N) = O(N)
         * S = O(N*2)
         *
         * N = length of the input string.
         */
        private static string StringCompression2(string str)
        {
            if (str.Length <= 1)
                return str;

            var compressedLength = GetCompressedLength(str);
            if (GetCompressedLength(str) >= str.Length)
                return str;

            return Compress(str, compressedLength);
        }

        private static string Compress(string str, int? computedLength = null)
        {
            var output = computedLength.HasValue ? new StringBuilder(computedLength.Value) : new StringBuilder();

            var count = 0;

            for (var i = 0; i < str.Length; i++)
            {
                count++;

                if (i + 1 < str.Length && str[i] == str[i + 1]) continue;

                output.Append(str[i]);
                output.Append(count);
                count = 0;
            }


            return output.ToString();
        }

        private static int GetCompressedLength(string str)
        {
            var result = 0;
            var count = 0;

            for (var i = 0; i < str.Length; i++)
            {
                count++;

                if (i + 1 < str.Length && str[i] == str[i + 1]) continue;

                result += GetCompressedLetterRepetitionLength(count);
                count = 0;
            }

            return result;
        }

        /**
         * It returns the number of characters needed after compressing the specified number of occurrences of a letter. 
         */
        private static int GetCompressedLetterRepetitionLength(int count)
        {
            return (int) Math.Floor(Math.Log10(count)) + 2;
        }
    }
}