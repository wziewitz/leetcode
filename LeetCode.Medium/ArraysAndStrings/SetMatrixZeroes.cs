using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using Xunit;

namespace Medium.ArraysAndStrings
{
    /// <summary>
    /// https://leetcode.com/explore/interview/card/top-interview-questions-medium/103/array-and-strings/777/
    /// </summary>
    public class SetMatrixZeroesSolution
    {
        public void SetZeroes(int[][] matrix)
        {
            // Use the zero indexed row and column to find and mark all the zeroes on the first pass.
            // Use a second pass to fill the marked rows and columns with zeros.
            // Zero indexed row and column is a special case.

            bool shouldFillRowZero = matrix[0].Contains(0);
            bool shouldFillColumnZero = ColumnContainsZero(matrix, 0);
            MarkZeros(matrix);
            FillZeros(matrix, shouldFillRowZero, shouldFillColumnZero);
        }

        private void MarkZeros(int[][] matrix)
        {
            // Skip the zero indexed row and column.
            for (int r = 1; r < matrix.Length; r++)
            {
                for (int c = 1; c < matrix[r].Length; c++)
                {
                    if (matrix[r][c] == 0)
                    {
                        matrix[0][c] = 0;
                        matrix[r][0] = 0;
                    }
                }
            }
        }

        private void FillZeros(int[][] matrix, bool shouldFillRowZero, bool shouldFillColumnZero)
        {            
            for (int r = matrix.Length - 1; r >= 0; r--)
            {
                for (int c = matrix[r].Length - 1; c >= 0; c--)
                {
                    if (matrix[0][c] == 0 || 
                        matrix[r][0] == 0 ||
                        (r == 0 && shouldFillRowZero) ||
                        (c == 0 && shouldFillColumnZero))
                    {
                        matrix[r][c] = 0;
                    }
                }
            }
        }

        private bool ColumnContainsZero(int[][] matrix, int c)
        {
            for (int r = 0; r < matrix.Length; r++)
            {
                if (matrix[r][c] == 0)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public class SetMatrixZeroesTests
    {

        public static IEnumerable<object[]> GetInputAndExpectedArrays()
        {
            yield return new object[] {
                new int[][]
                {
                    new int[] { 1, 1, 1 },
                    new int[] { 1, 0, 1 },
                    new int[] { 1, 1, 1 },
                },
                new int[][]
                {
                    new int[] { 1, 0, 1 },
                    new int[] { 0, 0, 0 },
                    new int[] { 1, 0, 1 },
                },
            };

            yield return new object[] {
                new int[][]
                {
                    new int[] { 0, 1, 2, 0 },
                    new int[] { 3, 4, 5, 2 },
                    new int[] { 1, 3, 1, 5 },
                },
                new int[][]
                {
                    new int[] { 0, 0, 0, 0 },
                    new int[] { 0, 4, 5, 0 },
                    new int[] { 0, 3, 1, 0 },
                },
            };

            yield return new object[] {
                new int[][]
                {
                    new int[] { 1, 0, 2, 1 },
                    new int[] { 3, 4, 5, 2 },
                    new int[] { 1, 3, 1, 5 },
                },
                new int[][]
                {
                    new int[] { 0, 0, 0, 0 },
                    new int[] { 3, 0, 5, 2 },
                    new int[] { 1, 0, 1, 5 },
                },
            };

            yield return new object[] {
                new int[][]
                {
                    new int[] { 1 },
                    new int[] { 0 },
                },
                new int[][]
                {
                    new int[] { 0 },
                    new int[] { 0 },
                },
            };
        }

        [Theory]
        [MemberData(nameof(GetInputAndExpectedArrays))]
        public void SetZeroes_ShouldModifyArrayInPlace(int[][] input, int[][] expected)
        {
            //arrange
            var solution = new SetMatrixZeroesSolution();

            //act
            solution.SetZeroes(input);

            //assert
            Assert.Equal(expected, input);
        }
    }
}
