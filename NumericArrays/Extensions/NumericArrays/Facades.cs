namespace NumericArrays {
    using System;
    using System.Linq;

    using NumericArrays.Virtual.Facades;

    public static partial class NA {
        #region AsType Methods
        public static INumericArray<TType> AsType<TType>(this INumericArray sourceArray, bool? toConcrete = null) 
            where TType : struct, IConvertible {

            return ConstructVirtualArray(new ChangeTypeArray<TType>(sourceArray), toConcrete);
        }
        public static INumericArray<TType> AsType<TType>(this INumericArray<TType> sourceArray, bool? toConcrete = null)
            where TType : struct, IConvertible => sourceArray;
        #endregion

        #region BroadcastTo Methods
        public static INumericArray BroadcastTo(this INumericArray sourceArray, int[] broadcastToShape, bool? toConcrete = null) {
            if (sourceArray == null)
            {
               throw new ArgumentNullException(nameof(sourceArray));
            }

            return sourceArray.ElementTypeCode switch {
                TypeCode.Boolean => BroadcastTo<bool>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.SByte => BroadcastTo<sbyte>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Byte => BroadcastTo<byte>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Int16 => BroadcastTo<short>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.UInt16 => BroadcastTo<ushort>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Int32 => BroadcastTo<int>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.UInt32 => BroadcastTo<uint>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Int64 => BroadcastTo<long>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.UInt64 => BroadcastTo<ulong>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Single => BroadcastTo<float>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Double => BroadcastTo<double>(sourceArray, broadcastToShape, toConcrete),
                TypeCode.Decimal => BroadcastTo<decimal>(sourceArray, broadcastToShape, toConcrete),
                _ => throw new NotImplementedException()
            };
        }
        public static INumericArray<TType> BroadcastTo<TType>(this INumericArray<TType> sourceArray, int[] broadcastToShape, bool? toConcrete = null)
            where TType : struct, IConvertible => ConstructVirtualArray(new BroadcastingArray<TType>(sourceArray, broadcastToShape), toConcrete);

        public static INumericArray<TType> BroadcastTo<TType>(this INumericArray sourceArray, int[] broadcastToShape, bool? toConcrete = null)
            where TType : struct, IConvertible => ConstructVirtualArray(new BroadcastingArray<TType>(sourceArray, broadcastToShape), toConcrete);
        #endregion

        #region Slice Methods
        public static INumericArray<TType> Slice<TType>(this INumericArray sourceArray, string slicingMask, bool? toConcrete = null)
            where TType : struct, IConvertible {

            return ConstructVirtualArray(new SlicingArray<TType>(sourceArray, slicingMask), toConcrete);
        }
        public static INumericArray<TType> Slice<TType>(this INumericArray<TType> sourceArray, string slicingMask, bool? toConcrete = null)
            where TType : struct, IConvertible => Slice<TType>(sourceArray as INumericArray, slicingMask, toConcrete);
        #endregion

        #region Transpose Methods
        public static INumericArray<TType> Transpose<TType>(this INumericArray sourceArray, int[] transposedAxises, bool? toConcrete = null)
            where TType : struct, IConvertible {

            return ConstructVirtualArray(new TransposingArray<TType>(sourceArray, transposedAxises), toConcrete);
        }
        public static INumericArray<TType> Transpose<TType>(this INumericArray<TType> sourceArray, int[] transposedAxises, bool? toConcrete = null)
            where TType : struct, IConvertible => Transpose<TType>(sourceArray as INumericArray, transposedAxises, toConcrete);
        #endregion

        #region SwapAxis Methods
        public static INumericArray<TType> SwapAxis<TType>(this INumericArray sourceArray, int axis1, int axis2, bool? toConcrete = null)
            where TType : struct, IConvertible {

            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }

            int[] axisIndices = Enumerable.Range(0, sourceArray.Rank).ToArray();
            axisIndices[axis1] = axis2;
            axisIndices[axis2] = axis1;

            return Transpose<TType>(sourceArray, axisIndices, toConcrete);
        }
        public static INumericArray<TType> SwapAxis<TType>(this INumericArray<TType> sourceArray, int axis1, int axis2, bool? toConcrete = null)
            where TType : struct, IConvertible => SwapAxis<TType>(sourceArray as INumericArray, axis1, axis2, toConcrete);
        #endregion

        #region View Methods
        public static INumericArray<TType> View<TType>(this INumericArray sourceArray, int[]? viewOffset = null, int[]? viewShape = null, bool? toConcrete = null)
            where TType : struct, IConvertible {

            return ConstructVirtualArray(new ViewArray<TType>(sourceArray, viewOffset, viewShape), toConcrete);
        }
        public static INumericArray<TType> View<TType>(this INumericArray<TType> sourceArray, int[]? viewOffset = null, int[]? viewShape = null, bool? toConcrete = null)
            where TType : struct, IConvertible => View<TType>(sourceArray as INumericArray, viewOffset, viewShape, toConcrete);
        public static INumericArray<TType> View<TType>(this INumericArray sourceArray, bool toConcrete)
            where TType : struct, IConvertible => View<TType>(sourceArray, null, null, toConcrete);
        public static INumericArray<TType> View<TType>(this INumericArray<TType> sourceArray, bool toConcrete)
            where TType : struct, IConvertible => View<TType>(sourceArray, null, null, toConcrete);
        #endregion
    }
}