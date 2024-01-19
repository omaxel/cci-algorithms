namespace CCI.Algorithms
{
    /**
     * Given an image represented by an N x N matrix, where each pixel in the image is represented by an integer,
     * write a method to rotate the image by 90 degrees. Can you do this in place?
     */
    public static class RotateMatrixTests
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[]
                {
                    new[,]
                    {
                        {1},
                    },
                    new[,]
                    {
                        {1}
                    }
                },
                new object[]
                {
                    new[,]
                    {
                        {1, 2, 3},
                        {4, 5, 6},
                        {7, 8, 9},
                    },
                    new[,]
                    {
                        {7, 4, 1},
                        {8, 5, 2},
                        {9, 6, 3},
                    }
                },
                new object[]
                {
                    new[,]
                    {
                        {1, 2, 3, 4},
                        {5, 6, 7, 8},
                        {9, 10, 11, 12},
                        {13, 14, 15, 16}
                    },
                    new[,]
                    {
                        {13, 9, 5, 1},
                        {14, 10, 6, 2},
                        {15, 11, 7, 3},
                        {16, 12, 8, 4}
                    }
                },
            };

        [Theory]
        [MemberData(nameof(Data))]
        public static void RotateMatrix(int[,] matrix, int[,] expected)
        {
            Assert.Equal(expected, RotateMatrix1(matrix));
            // Assert.Equal(expected, RotateMatrix2(matrixCopy2));
        }

        /**
         * T = O(N * M)
         * S = O(N * M)
         *
         * N = the number of rows in the matrix.
         * M = the number of columns in the matrix.
         */
        private static int[,] RotateMatrix1(int[,] matrix)
        {
            var rowsCount = matrix.GetLength(0);
            var colsCount = rowsCount;

            var result = new int[rowsCount, colsCount];

            for (var row = 0; row < rowsCount; row++)
            {
                for (var col = 0; col < colsCount; col++)
                {
                    result[col, colsCount - row - 1] = matrix[row, col];
                }
            }

            return result;
        }


        /**
         * T = 
         * S = 
         *
         * N = length of the input string.
         */
        private static int[,] RotateMatrix2(int[,] matrix)
        {
            return matrix;
        }
    }
}