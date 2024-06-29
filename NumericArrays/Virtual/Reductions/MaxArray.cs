namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class MaxArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public MaxArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public MaxArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                int start = linearIndex * step;
                int end = start + step;

                double maxValue = ((IConvertible)sourceArray[start]).ToDouble(null);

                for (int i = start + 1; i < end; i++)
                {
                    double elementValue = ((IConvertible)sourceArray[i]).ToDouble(null);
                    maxValue = elementValue > maxValue ? elementValue : maxValue;
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((maxValue != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)maxValue).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)maxValue).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)maxValue).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)maxValue).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)maxValue).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)maxValue).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)maxValue).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)maxValue).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)maxValue).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)maxValue).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)maxValue).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new MaxArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}