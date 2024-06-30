namespace NumericArrays {
    using System;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class NumericArray<TType> : INumericArray<TType>
        where TType : struct, IConvertible {

        #region Operator Overloads
        public static INumericArray<TType> operator +(NumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Add<TType>(rightArray);
        public static INumericArray<TType> operator -(NumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Subtract<TType>(rightArray);
        public static INumericArray<TType> operator *(NumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Multiply<TType>(rightArray);
        public static INumericArray<TType> operator /(NumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Divide<TType>(rightArray);
        public static INumericArray<TType> operator %(NumericArray<TType> leftArray, INumericArray rightArray) => leftArray.Mod<TType>(rightArray);
        public static INumericArray<TType> operator -(NumericArray<TType> array) => array.Negate<TType>();
        #endregion

        #region Private Fields
        private int[] shape;
        private int[] strides;
        private int length;
        private bool isDisposed = false;
        private readonly Type tType = typeof(TType);
        private readonly TypeCode tTypeCode = Type.GetTypeCode(typeof(TType));
        #endregion

        #region Constructors and Finalizers
        protected NumericArray(int[] shape) {
            if (shape == null || shape.Length == 0)
            {
                throw new ArgumentException("Shape must have at least one dimension.");
            }

            this.shape = shape.ToArray();
            this.strides = ComputeStrides(shape);
            this.length = strides[0] * shape[0];
        }

        ~NumericArray() {
            // Only needed if 'Dispose(bool disposing)' has code to free unmanaged resources
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(false);
        }
        #endregion

        #region Protected Properties
        protected int[] ProtectedShape => shape;
        protected int[] ProtectedStrides => strides;
        #endregion

        #region Public Properties
        public abstract TType this[int linearIndex] { get; set; }
        ValueType INumericArray.this[int linearIndex] {
            get => this[linearIndex];
            set {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                this[linearIndex] = ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((IConvertible)value).ToBoolean(null),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)value).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)value).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)value).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)value).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)value).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)value).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)value).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)value).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)value).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)value).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)value).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
        }
        public TType this[params int[] indices] {
            get => this[ComputeLinearIndex(indices)];
            set => this[ComputeLinearIndex(indices)] = value;
        }
        ValueType INumericArray.this[params int[] indices] {
            get => this[ComputeLinearIndex(indices)];
            set {
                this[ComputeLinearIndex(indices)] = ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((IConvertible)value).ToBoolean(null),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)value).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)value).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)value).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)value).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)value).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)value).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)value).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)value).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)value).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)value).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)value).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
        }
        public INumericArray<TType> this[string slicingMask] => this.Slice(slicingMask);
        INumericArray INumericArray.this[string slicingMask] => this[slicingMask];

        public Type ElementType => tType;
        public TypeCode ElementTypeCode => tTypeCode;

        public virtual int MemoryUsage {
            get {
                int memoryUsage = 
                    (sizeof(int) * shape.Length) + 
                    (sizeof(int) * strides.Length) + 
                    sizeof(int) + 
                    sizeof(bool) +
                    //sizeof(Type) +
                    sizeof(TypeCode);

                return memoryUsage;
            }
        }

        public int Rank => shape.Length;
        public int Length => length;
        public int[] Shape => shape.ToArray();
        public int[] Strides => strides.ToArray();
        #endregion

        #region Protected Methods
        protected virtual void Dispose(bool disposing) {
            if (!isDisposed)
            {
                //TotalArraysMemoryUsageInBytes -= SystemMemoryUsageInBytes;
                //TotalArraysAllocated--;

                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                isDisposed = true;
            }
        }
        #endregion

        #region Public Static Methods
        public static int[] ComputeStrides(int[] shape) {
            if (shape.Length == 0)
            {
                throw new ArgumentException("Shape must have at least one dimension.");
            }

            int[] strides = new int[shape.Length];

            strides[shape.Length - 1] = 1;

            for (int i = shape.Length - 2; i >= 0; i--)
            {
                if (shape[i + 1] <= 0)
                {
                    throw new ArgumentException("Shape dimensions must be greater than zero.");
                }

                strides[i] = strides[i + 1] * shape[i + 1];
            }

            return strides;
        }
        #endregion

        #region Public Methods

        #region INSArray Implementation
        public void Reshape(params int[] newShape) {
            if (newShape == null || newShape.Length == 0)
            {
                throw new ArgumentException("Shape must have at least one dimension.");
            }

            int[] newStrides = ComputeStrides(newShape);
            int newLength = newShape[0] * newStrides[0];

            if (newLength != length)
            {
                throw new ArgumentException("New shape must have the same number of elements as the original shape.");
            }

            shape = newShape.ToArray();
            strides = newStrides;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual int ComputeLinearIndex(int[] dimensionalIndex) {
            Debug.Assert(dimensionalIndex != null && dimensionalIndex.Length == shape.Length, "Invalid indices.");

            int linearIndex = 0;
            for (int i = 0; i < Rank; i++)
            {
                Debug.Assert(dimensionalIndex[i] >= 0 && dimensionalIndex[i] < shape[i], $"Dimensional Index: {i} is out of range.");
                linearIndex += dimensionalIndex[i] * strides[i];
            }

            return linearIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual int[] ComputeDimensionalIndex(int linearIndex) {
            if (linearIndex < 0 || linearIndex >= length)
            {
                throw new ArgumentOutOfRangeException("linearIndex", "Index out of range.");
            }

            int[] indices = new int[Rank];

            for (int i = 0; i < Rank; i++)
            {
                indices[i] = linearIndex / strides[i];
                linearIndex -= indices[i] * strides[i];
            }

            return indices;
        }
        #endregion

        #region ICloneable Implementation
        public abstract INumericArray<TType> Clone();
        object ICloneable.Clone() => Clone();
        #endregion

        #region IDisposable Implementation
        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IEnumerable Implementation
        public IEnumerator<TType> GetEnumerator() {
            for (int i = 0; i < length; i++)
            {
                yield return this[i];
            }
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion

        #region ToString Override
        public override string ToString() {
            var sb = new StringBuilder();

            _ = sb.AppendLine($"{GetType().Name}<{ElementType.Name}>");
            //_ = sb.AppendLine($"\tCPU Memory Usage In Bytes: " + SystemMemoryUsageInBytes);
            _ = sb.AppendLine($"\tShape   : [{string.Join(",", shape)}]");
            _ = sb.AppendLine($"\tStrides : [{string.Join(",", strides)}]");
            _ = sb.AppendLine($"\tLength  : {Length}");

            _ = sb.AppendLine($"\tValues  :\n");

            _ = sb.Append("\t[");
            for (int i = 0; i < Length; i++)
            {
                int newLineCnt = 0;

                int[] currentDimensionalIndex = ComputeDimensionalIndex(i);

                for (int j = 0; j < currentDimensionalIndex.Length - 1; j++)
                {
                    if (i % strides[j] == 0)
                    {
                        if (j == 0 && i > 0)
                        {
                            _ = sb.Append("\t ");
                        }

                        _ = sb.Append("[");
                    }
                    else if (currentDimensionalIndex[^1] == 0)
                    {
                        if (j == 0)
                        {
                            _ = sb.Append("\t ");
                        }

                        _ = sb.Append(" ");
                    }
                }

                _ = sb.Append(this[ComputeLinearIndex(currentDimensionalIndex)]);

                if (currentDimensionalIndex[^1] < Shape[^1] - 1)
                {
                    _ = sb.Append(" ");
                }

                for (int j = 0; j < currentDimensionalIndex.Length - 1; j++)
                {
                    if (i % strides[j] == strides[j] - 1)
                    {
                        _ = sb.Append("]");
                        newLineCnt++;
                    }
                }

                for (int j = 0; i < Length - 1 && j < newLineCnt; j++)
                {
                    _ = sb.Append("\n");
                }
            }

            _ = sb.Append("]\n");

            return sb.ToString();
        }
        #endregion

        #endregion
    }
}