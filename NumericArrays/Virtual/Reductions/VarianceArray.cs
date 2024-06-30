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

                double[] values = new double[iterations];
                double sum = 0;

                for (int i = start, j = 0; i < end; i += step, j ++)
                {
                    values[j] = ((IConvertible)sourceArray[i]).ToDouble(null);
                    sum += values[j];
                }

                double result = 0.0;
                double mean = sum / iterations;

                for (int i = 0; i < iterations; i++)
                {
                    double valueMinusMean = values[i] - mean;
                    result += (valueMinusMean * valueMinusMean);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((result != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)(result/iterations)).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)(result/iterations)).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)(result / iterations)).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)(result / iterations)).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)(result / iterations)).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)(result / iterations)).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)(result / iterations)).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)(result / iterations)).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)(result / iterations)).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)(result / iterations)).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)(result / iterations)).ToDecimal(null),
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