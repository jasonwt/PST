namespace NumericArrays {
    using System;

    using NumericArrays.Virtual.Reductions;

    public static partial class NA {
        #region Reduction Methods

        #region Sum Methods (Sum)
        public static INumericArray Sum(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Sum<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Sum<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Sum<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Sum<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Sum<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Sum<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Sum<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Sum<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Sum<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Sum<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Sum<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Sum<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Sum<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            return ConstructVirtualArray(new SumArray<TResult>(thisArray, axis), toConcrete);
        }
        public static INumericArray<TResult> Sum<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            return ConstructVirtualArray(new SumArray<TResult>(thisArray, axis), toConcrete);
        }
        #endregion

        #region Product Methods (Prod)
        public static INumericArray Prod(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Prod<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Prod<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Prod<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Prod<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Prod<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Prod<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Prod<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Prod<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Prod<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Prod<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Prod<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Prod<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Prod<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ProductArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Prod<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ProductArray<TResult>(thisArray, axis), toConcrete);
        #endregion 

        #region Mean Methods (Mean)
        public static INumericArray Mean(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Mean<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Mean<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Mean<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Mean<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Mean<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Mean<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Mean<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Mean<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Mean<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Mean<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Mean<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Mean<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Mean<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MeanArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Mean<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MeanArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region Min Methods (Min)
        public static INumericArray Min(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Min<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Min<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Min<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Min<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Min<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Min<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Min<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Min<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Min<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Min<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Min<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Min<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Min<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MinArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Min<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MinArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region Max Methods (Max)
        public static INumericArray Max(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Max<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Max<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Max<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Max<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Max<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Max<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Max<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Max<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Max<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Max<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Max<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Max<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Max<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MaxArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Max<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MaxArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region Median Methods (Median)
        public static INumericArray Median(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Median<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Median<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Median<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Median<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Median<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Median<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Median<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Median<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Median<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Median<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Median<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Median<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Median<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MedianArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Median<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MedianArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region Variance Methods (Var)
        public static INumericArray Var(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Var<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Var<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Var<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Var<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Var<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Var<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Var<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Var<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Var<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Var<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Var<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Var<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Var<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new VarianceArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Var<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new VarianceArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region Standard Deviation Methods (Std)
        public static INumericArray Std(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => Std<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => Std<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => Std<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => Std<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => Std<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => Std<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => Std<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => Std<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => Std<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => Std<float>(thisArray, axis, toConcrete),
                TypeCode.Double => Std<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => Std<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Std<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new StandardDeviationArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> Std<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new StandardDeviationArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region ArgMax Methods (ArgMax)
        public static INumericArray ArgMax(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => ArgMax<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => ArgMax<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => ArgMax<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => ArgMax<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => ArgMax<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => ArgMax<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => ArgMax<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => ArgMax<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => ArgMax<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => ArgMax<float>(thisArray, axis, toConcrete),
                TypeCode.Double => ArgMax<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => ArgMax<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> ArgMax<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ArgMaxArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> ArgMax<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ArgMaxArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #region ArgMin Methods (ArgMin)
        public static INumericArray ArgMin(this INumericArray thisArray, int? axis = null, bool? toConcrete = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            return thisArray.ElementTypeCode switch {
                TypeCode.Boolean => ArgMin<bool>(thisArray, axis, toConcrete),
                TypeCode.SByte => ArgMin<sbyte>(thisArray, axis, toConcrete),
                TypeCode.Byte => ArgMin<byte>(thisArray, axis, toConcrete),
                TypeCode.Int16 => ArgMin<short>(thisArray, axis, toConcrete),
                TypeCode.UInt16 => ArgMin<ushort>(thisArray, axis, toConcrete),
                TypeCode.Int32 => ArgMin<int>(thisArray, axis, toConcrete),
                TypeCode.UInt32 => ArgMin<uint>(thisArray, axis, toConcrete),
                TypeCode.Int64 => ArgMin<long>(thisArray, axis, toConcrete),
                TypeCode.UInt64 => ArgMin<ulong>(thisArray, axis, toConcrete),
                TypeCode.Single => ArgMin<float>(thisArray, axis, toConcrete),
                TypeCode.Double => ArgMin<double>(thisArray, axis, toConcrete),
                TypeCode.Decimal => ArgMin<decimal>(thisArray, axis, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> ArgMin<TResult>(this INumericArray thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ArgMinArray<TResult>(thisArray, axis), toConcrete);
        public static INumericArray<TResult> ArgMin<TResult>(this INumericArray<TResult> thisArray, int? axis = null, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ArgMinArray<TResult>(thisArray, axis), toConcrete);
        #endregion

        #endregion
    }
}