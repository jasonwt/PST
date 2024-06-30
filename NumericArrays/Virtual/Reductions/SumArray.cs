namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class SumArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public SumArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {

        }
        public SumArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                int start = 0;
                int iterations = sourceArray.Length / Length;
                int step = axis == null ? 1 : sourceArrayStrides[axis.Value];

                if (axis != null)
                {
                    int sAxisStride = sourceArrayStrides[axis.Value];
                    int sPrevAxisStride = axis.Value == 0 ? 1 : sourceArrayStrides[axis.Value - 1];

                    start = (linearIndex / sAxisStride * sPrevAxisStride) + (linearIndex % sAxisStride);
                }

                int end = start + (iterations * step);

                double sum = 0;

                for (int i = start; i < end; i += step)
                {
                    sum += ((IConvertible)sourceArray[i]).ToDouble(null);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)(sum != 0.0),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)sum).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)sum).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)sum).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)sum).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)sum).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)sum).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)sum).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)sum).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)sum).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)sum).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)sum).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Protected Methods
        // sIdx = ((idx / sStrides[axis]) * sStrides[(axis+1) % sStrides.Length]) + (idx % sStrides[axis])
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeSourceStartingLinearIndex(int linearIndex) {
            if (linearIndex < 0 || linearIndex >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(linearIndex));
            }

            if (axis == null)
            {
                return 0;
            }

            int sAxisStride = sourceArrayStrides[axis.Value];
            int sPrevAxisStride = axis.Value == 0 ? 1 : sourceArrayStrides[axis.Value - 1];

            return (linearIndex / sAxisStride * sPrevAxisStride) + (linearIndex % sAxisStride);
        }
        #endregion
        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new SumArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}