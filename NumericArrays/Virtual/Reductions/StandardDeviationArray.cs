namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class StandardDeviationArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public StandardDeviationArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public StandardDeviationArray(INumericArray<TType> leftArray, int? axis = null) :
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

                double mean = sum / step;

                double squared_deviations = 0;
                for (int i = 0; i < step; i++)
                {
                    double deviation = values[i] - mean;
                    squared_deviations += (deviation * deviation);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((Math.Sqrt(squared_deviations / step) != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)Math.Sqrt(squared_deviations / step)).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new StandardDeviationArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}