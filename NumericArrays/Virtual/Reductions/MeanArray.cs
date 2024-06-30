namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class MeanArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public MeanArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {

        }
        public MeanArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                double sum = 0;

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

                for (int i = start; i < end; i += step)
                {
                    sum += ((IConvertible)sourceArray[i]).ToDouble(null);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)(((sum / iterations) != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)(sum/ iterations)).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)(sum / iterations)).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)(sum / iterations)).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)(sum / iterations)).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)(sum / iterations)).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)(sum / iterations)).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new MeanArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}