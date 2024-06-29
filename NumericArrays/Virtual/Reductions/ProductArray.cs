namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class ProductArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public ProductArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {

        }
        public ProductArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                double product = 1;

                int start = linearIndex * step;
                int end = start + step;

                for (int i = start; i < end; i++)
                {
                    product *= ((IConvertible)sourceArray[i]).ToDouble(null);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((product != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)product).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)product).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)product).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)product).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)product).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)product).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)product).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)product).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)product).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)product).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)product).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ProductArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}