using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using static System.Math;
using static AtCoderTemplate.MyConstants;
using static AtCoderTemplate.MyInputOutputs;
using static AtCoderTemplate.MyNumericFunctions;
using static AtCoderTemplate.MyAlgorithm;
using static AtCoderTemplate.MyExtensions;

namespace AtCoderTemplate {
    public class Program {
        public static void Main (string[] args) { }
    }

    public static class MyInputOutputs {
        /* Input & Output*/
        public static string Read () {
            return Console.ReadLine ();
        }

        public static List<string> Reads () {
            return Console.ReadLine ().Split (' ').ToList ();
        }

        public static List<List<string>> ReadRows (int rowNum) {
            /*
            入力例
            A1 B1 C1 ... Z1
            A2 B2 C2 ... Z2
            ...
            An Bn Cn ... Zn
           

            出力例
            [[A1, B1, C1, ... Z1], [A2, B2, C2, ... Z2], ... [An, Bn, Cn, ... Zn]]
            */
            return Enumerable.Range (0, rowNum).Select (i => Reads ()).ToList ();
        }

        public static List<List<string>> ReadColumns (int rowNum, int colNum) {
            /*
            入力例
            A1 B1 C1 ... Z1
            A2 B2 C2 ... Z2
            ...
            An Bn Cn ... Zn
           

            出力例
            [[A1, A2, A3, ... An], [B1, B2, B3, ... Bn], ... [Z1, Z2, Z3, ... Zn]]
            */
            var rows = ReadRows (rowNum);
            return Enumerable.Range (0, colNum).Select (i => rows.Select (items => items[i].ToString ()).ToList ()).ToList ();
        }

        public static List<List<string>> ReadGridGraph (int height, int width) {
            /*
            入力例
            A1B1C1...Z1
            A2B2C2...Z2
            ...
            AnBnCn...Zn
           

            出力例
            [[A1, B1, C1, ... Z1], [A2, B2, C2, ... Z2], ... [An, Bn, Cn, ... Zn]]
            */
            return Enumerable.Range (0, height)
                .Select (i =>
                    Read ()
                    .Select (c => c.ToString ())
                    .ToList ()
                ).ToList ();
        }

        public static int ToInt (this string str) {
            return int.Parse (str);
        }

        public static long ToLong (this string str) {
            return long.Parse (str);
        }

        public static List<int> ToInts (this List<string> strs) {
            return strs.Select (str => str.ToInt ()).ToList ();
        }

        public static List<long> ToLongs (this List<string> strs) {
            return strs.Select (str => str.ToLong ()).ToList ();
        }

        public static int ReadInt () {
            return Read ().ToInt ();
        }
        public static long ReadLong () {
            return Read ().ToLong ();
        }

        public static List<int> ReadInts () {
            return Reads ().ToInts ();
        }
        public static List<long> ReadLongs () {
            return Reads ().ToLongs ();
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
        public static void PrintRows<T> (IEnumerable<IEnumerable<T>> sources) {
            foreach (var row in sources) {
                PrintRow (row);
            }
        }

        public static void PrintGridGraph<T> (IEnumerable<IEnumerable<T>> sources) {
            foreach (var row in sources) {
                Print (String.Concat (row));
            }
        }
    }

    public static class MyConstants {
        public static IEnumerable<char> lowerAlphabets = Enumerable.Range ('a', 'z' - 'a' + 1).Select (i => (char) i);
        public static IEnumerable<char> upperAlphabets = Enumerable.Range ('A', 'Z' - 'A' + 1).Select (i => (char) i);

        public static int p10_9plus7 = (int) Pow (10, 9) + 7;
    }

    public static class MyNumericFunctions {
        public static bool IsEven (int a) {
            return a % 2 == 0;
        }
        public static bool IsEven (long a) {
            return a % 2 == 0;
        }
        public static bool IsOdd (int a) {
            return !IsEven (a);
        }
        public static bool IsOdd (long a) {
            return !IsEven (a);
        }

        /// <summary>
        /// 冪を得る
        /// </summary>
        /// <param name="b">底</param>
        /// <param name="n">冪指数</param>
        /// <param name="divisor">返り値がintを超えないようにdivisorで割ったあまりを得る</param>
        /// <returns>bのn乗(をdivisorで割ったあまり)</returns>
        public static int PowRem (long b, int n, int divisor) {
            return Enumerable.Repeat (b, n)
                .Aggregate (1, (accm, i) => (int) ((accm * i) % divisor));
        }

        /// <summary>
        /// 順列の総数を得る
        /// O(N-K)
        /// </summary>
        /// <param name="n">全体の数</param>
        /// <param name="k">並べる数</param>
        /// <param name="divisor">返り値がintを超えないようにdivisorで割った余りを得る</param>
        /// <returns>nPk (をdivisorで割った余り)</returns>
        public static int nPk (int n, int k, int divisor) {
            if (k > n) {
                return 0;
            } else {
                return Enumerable.Range (n - k + 1, k).Aggregate (1, ((i, m) => (i * m) % divisor));
            }
        }

        public static long nPk (int n, int k) {
            if (k > n) {
                return 0L;
            } else {
                return Enumerable.Range (n - k + 1, k).Aggregate (1L, ((i, m) => (i * m)));
            }
        }

        /// <summary>
        /// 階乗を得る
        /// O(N)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="divisor">返り値がintを超えないようにdivisorで割った余りを得る</param>
        /// <returns>n! (をdivisorで割った余り)</returns>
        public static int Fact (int n, int divisor) {
            return nPk (n, n, divisor);
        }

        public static long Fact (int n) {
            return nPk (n, n);
        }

        /// <summary>
        /// 組み合わせの総数を得る
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"></param>
        /// <returns>nCk</returns>
        public static long nCk (int n, int k) {
            if (k > n) {
                return 0L;
            } else {
                return nPk (n, k) / Fact (k);
            }
        }

        /// <summary>
        /// 最大公約数を得る 
        /// O(log N)
        /// </summary>
        /// <param name="m">自然数</param>
        /// <param name="n">自然数</param>
        /// <returns></returns>
        public static long GCD (long m, long n) {
            // GCD(m,n) = GCD(n, m%n)を利用
            // m%n = 0のとき、mはnで割り切れるので、nが最大公約数
            if (m <= 0L || n <= 0L) throw new ArgumentOutOfRangeException ();

            if (m < n) return GCD (n, m);
            while (m % n != 0L) {
                var n2 = m % n;
                m = n;
                n = n2;
            }
            return n;
        }

        /// <summary>
        /// 最小公倍数を得る
        /// O(log N)
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long LCM (long m, long n) {
            var ans = checked ((long) (BigInteger.Multiply (m, n) / GCD (m, n)));
            return ans;
        }

        /// <summary>
        /// 約数列挙(非順序)
        /// O(√N)
        /// </summary>
        /// <param name="m">m > 0</param>
        /// <returns></returns>
        public static IEnumerable<long> Divisor (long m) {
            if (!(m > 0)) throw new ArgumentOutOfRangeException ();

            var front = Enumerable.Range (1, (int) Sqrt (m))
                .Select (i => (long) i)
                .Where (d => m % d == 0);
            return front.Concat (front.Where (x => x * x != m).Select (x => m / x));
        }

        public static IEnumerable<int> Divisor (int m) {
            if (!(m > 0)) throw new ArgumentOutOfRangeException ();

            var front = Enumerable.Range (1, (int) Sqrt (m))
                .Where (d => m % d == 0);
            return front.Concat (front.Where (x => x * x != m).Select (x => m / x));
        }

        /// <summary>
        /// 公約数列挙(非順序)
        /// O(√N)
        /// </summary>
        /// <param name="m">m > 0</param>
        /// <param name="n">n > 0 </param>
        /// <returns></returns>
        public static IEnumerable<long> CommonDivisor (long m, long n) {
            if (!(m > 0)) throw new ArgumentOutOfRangeException ();

            if (m < n) return CommonDivisor (n, m);
            return Divisor (m).Where (md => n % md == 0);
        }

        public static IEnumerable<int> CommonDivisor (int m, int n) {
            if (!(m > 0)) throw new ArgumentOutOfRangeException ();

            if (m < n) return CommonDivisor (n, m);
            return Divisor (m).Where (md => n % md == 0);
        }

        /// <summary>
        /// nまでの素数を得る
        /// </summary>
        /// <param name="n">n > 1</param>
        /// <returns></returns>
        public static IEnumerable<int> Prime (int n) {
            if (!(n > 1)) throw new ArgumentOutOfRangeException ();

            var primes = new List<int> ();
            foreach (var i in Enumerable.Range (2, n - 1)) {
                if (primes.All (p => i % p != 0)) {
                    primes.Add (i);
                }
            }
            return primes;
        }

        /// <summary>
        /// 順列を得る
        /// O(N!)
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Perm<T> (IEnumerable<T> source, int n) {
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
    }

    public static class MyAlgorithm {
        /// <summary>
        /// めぐる式二分探索法
        /// O(log N)
        /// </summary>
        /// <param name="list">探索するリスト</param>
        /// <param name="predicate">条件の述語関数</param>
        /// <param name="ng">条件を満たさない既知のindex</param>
        /// <param name="ok">条件を満たす既知のindex</param>
        /// <returns>条件を満たすindexの内、隣がfalseとなるtrueのindexを返す</returns>
        public static int BinarySearch<T> (List<T> list, Func<T, bool> predicate, int ng, int ok) {
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
        /// 左二分探索法
        /// </summary>
        /// <param name="list">条件の境界(falseとtrueの変わるところ)で、trueが左にあるリスト</param>
        /// <param name="predicate">条件の述語関数</param>
        /// <returns>右隣がfalseになるtrueのindexを返す</returns>
        public static int LeftBinarySearch<T> (List<T> list, Func<T, bool> predicate) {
            return BinarySearch (list, predicate, list.Count, -1);
        }

        /// <summary>
        /// 右二分探索法
        /// </summary>
        /// <param name="list">条件の境界(falseとtrueの変わるところ)で、trueが右にあるリスト</param>
        /// <param name="predicate">条件の述語関数</param>
        /// <returns>左隣がfalseになるtrueのindexを返す</returns>
        public static int RightBinarySearch<T> (List<T> list, Func<T, bool> predicate) {
            return BinarySearch (list, predicate, -1, list.Count);
        }

        // Dictionaryがcapacity近くになるとゲロ重かったので削除
        // 配列にするかも

        // /// <summary>
        // /// DynamicProgrammingの形式(貰うDP)
        // /// 項dp_(i,j,k...)の漸化式、初項、漸化式の計算順序（添字のパターン）、から漸化式を計算する
        // /// </summary>
        // /// <param name="calculationOrders">漸化式の計算順序、項dp_(i,j,k...)の添字(i,j,k...)のトポロジカル順序を持つシーケンス</param>
        // /// <param name="initialConditions">初項とその添字のペア、のシーケンス</param>
        // /// <param name="recurrenceRelation">項dp_(i,j,k...)の漸化式、以前の項の計算はDictonaryから得る</param>
        // /// <typeparam name="Indexes">項dp_(i,j,k...)の添字(i,j,k...)</typeparam>
        // /// <typeparam name="Result">項dp_(i,j,k...)の計算結果</typeparam>
        // /// <returns>項dp_(i,j,k...)の全ての計算結果をDictionaryで返す</returns>
        // public static Dictionary<Indexes, Result> DynamicProgramming<Indexes, Result> (
        //     IEnumerable<Indexes> calculationOrders //
        //     , IEnumerable<KeyValuePair<Indexes, Result>> initialConditions //
        //     , Func<Dictionary<Indexes, Result>, Indexes, Result> recurrenceRelation) {
        //     var conditions = initialConditions.ToDictionary (kv => kv.Key, kv => kv.Value);
        //     foreach (var order in calculationOrders) {
        //         conditions.Add (order, recurrenceRelation (conditions, order));
        //     }
        //     return conditions;
        // }

        // .NetFramework 4.6.2 ではタプルの記法がまだ使えない

        // /// <summary>
        // /// DynamicProgrammingの形式(貰うDP)
        // /// 項dp_(i,j,k...)の漸化式、初項、漸化式の計算順序（添字のパターン）、から漸化式を計算する
        // /// </summary>
        // /// <param name="calculationOrders">漸化式の計算順序、項dp_(i,j,k...)の添字(i,j,k...)のトポロジカル順序を持つシーケンス</param>
        // /// <param name="initialConditions">初項とその添字のペア、のシーケンス</param>
        // /// <param name="recurrenceRelation">項dp_(i,j,k...)の漸化式、以前の項の計算はDictonaryから得る</param>
        // /// <typeparam name="Indexes">項dp_(i,j,k...)の添字(i,j,k...)</typeparam>
        // /// <typeparam name="Result">項dp_(i,j,k...)の計算結果</typeparam>
        // /// <returns>項dp_(i,j,k...)の全ての計算結果をDictionaryで返す</returns>
        // public static Dictionary<Indexes, Result> DynamicProgramming<Indexes, Result> (
        //     IEnumerable<Indexes> calculationOrders //
        //     , IEnumerable < (Indexes indexes, Result result) > initialConditions //
        //     , Func<Dictionary<Indexes, Result>, Indexes, Result> recurrenceRelation) {
        //     var conditions = initialConditions.ToDictionary (kv => kv.indexes, kv => kv.result);
        //     foreach (var order in calculationOrders) {
        //         conditions.Add (order, recurrenceRelation (conditions, order));
        //     }
        //     return conditions;
        // }

        /// <summary>
        /// 右へのしゃくとり法の形式
        /// </summary>
        /// <param name="n">なめるシーケンスの長さ</param>
        /// <param name="Predicate">更新のための条件関数。indexの(left,right)をとり、条件を満たすとUpdateConditionを行う</param>
        /// <param name="initialCondition">初期状態。</param>
        /// <param name="Update">状態更新関数。indexのleft, rightと前のconditionをとり、更新したconditionを返す</param>
        /// <typeparam name="TR"></typeparam>
        /// <returns></returns>
        public static TR TwoPointersRight<TR> (int n, Func<int, int, bool> Predicate, TR initialCondition, Func<int, int, TR, TR> Update) {
            var l = 0;
            var r = 0;
            TR condition = initialCondition;
            while (r < n) {
                while (r < n && !Predicate (l, r)) {
                    r += 1;
                }
                while (r < n && l != r && Predicate (l, r)) {
                    condition = Update (l, r, condition);
                    l += 1;
                }
            }
            return condition;
        }

        /// <summary>
        /// グラフの辺
        /// </summary>
        public struct Edge {
            public int SourceNode { get; }
            public int TargetNode { get; }
            public long Weight { get; }

            public Edge (int sourceNode, int targetNode, long weight) {
                SourceNode = sourceNode;
                TargetNode = targetNode;
                Weight = weight;
            }
        }

        public static IEnumerable<Edge> ToEdges (List<int> sourceNodes, List<int> targetNodes, List<long> weights) {
            return Enumerable.Range (0, sourceNodes.Count)
                .Select (i => new Edge (sourceNode: sourceNodes[i], targetNode: targetNodes[i], weight: weights[i]));
        }

        public static List<IEnumerable<int>> ToAdjacencyListOfUndirectedGraph (IEnumerable<Edge> edges, int nodeNum) {
            var adjacencyList = Enumerable.Range (0, nodeNum).Select (_ => Enumerable.Empty<int> ()).ToList ();
            foreach (var edge in edges) {
                adjacencyList[edge.SourceNode] = adjacencyList[edge.SourceNode].Append (edge.TargetNode);
                adjacencyList[edge.TargetNode] = adjacencyList[edge.TargetNode].Append (edge.SourceNode);
            }
            return adjacencyList;
        }

        public static List<List<long>> ToAdjacencyMatrixOfUndirectedGraph (IEnumerable<Edge> edges, int nodeNum, long weightOfNoEdge) {
            var adjacencyMatrix = Enumerable.Range (0, nodeNum)
                .Select (i =>
                    Enumerable.Range (0, nodeNum)
                    .Select (k =>
                        weightOfNoEdge
                    ).ToList ()
                ).ToList ();
            foreach (var edge in edges) {
                adjacencyMatrix[edge.SourceNode][edge.TargetNode] = edge.Weight;
                adjacencyMatrix[edge.TargetNode][edge.SourceNode] = edge.Weight;
            }
            return adjacencyMatrix;
        }

        /// <summary>
        /// ワーシャルフロイド法
        /// O(N^3)
        /// </summary>
        /// <param name="adjacencyMatrix">隣接行列</param>
        /// <param name="nodeNum">ノードの数</param>
        /// <returns>各ノード間の最短距離を重みとする隣接行列</returns>
        public static List<List<long>> WarshallFloyd (List<List<long>> adjacencyMatrix) {
            var nodeNum = adjacencyMatrix.Count;
            var res = Enumerable.Range (0, nodeNum).Select (i => new List<long> (adjacencyMatrix[i])).ToList ();
            foreach (var b in Enumerable.Range (0, nodeNum)) {
                foreach (var a in Enumerable.Range (0, nodeNum)) {
                    foreach (var c in Enumerable.Range (0, nodeNum)) {
                        res[a][c] = Min (res[a][c], res[a][b] + res[b][c]);
                    }
                }
            }
            return res;
        }
    }

    public static class MyExtensions {
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
        /// シーケンスの要素ごとに副作用を起こす（シーケンスはそのまま）
        /// </summary>
        /// <param name="source">シーケンス</param>
        /// <param name="action">副作用。要素をとるアクション</param>
        /// <typeparam name="T">シーケンスの要素の型</typeparam>
        /// <returns>元のシーケンス</returns>
        public static IEnumerable<T> Do<T> (this IEnumerable<T> source, Action<T> action) {
            foreach (var item in source) {
                action (item);
            }
            return source;
        }

        /// <summary>
        /// パイプライン演算子のようにデータを変換する（関数を適用する）
        /// </summary>
        /// <param name="arg">変換するデータ</param>
        /// <param name="func">変換</param>
        /// <typeparam name="T">変換元のデータの型</typeparam>
        /// <typeparam name="TR">変換先のデータの型</typeparam>
        /// <returns>変換されたデータ</returns>
        public static TR Apply<T, TR> (this T arg, Func<T, TR> func) {
            return func (arg);
        }

        /// <summary>
        /// データを標準出力に流す（データはそのまま）
        /// </summary>
        /// <param name="item">データ</param>
        /// <typeparam name="T">データの型</typeparam>
        /// <returns>元のデータ</returns>
        public static T Trace<T> (this T item) {
            Print (item);
            return item;
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
                .Select (i => func (list[i - 1], list[i]));
        }

        /// <summary>
        /// 累積項を要素にもつシーケンスを得る(初項は、first)
        /// <para>O(N)</para>
        /// </summary>
        /// <param name="source">元のシーケンス</param>
        /// <param name="func">2引数関数f</param>
        /// <param name="first">func(first, source[0])のための初項</param>
        /// <example> [1,2,3].Scanl1(0,f) => [0, f(0,1), f(f(0,1),2), f(f(f(0,1),2),3)]</example>
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
        /// <param name="source">要素数1以上のシーケンス</param>
        /// <param name="func">2引数関数f</param>
        /// <example> [1,2,3].Scanl1(f) => [1, f(1,2), f(f(1,2),3)]</example>
        public static IEnumerable<T> Scanl1<T> (this IEnumerable<T> source, Func<T, T, T> func) {
            if (source.IsEmpty ()) throw new ArgumentOutOfRangeException ();
            var list = source.ToList ();
            var result = new List<T> { list[0] };
            foreach (var i in Enumerable.Range (1, source.Count () - 1)) {
                result.Add (func (result[i - 1], list[i]));
            }
            return result;
        }
    }

    public static class MyEnumerable {
        /// <summary>
        /// 左閉右開区間 [startIndex,endIndex) を得る
        /// </summary>
        /// <param name="startIndex">始まりのインデックス。含む</param>
        /// <param name="endIndex">終わりのインデックス。含まない</param>
        public static IEnumerable<int> Interval (int startIndex, int endIndex) {
            if (endIndex - startIndex < 0) new ArgumentException ();
            return Enumerable.Range (startIndex, endIndex - startIndex);
        }
    }
}