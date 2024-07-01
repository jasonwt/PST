namespace NumericArrays.Virtual.Mathematics {
    using System;
    using System.Diagnostics;

    public class DivisionArray<TType> : MathematicsArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public DivisionArray(INumericArray leftArray, INumericArray rightArray) :
            base(leftArray, rightArray) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex <= Length);

                double leftValue = ((IConvertible)sourceArray[!isLeftArrayBroadcasting ? linearIndex : ComputeLeftArrayLinearIndex(linearIndex)]).ToDouble(null);
                double rightValue = ((IConvertible)rightArray[!isRightArrayBroadcasting ? linearIndex : ComputeRightArrayLinearIndex(linearIndex)]).ToDouble(null);

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((leftValue / rightValue) != 0),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)(leftValue / rightValue)).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new DivisionArray<TType>(sourceArray, rightArray);
        #endregion
        #endregion
    }
}