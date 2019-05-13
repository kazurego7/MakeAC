using System;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class LCM {
        [Theory]
        [InlineData (12, 4, 6)] // 普通の合成数
        [InlineData (3 * 7 * 11 * 2, 3 * 7, 11 * 2)] // 互いに素
        public void Value (long expected, long m, long n) {
            Assert.Equal (expected, LCM (m, n));
        }

        [Theory]
        [InlineData (long.MaxValue, long.MaxValue - 1)] // 返り値がlongの範囲外
        public void Exception (long m, long n) {
            Assert.Throws<OverflowException> (() => LCM (m, n));
        }
    }
}