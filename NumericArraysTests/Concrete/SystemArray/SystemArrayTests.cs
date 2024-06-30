namespace NumericArrays.Concrete.SystemArray.Tests {
    using NumericArraysTests;

    [TestClass()]
    public class SystemArrayConstructTests : ArrayTests {
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) {
            var newArray = new SystemArray<T>(shape);

            if (values != null)
            {
                newArray.Fill(values);
            }

            return newArray;
        }
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType value) {
            var newArray = new SystemArray<T>(shape);
            newArray.Fill(value);
            return newArray;
        }

        [TestMethod()]
        public void ConstructTests() {
            var systemArray = CreateArray<int>(CreateArrayShape);
            Assert.IsNotNull(systemArray);
            Assert.AreEqual(typeof(int), systemArray.ElementType);
            Assert.IsTrue(systemArray.Shape.SequenceEqual(CreateArrayShape));
            Assert.AreEqual(3, systemArray.Rank);
            Assert.AreEqual(systemArray.Shape.Length, systemArray.Strides.Length);
            Assert.AreEqual(2 * 3 * 4, systemArray.Length);

            var systemArray2 = new SystemArray<float>([2, 3, 4, 5]);
            Assert.IsNotNull(systemArray2);
            Assert.AreEqual(typeof(float), systemArray2.ElementType);
            Assert.IsTrue(systemArray2.Shape.SequenceEqual([2, 3, 4, 5]));
            Assert.AreEqual(4, systemArray2.Rank);
            Assert.AreEqual(systemArray2.Shape.Length, systemArray2.Strides.Length);
            Assert.AreEqual(2 * 3 * 4 * 5, systemArray2.Length);

            var systemArray3 = new SystemArray<decimal>([2, 3, 4, 5, 6]);
            Assert.IsNotNull(systemArray3);
            Assert.AreEqual(typeof(decimal), systemArray3.ElementType);
            Assert.IsTrue(systemArray3.Shape.SequenceEqual([2, 3, 4, 5, 6]));
            Assert.AreEqual(5, systemArray3.Rank);
            Assert.AreEqual(systemArray3.Shape.Length, systemArray3.Strides.Length);
            Assert.AreEqual(2 * 3 * 4 * 5 * 6, systemArray3.Length);
        }
    }

    [TestClass()]
    public class SystemArrayFillTests : ArrayFillTests {
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) {
            var newArray = new SystemArray<T>(shape);

            if (values != null)
            {
                newArray.Fill(values);
            }

            return newArray;
        }
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType value) {
            var newArray = new SystemArray<T>(shape);
            newArray.Fill(value);
            return newArray;
        }
    }

    [TestClass()]
    public class SystemArrayFacadeTests : ArrayFacadeTests
    {
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) {
            var newArray = new SystemArray<T>(shape);

            if (values != null)
            {
                newArray.Fill(values);
            }

            return newArray;
        }
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType value) {
            var newArray = new SystemArray<T>(shape);
            newArray.Fill(value);
            return newArray;
        }
    }

    [TestClass()]
    public class SystemArrayMathematicsTests : ArrayMathematicTests
    {
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) {
            var newArray = new SystemArray<T>(shape);

            if (values != null)
            {
                newArray.Fill(values);
            }

            return newArray;
        }
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType value) {
            var newArray = new SystemArray<T>(shape);
            newArray.Fill(value);
            return newArray;
        }
    }

    [TestClass()]
    public class SystemArrayReductionsTests : ArrayReductionTests
    {
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) {
            var newArray = new SystemArray<T>(shape);

            if (values != null)
            {
                newArray.Fill(values);
            }

            return newArray;
        }
        protected override INumericArray<T> CreateArray<T>(int[] shape, ValueType value) {
            var newArray = new SystemArray<T>(shape);
            newArray.Fill(value);
            return newArray;
        }
    }
}