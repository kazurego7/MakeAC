using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AtCoderTemplate;
using Xunit;
using static AtCoderTemplate.MyNumericFunctions;

namespace MyNumericFunctions.Test {
    public class Divisor {
        // MemberData用のデータ作成メソッド
        public static IEnumerable<object[]> GetData () {
            Func<List<long>, long, object[]> ToObj = ToObjectCaster<List<long>, long>.MakeCast ();
            return new List<object[]> {
                ToObj (new List<long> { 1, 2, 3, 6 }, 6),
                ToObj (new List<long> { 1, 2, 4 }, 4), // あっ……！
            };
        }

        // 値のテスト
        [Theory]
        [MemberData (nameof (GetData))]
        public void Value (IEnumerable<long> expected, long m) {
            Assert.Equal (expected, Divisor (m));
        }

        // // 例外のテスト
        // public void Exception () {
        //     Assert.Throws<ConcreteException>(() => Hoge())
        // }
    }
}