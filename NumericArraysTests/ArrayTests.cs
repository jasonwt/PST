using NumericArrays;
using NumericArrays.Concrete;

namespace NumericArraysTests {
    public abstract partial class ArrayTests {
        protected abstract INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) where T : struct, IConvertible;
        protected abstract INumericArray<T> CreateArray<T>(int[] shape, ValueType value) where T : struct, IConvertible;

        #region Construct Tests
        [TestMethod()]
        public virtual void ConstructTests() {
            var systemArray = CreateArray<int>(new int[] { 2, 3, 4 });
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
        #endregion

        #region Fill Tests
        [TestMethod()]
        public virtual void FillTests() {
            var systemArray = CreateArray<int>(new int[] { 2, 3, 4 }, 5.0f);
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
        #endregion

        #region Facade Tests

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

        #endregion

        #region Mathematic Tests

        [TestMethod()]
        public  void TestAddition() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a + b)).ToArray();
            var additionArray = systemArray1.Add<long>(systemArray2);
            var additionArray3 = systemArray1.Add(systemArray2);
            Assert.AreEqual(TypeCode.Int64, additionArray.ElementTypeCode);

            var additionArray2 = systemArray1 + systemArray2;
            Assert.AreEqual(TypeCode.Int32, additionArray2.ElementTypeCode);
            Assert.IsTrue(additionArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(additionArray2.SequenceEqual(additionArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));
        }

        [TestMethod()]
        public virtual void TestSubtraction() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a - b)).ToArray();
            var subtractionArray = systemArray1.Subtract<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, subtractionArray.ElementTypeCode);

            var subtractionArray2 = systemArray1 - systemArray2;
            Assert.AreEqual(TypeCode.Int32, subtractionArray2.ElementTypeCode);
            Assert.IsTrue(subtractionArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(subtractionArray2.SequenceEqual(subtractionArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));
        }

        [TestMethod()]
        public virtual void TestMultiplication() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a * b)).ToArray();
            var multiplicationArray = systemArray1.Multiply<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, multiplicationArray.ElementTypeCode);

            var multiplicationArray2 = systemArray1 * systemArray2;
            Assert.AreEqual(TypeCode.Int32, multiplicationArray2.ElementTypeCode);
            Assert.IsTrue(multiplicationArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(multiplicationArray2.SequenceEqual(multiplicationArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));
        }

        [TestMethod()]
        public virtual void TestDivision() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a / b)).ToArray();
            var divisionArray = systemArray1.Divide<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, divisionArray.ElementTypeCode);
            Assert.IsTrue(divisionArray.SequenceEqual(expectedValues1));

            var divisionArray2 = systemArray1 / systemArray2;
            Assert.AreEqual(TypeCode.Int32, divisionArray2.ElementTypeCode);
            Assert.IsTrue(divisionArray2.SequenceEqual(divisionArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));
        }

        [TestMethod()]
        public virtual void TestModulus() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a % b)).ToArray();
            var modulusArray = systemArray1.Mod<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, modulusArray.ElementTypeCode);
            Assert.IsTrue(modulusArray.SequenceEqual(expectedValues1));

            var modulusArray2 = systemArray1 % systemArray2;
            Assert.AreEqual(TypeCode.Int32, modulusArray2.ElementTypeCode);
            Assert.IsTrue(modulusArray2.SequenceEqual(modulusArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));
        }

        [TestMethod()]
        public virtual void TestPower() {
            var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = new SystemArray<float>(new int[] { 2, 3, 4 });
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(Math.Pow(a, b))).ToArray();
            var powerArray = systemArray1.Pow<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, powerArray.ElementTypeCode);
            Assert.IsTrue(powerArray.SequenceEqual(expectedValues1));
        }

        [TestMethod()]
        public virtual void TestNegation() {
            var systemByteArray = new SystemArray<byte>(new int[] { 2, 3, 4 });
            systemByteArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var expectedValues = Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToInt16(-a)).ToArray();
            var shortNegationArray = systemByteArray.Negate();
            Assert.AreEqual(TypeCode.Int16, shortNegationArray.ElementTypeCode);
            Assert.IsTrue(shortNegationArray.SequenceEqual(expectedValues));

            shortNegationArray = -shortNegationArray;
            Assert.AreEqual(TypeCode.Int16, shortNegationArray.ElementTypeCode);
            Assert.IsTrue(shortNegationArray.SequenceEqual(expectedValues.Select(a => (short)-a).ToArray()));

            var ushortNegationArray = shortNegationArray.AsType<ushort>();
            Assert.AreEqual(TypeCode.UInt16, ushortNegationArray.ElementTypeCode);
            Assert.IsTrue(ushortNegationArray.SequenceEqual(expectedValues.Select(a => (ushort)-a).ToArray()));

            var intNegationArray = ushortNegationArray.Negate();
            Assert.AreEqual(TypeCode.Int32, intNegationArray.ElementTypeCode);
            Assert.IsTrue(intNegationArray.SequenceEqual(expectedValues.Select(a => (int)a).ToArray()));

            intNegationArray = -intNegationArray;
            Assert.AreEqual(TypeCode.Int32, intNegationArray.ElementTypeCode);
            Assert.IsTrue(intNegationArray.SequenceEqual(expectedValues.Select(a => -a).ToArray()));

            var doubleNegationArray = intNegationArray.Negate<double>();
            Assert.AreEqual(TypeCode.Double, doubleNegationArray.ElementTypeCode);
            Assert.IsTrue(doubleNegationArray.SequenceEqual(expectedValues.Select(a => (double)a).ToArray()));
        }

        [TestMethod()]
        public virtual void TestAbs() {
            var systemByteArray = new SystemArray<byte>(new int[] { 2, 3, 4 });
            systemByteArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var expectedValues = Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToByte(Math.Abs(a))).ToArray();
            var shortAbsArray = systemByteArray.Abs();
            Assert.AreEqual(TypeCode.Byte, shortAbsArray.ElementTypeCode);
            Assert.IsTrue(shortAbsArray.SequenceEqual(expectedValues));


        }

        #endregion

        #region Reduction Tests

        [TestMethod()]
        public virtual void TestSum() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var sumInt = systemArray.Sum();
            var sumIntAxis0 = systemArray.Sum(0);
            var sumIntAxis1 = systemArray.Sum(1);
            var sumIntAxis2 = systemArray.Sum(2);
        }

        [TestMethod()]
        public virtual void TestProd() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var prodInt = systemArray.Prod();
            var prodIntAxis0 = systemArray.Prod(0);
            var prodIntAxis1 = systemArray.Prod(1);
            var prodIntAxis2 = systemArray.Prod(2);
        }

        [TestMethod()]
        public virtual void TestMean() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());


            var meanInt = systemArray.Mean();
            var meanIntAxis0 = systemArray.Mean(0);
            var meanIntAxis1 = systemArray.Mean(1);
            var meanIntAxis2 = systemArray.Mean(2);
        }

        [TestMethod()]
        public virtual void TestMin() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var minInt = systemArray.Min();
            var minIntAxis0 = systemArray.Min(0);
            var minIntAxis1 = systemArray.Min(1);
            var minIntAxis2 = systemArray.Min(2);
        }

        [TestMethod()]
        public virtual void TestMax() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var maxInt = systemArray.Max();
            var maxIntAxis0 = systemArray.Max(0);
            var maxIntAxis1 = systemArray.Max(1);
            var maxIntAxis2 = systemArray.Max(2);
        }

        [TestMethod()]
        public virtual void TestMedian() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var medianInt = systemArray.Median();
            var medianIntAxis0 = systemArray.Median(0);
            var medianIntAxis1 = systemArray.Median(1);
            var medianIntAxis2 = systemArray.Median(2);
        }

        [TestMethod()]
        public virtual void TestVar() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var varianceInt = systemArray.Var();
            var varianceIntAxis0 = systemArray.Var(0);
            var varianceIntAxis1 = systemArray.Var(1);
            var varianceIntAxis2 = systemArray.Var(2);
        }

        [TestMethod()]
        public virtual void TestStd() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var stdInt = systemArray.Std();
            var stdIntAxis0 = systemArray.Std(0);
            var stdIntAxis1 = systemArray.Std(1);
            var stdIntAxis2 = systemArray.Std(2);
        }

        [TestMethod()]
        public virtual void TestArgMax() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var argMaxInt = systemArray.ArgMax();
            var argMaxIntAxis0 = systemArray.ArgMax(0);
            var argMaxIntAxis1 = systemArray.ArgMax(1);
            var argMaxIntAxis2 = systemArray.ArgMax(2);
        }

        [TestMethod()]
        public virtual void TestArgMin() {
            var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
            systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var argMinInt = systemArray.ArgMin();
            var argMinIntAxis0 = systemArray.ArgMin(0);
            var argMinIntAxis1 = systemArray.ArgMin(1);
            var argMinIntAxis2 = systemArray.ArgMin(2);
        }
        #endregion
    }
}