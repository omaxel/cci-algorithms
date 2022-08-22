using System.Collections.Generic;
using System.Text;

namespace CCI.Algorithms
{
    /**
     * Write a method to replace all spaces in a string with '%20'. You may assume tha the string has sufficient space
     * at the end to hold the additional characters, and that you are given a "true" length of string.
     */
    public static class URLifyTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", 0, ""},
                new object[] {"   ", 1, "%20"},
                new object[] {"      ", 2, "%20%20"},
                new object[] {"         ", 3, "%20%20%20"},
                new object[] {"Mr John Smith    ", 13, "Mr%20John%20Smith"},
                new object[] {"Mr John Smith       ", 14, "Mr%20John%20Smith%20"},
                new object[] {" Mr John Smith      ", 14, "%20Mr%20John%20Smith"},
                new object[] {" Mr John Smith         ", 15, "%20Mr%20John%20Smith%20"},
                new object[] {"Mr John Smith          ", 15, "Mr%20John%20Smith%20%20"},
                new object[] {"  Mr John Smith        ", 15, "%20%20Mr%20John%20Smith"},
                new object[] {"  Mr John Smith              ", 17, "%20%20Mr%20John%20Smith%20%20"},
                new object[] {"Mr  John  Smith        ", 15, "Mr%20%20John%20%20Smith"},
                new object[] {"Mr  John  Smith                   ", 15, "Mr%20%20John%20%20Smith           "},
                new object[] {"a", 1, "a"},
                new object[] {"abc", 3, "abc"},
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void URLify(string str, int length, string expected)
        {
            Assert.Equal(expected, new string(URLify1(str.ToCharArray(), length)));
            Assert.Equal(expected, new string(URLify2(str.ToCharArray(), length)));
        }

        /**
         * Solution using a StringBuilder. This solution doesn't take advantage of the additional space already allocated
         * into the string.
         * T = O(N)
         * S = O(N)
         *
         * N = length of the input string.
         */
        private static char[] URLify1(char[] str, int length)
        {
            if (str.Length == length)
                return str;

            var result = new StringBuilder();

            var count = str.Length;
            for (var i = 0; i < count; i++)
            {
                if (str[i] == ' ' && i < length)
                {
                    result.Append("%20");
                    count -= 2;
                }
                else
                    result.Append(str[i]);
            }

            return result.ToString().ToCharArray();
        }

        /**
         * T = O(N)
         * S = O(1)
         *
         * N = length of the input strings. If the strings have different lengths, then T = O(1).
         */
        private static char[] URLify2(char[] str, int length)
        {
            if (str.Length == length)
                return str;

            var spaceCount = GetSpacesCount(str, length);

            var writeIndex = length - 1 + (spaceCount * 2);

            for (var readIndex = length - 1; readIndex >= 0; readIndex--)
            {
                var readChar = str[readIndex];

                if (readChar == ' ')
                {
                    str[writeIndex] = '0';
                    str[writeIndex - 1] = '2';
                    str[writeIndex - 2] = '%';
                    writeIndex -= 3;
                }
                else
                {
                    str[writeIndex] = readChar;
                    writeIndex--;
                }
            }

            return str;
        }

        private static int GetSpacesCount(char[] str, int maxLength)
        {
            var result = 0;
            for (var i = 0; i < maxLength; i++)
            {
                if (str[i] == ' ')
                    result++;
            }

            return result;
        }
    }
}