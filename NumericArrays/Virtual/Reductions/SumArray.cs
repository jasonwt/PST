namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class SumArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public SumArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {

        }
        public SumArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                double sum = 0;

                int start = linearIndex * step;
                int end = start + step;

                for (int i = start; i < end; i++)
                {
                    sum += ((IConvertible)sourceArray[i]).ToDouble(null);
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((sum != 0.0)),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)sum).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)sum).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)sum).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)sum).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)sum).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)sum).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)sum).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)sum).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)sum).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)sum).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)sum).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new SumArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}