using System;

public static class ToObjectCaster<T1> {
    public static Func<T1, object[]> MakeCast () {
        return (T1 p1) => new object[] { p1 };
    }
}

public static class ToObjectCaster<T1, T2> {
    public static Func<T1, T2, object[]> MakeCast () {
        return (T1 p1, T2 p2) => new object[] { p1, p2 };
    }
}

public static class ToObjectCaster<T1, T2, T3> {
    public static Func<T1, T2, T3, object[]> MakeCast () {
        return (T1 p1, T2 p2, T3 p3) => new object[] { p1, p2, p3 };
    }
}

public static class ToObjectCaster<T1, T2, T3, T4> {
    public static Func<T1, T2, T3, T4, object[]> MakeCast () {
        return (T1 p1, T2 p2, T3 p3, T4 p4) => new object[] { p1, p2, p3, p4 };
    }
}

public static class ToObjectCaster<T1, T2, T3, T4, T5> {
    public static Func<T1, T2, T3, T4, T5, object[]> MakeCast () {
        return (T1 p1, T2 p2, T3 p3, T4 p4, T5 a4) => new object[] { p1, p2, p3, p4, a4 };
    }
}