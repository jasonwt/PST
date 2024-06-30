namespace NumericArraysTests {
    using NumericArrays;

    public abstract class ArrayFillTests : ArrayTests {
        [TestMethod()]
        public virtual void FillTests() {
            var arrayToFill = CreateArray<int>(new int[] { 2, 3, 4 }, 5.0f);
            Assert.IsTrue(arrayToFill.All(x => x == 5));

            ValueType[] valueTypeArray = new ValueType[] {
                false, true, (sbyte) 2, (byte) 3,
                (short) 4, (ushort) 5, (int) 6, (uint) 7,
                (long) 8, (ulong) 9, (float) 10.0f, (double) 11.0,
                (decimal) 12.0, (sbyte) 13, (byte) 14, (short) 15,
                (ushort) 16, (int) 17, (uint) 18, (long) 19,
                (ulong) 20, (float) 21.0f, (double) 22.0, (decimal) 23.0
            };

            arrayToFill.Fill(valueTypeArray);
            Assert.IsTrue(arrayToFill.SequenceEqual(Enumerable.Range(0, 24)));

            arrayToFill.Fill((int i) => i * 2);
            Assert.IsTrue(arrayToFill.SequenceEqual(Enumerable.Range(0, 24).Select(x => x * 2)));
        }
    }
}