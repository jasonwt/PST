namespace NumericArraysTests {
    public abstract class ArrayForEachElementTests: ArrayTests {
        [TestMethod()]
        public virtual void ForEachElementTest() {
            var arrayToTest = CreateArray<int>(CreateArrayShape);
        }
    }
}