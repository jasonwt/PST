namespace NumericArrays.Virtual.Mathematics {
    using System;
    using System.Diagnostics;

    public class PowerArray<TType> : MathematicsArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public PowerArray(INumericArray leftArray, INumericArray rightArray) :
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
                    TypeCode.Boolean => (TType)(ValueType)((Math.Pow(leftValue,rightValue)) != 0),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)(Math.Pow(leftValue, rightValue))).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new PowerArray<TType>(sourceArray, rightArray);
        #endregion
        #endregion
    }
}