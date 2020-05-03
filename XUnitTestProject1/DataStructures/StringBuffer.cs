using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Easy.DataStructures
{
    public class StringBuffer
    {
        private readonly List<string> parts;
        private int currentLength;

        public StringBuffer()
        {
            parts = new List<string>();
            currentLength = 0;
        }

        public StringBuffer Append(string s)
        {
            parts.Add(s);
            currentLength += s.Length;
            return this;
        }

        public override string ToString()
        {
            char[] chars = new char[currentLength];
            int i = 0;
            foreach(string part in parts)
            {
                foreach(char c in part)
                {
                    chars[i] = c;
                    i++;
                }
            }
            return new string(chars);
        }
    }

    public class StringBufferTests
    {
        [Fact]
        public void AppendToString_SixStrings_ShouldAppendAllStrings()
        {
            //arrange
            string expected = "The quick brown fox jumps over the lazy dog.";
            var stringBuffer = new StringBuffer();

            //act
            stringBuffer.Append("The quick ")
                        .Append("brown fox ")
                        .Append("jumps over ")
                        .Append("the ")
                        .Append("lazy ")
                        .Append("dog.");
            string sentence = stringBuffer.ToString();

            //assert
            Assert.Equal(expected, sentence);
        }

        [Fact]
        public void AppendToString_EmptyStringBuffer_ShouldReturnAnEmptyString()
        {
            //arrange
            string expected = string.Empty;
            var stringBuffer = new StringBuffer();

            //act
            string sentence = stringBuffer.ToString();

            //assert
            Assert.Equal(expected, sentence);
        }
    }
}
