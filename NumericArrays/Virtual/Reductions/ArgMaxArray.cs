namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Diagnostics;

    public class ArgMaxArray<TType> : ReductionArray<TType>
        where TType : struct, IConvertible {

        #region Public Constructors
        public ArgMaxArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, axis) {
        }
        public ArgMaxArray(INumericArray<TType> leftArray, int? axis = null) :
            this(leftArray as INumericArray, axis) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length);

                int start = linearIndex * step;
                int end = start + step;

                double argMaxValue = ((IConvertible)sourceArray[start]).ToDouble(null);
                int argMaxIndex = start;

                for (int i = start; i < end; i++)
                {
                    double value = ((IConvertible)sourceArray[i]).ToDouble(null);

                    if (value > argMaxValue)
                    {
                        argMaxValue = value;
                        argMaxIndex = i;
                    }
                }

                return ElementTypeCode switch {
                    TypeCode.Boolean => (TType)(ValueType)(argMaxIndex != 0.0),
                    TypeCode.SByte => (TType)(ValueType)((IConvertible)argMaxIndex).ToSByte(null),
                    TypeCode.Byte => (TType)(ValueType)((IConvertible)argMaxIndex).ToByte(null),
                    TypeCode.Int16 => (TType)(ValueType)((IConvertible)argMaxIndex).ToInt16(null),
                    TypeCode.UInt16 => (TType)(ValueType)((IConvertible)argMaxIndex).ToUInt16(null),
                    TypeCode.Int32 => (TType)(ValueType)((IConvertible)argMaxIndex).ToInt32(null),
                    TypeCode.UInt32 => (TType)(ValueType)((IConvertible)argMaxIndex).ToUInt32(null),
                    TypeCode.Int64 => (TType)(ValueType)((IConvertible)argMaxIndex).ToInt64(null),
                    TypeCode.UInt64 => (TType)(ValueType)((IConvertible)argMaxIndex).ToUInt64(null),
                    TypeCode.Single => (TType)(ValueType)((IConvertible)argMaxIndex).ToSingle(null),
                    TypeCode.Double => (TType)(ValueType)((IConvertible)argMaxIndex).ToDouble(null),
                    TypeCode.Decimal => (TType)(ValueType)((IConvertible)argMaxIndex).ToDecimal(null),
                    _ => throw new NotImplementedException()
                };
            }
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Public Methods
        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ArgMaxArray<TType>(sourceArray, axis);
        #endregion
        #endregion
    }
}