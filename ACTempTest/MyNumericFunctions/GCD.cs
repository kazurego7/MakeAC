using System;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class GCD {
        [Theory]
        [InlineData (5, 50, 5)] // 普通の合成数
        [InlineData (5, 5, 50)] // m < n
        [InlineData (5, 5, 5)] // m = n
        [InlineData (11, 7 * 11, 3 * 11)] // 素数同士
        [InlineData (1, 120, 11)] // 互いに素
        public void Value (long expected, long m, long n) {
            Assert.Equal (expected, GCD (m, n));
        }

        [Theory]
        [InlineData (0, 4)] // m が自然数の範囲外
        [InlineData (6, 0)] // n が自然数の範囲外
        public void Exception (long m, long n) {
            Assert.Throws<ArgumentOutOfRangeException> (() => GCD (m, n));
        }
    }
}