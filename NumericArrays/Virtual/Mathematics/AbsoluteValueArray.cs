namespace NumericArrays.Virtual.Mathematics {
    using System;
    using System.Diagnostics;

    public class AbsoluteValueArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public AbsoluteValueArray(INumericArray sourceArray) :
            base(sourceArray ?? throw new ArgumentNullException(nameof(sourceArray)), sourceArray.Shape) {
        }
        public AbsoluteValueArray(INumericArray<TType> sourceArray) :
            this(sourceArray as INumericArray) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                double value = ((IConvertible)sourceArray[linearIndex]).ToDouble(null);

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToBoolean(null),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)Math.Abs(value)).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new AbsoluteValueArray<TType>(sourceArray);
        #endregion
        #endregion
    }
}