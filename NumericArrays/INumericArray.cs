namespace NumericArrays {
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public interface INumericArray : ICloneable, IDisposable, IEnumerable {
        #region Operator Overloads
        public static INumericArray operator +(INumericArray leftArray, INumericArray rightArray) => leftArray.Add(rightArray);
        public static INumericArray operator -(INumericArray leftArray, INumericArray rightArray) => leftArray.Subtract(rightArray);
        public static INumericArray operator *(INumericArray leftArray, INumericArray rightArray) => leftArray.Multiply(rightArray);
        public static INumericArray operator /(INumericArray leftArray, INumericArray rightArray) => leftArray.Divide(rightArray);
        public static INumericArray operator %(INumericArray leftArray, INumericArray rightArray) => leftArray.Mod(rightArray);
        public static INumericArray operator -(INumericArray array) => array.Negate();
        #endregion

        #region Properties
        ValueType this[int linearIndex] { get; set; }
        ValueType this[params int[] indices] { get; set; }

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
        public static INumericArray<TType> operator +(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Add<TType>(rightArray);
        public static INumericArray<TType> operator -(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Subtract<TType>(rightArray);
        public static INumericArray<TType> operator *(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Multiply<TType>(rightArray);
        public static INumericArray<TType> operator /(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Divide<TType>(rightArray);
        public static INumericArray<TType> operator %(INumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Mod<TType>(rightArray);
        public static INumericArray<TType> operator -(INumericArray<TType> array) => array.Negate<TType>();
        #endregion

        #region Properties
        new TType this[int linearIndex] { get; set; }
        new TType this[params int[] indices] { get; set; }

        new INumericArray<TType> Clone();
        #endregion
    }
}