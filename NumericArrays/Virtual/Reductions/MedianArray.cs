namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class MedianArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public MedianArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public MedianArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                double[] values = new double[step];

                int start = linearIndex * step;
                int end = start + step;

                for (int i = start, j = 0; i < end; i++, j ++)
                {
                    values[j] = ((IConvertible)sourceArray[i]).ToDouble(null);
                }

                Array.Sort(values);

                int offset = step / 2;

                double median = step % 2 == 0 ? (values[offset - 1] + values[offset]) / 2 : values[offset];

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((median != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)median).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)median).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)median).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)median).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)median).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)median).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)median).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)median).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)median).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)median).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)median).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new MedianArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}