namespace NumericArraysTests {
    using NumericArrays;

    public abstract class ArrayMathematicTests : ArrayTests {
        [TestMethod()]
        public void TestAddition() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a + b)).ToArray();
            var additionArray = systemArray1.Add<long>(systemArray2);

            var additionArray3 = systemArray1.Add(systemArray2);
            Assert.AreEqual(TypeCode.Int64, additionArray.ElementTypeCode);

            var additionArray2 = systemArray1 + systemArray2;
            Assert.AreEqual(TypeCode.Int32, additionArray2.ElementTypeCode);
            Assert.IsTrue(additionArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(additionArray2.SequenceEqual(additionArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));

            Assert.IsTrue((systemArray1 + 5).SequenceEqual(systemArray1.Select(a => a + 5).ToArray()));
        }

        [TestMethod()]
        public virtual void TestSubtraction() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a - b)).ToArray();
            var subtractionArray = systemArray1.Subtract<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, subtractionArray.ElementTypeCode);

            var subtractionArray2 = systemArray1 - systemArray2;
            Assert.AreEqual(TypeCode.Int32, subtractionArray2.ElementTypeCode);
            Assert.IsTrue(subtractionArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(subtractionArray2.SequenceEqual(subtractionArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));

            Assert.IsTrue((systemArray1 - 1).SequenceEqual(systemArray1.Select(a => a - 1).ToArray()));
        }

        [TestMethod()]
        public virtual void TestMultiplication() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a * b)).ToArray();
            var multiplicationArray = systemArray1.Multiply<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, multiplicationArray.ElementTypeCode);

            var multiplicationArray2 = systemArray1 * systemArray2;
            Assert.AreEqual(TypeCode.Int32, multiplicationArray2.ElementTypeCode);
            Assert.IsTrue(multiplicationArray.SequenceEqual(expectedValues1));
            Assert.IsTrue(multiplicationArray2.SequenceEqual(multiplicationArray.Select(a => ((IConvertible)a).ToInt32(null))));

            Assert.IsTrue((systemArray1 * 2).SequenceEqual(systemArray1.Select(a => a * 2)));
        }

        [TestMethod()]
        public virtual void TestDivision() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a / b)).ToArray();
            var divisionArray = systemArray1.Divide<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, divisionArray.ElementTypeCode);
            Assert.IsTrue(divisionArray.SequenceEqual(expectedValues1));

            var divisionArray2 = systemArray1 / systemArray2;
            Assert.AreEqual(TypeCode.Int32, divisionArray2.ElementTypeCode);
            Assert.IsTrue(divisionArray2.SequenceEqual(divisionArray.Select(a => ((IConvertible)a).ToInt32(null))));
            
            Assert.IsTrue((systemArray2 / 2.0f).SequenceEqual(systemArray2.Select(a => a / 2)));
        }

        [TestMethod()]
        public virtual void TestModulus() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(a % b)).ToArray();
            var modulusArray = systemArray1.Mod<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, modulusArray.ElementTypeCode);
            Assert.IsTrue(modulusArray.SequenceEqual(expectedValues1));

            var modulusArray2 = systemArray1 % systemArray2;
            Assert.AreEqual(TypeCode.Int32, modulusArray2.ElementTypeCode);
            Assert.IsTrue(modulusArray2.SequenceEqual(modulusArray.Select(a => ((IConvertible)a).ToInt32(null)).ToArray()));

            Assert.IsTrue((systemArray1 % 2).SequenceEqual(systemArray1.Select(a => a % 2).ToArray()));
        }

        [TestMethod()]
        public virtual void TestPower() {
            var systemArray1 = CreateArray<int>(CreateArrayShape);
            systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var systemArray2 = CreateArray<float>(CreateArrayShape);
            systemArray2.Fill(Enumerable.Range(1, 2 * 3 * 4).Reverse().ToArray());

            var expectedValues1 = Enumerable.Range(1, 2 * 3 * 4).Zip(Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToSingle(a)).Reverse(), (a, b) => Convert.ToInt64(Math.Pow(a, b))).ToArray();
            var powerArray = systemArray1.Pow<long>(systemArray2);
            Assert.AreEqual(TypeCode.Int64, powerArray.ElementTypeCode);
            Assert.IsTrue(powerArray.SequenceEqual(expectedValues1));

            // TODO: Add Pow test with ValueType

        }

        [TestMethod()]
        public virtual void TestNegation() {
            var systemByteArray = CreateArray<byte>(CreateArrayShape);
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
            var systemByteArray = CreateArray<byte>(CreateArrayShape);
            systemByteArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

            var expectedValues = Enumerable.Range(1, 2 * 3 * 4).Select(a => Convert.ToByte(Math.Abs(a))).ToArray();
            var shortAbsArray = systemByteArray.Abs();
            Assert.AreEqual(TypeCode.Byte, shortAbsArray.ElementTypeCode);
            Assert.IsTrue(shortAbsArray.SequenceEqual(expectedValues));
        }
    }
}