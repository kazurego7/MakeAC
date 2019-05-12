using System;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class GCD {
        [Theory]
        [InlineData (50, 5, 5)] // 普通の合成数
        [InlineData (5, 50, 5)] // m < n
        [InlineData (5, 5, 5)] // m = n
        [InlineData (7 * 11, 3 * 11, 11)] // 素数同士
        [InlineData (120, 11, 1)] // 互いに素
        public void Value (long m, long n, long ans) {
            Assert.Equal (GCD (m, n), ans);
        }

        [Theory]
        [InlineData (0, 4)] // m が自然数の範囲外
        [InlineData (6, 0)] // n が自然数の範囲外
        public void Exception (long m, long n) {
            Assert.Throws<ArgumentException> (() => GCD (m, n));
        }
    }
}