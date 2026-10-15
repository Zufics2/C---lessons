using Xunit;
using System;
using System.Collections.Generic;

namespace MyXUnit
{
    public class TestForTesting
    {
        private readonly ForTesting _test = new ForTesting();

        [Fact]
        public void GetSum_ReturnsCorrectSum()
        {
            int a = 2;
            int b = 3;
            int expected = 5;
            var result = _test.GetSum(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ConvertFromString_ValidDateString_ReturnsCorrectDateTime()
        {
            string dateString = "2026-10-15 12:00:00";
            DateTime result = _test.ConvertFromString(dateString);
            
            Assert.Equal(new DateTime(2026, 10, 15, 12, 0, 0), result);
        }

        [Fact]
        public void ConvertFromString_InvalidString_ThrowsFormatException()
        {
            string invalidString = "not-a-date";
            
            Assert.Throws<FormatException>(() => _test.ConvertFromString(invalidString));
        }

        [Fact]
        public void GetList_ReturnsExpectedElements()
        {
            var expected = new List<string> { "1", "2", "3", "4" };
            List<string> result = _test.GetList();
            
            Assert.NotNull(result);
            Assert.Equal(4, result.Count);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("2", "3", "5")]
        [InlineData("0", "0", "0")]
        [InlineData("-2", "-3", "-5")]
        [InlineData("-5", "10", "5")]
        [InlineData("100", "200", "300")]
        public void GetSum_ReturnCorrectSum2(int a, int b, int expected)
        {
            var result = _test.GetSum(a, b);
            
            Assert.Equal(expected, result);
        }
    }
}