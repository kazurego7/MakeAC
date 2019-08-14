using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class Divisor {
        // 値のテスト用のデータ作成メソッド
        public static IEnumerable<object[]> GetValueData =>
            new TheoryData<IEnumerable<long>, long> {
                // 正の整数
                { new List<long> { 1, 2, 3, 6 }, 6 },
                // 素数
                { new List<long> { 1, 13 }, 13 },
                // 2乗根が整数になる（ため、2乗根までしか計算してない、とかで）
                { new List<long> { 1, 2, 4 }, 4 },
                // 下限
                { new List<long> { 1 }, 1 },
            };

        // 値のテスト
        [Theory]
        [MemberData (nameof (GetValueData))]
        public void Value (IEnumerable<long> expected, long m) {
            Assert.Equal (expected, Divisor (m).OrderBy (x => x));
        }

        // 例外のテスト用のデータ作成メソッド
        public static IEnumerable<object[]> GetExceptionData =>
            new TheoryData<long> {
                // 0
                { 0 },
                // 負の数
                {-6 },
            };

        [Theory]
        [MemberData (nameof (GetExceptionData))]
        public void Exception (long m) {
            Assert.Throws<ArgumentOutOfRangeException> (() => Divisor (m));
        }
    }
}