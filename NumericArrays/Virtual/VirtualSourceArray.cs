namespace NumericArrays.Virtual {
    using System;
    using System.Diagnostics;

    public abstract class VirtualSourceArray<TType> : VirtualArray<TType>, IVirtualArray<TType>
        where TType : struct, IConvertible {

        #region Protected Fields
        protected readonly INumericArray sourceArray;
        #endregion

        #region Protected Constructors
        protected VirtualSourceArray(INumericArray sourceArray, int[]? arrayShape = null) : 
            base(arrayShape ?? sourceArray?.Shape ?? throw new ArgumentNullException(nameof(sourceArray))) {

            this.sourceArray = sourceArray ?? throw new ArgumentNullException(nameof(sourceArray));
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex <= sourceArray.Length);
                
                if (ElementTypeCode != sourceArray.ElementTypeCode)
                {
                    return ElementTypeCode switch {
                        TypeCode.Boolean => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToBoolean(null),
                        TypeCode.SByte => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToSByte(null),
                        TypeCode.Byte => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToByte(null),
                        TypeCode.Int16 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToInt16(null),
                        TypeCode.UInt16 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToUInt16(null),
                        TypeCode.Int32 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToInt32(null),
                        TypeCode.UInt32 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToUInt32(null),
                        TypeCode.Int64 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToInt64(null),
                        TypeCode.UInt64 => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToUInt64(null),
                        TypeCode.Single => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToSingle(null),
                        TypeCode.Double => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToDouble(null),
                        TypeCode.Decimal => (TType)(ValueType)((IConvertible)sourceArray[linearIndex]).ToDecimal(null),
                        _ => throw new NotImplementedException()
                    };
                }

                return (TType) sourceArray[linearIndex];
            }
            set {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                if (ElementTypeCode != sourceArray.ElementTypeCode)
                {
                    sourceArray[linearIndex] = ElementTypeCode switch {
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

                    return;
                }

                sourceArray[linearIndex] = (TType) sourceArray[linearIndex];
            }
        }
        #endregion
    }
}