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
        public static IEnumerable<object[]> GetValueData () {
            var ToObj = ToObjectCaster<List<long>, long>.MakeCast ();
            return new List<object[]> {
                ToObj (new List<long> { 1, 2, 3, 6 }, 6), // 正の整数
                ToObj (new List<long> { 1, 13 }, 13), // 素数
                ToObj (new List<long> { 1, 2, 4 }, 4), // 2乗根が整数になる（ため、2乗根までしか計算してない、とかで）
                ToObj (new List<long> { 1 }, 1) // 下限
            };
        }

        // 値のテスト
        [Theory]
        [MemberData (nameof (GetValueData))]
        public void Value (IEnumerable<long> expected, long m) {
            Assert.Equal (expected, Divisor (m).OrderBy (x => x));
        }

        // 例外のテスト用のデータ作成メソッド
        public static IEnumerable<object[]> GetExceptionData () {
            var ToObj = ToObjectCaster<long>.MakeCast ();
            return new List<object[]> {
                ToObj (0), // 0
                ToObj (-6) // 負の数
            };
        }

        [Theory]
        [MemberData (nameof (GetExceptionData))]
        public void Exception (long m) {
            Assert.Throws<ArgumentOutOfRangeException> (() => Divisor (m));
        }
    }
}