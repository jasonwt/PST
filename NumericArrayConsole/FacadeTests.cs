namespace NumericArrays.Profile;

using System.Linq;

using NumericArrays.Concrete;

public static partial class ProfileApplication {
    public static void AsTypeTests() {
        var systemArray = new SystemArray<int>([2,3,4]);
        systemArray.Fill(Enumerable.Range(0, 24).Select(x => x).Cast<ValueType>().ToArray());
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(0, 24)));

        var floatArray = systemArray.AsType<float>();
        floatArray.SequenceEqual(Enumerable.Range(0, 24).Select(x => (float)x));
    }
    
    public static void TransposeTests() {
        var systemArray = new SystemArray<int>([2,3,4]);

        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
        Assert.AreEqual(typeof(int), systemArray.ElementType);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));

        var transpose102Array = systemArray.Transpose<float>([1, 0, 2]);
        Assert.AreEqual(typeof(float), transpose102Array.ElementType);
        Assert.IsTrue(transpose102Array.Shape.SequenceEqual([3, 2, 4]));
        Assert.IsTrue(transpose102Array.SequenceEqual([
                1f,2f,3f,4f,
                13f,14f,15f,16f,

                5f,6f,7f,8f,
                17f,18f,19f,20f,

                9f,10f,11f,12f,
                21f,22f,23f,24f
            ]));

        var transpose021Array = systemArray.Transpose<float>([0, 2, 1]);
        Assert.AreEqual(typeof(float), transpose021Array.ElementType);
        Assert.IsTrue(transpose021Array.Shape.SequenceEqual([2, 4, 3]));
        Assert.IsTrue(transpose021Array.SequenceEqual([
                1f, 5f, 9f,
                2f, 6f, 10f,
                3f, 7f, 11f,
                4f, 8f, 12f,

                13f, 17f, 21f,
                14f, 18f, 22f,
                15f, 19f, 23f,
                16f, 20f, 24f
            ]));

        var transpose210Array = systemArray.Transpose<float>([2, 1, 0]);
        Assert.AreEqual(typeof(float), transpose210Array.ElementType);
        Assert.IsTrue(transpose210Array.Shape.SequenceEqual([4, 3, 2]));
        Assert.IsTrue(transpose210Array.SequenceEqual([
                1f, 13f,
                5f, 17f,
                9f, 21f,

                2f, 14f,
                6f, 18f,
                10f, 22f,

                3f, 15f,
                7f, 19f,
                11f, 23f,

                4f, 16f,
                8f, 20f,
                12f, 24f
            ]));
    }
    public static void SwapAxisTests() {
        var systemArray = new SystemArray<int>([2,3,4]);
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
        Assert.AreEqual(typeof(int), systemArray.ElementType);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));

        var swapAxis01Array = systemArray.SwapAxis<float>(0, 1);
        Assert.AreEqual(typeof(float), swapAxis01Array.ElementType);
        Assert.IsTrue(swapAxis01Array.Shape.SequenceEqual([3, 2, 4 ]));
        Assert.IsTrue(swapAxis01Array.SequenceEqual([
                1f,2f,3f,4f,
                13f,14f,15f,16f,

                5f,6f,7f,8f,
                17f,18f,19f,20f,

                9f,10f,11f,12f,
                21f,22f,23f,24f
            ]));

        var swapAxis12Array = systemArray.SwapAxis<float>(1, 2);
        Assert.AreEqual(typeof(float), swapAxis12Array.ElementType);
        Assert.IsTrue(swapAxis12Array.Shape.SequenceEqual([2, 4, 3 ]));
        Assert.IsTrue(swapAxis12Array.SequenceEqual([
                1f, 5f, 9f,
                2f, 6f, 10f,
                3f, 7f, 11f,
                4f, 8f, 12f,

                13f, 17f, 21f,
                14f, 18f, 22f,
                15f, 19f, 23f,
                16f, 20f, 24f
            ]));

        var swapAxis02Array = systemArray.SwapAxis<float>(0, 2);
        Assert.AreEqual(typeof(float), swapAxis02Array.ElementType);
        Assert.IsTrue(swapAxis02Array.Shape.SequenceEqual([4, 3, 2 ]));
        Assert.IsTrue(swapAxis02Array.SequenceEqual([
                1f, 13f,
                5f, 17f,
                9f, 21f,

                2f, 14f,
                6f, 18f,
                10f, 22f,

                3f, 15f,
                7f, 19f,
                11f, 23f,

                4f, 16f,
                8f, 20f,
                12f, 24f
            ]));
    }
    public static void SlicingTests() {
        var systemArray = new SystemArray<int>([2,3,4]);
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
        Assert.AreEqual(typeof(int), systemArray.ElementType);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));
    }
    public static void ViewTests() {
        var systemArray = new SystemArray<int>([2,3,4]);
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
        Assert.AreEqual(typeof(int), systemArray.ElementType);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));
    }

    public static void FacadeTests() {
        AsTypeTests();
        TransposeTests();
        SwapAxisTests();
        //        SlicingTests();
        //        ViewTests();


    }
}
