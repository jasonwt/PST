namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class ArgMinArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public ArgMinArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public ArgMinArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                int start = linearIndex * step;
                int end = start + step;

                double argMinValue = ((IConvertible)sourceArray[start]).ToDouble(null);
                int argMinIndex = start;

                for (int i = start; i < end; i++)
                {
                    double value = ((IConvertible)sourceArray[i]).ToDouble(null);

                    if (value < argMinValue)
                    {
                        argMinValue = value;
                        argMinIndex = i;
                    }
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)((IConvertible)argMinIndex).ToSByte(null),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)argMinIndex).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)argMinIndex).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)argMinIndex).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)argMinIndex).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)argMinIndex).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)argMinIndex).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)argMinIndex).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)argMinIndex).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)argMinIndex).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)argMinIndex).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)argMinIndex).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ArgMinArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}