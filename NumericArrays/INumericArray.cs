namespace NumericArrays {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface INumericArray : ICloneable, IDisposable, IEnumerable {
        #region Operator Overloads
        public static INumericArray operator +(INumericArray leftArray, INumericArray rightArray) => leftArray.Add(rightArray);
        public static INumericArray operator +(INumericArray leftArray, ValueType rightValue) => leftArray.Add(rightValue);
        public static INumericArray operator -(INumericArray leftArray, INumericArray rightArray) => leftArray.Subtract(rightArray);
        public static INumericArray operator -(INumericArray leftArray, ValueType rightValue) => leftArray.Subtract(rightValue);
        public static INumericArray operator *(INumericArray leftArray, INumericArray rightArray) => leftArray.Multiply(rightArray);
        public static INumericArray operator *(INumericArray leftArray, ValueType rightValue) => leftArray.Multiply(rightValue);
        public static INumericArray operator /(INumericArray leftArray, INumericArray rightArray) => leftArray.Divide(rightArray);
        public static INumericArray operator /(INumericArray leftArray, ValueType rightValue) => leftArray.Divide(rightValue);
        public static INumericArray operator %(INumericArray leftArray, INumericArray rightArray) => leftArray.Mod(rightArray);
        public static INumericArray operator %(INumericArray leftArray, ValueType rightValue) => leftArray.Mod(rightValue);
        public static INumericArray operator -(INumericArray array) => array.Negate();
        #endregion

        #region Properties
        ValueType this[int linearIndex] { get; set; }
        ValueType this[params int[] indices] { get; set; }

        INumericArray this[string slicingMask] { get; }

        Type ElementType { get; }
        TypeCode ElementTypeCode { get; }

        int MemoryUsage { get; }

        int Rank { get; }
        int Length { get; }
        int[] Shape { get; }
        int[] Strides { get; }
        #endregion

        #region Methods
        void Reshape(params int[] newShape);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int ComputeLinearIndex(int[] dimensionalIndex);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        int[] ComputeDimensionalIndex(int linearIndex);
        #endregion
    }

    public interface INumericArray<TType> : INumericArray, IEnumerable<TType>
        where TType : struct, IConvertible {

        #region Operator Overloads
        public static INumericArray<TType> operator +(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Add(rightArray);
        public static INumericArray<TType> operator +(INumericArray<TType> leftArray, ValueType rightValue) => leftArray.Add(rightValue);
        public static INumericArray<TType> operator -(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Subtract(rightArray);
        public static INumericArray<TType> operator -(INumericArray<TType> leftArray, ValueType rightValue) => leftArray.Subtract(rightValue);
        public static INumericArray<TType> operator *(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Multiply(rightArray);
        public static INumericArray<TType> operator *(INumericArray<TType> leftArray, ValueType rightValue) => leftArray.Multiply(rightValue);
        public static INumericArray<TType> operator /(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Divide(rightArray);
        public static INumericArray<TType> operator /(INumericArray<TType> leftArray, ValueType rightValue) => leftArray.Divide(rightValue);
        public static INumericArray<TType> operator %(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Mod(rightArray);
        public static INumericArray<TType> operator %(INumericArray<TType> leftArray, ValueType rightValue) => leftArray.Mod(rightValue);
        public static INumericArray<TType> operator -(INumericArray<TType> array) => array.Negate();
        #endregion

        #region Properties
        new TType this[int linearIndex] { get; set; }
        new TType this[params int[] indices] { get; set; }
        new INumericArray<TType> this[string slicingMask] { get; }

        new INumericArray<TType> Clone();
        #endregion
    }
}