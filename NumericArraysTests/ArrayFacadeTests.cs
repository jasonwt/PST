namespace NumericArraysTests {
    using NumericArrays;
    using NumericArrays.Extensions;

    public abstract class ArrayFacadeTests : ArrayTests {
        [TestMethod()]
        public virtual void AsTypeTests() {
            var arrayToTest = CreateArray<long>(CreateArrayShape, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());
            Assert.IsTrue(arrayToTest.SequenceEqual(Enumerable.Range(1, 24).Select(a => a.ToInt64())));

            var doubleArray = arrayToTest.AsType<double>();
            Assert.IsTrue(doubleArray.SequenceEqual(Enumerable.Range(1, 24).Select(x => (double)x)));
        }

        [TestMethod()]
        public virtual void TransposeTests() {
            var arrayToTest = CreateArray<int>(CreateArrayShape, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());
            Assert.AreEqual(typeof(int), arrayToTest.ElementType);
            Assert.IsTrue(arrayToTest.SequenceEqual(Enumerable.Range(1, 24)));

            var transpose102Array = arrayToTest.Transpose<float>([1, 0, 2]);
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

            var transpose021Array = arrayToTest.Transpose<float>([0, 2, 1]);
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

            var transpose210Array = arrayToTest.Transpose<float>([2, 1, 0 ]);
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

        [TestMethod()]
        public virtual void SwapAxisTests() {
            var arrayToTest = CreateArray<int>(CreateArrayShape, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());
            Assert.AreEqual(typeof(int), arrayToTest.ElementType);
            Assert.IsTrue(arrayToTest.SequenceEqual(Enumerable.Range(1, 24)));

            var swapAxis01Array = arrayToTest.SwapAxis<float>(0, 1);
            Assert.AreEqual(typeof(float), swapAxis01Array.ElementType);
            Assert.IsTrue(swapAxis01Array.Shape.SequenceEqual([3,2,4]));
            Assert.IsTrue(swapAxis01Array.SequenceEqual([
                1f,2f,3f,4f,
                13f,14f,15f,16f,

                5f,6f,7f,8f,
                17f,18f,19f,20f,

                9f,10f,11f,12f,
                21f,22f,23f,24f
            ]));

            var swapAxis12Array = arrayToTest.SwapAxis<float>(1, 2);
            Assert.AreEqual(typeof(float), swapAxis12Array.ElementType);
            Assert.IsTrue(swapAxis12Array.Shape.SequenceEqual([2, 4, 3]));
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

            var swapAxis02Array = arrayToTest.SwapAxis<float>(0, 2);
            Assert.AreEqual(typeof(float), swapAxis02Array.ElementType);
            Assert.IsTrue(swapAxis02Array.Shape.SequenceEqual([4, 3, 2]));
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

        [TestMethod()]
        public virtual void SlicingTests() {
            var arrayToTest = CreateArray<int>(CreateArrayShape, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());
            Assert.AreEqual(typeof(int), arrayToTest.ElementType);
            Assert.IsTrue(arrayToTest.SequenceEqual(Enumerable.Range(1, 24)));

            // First Dimension
            Assert.IsTrue(arrayToTest[":"].SequenceEqual(Enumerable.Range(1, 24)));
            Assert.IsTrue(arrayToTest["0"].SequenceEqual(Enumerable.Range(1, 12)));
            Assert.IsTrue(arrayToTest["1"].SequenceEqual(Enumerable.Range(13, 12)));
            // Slicing with step
            Assert.IsTrue(arrayToTest["0:2"].SequenceEqual(Enumerable.Range(1, 24)));
            // TODO: BUG: not working with negative indices
            Assert.IsTrue(arrayToTest["-1"].SequenceEqual(Enumerable.Range(13, 12)));
            Assert.IsTrue(arrayToTest["-2"].SequenceEqual(Enumerable.Range(1, 12)));

            // Second Dimension
            Assert.IsTrue(arrayToTest[":,0"].SequenceEqual([1,2,3,4,13,14,15,16]));
            Assert.IsTrue(arrayToTest[":,1"].SequenceEqual([5, 6, 7, 8, 17, 18, 19, 20]));
            Assert.IsTrue(arrayToTest[":,2"].SequenceEqual([9,10,11,12,21,22,23,24]));
            // Slicing with step
            Assert.IsTrue(arrayToTest[":,0:2,:"].SequenceEqual([1,2,3,4,5,6,7,8,13,14,15,16,17,18,19,20]));
            // TODO: BUG: not working with negative indices
            Assert.IsTrue(arrayToTest[":,-1"].SequenceEqual([9, 10, 11, 12, 21, 22, 23, 24 ]));
            Assert.IsTrue(arrayToTest[":,-2"].SequenceEqual([5, 6, 7, 8, 17, 18, 19, 20 ]));

            // Third Dimension
            Assert.IsTrue(arrayToTest[":,:,0"].SequenceEqual([1, 5, 9, 13, 17, 21]));
            Assert.IsTrue(arrayToTest[":,:,1"].SequenceEqual([2, 6, 10, 14, 18, 22]));
            Assert.IsTrue(arrayToTest[":,:,2"].SequenceEqual([3, 7, 11, 15, 19, 23]));
            Assert.IsTrue(arrayToTest[":,:,3"].SequenceEqual([4, 8, 12, 16, 20, 24]));
            // Slicing with step
            Assert.IsTrue(arrayToTest[":,:,0:2"].SequenceEqual([1, 2, 5, 6, 9, 10, 13, 14, 17, 18, 21, 22]));
            Assert.IsTrue(arrayToTest[":,:,1:2"].SequenceEqual([2, 6, 10, 14, 18, 22]));
            // TODO: BUG: not working with negative indices
            Assert.IsTrue(arrayToTest[":,:,-1"].SequenceEqual([4, 8, 12, 16, 20, 24 ]));
            Assert.IsTrue(arrayToTest[":,:,-2"].SequenceEqual([3, 7, 11, 15, 19, 23 ]));
            Assert.IsTrue(arrayToTest[":,:,-3"].SequenceEqual([2, 6, 10, 14, 18, 22 ]));
            Assert.IsTrue(arrayToTest[":,:,-4"].SequenceEqual([1, 5, 9, 13, 17, 21 ]));
        }

        [TestMethod()]
        public virtual void ViewTests() {
            var arrayToTest = CreateArray<int>(CreateArrayShape, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());
            Assert.AreEqual(typeof(int), arrayToTest.ElementType);
            Assert.IsTrue(arrayToTest.SequenceEqual(Enumerable.Range(1, 24)));
        }
    }
}