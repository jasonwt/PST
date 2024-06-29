namespace NumericArrays {
    using System;
    using System.Collections.Generic;

    public static partial class NA {
        public static IEnumerable<int[]> LinearIndexIterator(this INumericArray array, int? axis = null) {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int step = axis == null ? array.Length : array.Shape[axis.Value];
            int length = array.Length;

            int[] indices = new int[step];

            for (int i = 0; i < length; i += step)
            {
                for (int j = 0; j < step; j++)
                {
                    indices[j] = i + j;
                }

                yield return indices;
            }
        }

        public static IEnumerable<ValueType[]> LinearValueIterator(this INumericArray array, int? axis = null) {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int step = axis == null ? array.Length : array.Shape[axis.Value];
            int length = array.Length / step;

            ValueType[] values = new ValueType[step];

            for (int i = 0; i < length; i += step)
            {
                for (int j = 0; j < step; j++)
                {
                    values[j] = array[i + j];
                }

                yield return values;
            }
        }

        public static IEnumerable<TType[]> LinearValueIterator<TType>(this INumericArray<TType> array, int? axis = null)
            where TType: struct, IConvertible {

            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int step = axis == null ? array.Length : array.Shape[axis.Value];
            int length = array.Length / step;

            TType[] values = new TType[step];

            for (int i = 0; i < length; i += step)
            {
                for (int j = 0; j < step; j++)
                {
                    values[j] = array[i + j];
                }

                yield return values;
            }
        }
    }
}