using System;

public static class ToObjectCaster<Expected, Arg1> {
    public static Func<Expected, Arg1, object[]> MakeCast () {
        return (Expected exp, Arg1 a1) => new object[] { exp, a1 };
    }
}

public static class ToObjectCaster<Expected, Arg1, Arg2> {
    public static Func<Expected, Arg1, Arg2, object[]> MakeCast () {
        return (Expected exp, Arg1 a1, Arg2 a2) => new object[] { exp, a1, a2 };
    }
}

public static class ToObjectCaster<Expected, Arg1, Arg2, Arg3> {
    public static Func<Expected, Arg1, Arg2, Arg3, object[]> MakeCast () {
        return (Expected exp, Arg1 a1, Arg2 a2, Arg3 a3) => new object[] { exp, a1, a2, a3 };
    }
}

public static class ToObjectCaster<Expected, Arg1, Arg2, Arg3, Arg4> {
    public static Func<Expected, Arg1, Arg2, Arg3, Arg4, object[]> MakeCast () {
        return (Expected exp, Arg1 a1, Arg2 a2, Arg3 a3, Arg4 a4) => new object[] { exp, a1, a2, a3, a4 };
    }
}