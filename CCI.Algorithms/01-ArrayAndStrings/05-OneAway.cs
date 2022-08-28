namespace CCI.Algorithms
{
    /**
     * There are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character.
     * Given two strings, write a function to check if they are one edit (or zero edits) away.
     */
    public static class OneAwayTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] {"", "", true},
                new object[] {"a", "a", true},
                new object[] {"pale", "pale", true},

                // Insert one
                new object[] {"pale", "pales", true},
                new object[] {"pale", "spale", true},
                new object[] {"pale", "psale", true},

                // Edit one
                new object[] {"pale", "bale", true},
                new object[] {"pale", "psle", true},
                new object[] {"pale", "palt", true},

                // Remove one
                new object[] {"pale", "ale", true},
                new object[] {"pale", "pal", true},
                new object[] {"pale", "pae", true},

                // Two inserts
                new object[] {"pale", "paless", false},
                new object[] {"pale", "sspale", false},
                new object[] {"pale", "pssale", false},
                new object[] {"pale", "spsale", false},
                new object[] {"pale", "spales", false},

                // Two edits
                new object[] {"pale", "balt", false},
                new object[] {"pale", "salt", false},
                new object[] {"pale", "proe", false},

                // Two remove
                new object[] {"pale", "pl", false},
                new object[] {"pale", "al", false},
                new object[] {"pale", "pa", false},

                // Same letters, insert one
                new object[] {"pala", "palas", true},
                new object[] {"pala", "paala", true},
                new object[] {"pala", "palla", true},

                // Same letters, insert two
                new object[] {"pala", "palaaa", false},
                new object[] {"pala", "paaala", false},
                new object[] {"pala", "pallla", false},

                // Same letters, edit one
                new object[] {"pala", "paaa", true},
                new object[] {"pala", "ppla", true},
                new object[] {"pala", "aala", true},

                // Same letters, edit two
                new object[] {"pala", "apla", false},
                new object[] {"pala", "paal", false},
                new object[] {"pala", "alla", false},

                // Insert and edit
                new object[] {"pala", "appla", false},
                new object[] {"pala", "lpapa", false},
                new object[] {"pala", "lalaa", false},
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void OneAway(string str1, string str2, bool expected)
        {
            Assert.Equal(expected, OneAway1(str1, str2));
        }

        /**
         * T = O(min(N, M))
         * S = O(1)
         *
         * N = length of the first input string.
         * M = length of the second input string.
         */
        private static bool OneAway1(string str1, string str2)
        {
            var lengthDiff = Math.Abs(str1.Length - str2.Length);

            if (lengthDiff > 1)
                return false;

            if (lengthDiff == 0)
                return CheckOneDiffAtMost(str1, str2);

            return CheckOneShiftAtMost(str1, str2);
        }

        private static bool CheckOneDiffAtMost(string str1, string str2)
        {
            var foundDuplicate = false;
            for (var i = 0; i < str1.Length; i++)
            {
                if (str1[i] == str2[i]) continue;

                if (foundDuplicate)
                    return false;

                foundDuplicate = true;
            }

            return true;
        }

        private static bool CheckOneShiftAtMost(string str1, string str2)
        {
            var idxSmaller = 0;
            var idxGreater = 0;

            var greaterString = str1.Length > str2.Length ? str1 : str2;
            var smallerString = str1.Length > str2.Length ? str2 : str1;

            var diffCount = 0;

            while (idxSmaller < smallerString.Length)
            {
                if (greaterString[idxGreater] == smallerString[idxSmaller])
                {
                    idxGreater++;
                    idxSmaller++;
                    continue;
                }

                idxGreater++;
                diffCount++;

                if (diffCount > 1)
                    return false;
            }

            return true;
        }
    }
}