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