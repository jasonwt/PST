namespace NumericArrays.Extensions {
    using System;
    using System.Runtime.CompilerServices;

    public static class ValueTypes {
        #region Is Methods

        #region IsSignedInteger
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSignedInteger(this ValueType value) {
            return Type.GetTypeCode(value.GetType()) switch {
                TypeCode.SByte => true,
                TypeCode.Int16 => true,
                TypeCode.Int32 => true,
                TypeCode.Int64 => true,
                _ => false
            };
        }
        #endregion

        #region IsUnsignedInteger
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsUnsignedInteger(this ValueType value) {
            return Type.GetTypeCode(value.GetType()) switch {
                TypeCode.Byte => true,
                TypeCode.UInt16 => true,
                TypeCode.UInt32 => true,
                TypeCode.UInt64 => true,
                _ => false
            };
        }
        #endregion

        #region IsInteger
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInteger(this ValueType value) {
            return value.IsSignedInteger() || value.IsUnsignedInteger();
        }
        #endregion

        #region IsFloatingPoint
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFloatingPoint(this ValueType value) {
            return Type.GetTypeCode(value.GetType()) switch {
                TypeCode.Single => true,
                TypeCode.Double => true,
                TypeCode.Decimal => true,
                _ => false
            };
        }
        #endregion

        #region IsNumeric
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNumeric(this ValueType value) {
            return value.IsInteger() || value.IsFloatingPoint();
        }
        #endregion

        #endregion

        #region To Type

        #region ToBoolean
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ToBoolean<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToBoolean(null);
        #endregion

        #region ToSByte
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ToSByte<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToSByte(null);
        #endregion

        #region ToByte
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToByte<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToByte(null);
        #endregion

        #region ToInt16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ToInt16<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToInt16(null);
        #endregion

        #region ToUInt16
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ToUInt16<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToUInt16(null);
        #endregion

        #region ToInt32
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToInt32(null);
        #endregion

        #region ToUInt32
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ToUInt32<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToUInt32(null);
        #endregion

        #region ToInt64
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ToInt64<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToInt64(null);
        #endregion

        #region ToUInt64
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ToUInt64<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToUInt64(null);
        #endregion

        #region ToSingle
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToSingle<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToSingle(null);
        #endregion

        #region ToDouble
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ToDouble<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToDouble(null);
        #endregion

        #region ToDecimal
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static decimal ToDecimal<TValue>(this TValue value) where TValue : IConvertible => ((IConvertible)value).ToDecimal(null);
        #endregion

        #endregion Methods

        #region Change Type
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TTo ChangeType<TFrom, TTo>(this TFrom value)
            where TFrom : struct, IConvertible
            where TTo : struct {

            return Type.GetTypeCode(typeof(TTo)) switch {
                TypeCode.Boolean => (TTo)(ValueType)((IConvertible)value).ToBoolean(null),
                TypeCode.SByte => (TTo)(ValueType)((IConvertible)value).ToSByte(null),
                TypeCode.Byte => (TTo)(ValueType)((IConvertible)value).ToByte(null),
                TypeCode.Int16 => (TTo)(ValueType)((IConvertible)value).ToInt16(null),
                TypeCode.UInt16 => (TTo)(ValueType)((IConvertible)value).ToUInt16(null),
                TypeCode.Int32 => (TTo)(ValueType)((IConvertible)value).ToInt32(null),
                TypeCode.UInt32 => (TTo)(ValueType)((IConvertible)value).ToUInt32(null),
                TypeCode.Int64 => (TTo)(ValueType)((IConvertible)value).ToInt64(null),
                TypeCode.UInt64 => (TTo)(ValueType)((IConvertible)value).ToUInt64(null),
                TypeCode.Single => (TTo)(ValueType)((IConvertible)value).ToSingle(null),
                TypeCode.Double => (TTo)(ValueType)((IConvertible)value).ToDouble(null),
                TypeCode.Decimal => (TTo)(ValueType)((IConvertible)value).ToDecimal(null),
                _ => (TTo)Convert.ChangeType(value, typeof(TTo))
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueType ChangeType<TFrom>(this TFrom value, Type toType)
            where TFrom : struct {

            return Type.GetTypeCode(toType) switch {
                TypeCode.Boolean => ((IConvertible)value).ToBoolean(null),
                TypeCode.SByte => ((IConvertible)value).ToSByte(null),
                TypeCode.Byte => ((IConvertible)value).ToByte(null),
                TypeCode.Int16 => ((IConvertible)value).ToInt16(null),
                TypeCode.UInt16 => ((IConvertible)value).ToUInt16(null),
                TypeCode.Int32 => ((IConvertible)value).ToInt32(null),
                TypeCode.UInt32 => ((IConvertible)value).ToUInt32(null),
                TypeCode.Int64 => ((IConvertible)value).ToInt64(null),
                TypeCode.UInt64 => ((IConvertible)value).ToUInt64(null),
                TypeCode.Single => ((IConvertible)value).ToSingle(null),
                TypeCode.Double => ((IConvertible)value).ToDouble(null),
                TypeCode.Decimal => ((IConvertible)value).ToDecimal(null),
                _ => (ValueType)Convert.ChangeType(value, toType)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ValueType ChangeType(this ValueType value, Type toType) {

            return Type.GetTypeCode(toType) switch {
                TypeCode.Boolean => ((IConvertible)value).ToBoolean(null),
                TypeCode.SByte => ((IConvertible)value).ToSByte(null),
                TypeCode.Byte => ((IConvertible)value).ToByte(null),
                TypeCode.Int16 => ((IConvertible)value).ToInt16(null),
                TypeCode.UInt16 => ((IConvertible)value).ToUInt16(null),
                TypeCode.Int32 => ((IConvertible)value).ToInt32(null),
                TypeCode.UInt32 => ((IConvertible)value).ToUInt32(null),
                TypeCode.Int64 => ((IConvertible)value).ToInt64(null),
                TypeCode.UInt64 => ((IConvertible)value).ToUInt64(null),
                TypeCode.Single => ((IConvertible)value).ToSingle(null),
                TypeCode.Double => ((IConvertible)value).ToDouble(null),
                TypeCode.Decimal => ((IConvertible)value).ToDecimal(null),
                _ => (ValueType)Convert.ChangeType(value, toType)
            };
        }

        #endregion Methods

        #region Arithmetic Methods

        #region Addition
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Add<TResult, TLeft, TRight>(this TLeft left, TRight right)
            where TResult : struct, IConvertible
            where TLeft : struct, IConvertible
            where TRight : struct, IConvertible {

            return ChangeType<double, TResult>(left.ToDouble() + right.ToDouble());
        }
        #endregion

        #region Subtraction
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Subtract<TResult, TLeft, TRight>(this TLeft left, TRight right)
            where TResult : struct, IConvertible
            where TLeft : struct, IConvertible
            where TRight : struct, IConvertible {

            return ChangeType<double, TResult>(left.ToDouble() - right.ToDouble());
        }
        #endregion

        #region Multiplication
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Multiply<TResult, TLeft, TRight>(this TLeft left, TRight right)
            where TResult : struct, IConvertible
            where TLeft : struct, IConvertible
            where TRight : struct, IConvertible {

            return ChangeType<double, TResult>(left.ToDouble() * right.ToDouble());
        }
        #endregion

        #region Division
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResult Divide<TResult, TLeft, TRight>(this TLeft left, TRight right)
            where TResult : struct, IConvertible
            where TLeft : struct, IConvertible
            where TRight : struct, IConvertible {

            return ChangeType<double, TResult>(left.ToDouble() / right.ToDouble());
        }
        #endregion

        #endregion
    }
}