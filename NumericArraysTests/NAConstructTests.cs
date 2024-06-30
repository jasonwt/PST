namespace NumericArrays.Tests {
    using NumericArrays;

    [TestClass()]
    public class NAConstructTests {
        protected virtual int[] CreateArrayShape => [2, 3, 4];

        [TestMethod()]
        public virtual void ConstructFull() {
            var fullArray = NA.Full<int>(CreateArrayShape, 5);
            Assert.IsTrue(fullArray.SequenceEqual(Enumerable.Repeat(5, 2 * 3 * 4).ToArray()));
        }

        [TestMethod()]
        public virtual void ConstructZeros() {
            var zerosArray = NA.Zeros<int>(CreateArrayShape);
            Assert.IsTrue(zerosArray.SequenceEqual(Enumerable.Repeat(0, 2 * 3 * 4).ToArray()));
        }

        [TestMethod()]
        public virtual void ConstructOnes() {
            var onesArray = NA.Ones<int>(CreateArrayShape);
            Assert.IsTrue(onesArray.SequenceEqual(Enumerable.Repeat(1, 2 * 3 * 4).ToArray()));
        }

        [TestMethod()]
        public virtual void ConstructARange() {
            var arangeArray = NA.ARange<float>(0.0f, 1.0f, 0.25f);
            Assert.IsTrue(arangeArray.SequenceEqual([0.0f, 0.25f, 0.5f, 0.75f, 1.0f]));
        }

        [TestMethod()]
        public virtual void ConstructLinspaceTest() {
            var linspaceArray9 = NA.Linspace<int>(1, 9, 9);
            Assert.IsTrue(linspaceArray9.SequenceEqual([1, 2, 3, 4, 5, 6, 7, 8, 9]));

            var linspaceArray21 = NA.Linspace<float>(0, 10, 21);
            Assert.IsTrue(linspaceArray21.SequenceEqual([
                0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f, 4.0f, 4.5f, 5.0f, 5.5f, 6.0f, 6.5f, 7.0f, 7.5f, 8.0f, 8.5f, 9.0f, 9.5f, 10.0f
            ]));
        }

        [TestMethod()]
        public virtual void ConstructEyeTest() {
            var eyeArray2 = NA.Eye<int>(3);
            Assert.IsTrue(eyeArray2.SequenceEqual([1, 0, 0, 0, 1, 0, 0, 0, 1]));
            Console.WriteLine(eyeArray2);

            var eyeArray3 = NA.Eye<int>(3, 3);
            Assert.IsTrue(eyeArray3.SequenceEqual([
                1, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 1, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 1
            ]));
            Console.WriteLine(eyeArray3);
        }
    }
}