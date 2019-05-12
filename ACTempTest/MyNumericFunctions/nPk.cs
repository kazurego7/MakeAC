using System;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class nPk {
        const long bigdiv = 1000000007L;
        [Theory]
        [InlineData (7, 3, 20, (7 * 6 * 5) % 20)] // 普通の値
        [InlineData (12, 4, 3, (12 * 11 * 10 * 9) % 3)]
        // [InlineData ()]
        public void Value (int n, int k, long divisor, long ans) {
            Assert.Equal (nPk (n, k, divisor), ans);
        }

        // public void Exception () {
        //     Assert.Throws<ConcreteException>(() => Hoge())
        // }
    }
}