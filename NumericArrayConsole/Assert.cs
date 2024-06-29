namespace NumericArrays.Profile;

public static class Assert {
    public static void IsNotNull(object obj) {
        if (obj == null)
        {
            throw new Exception("Object is null");
        }
    }

    public static void AreEqual<T>(T expected, T actual) {
        if (expected == null)
        {
            if (actual != null)
            {
                throw new Exception($"Expected {expected} but got {actual}");
            }

            if (!expected!.Equals(actual))
            {
                throw new Exception($"Expected {expected} but got {actual}");
            }
        }
    }

    public static void IsTrue(bool condition) {
        if (!condition)
        {
            throw new Exception("Condition is false");
        }
    }
}