using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeetCode.Medium.DynamicProgramming
{
    /// <summary>
    /// https://leetcode.com/explore/interview/card/top-interview-questions-medium/111/dynamic-programming/808/
    /// </summary>
    public class UniquePathsSolution
    {
        public int UniquePathsBottomUp(int m, int n)
        {
            int[] rowAbove = new int[m];
            int[] currentRow = new int[m];
            for (int r = 0; r < n; r++)
            {
                for (int c = 0; c < m; c++)
                {                    
                    if (c == 0)
                    {
                        currentRow[c] = 1;
                    }
                    else
                    {
                        currentRow[c] = currentRow[c - 1] + rowAbove[c];
                    }
                }
                rowAbove = currentRow.ToArray();
            }
            return currentRow[m - 1];
        }

        public int UniquePathsTopDown(int m, int n)
        {
            int[,] memo = new int[n, m];
            return UniquePathsTopDown(m, n, memo);
        }

        private int UniquePathsTopDown(int m, int n, int[,] memo)
        {
            if (m == 1 || n == 1)
            {
                return 1;
            }

            if (memo[n-1, m-1] > 0)
            {
                return memo[n-1, m-1];
            }

            int uniquePathsAbove = UniquePathsTopDown(m, n - 1, memo);
            int uniquePathsToTheLeft = UniquePathsTopDown(m - 1, n, memo);
            int uniquePaths = uniquePathsAbove + uniquePathsToTheLeft;
            memo[n-1, m-1] = uniquePaths;
            return uniquePaths;
        }
    }

    public class UniquePathsTests
    {
        public static IEnumerable<object[]> GetGridSizeAndExpectedUniquePaths()
        {            
            yield return new object[] { 3, 2, 3 };
            yield return new object[] { 3, 3, 6 };
            yield return new object[] { 7, 3, 28 };
            yield return new object[] { 23, 12, 193536720 };
        }

        [Theory]
        [MemberData(nameof(GetGridSizeAndExpectedUniquePaths))]
        public void UniquePathsTopDown_ShouldReturnExpectedUniquePaths(int m, int n, int expectedUniquePaths)
        {
            //arrange
            var solution = new UniquePathsSolution();

            //act
            int uniquePaths = solution.UniquePathsTopDown(m, n);

            //assert
            Assert.Equal(expectedUniquePaths, uniquePaths);
        }

        [Theory]
        [MemberData(nameof(GetGridSizeAndExpectedUniquePaths))]
        public void UniquePathsBottomUp_ShouldReturnExpectedUniquePaths(int m, int n, int expectedUniquePaths)
        {
            //arrange
            var solution = new UniquePathsSolution();

            //act
            int uniquePaths = solution.UniquePathsBottomUp(m, n);

            //assert
            Assert.Equal(expectedUniquePaths, uniquePaths);
        }
    }
}
