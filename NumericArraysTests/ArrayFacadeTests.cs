namespace NumericArraysTests {
    using NumericArrays;
    using NumericArrays.Concrete;

    public abstract class ArrayFacadeTests : ArrayTests {
        [TestMethod()]
        public virtual void AsTypeTests() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(0, 24).Select(x => x).Cast<ValueType>().ToArray());
            Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(0, 24)));

            var floatArray = systemArray.AsType<float>();
            floatArray.SequenceEqual(Enumerable.Range(0, 24).Select(x => (float)x));
        }

        [TestMethod()]
        public virtual void BroadcastToTests() {
            //Rule 1: 
            //    If the two arrays differ in their number of dimensions, the shape of the one with fewer dimensions 
            //    is padded with ones on its leading(left) side.
            //Rule 2: 
            //    If the shape of the two arrays does not match in any dimension, the array with shape equal to 1 in 
            //    that dimension is stretched to match the other shape.
            //Rule 3: 
            //    If in any dimension the sizes disagree and neither is equal to 1, an error is raised.

            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
            Assert.AreEqual(typeof(int), systemArray.ElementType);

            // Rule 1 Passed
            // systemArray should pad with ones on its leading(left) side
            // in this cast it will padd with two ones on the left side
            // Rule 2 Passed
            // systemArray the ones that were added to the left will be
            // stretched to match the other shape at the corresponding dimension
            // Rule 3 Passed
            // systemArray and broadcastedArray have the same shape because of Rule 1 and Rule 2
            var broadcastedArray = systemArray.BroadcastTo(new int[] { 2, 2, 2, 3, 4 });
            var expectedValues1 = systemArray.Concat(systemArray).Concat(systemArray).Concat(systemArray).ToArray();
            Assert.IsTrue(broadcastedArray.Shape.SequenceEqual(new int[] { 2, 2, 2, 3, 4 }));
            Assert.IsTrue(broadcastedArray.SequenceEqual(expectedValues1));

            // Rule 1 Passed
            // The two shapes are the same rank so nothing should be done
            // Rule 2 Passed
            // All dimensions are the same other then the middle value of the broadcastToShape
            // which is a 1 so it can be exapanded to match that of the systemArray dimension at the same index
            // Rule 3 Passed
            // systemArray and broadcastedArray have the same shape because of Rule 1 and Rule 2
            broadcastedArray = systemArray.BroadcastTo(new int[] { 2, 1, 4 });
            Assert.IsTrue(broadcastedArray.Shape.SequenceEqual(new int[] { 2, 1, 4 }));
            Assert.IsTrue(broadcastedArray.SequenceEqual(new int[] { 1, 2, 3, 4, 13, 14, 15, 16 }));

            broadcastedArray = systemArray.BroadcastTo(new int[] { 1 });
            Assert.IsTrue(broadcastedArray.Shape.SequenceEqual(new int[] { 1 }));
            Assert.IsTrue(broadcastedArray.SequenceEqual(new int[] { 1 }));
        }

        [TestMethod()]
        public virtual void TransposeTests() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });

            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
            Assert.AreEqual(typeof(int), systemArray.ElementType);
            Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));

            var transpose102Array = systemArray.Transpose<float>(new int[] { 1, 0, 2 });
            Assert.AreEqual(typeof(float), transpose102Array.ElementType);
            Assert.IsTrue(transpose102Array.Shape.SequenceEqual(new int[] { 3, 2, 4 }));
            Assert.IsTrue(transpose102Array.SequenceEqual(new float[] {
                1f,2f,3f,4f,
                13f,14f,15f,16f,

                5f,6f,7f,8f,
                17f,18f,19f,20f,

                9f,10f,11f,12f,
                21f,22f,23f,24f
            }));

            var transpose021Array = systemArray.Transpose<float>(new int[] { 0, 2, 1 });
            Assert.AreEqual(typeof(float), transpose021Array.ElementType);
            Assert.IsTrue(transpose021Array.Shape.SequenceEqual(new int[] { 2, 4, 3 }));
            Assert.IsTrue(transpose021Array.SequenceEqual(new float[] {
                1f, 5f, 9f,
                2f, 6f, 10f,
                3f, 7f, 11f,
                4f, 8f, 12f,

                13f, 17f, 21f,
                14f, 18f, 22f,
                15f, 19f, 23f,
                16f, 20f, 24f
            }));

            var transpose210Array = systemArray.Transpose<float>(new int[] { 2, 1, 0 });
            Assert.AreEqual(typeof(float), transpose210Array.ElementType);
            Assert.IsTrue(transpose210Array.Shape.SequenceEqual(new int[] { 4, 3, 2 }));
            Assert.IsTrue(transpose210Array.SequenceEqual(new float[] {
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
            }));
        }

        [TestMethod()]
        public virtual void SwapAxisTests() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
            Assert.AreEqual(typeof(int), systemArray.ElementType);
            Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));

            var swapAxis01Array = systemArray.SwapAxis<float>(0, 1);
            Assert.AreEqual(typeof(float), swapAxis01Array.ElementType);
            Assert.IsTrue(swapAxis01Array.Shape.SequenceEqual(new int[] { 3, 2, 4 }));
            Assert.IsTrue(swapAxis01Array.SequenceEqual(new float[] {
                1f,2f,3f,4f,
                13f,14f,15f,16f,

                5f,6f,7f,8f,
                17f,18f,19f,20f,

                9f,10f,11f,12f,
                21f,22f,23f,24f
            }));

            var swapAxis12Array = systemArray.SwapAxis<float>(1, 2);
            Assert.AreEqual(typeof(float), swapAxis12Array.ElementType);
            Assert.IsTrue(swapAxis12Array.Shape.SequenceEqual(new int[] { 2, 4, 3 }));
            Assert.IsTrue(swapAxis12Array.SequenceEqual(new float[] {
                1f, 5f, 9f,
                2f, 6f, 10f,
                3f, 7f, 11f,
                4f, 8f, 12f,

                13f, 17f, 21f,
                14f, 18f, 22f,
                15f, 19f, 23f,
                16f, 20f, 24f
            }));

            var swapAxis02Array = systemArray.SwapAxis<float>(0, 2);
            Assert.AreEqual(typeof(float), swapAxis02Array.ElementType);
            Assert.IsTrue(swapAxis02Array.Shape.SequenceEqual(new int[] { 4, 3, 2 }));
            Assert.IsTrue(swapAxis02Array.SequenceEqual(new float[] {
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
            }));
        }

        [TestMethod()]
        public virtual void SlicingTests() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
            Assert.AreEqual(typeof(int), systemArray.ElementType);
            Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));
        }

        [TestMethod()]
        public virtual void ViewTests() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
            Assert.AreEqual(typeof(int), systemArray.ElementType);
            Assert.IsTrue(systemArray.SequenceEqual(Enumerable.Range(1, 24)));
        }
    }
}