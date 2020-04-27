using System;
using System.Collections.Generic;
using Xunit;

namespace LeetCode.Medium.DynamicProgramming
{
    /// <summary>
    /// https://leetcode.com/explore/interview/card/top-interview-questions-medium/111/dynamic-programming/807/
    /// </summary>
    public class JumpGameSolution
    {
        public bool CanJumpBottomUp(int[] nums)
        {
            if (nums.Length == 1)
            {
                return true;
            }

            int previousJumpPotential = 1;
            int maxJumpPotential = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                previousJumpPotential--;
                if (previousJumpPotential < 0)
                {
                    return false;
                }

                maxJumpPotential = Math.Max(nums[i], previousJumpPotential);
                previousJumpPotential = maxJumpPotential;
            }
            return maxJumpPotential >= 1;
        }

        public bool CanJumpTopDown(int[] nums)
        {
            if (nums.Length == 1)
            {
                return true;
            }

            var memo = new Dictionary<int, int>();
            int jumpPotentialAtSecondToLastIndex = JumpPotential(nums, nums.Length - 2, memo);
            return jumpPotentialAtSecondToLastIndex >= 1;
        }

        private int JumpPotential(int[] nums, int i, Dictionary<int, int> memo)
        {
            if (i == 0)
            {
                return nums[0];
            }

            if (memo.ContainsKey(i))
            {
                return memo[i];
            }

            int currentPositionJumpPotential = nums[i];
            int previousPositionJumpPotential = JumpPotential(nums, i - 1, memo) - 1;
            int maxJumpPotential = previousPositionJumpPotential < 0
                ? 0 // If we couldn't get to the previous jump position, it doesn't matter what the current position's jump potential is.
                : Math.Max(currentPositionJumpPotential, previousPositionJumpPotential);

            memo[i] = maxJumpPotential;
            return maxJumpPotential;
        }
    }

    public class JumpGameTests
    {
        public static IEnumerable<object[]> GetNumsThatShouldReturnTrue()
        {
            yield return new object[] { new int[] { 2, 3, 1, 1, 4 } };
            yield return new object[] { new int[] { 2, 0, 4 } };
            yield return new object[] { new int[] { 1, 2, 3 } };
            yield return new object[] { new int[] { 1, 0 } };
        }

        public static IEnumerable<object[]> GetNumsThatShouldReturnFalse()
        {
            yield return new object[] { new int[] { 3, 2, 1, 0, 4 } };
            yield return new object[] { new int[] { 0, 2, 3 } };
            yield return new object[] { new int[] { 0, 1 } };
        }

        [Theory]
        [MemberData(nameof(GetNumsThatShouldReturnTrue))]
        public void CanJumpTopDown_ShouldReturnTrue(int[] nums)
        {
            //arrange
            var solution = new JumpGameSolution();

            //act
            bool canJump = solution.CanJumpTopDown(nums);

            //assert
            Assert.True(canJump);
        }

        [Theory]
        [MemberData(nameof(GetNumsThatShouldReturnFalse))]
        public void CanJumpTopDown_ShouldReturnFalse(int[] nums)
        {
            //arrange
            var solution = new JumpGameSolution();

            //act
            bool canJump = solution.CanJumpTopDown(nums);

            //assert
            Assert.False(canJump);
        }

        [Theory]
        [MemberData(nameof(GetNumsThatShouldReturnTrue))]
        public void CanJumpBottomUp_ShouldReturnTrue(int[] nums)
        {
            //arrange
            var solution = new JumpGameSolution();

            //act
            bool canJump = solution.CanJumpBottomUp(nums);

            //assert
            Assert.True(canJump);
        }

        [Theory]
        [MemberData(nameof(GetNumsThatShouldReturnFalse))]
        public void CanJumpBottomUp_ShouldReturnFalse(int[] nums)
        {
            //arrange
            var solution = new JumpGameSolution();

            //act
            bool canJump = solution.CanJumpBottomUp(nums);

            //assert
            Assert.False(canJump);
        }
    }
}
