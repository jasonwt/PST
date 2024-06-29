using NumericArraysTests;

namespace NumericArrays.Concrete.Tests {
    [TestClass()]
    public class SystemArrayTests : ArrayTests {
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

        //[TestMethod()]
        //public void CloneTest() {
        //    Assert.Fail();
        //}
    }
}