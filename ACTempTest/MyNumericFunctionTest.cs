using System;
using AtCoderTemplate;
using static AtCoderTemplate.MyNumericFunctions;
using Xunit;

namespace Test {
    public class MyNumericFunctionTest {
        [Theory]
        [InlineData (50, 5, 5)] // 普通の合成数
        [InlineData (5, 50, 5)] // m < n
        [InlineData (5, 5, 5)] // m = n
        [InlineData (7 * 11, 3 * 11, 11)] // 素数同士
        [InlineData (120, 11, 1)] // 互いに素
        public void GCDValue (long m, long n, long ans) {
            Assert.Equal (GCD (m, n), ans);
        }

        [Theory]
        [InlineData (0, 4)] // m が自然数の範囲外
        [InlineData (6, 0)] // n が自然数の範囲外
        public void GCDException (long m, long n) {
            Assert.Throws<ArgumentException> (() => GCD (m, n));
        }

        [Theory]
        [InlineData (4, 6, 12)] // 普通の合成数
        [InlineData (3 * 7, 11 * 2, 3 * 7 * 11 * 2)] // 互いに素
        public void LCMValue (long m, long n, long ans) {
            Assert.Equal (LCM (m, n), ans);
        }

        [Theory]
        [InlineData (long.MaxValue, long.MaxValue - 1)] // 返り値がlongの範囲外
        public void LCMException (long m, long n) {
            Assert.Throws<OverflowException> (() => LCM (m, n));
        }
    }
}