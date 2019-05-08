using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Math;
using static AtCoderTemplate.MyExtensions;
using static AtCoderTemplate.MyInputOutputs;
using static AtCoderTemplate.MyNumericFunctions;
using static AtCoderTemplate.MyAlgorithm;

namespace AtCoderTemplate {
    public class Program {
        public static void Main (string[] args) { }
    }

    static class MyInputOutputs {
        /* Input & Output*/
        public static int ReadInt () {
            return int.Parse (Console.ReadLine ());
        }
        public static long ReadLong () {
            return long.Parse (Console.ReadLine ());
        }
        public static List<int> ReadInts () {
            return Console.ReadLine ().Split (' ').Select (c => int.Parse (c)).ToList ();
        }
        public static List<long> ReadLongs () {
            return Console.ReadLine ().Split (' ').Select (c => long.Parse (c)).ToList ();
        }
        public static List<List<int>> ReadIntColumns (int n) {
            /*
            入力例
            A1 B1
            A2 B2
            ...
            An Bn

            出力例
            [[A1,A2,...,An], [B1,B2,...,Bn]]
            */
            var rows = Enumerable.Range (0, n).Select (i => ReadInts ()).ToList ();
            var m = rows.FirstOrDefault ()?.Count () ?? 0;
            return Enumerable.Range (0, m).Select (i => rows.Select (items => items[i]).ToList ()).ToList ();
        }
        public static List<List<long>> ReadLongColumns (int n) {
            /*
            入力例
            A1 B1
            A2 B2
            ...
            An Bn

            出力例
            [[A1,A2,...,An], [B1,B2,...,Bn]]
            */
            var rows = Enumerable.Range (0, n).Select (i => ReadLongs ()).ToList ();
            var m = rows.FirstOrDefault ()?.Count () ?? 0;
            return Enumerable.Range (0, m).Select (i => rows.Select (items => items[i]).ToList ()).ToList ();
        }

        public static void Print<T> (T item) {
            Console.WriteLine (item);
        }
        public static void PrintIf<T1, T2> (bool condition, T1 trueResult, T2 falseResult) {
            if (condition) {
                Console.WriteLine (trueResult);
            } else {
                Console.WriteLine (falseResult);
            }
        }

        public static void PrintRow<T> (IEnumerable<T> list) {
            /* 横ベクトルで表示
            A B C D ...
            */
            if (!list.IsEmpty ()) {
                Console.Write (list.First ());
                foreach (var item in list.Skip (1)) {
                    Console.Write ($" {item}");
                }
            }
            Console.Write ("\n");
        }
        public static void PrintColomn<T> (IEnumerable<T> list) {
            /* 縦ベクトルで表示
            A
            B
            C
            D
            ...
            */
            foreach (var item in list) {
                Console.WriteLine (item);
            }
        }
        public static void Print2DArray<T> (IEnumerable<IEnumerable<T>> sources) {
            foreach (var row in sources) {
                PrintRow (row);
            }
        }
    }

    static class MyNumericFunctions {
        public static bool isEven (int a) {
            return a % 2 == 0;
        }
        public static bool isEven (long a) {
            return a % 2 == 0;
        }
        public static bool isOdd (int a) {
            return !isEven (a);
        }
        public static bool isOdd (long a) {
            return !isEven (a);
        }

        public static long nPk (int n, int k) {
            if (k > n) {
                return 0;
            } else {
                return Enumerable.Range (n - k + 1, k).Aggregate ((long) 1, ((i, m) => i * m));
            }
        }
        public static long Fact (int n) {
            return nPk (n, n);
        }
        public static long nCk (int n, int k) {
            if (k > n) {
                return 0;
            } else {
                return nPk (n, k) / Fact (k);
            }
        }
        // 最大公約数
        public static long GCD (long m, long n) {
            if (m < n) {
                return GCD (n, m);
            }
            while (n != 0) {
                var n2 = m % n;
                m = n;
                n = n2;
            }
            return m;
        }
        // 最小公倍数
        public static long LCM (long m, long n) {
            if (m < n) {
                return LCM (n, m);
            } else {
                return (long) (BigInteger.Multiply (m, n) / GCD (m, n));
            }
        }
    }

    static class MyAlgorithm {
        /// <summary>
        /// 二分探索法
        /// O(log N)
        /// </summary>
        /// <param name="list">探索するリスト</param>
        /// <param name="predicate">条件の述語関数</param>
        /// <param name="ng">条件を満たさない既知のindex</param>
        /// <param name="ok">条件を満たす既知のindex</param>
        /// <typeparam name="T">順序関係を持つ型(IComparableを実装する)</typeparam>
        /// <returns>条件を満たすindexの内、境界に最も近いものを返す</returns>
        public static int BinarySearch<T> (IList<T> list, Func<T, bool> predicate, int ng, int ok)
        where T : IComparable<T> {
            while (Abs (ok - ng) > 1) {
                int mid = (ok + ng) / 2;
                if (predicate (list[mid])) {
                    ok = mid;
                } else {
                    ng = mid;
                }
            }
            return ok;
        }

        /// <summary>
        /// 辺の集まりを操作するオブジェクト
        /// </summary>
        public class Edge {
            long[, ] edge;
            public int NodeNum { get; }
            public Edge (int nodeNum, long overDistance) {
                var edge = new long[nodeNum, nodeNum];
                foreach (var i in Enumerable.Range (0, nodeNum)) {
                    foreach (var j in Enumerable.Range (0, nodeNum)) {
                        if (i != j) {
                            edge[i, j] = overDistance;
                        } else {
                            edge[i, j] = 0;
                        }
                    }
                }

                this.edge = edge;
                this.NodeNum = nodeNum;
            }
            public Edge (Edge edge) {
                this.edge = new long[edge.NodeNum, edge.NodeNum];
                foreach (var i in Enumerable.Range (0, edge.NodeNum)) {
                    foreach (var j in Enumerable.Range (0, edge.NodeNum)) {
                        this.edge[i, j] = edge.GetLength (i, j);
                    }
                }
                this.NodeNum = edge.NodeNum;
            }

            public List<List<long>> ToList () {
                return Enumerable.Range (0, NodeNum).Select (i =>
                    Enumerable.Range (0, NodeNum).Select (j =>
                        edge[i, j]
                    ).ToList ()
                ).ToList ();
            }

            public void Add (int node1, int node2, long distance) {
                edge[node1, node2] = distance;
            }

            public long GetLength (int node1, int node2) {
                return edge[node1, node2];
            }
        }

        /// <summary>
        /// ワーシャルフロイド法
        /// O(N^3)
        /// </summary>
        /// <param name="edge">Edgeオブジェクト</param>
        /// <param name="nodeNum">ノードの数</param>
        /// <returns>各ノード間の最短距離を辺として持つEdgeオブジェクト</returns>
        public static Edge WarshallFloyd (Edge edge) {
            var res = new Edge (edge);
            foreach (var b in Enumerable.Range (0, edge.NodeNum)) {
                foreach (var a in Enumerable.Range (0, edge.NodeNum)) {
                    foreach (var c in Enumerable.Range (0, edge.NodeNum)) {
                        res.Add (a, c, Min (res.GetLength (a, c), res.GetLength (a, b) + res.GetLength (b, c)));
                    }
                }
            }
            return res;
        }
    }

    static class MyExtensions {
        // AppendとPrependが、.NET Standard 1.6からの追加で、Mono 4.6.2 はそれに対応して仕様はあるが、実装がない
        public static IEnumerable<T> Append<T> (this IEnumerable<T> source, T element) {
            return source.Concat (Enumerable.Repeat (element, 1));
        }

        public static IEnumerable<T> Prepend<T> (this IEnumerable<T> source, T element) {
            return Enumerable.Repeat (element, 1).Concat (source);
        }

        // TakeLastとSkipLastが、.Net Standard 2.1からの追加で、Mono 4.6.2 はそれに対応していない
        public static IEnumerable<T> TakeLast<T> (this IEnumerable<T> source, int count) {
            return source.Skip (source.Count () - count);
        }

        public static IEnumerable<T> SkipLast<T> (this IEnumerable<T> source, int count) {
            return source.Take (source.Count () - count);
        }

        public static bool IsEmpty<T> (this IEnumerable<T> source) {
            return !source.Any ();
        }

        /// <summary>
        /// インデックスiの位置の要素からk個取り除く
        /// O(N)
        /// </summary>
        public static IEnumerable<T> TakeAwayRange<T> (this IEnumerable<T> source, int i, int count) {
            return source.Take (i).Concat (source.Skip (i + count));
        }

        /// <summary>
        /// インデックスiの位置の要素を取り除く
        /// O(N)
        /// </summary>
        public static IEnumerable<T> TakeAwayAt<T> (this IEnumerable<T> source, int i) {
            return source.TakeAwayRange (i, 1);
        }

        /// <summary>
        /// インデックスiの位置にシーケンスを挿入する
        /// O(N + K)
        /// </summary>
        public static IEnumerable<T> InsertEnumAt<T> (this IEnumerable<T> source, int i, IEnumerable<T> inserted) {
            return source.Take (i).Concat (inserted).Concat (source.Skip (i));
        }

        /// <summary>
        /// 順列を得る
        /// O(N!)
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Perm<T> (this IEnumerable<T> source, int n) {
            if (n == 0 || source.IsEmpty () || source.Count () < n) {
                return Enumerable.Empty<IEnumerable<T>> ();
            } else if (n == 1) {
                return source.Select (i => new List<T> { i });
            } else {
                var nexts = source.Select ((x, i) =>
                    new { next = source.Take (i).Concat (source.Skip (i + 1)), selected = source.Take (i + 1).Last () });
                return nexts.SelectMany (next => Perm (next.next, n - 1).Select (item => item.Prepend (next.selected)));
            }
        }

        /// <summary>
        /// シーケンスの隣り合う要素を2引数の関数に適用したシーケンスを得る
        /// </summary>
        /// <para>O(N)</para>
        /// <param name="source">元のシーケンス</param>
        /// <param name="func">2引数関数</param>
        /// <example>[1,2,3,4].MapAdjacent(f) => [f(1,2), f(2,3), f(3,4)]</example>
        public static IEnumerable<TR> MapAdjacent<T1, TR> (this IEnumerable<T1> source, Func<T1, T1, TR> func) {
            var list = source.ToList ();
            return Enumerable.Range (1, list.Count - 1)
                .Select (i => func (list[i], list[i - 1]));
        }

        /// <summary>
        /// 累積項を要素にもつシーケンスを得る(初項は、first)
        /// <para>O(N)</para>
        /// </summary>
        /// <param name="source">元のシーケンス</param>
        /// <param name="func">2引数関数f</param>
        /// <param name="first">func(first, source[0])のための初項</param>
        /// <example> [1,2,3].Scanl1(f,0) => [0, f(0,1), f(f(0,1),2), f(f(f(0,1),2),3)]</example>
        public static IEnumerable<TR> Scanl<T, TR> (this IEnumerable<T> source, TR first, Func<TR, T, TR> func) {
            var list = source.ToList ();
            var result = new List<TR> { first };
            foreach (var i in Enumerable.Range (0, source.Count ())) {
                result.Add (func (result[i], list[i]));
            }
            return result;
        }
        /// <summary>
        /// 累積項を要素にもつシーケンスを得る（初項は、source.First()）
        /// <para>O(N)</para>
        /// </summary>
        /// <param name="source">元のシーケンス</param>
        /// <param name="func">2引数関数f</param>
        /// <example> [1,2,3].Scanl1(f) => [1, f(1,2), f(f(1,2),3)]</example>
        public static IEnumerable<T> Scanl1<T> (this IEnumerable<T> source, Func<T, T, T> func) {
            var list = source.ToList ();
            var result = new List<T> { list[0] };
            foreach (var i in Enumerable.Range (1, source.Count () - 1)) {
                result.Add (func (result[i - 1], list[i]));
            }
            return result;
        }

        /// <summary>
        /// 昇順にソートしたインデックスを得る
        /// </summary>
        /// <para>O(N * log N)</para>
        public static IEnumerable<int> SortIndex<T> (this IEnumerable<T> source) {
            return source
                .Select ((item, i) => new { Item = item, Index = i })
                .OrderBy (x => x.Item)
                .Select (x => x.Index);
        }
    }
}