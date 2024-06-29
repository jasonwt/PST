namespace NumericArrays.Profile;

using NumericArrays.Concrete;

using System.Linq;

public static partial class ProfileApplication {
    public static void ConstructTests() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        Assert.IsNotNull(systemArray);
        Assert.AreEqual(typeof(int), systemArray.ElementType);
        Assert.IsTrue(systemArray.Shape.SequenceEqual(new int[] { 2, 3, 4 }));
        Assert.AreEqual(3, systemArray.Rank);
        Assert.AreEqual(systemArray.Shape.Length, systemArray.Strides.Length);
        Assert.AreEqual(2 * 3 * 4, systemArray.Length);

        var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4, 5 });
        Assert.IsNotNull(systemArray2);
        Assert.AreEqual(typeof(float), systemArray2.ElementType);
        Assert.IsTrue(systemArray2.Shape.SequenceEqual(new int[] { 2, 3, 4, 5 }));
        Assert.AreEqual(4, systemArray2.Rank);
        Assert.AreEqual(systemArray2.Shape.Length, systemArray2.Strides.Length);
        Assert.AreEqual(2 * 3 * 4 * 5, systemArray2.Length);

        var systemArray3 = new SystemArray<decimal>(new int[] { 2, 3, 4, 5, 6 });
        Assert.IsNotNull(systemArray3);
        Assert.AreEqual(typeof(decimal), systemArray3.ElementType);
        Assert.IsTrue(systemArray3.Shape.SequenceEqual(new int[] { 2, 3, 4, 5, 6 }));
        Assert.AreEqual(5, systemArray3.Rank);
        Assert.AreEqual(systemArray3.Shape.Length, systemArray3.Strides.Length);
        Assert.AreEqual(2 * 3 * 4 * 5 * 6, systemArray3.Length);
    }

    public static void FillTests() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(5.0f);
        Assert.IsTrue(systemArray.All(x => x == 5));

        ValueType[] valueTypeArray = new ValueType[] {
            false, true, (sbyte) 2, (byte) 3,
            (short) 4, (ushort) 5, (int) 6, (uint) 7,
            (long) 8, (ulong) 9, (float) 10.0f, (double) 11.0,
            (decimal) 12.0, (sbyte) 13, (byte) 14, (short) 15,
            (ushort) 16, (int) 17, (uint) 18, (long) 19,
            (ulong) 20, (float) 21.0f, (double) 22.0, (decimal) 23.0
        };

        systemArray.Fill(valueTypeArray);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(0, 24)));

        systemArray.Fill((int i) => i * 2);
        Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(0, 24).Select(x => x * 2)));
        
    }

    public static void Main() {
        ConstructTests();
        FillTests();
        FacadeTests();
        MathematicTests();
        ReductionTests();

        Console.WriteLine("All tests passed");
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
