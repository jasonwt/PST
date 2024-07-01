namespace NumericArrays.Virtual.Facades {
    using System;
    using System.Diagnostics;

    public class ValueExpandingArray<TType> : VirtualArray<TType>
        where TType : struct, IConvertible {

        #region Private Fields
        protected readonly ValueType value;
        protected Type valueType;
        protected TypeCode valueTypeCode;
        #endregion

        #region Public Constructors
        public ValueExpandingArray(ValueType value, int[] arrayShape) :
            base(arrayShape) {

            this.value = value;
            valueType = value.GetType();
            valueTypeCode = Type.GetTypeCode(valueType);
        }
        public ValueExpandingArray(TType value, int[] arrayShape) :
            this((ValueType) value, arrayShape) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                if (ElementTypeCode != valueTypeCode) {
                    return ElementTypeCode switch {
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
                
                return (TType)value;
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }

        public override int MemoryUsage => base.MemoryUsage + valueTypeCode switch {
            TypeCode.Boolean => sizeof(bool),
            TypeCode.SByte => sizeof(sbyte),
            TypeCode.Byte => sizeof(byte),
            TypeCode.Int16 => sizeof(short),
            TypeCode.UInt16 => sizeof(ushort),
            TypeCode.Int32 => sizeof(int),
            TypeCode.UInt32 => sizeof(uint),
            TypeCode.Int64 => sizeof(long),
            TypeCode.UInt64 => sizeof(ulong),
            TypeCode.Single => sizeof(float),
            TypeCode.Double => sizeof(double),
            TypeCode.Decimal => sizeof(decimal),
            _ => throw new NotImplementedException()
        };
        #endregion

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ValueExpandingArray<TType>(value, Shape);
        #endregion

        #endregion
    }
}