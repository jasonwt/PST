namespace NumericArrays {
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public static partial class NA {
        public static IEnumerable<int[]> LinearIndexIterator(this INumericArray array, int? axis = null) {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int[] sourceArrayStrides = array.Strides;
            int[] sourceArrayShape = array.Shape;

            int numLinearIndicies = axis == null ? 1 : array.Length / sourceArrayShape[axis.Value];
            int iterations = array.Length / numLinearIndicies;
            int step = axis == null ? 1 : sourceArrayStrides[axis.Value];

            for (int linearIndex = 0; linearIndex < numLinearIndicies; linearIndex ++)
            {
                int start = 0;
                if (axis != null)
                {
                    int sAxisStride = sourceArrayStrides[axis.Value];
                    int sPrevAxisStride = axis.Value == 0 ? 1 : sourceArrayStrides[axis.Value - 1];

                    start = (linearIndex / sAxisStride * sPrevAxisStride) + (linearIndex % sAxisStride);
                }

                int end = start + (iterations * step);

                int[] sourceArrayIndicies = new int[iterations];

                for (int i = start, j = 0; i < end; i += step, j ++)
                {
                    sourceArrayIndicies[j] = i;
                }

                yield return sourceArrayIndicies;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<ValueType[]> LinearValueIterator(this INumericArray array, int? axis = null) {
            return LinearIndexIterator(array, axis).Select(indices => {
                var values = new ValueType[indices.Length];
                for (int i = 0; i < indices.Length; i++)
                {
                    values[i] = array[indices[i]];
                }
                return values;
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<TType[]> LinearValueIterator<TType>(this INumericArray<TType> array, int? axis = null)
            where TType: struct, IConvertible {

            return LinearIndexIterator(array, axis).Select(indices => {
                var values = new TType[indices.Length];
                for (int i = 0; i < indices.Length; i++)
                {
                    values[i] = array[indices[i]];
                }
                return values;
            });
        }
    }
}