namespace NumericArrays {
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static partial class NA {
        public static void Fill(this INumericArray thisArray, ValueType value, int inclusiveStartingIndex = 0, int exclusiveStartingIndex = 0, int? requestedThreads = null) {
            thisArray.ForEachElement((int i, INumericArray array) => array[i] = value, inclusiveStartingIndex, exclusiveStartingIndex, requestedThreads ?? 1);
        }
        public static void Fill(this INumericArray thisArray, ValueType value, int requestedThreads) {
            thisArray.ForEachElement((int i, INumericArray array) => array[i] = value, 0, 0, requestedThreads);
        }

        public static void Fill(this INumericArray thisArray, ValueType[] values, int? requestedThreads = null) {
            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length != thisArray.Length)
            {
                throw new ArgumentException("The values array must have the same length as the array being filled.");
            }

            thisArray.ForEachElement((int i, INumericArray array) => array[i] = values[i], 0, 0, requestedThreads ?? 1);
        }
        public static void Fill<TType>(this INumericArray thisArray, TType[] values, int? requestedThreads = null)
            where TType: struct {

            if (thisArray == null)
            {
                throw new ArgumentNullException(nameof(thisArray));
            }

            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values.Length != thisArray.Length)
            {
                throw new ArgumentException("The values array must have the same length as the array being filled.");
            }

            thisArray.ForEachElement((int i, INumericArray array) => array[i] = values[i], 0, 0, requestedThreads ?? 1);
        }

        public static void Fill(this INumericArray thisArray, Func<int, ValueType> valueGenerator, int inclusiveStartingIndex = 0, int exclusiveEndingIndex = 0, int? requestedThreads = null) {
            thisArray.ForEachElement((int i, INumericArray array) => array[i] = valueGenerator(i), inclusiveStartingIndex, exclusiveEndingIndex, requestedThreads ?? 1);
        }
        public static void Fill(this INumericArray thisArray, Func<int, ValueType> valueGenerator, int requestedThreads) {
            thisArray.ForEachElement((int i, INumericArray array) => array[i] = valueGenerator(i), 0, 0, requestedThreads);
        }

        public static void Fill(this INumericArray thisArray, INumericArray sourceArray) {
            if (thisArray == null)
                {
                throw new ArgumentNullException(nameof(thisArray));
            }

            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }

            if (sourceArray.Shape.SequenceEqual(thisArray.Shape) == false)
            {
                throw new ArgumentException("The source array must have the same shape as the array being filled.");
            }

            thisArray.ForEachElement((int i, INumericArray array) => array[i] = sourceArray[i], 0, 0, 1);
        }

        public static async Task FillAsync(this INumericArray thisArray, ValueType value, int inclusiveStartingIndex = 0, int exclusiveEndingIndex = 0, int? requestedThreads = null) {
            await thisArray.ForEachElementAsync((int i, INumericArray array) => array[i] = value, inclusiveStartingIndex, exclusiveEndingIndex, requestedThreads ?? 1);
        }
        public static async Task FillAsync(this INumericArray thisArray, ValueType value, int requestedThreads) {
            await thisArray.ForEachElementAsync((int i, INumericArray array) => array[i] = value, 0, 0, requestedThreads);
        }

        public static async Task FillAsync(this INumericArray thisArray, ValueType[] values, int? requestedThreads = null) {
            await thisArray.ForEachElementAsync((int i, INumericArray array) => array[i] = values[i], 0, 0, requestedThreads ?? 1);
        }

        public static async Task FillAsync(this INumericArray thisArray, Func<int, ValueType> valueGenerator, int inclusiveStartingIndex = 0, int exclusiveStartingIndex = 0, int? requestedThreads = null) {
            await thisArray.ForEachElementAsync((int i, INumericArray array) => array[i] = valueGenerator(i), inclusiveStartingIndex, exclusiveStartingIndex, requestedThreads ?? 1);
        }
        public static async Task FillAsync(this INumericArray thisArray, Func<int, ValueType> valueGenerator, int requestedThreads) {
            await thisArray.ForEachElementAsync((int i, INumericArray array) => array[i] = valueGenerator(i), 0, 0, requestedThreads);
        }
    }
}