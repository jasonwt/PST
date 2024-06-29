namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class MinArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public MinArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public MinArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                int start = linearIndex * step;
                int end = start + step;

                double minValue = ((IConvertible)sourceArray[start]).ToDouble(null);

                for (int i = start+1; i < end; i++)
                {
                    double elementValue = ((IConvertible)sourceArray[i]).ToDouble(null);
                    minValue = elementValue < minValue ? elementValue : minValue;
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((minValue != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)minValue).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)minValue).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)minValue).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)minValue).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)minValue).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)minValue).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)minValue).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)minValue).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)minValue).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)minValue).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)minValue).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new MinArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}