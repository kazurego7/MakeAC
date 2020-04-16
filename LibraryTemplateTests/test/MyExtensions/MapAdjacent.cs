using System;
using System.Collections.Generic;
using AtCoderTemplate;
using Xunit;

namespace AtCoderTemplate.MyExtensions.Test {
    public class MapAdjcent {
        // 値のテスト用のデータ作成メソッド
        public static IEnumerable<object[]> GetValueData () {
            return new TheoryData<IEnumerable<string>, IEnumerable<string>, Func<string, string, string>> {
                // 文字の結合
                { new List<string> { "ab", "bc", "cd" }, new List<string> { "a", "b", "c", "d" }, (s1, s2) => s1 + s2 },
            };
        }

        // 値のテスト
        [Theory]
        [MemberData (nameof (GetValueData))]
        public void Value (IEnumerable<string> expected, IEnumerable<string> source, Func<string, string, string> func) {
            Assert.Equal (expected, source.MapAdjacent (func));
        }

        // // 例外のテスト用のデータ作成メソッド
        // public static IEnumerable<object[]> GetExceptionData () {
        //     return new TheoryData<T> {
        //         // 普通のテスト
        //         {},
        //     };
        // }

        // // 例外のテスト
        // [Theory]
        // [MemberData(nameof(GetExceptionData))]
        // public void Exception () {
        //     Assert.Throws<ConcreteException>(() => Hoge())
        // }
    }
}