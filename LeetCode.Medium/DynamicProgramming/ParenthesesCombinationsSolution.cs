using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LeetCode.Medium.DynamicProgramming
{
    /// <summary>
    /// Not actually a leetcode problem. From cracking the coding interview book.
    /// "Implement an algorithm to print all valid (e.g, properly opened and closed) 
    /// combinations of n-pairs of parentheses.
    /// </summary>
    public class ParenthesesCombinationsSolution
    {
        public HashSet<string> ValidParenthesesCombinations(int n)
        {
            char[] str = new char[n * 2];
            var set = new HashSet<string>();
            AddParen(set, n, n, str, 0);
            return set;
        }

        public void AddParen(HashSet<string> set, int leftRem, int rightRem, char[] str, int i)
        {
            if (leftRem < 0 || rightRem < leftRem)
            {
                // Invalid state.
                return; 
            }

            if (leftRem == 0 && rightRem == 0)
            {
                string s = new string(str);
                set.Add(s);
            }
            else
            {
                // Add left parens first.
                if (leftRem > 0)
                {
                    str[i] = '(';
                    AddParen(set, leftRem - 1, rightRem, str, i + 1);
                }

                // Only add a right paren if it's valid.
                if (rightRem > leftRem)
                {
                    str[i] = ')';
                    AddParen(set, leftRem, rightRem - 1, str, i + 1);
                }
            }
        }
    }

    public class ParenthesesCombinationsTests
    {
        [Fact]
        public void ValidParenthesesCombinations_Example()
        {
            //arrange
            var solution = new ParenthesesCombinationsSolution();
            int n = 3;

            //act
            HashSet<string> validParenthesesCombinations = solution.ValidParenthesesCombinations(n);

            //assert
            Assert.Equal(5, validParenthesesCombinations.Count);
            Assert.Contains("((()))", validParenthesesCombinations);
            Assert.Contains("(()())", validParenthesesCombinations);
            Assert.Contains("(())()", validParenthesesCombinations);
            Assert.Contains("()(())", validParenthesesCombinations);
            Assert.Contains("()()()", validParenthesesCombinations);
        }
    }
}
