namespace NumericArrays {
    using System;

    using NumericArrays.Virtual.Facades;
    using NumericArrays.Virtual.Mathematics;

    public static partial class NA {
        #region Addition Methods (Add)
        public static INumericArray Add(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Add<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Add<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Add<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Add<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Add<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Add<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Add<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Add<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Add<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Add<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Add<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Add<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Add<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new AdditionArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Add<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new AdditionArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Add(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Add<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Add<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Add<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Add<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Add<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Add<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Add<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Add<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Add<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Add<float>(leftArray, value, toConcrete),
                TypeCode.Double => Add<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Add<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Add<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new AdditionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Add<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new AdditionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }

        #endregion

        #region Subtraction Methods (Subtract)
        public static INumericArray Subtract(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Subtract<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Subtract<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Subtract<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Subtract<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Subtract<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Subtract<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Subtract<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Subtract<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Subtract<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Subtract<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Subtract<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Subtract<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Subtract<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new SubtractionArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Subtract<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new SubtractionArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Subtract(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Subtract<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Subtract<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Subtract<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Subtract<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Subtract<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Subtract<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Subtract<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Subtract<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Subtract<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Subtract<float>(leftArray, value, toConcrete),
                TypeCode.Double => Subtract<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Subtract<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Subtract<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new SubtractionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Subtract<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new SubtractionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        #endregion

        #region Multiplication Methods (Multiply)
        public static INumericArray Multiply(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Multiply<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Multiply<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Multiply<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Multiply<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Multiply<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Multiply<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Multiply<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Multiply<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Multiply<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Multiply<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Multiply<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Multiply<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Multiply<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MultiplicationArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Multiply<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new MultiplicationArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Multiply(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Multiply<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Multiply<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Multiply<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Multiply<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Multiply<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Multiply<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Multiply<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Multiply<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Multiply<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Multiply<float>(leftArray, value, toConcrete),
                TypeCode.Double => Multiply<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Multiply<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Multiply<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new MultiplicationArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Multiply<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new MultiplicationArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        #endregion

        #region Division Methods (Divide)
        public static INumericArray Divide(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Divide<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Divide<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Divide<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Divide<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Divide<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Divide<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Divide<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Divide<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Divide<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Divide<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Divide<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Divide<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Divide<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new DivisionArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Divide<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new DivisionArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Divide(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Divide<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Divide<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Divide<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Divide<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Divide<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Divide<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Divide<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Divide<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Divide<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Divide<float>(leftArray, value, toConcrete),
                TypeCode.Double => Divide<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Divide<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Divide<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new DivisionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Divide<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new DivisionArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        #endregion

        #region Modulus Methods (Mod)
        public static INumericArray Mod(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Mod<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Mod<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Mod<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Mod<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Mod<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Mod<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Mod<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Mod<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Mod<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Mod<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Mod<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Mod<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Mod<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ModulusArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Mod<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new ModulusArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Mod(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Mod<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Mod<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Mod<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Mod<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Mod<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Mod<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Mod<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Mod<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Mod<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Mod<float>(leftArray, value, toConcrete),
                TypeCode.Double => Mod<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Mod<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Mod<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new ModulusArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Mod<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new ModulusArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        #endregion

        #region Power Methods (Pow)
        public static INumericArray Pow(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            if (rightArray == null)
            {
                throw new ArgumentNullException(nameof(rightArray));
            }

            return leftArray.ElementTypeCode switch {
                TypeCode.Boolean => Pow<bool>(leftArray, rightArray, toConcrete),
                TypeCode.SByte => Pow<sbyte>(leftArray, rightArray, toConcrete),
                TypeCode.Byte => Pow<byte>(leftArray, rightArray, toConcrete),
                TypeCode.Int16 => Pow<short>(leftArray, rightArray, toConcrete),
                TypeCode.UInt16 => Pow<ushort>(leftArray, rightArray, toConcrete),
                TypeCode.Int32 => Pow<int>(leftArray, rightArray, toConcrete),
                TypeCode.UInt32 => Pow<uint>(leftArray, rightArray, toConcrete),
                TypeCode.Int64 => Pow<long>(leftArray, rightArray, toConcrete),
                TypeCode.UInt64 => Pow<ulong>(leftArray, rightArray, toConcrete),
                TypeCode.Single => Pow<float>(leftArray, rightArray, toConcrete),
                TypeCode.Double => Pow<double>(leftArray, rightArray, toConcrete),
                TypeCode.Decimal => Pow<decimal>(leftArray, rightArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Pow<TResult>(this INumericArray leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new PowerArray<TResult>(leftArray, rightArray), toConcrete);
        public static INumericArray<TResult> Pow<TResult>(this INumericArray<TResult> leftArray, INumericArray rightArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new PowerArray<TResult>(leftArray, rightArray), toConcrete);

        public static INumericArray Pow(this INumericArray leftArray, ValueType value, bool? toConcrete = null) {
            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            return leftArray.ElementTypeCode switch
            {
                TypeCode.Boolean => Pow<bool>(leftArray, value, toConcrete),
                TypeCode.SByte => Pow<sbyte>(leftArray, value, toConcrete),
                TypeCode.Byte => Pow<byte>(leftArray, value, toConcrete),
                TypeCode.Int16 => Pow<short>(leftArray, value, toConcrete),
                TypeCode.UInt16 => Pow<ushort>(leftArray, value, toConcrete),
                TypeCode.Int32 => Pow<int>(leftArray, value, toConcrete),
                TypeCode.UInt32 => Pow<uint>(leftArray, value, toConcrete),
                TypeCode.Int64 => Pow<long>(leftArray, value, toConcrete),
                TypeCode.UInt64 => Pow<ulong>(leftArray, value, toConcrete),
                TypeCode.Single => Pow<float>(leftArray, value, toConcrete),
                TypeCode.Double => Pow<double>(leftArray, value, toConcrete),
                TypeCode.Decimal => Pow<decimal>(leftArray, value, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Pow<TResult>(this INumericArray leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new PowerArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        public static INumericArray<TResult> Pow<TResult>(this INumericArray<TResult> leftArray, ValueType value, bool? toConcrete = null)
            where TResult : struct, IConvertible {

            if (leftArray == null)
            {
                throw new ArgumentNullException(nameof(leftArray));
            }

            var valueExpandingArray = new ValueExpandingArray<TResult>(value, leftArray.Shape);

            return ConstructVirtualArray(new PowerArray<TResult>(leftArray, valueExpandingArray), toConcrete);
        }
        #endregion

        #region Absolute Value Methods (Abs)
        public static INumericArray Abs(this INumericArray sourceArray, bool? toConcrete = null) {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }

            return sourceArray.ElementTypeCode switch {
                TypeCode.Boolean => Abs<bool>(sourceArray, toConcrete),
                TypeCode.SByte => Abs<sbyte>(sourceArray, toConcrete),
                TypeCode.Byte => Abs<byte>(sourceArray, toConcrete),
                TypeCode.Int16 => Abs<short>(sourceArray, toConcrete),
                TypeCode.UInt16 => Abs<ushort>(sourceArray, toConcrete),
                TypeCode.Int32 => Abs<int>(sourceArray, toConcrete),
                TypeCode.UInt32 => Abs<uint>(sourceArray, toConcrete),
                TypeCode.Int64 => Abs<long>(sourceArray, toConcrete),
                TypeCode.UInt64 => Abs<ulong>(sourceArray, toConcrete),
                TypeCode.Single => Abs<float>(sourceArray, toConcrete),
                TypeCode.Double => Abs<double>(sourceArray, toConcrete),
                TypeCode.Decimal => Abs<decimal>(sourceArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TResult> Abs<TResult>(this INumericArray sourceArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new AbsoluteValueArray<TResult>(sourceArray), toConcrete);
        public static INumericArray<TResult> Abs<TResult>(this INumericArray<TResult> sourceArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new AbsoluteValueArray<TResult>(sourceArray), toConcrete);
        #endregion

        #region Negation Methods (Negate)
        public static INumericArray Negate(this INumericArray sourceArray, bool? toConcrete = null) {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }

            return sourceArray.ElementTypeCode switch {
                TypeCode.Boolean => Negate<bool>(sourceArray, toConcrete),
                TypeCode.SByte => Negate<sbyte>(sourceArray, toConcrete),
                TypeCode.Byte => Negate<short>(sourceArray, toConcrete),
                TypeCode.Int16 => Negate<short>(sourceArray, toConcrete),
                TypeCode.UInt16 => Negate<int>(sourceArray, toConcrete),
                TypeCode.Int32 => Negate<int>(sourceArray, toConcrete),
                TypeCode.UInt32 => Negate<long>(sourceArray, toConcrete),
                TypeCode.Int64 => Negate<long>(sourceArray, toConcrete),
                TypeCode.UInt64 => Negate<long>(sourceArray, toConcrete),
                TypeCode.Single => Negate<float>(sourceArray, toConcrete),
                TypeCode.Double => Negate<double>(sourceArray, toConcrete),
                TypeCode.Decimal => Negate<decimal>(sourceArray, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<short> Negate(this INumericArray<byte> sourceArray, bool? toConcrete = null) {
            return ConstructVirtualArray(new NegationArray<short>(sourceArray), toConcrete);
        }
        public static INumericArray<int> Negate(this INumericArray<ushort> sourceArray, bool? toConcrete = null) {
            return ConstructVirtualArray(new NegationArray<int>(sourceArray), toConcrete);
        }
        public static INumericArray<long> Negate(this INumericArray<uint> sourceArray, bool? toConcrete = null) {
            return ConstructVirtualArray(new NegationArray<long>(sourceArray), toConcrete);
        }
        public static INumericArray<long> Negate(this INumericArray<ulong> sourceArray, bool? toConcrete = null) {
            return ConstructVirtualArray(new NegationArray<long>(sourceArray), toConcrete);
        }
        public static INumericArray<TResult> Negate<TResult>(this INumericArray sourceArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new NegationArray<TResult>(sourceArray), toConcrete);
        public static INumericArray<TResult> Negate<TResult>(this INumericArray<TResult> sourceArray, bool? toConcrete = null)
            where TResult : struct, IConvertible => ConstructVirtualArray(new NegationArray<TResult>(sourceArray), toConcrete);
        #endregion
    }
}