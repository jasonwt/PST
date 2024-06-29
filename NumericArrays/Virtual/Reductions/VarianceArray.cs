namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class VarianceArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public VarianceArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public VarianceArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                double[] values = new double[step];
                double sum = 0;

                int start = linearIndex * step;
                int end = start + step;

                for (int i = start, j = 0; i < end; i++, j++)
                {
                    values[j] = ((IConvertible)sourceArray[i]).ToDouble(null);
                    sum += values[j];
                }

                double result = 0.0;
                double mean = sum / step;

                for (int i = 0; i < step; i++)
                {
                    double valueMinusMean = values[i] - mean;
                    result += (valueMinusMean * valueMinusMean);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((result != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)result).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)result).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)result).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)result).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)result).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)result).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)result).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)result).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)result).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)result).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)result).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new VarianceArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}