namespace NumericArraysTests {
    using NumericArrays;

    public abstract partial class ArrayTests {
        protected virtual int[] CreateArrayShape { get; } = [2, 3, 4];
        protected abstract INumericArray<T> CreateArray<T>(int[] shape, ValueType[]? values = null) where T : struct, IConvertible;
        protected abstract INumericArray<T> CreateArray<T>(int[] shape, ValueType value) where T : struct, IConvertible;
    }
}