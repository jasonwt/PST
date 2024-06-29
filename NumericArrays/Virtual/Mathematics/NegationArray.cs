namespace NumericArrays.Virtual.Mathematics {
    using System;
    using System.Diagnostics;

    public class NegationArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public NegationArray(INumericArray sourceArray) :
            base(sourceArray ?? throw new ArgumentNullException(nameof(sourceArray)), sourceArray.Shape) {

            if (ElementTypeCode is TypeCode.Byte or TypeCode.UInt16 or TypeCode.UInt32 or TypeCode.UInt64)
            {
                throw new NotSupportedException($"Unsupported type: {ElementTypeCode}.  Unsigned types are not supported with the virtual NegationArray.");
            }
        }
        public NegationArray(INumericArray<TType> sourceArray) :
            this(sourceArray as INumericArray) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((!((IConvertible)sourceArray[linearIndex]).ToBoolean(null))),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToSByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)(-((IConvertible)sourceArray[linearIndex]).ToDouble(null))).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new NegationArray<TType>(sourceArray);
        #endregion
        #endregion
    }
}