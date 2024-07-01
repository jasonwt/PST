namespace NumericArrays {
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static partial class NA {
        #region Linear Index
        private static void ValidateForEachElementParameters(INumericArray numericArray, int inclusiveStartingLinearIndex, int exclusiveEndingLinearIndex) {
            if (numericArray == null)
            {
                throw new ArgumentNullException(nameof(numericArray));
            }

            if (exclusiveEndingLinearIndex == 0)
            {
                exclusiveEndingLinearIndex = numericArray.Length;
            }

            if (inclusiveStartingLinearIndex < 0 || inclusiveStartingLinearIndex > numericArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(inclusiveStartingLinearIndex));
            }

            if (exclusiveEndingLinearIndex < 0 || exclusiveEndingLinearIndex > numericArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(exclusiveEndingLinearIndex));
            }

            if (inclusiveStartingLinearIndex == exclusiveEndingLinearIndex)
            {
                throw new ArgumentException("inclusiveStartingLinearIndex must not equal exclusiveEndingLinearIndex");
            }
        }
        private static void ValidateForEachElementParameters(INumericArray numericArray, Action<int, INumericArray> action, int inclusiveStartingLinearIndex, int exclusiveEndingLinearIndex) {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            ValidateForEachElementParameters(numericArray, inclusiveStartingLinearIndex, exclusiveEndingLinearIndex);
        }

        public static void ForEachElement(this INumericArray thisArray, Action<int, INumericArray> action, int inclusiveStartingLinearIndex = 0, int exclusiveEndingLinearIndex = 0, int requestedThreads = 1) {
            ValidateForEachElementParameters(thisArray, action, inclusiveStartingLinearIndex, exclusiveEndingLinearIndex);

            exclusiveEndingLinearIndex = exclusiveEndingLinearIndex == 0 ? thisArray.Length : exclusiveEndingLinearIndex;

            int step = inclusiveStartingLinearIndex < exclusiveEndingLinearIndex ? 1 : -1;

            int totalIterations = step > 0 ?
                exclusiveEndingLinearIndex - inclusiveStartingLinearIndex :
                inclusiveStartingLinearIndex - exclusiveEndingLinearIndex;

            int numThreads = requestedThreads <= 0 ? Environment.ProcessorCount : requestedThreads;
            numThreads = totalIterations < requestedThreads ? totalIterations : requestedThreads;

            if (numThreads == 1)
            {
                for (int i = inclusiveStartingLinearIndex; i != exclusiveEndingLinearIndex; i += step)
                {
                    action(i, thisArray);
                }
                return;
            }

            var tasks = new Task[numThreads];

            int iterationsPerThread = totalIterations / numThreads;

            for (int i = 0; i < numThreads; i++)
            {
                int startingIndex = inclusiveStartingLinearIndex + (i * iterationsPerThread * step);
                int endingIndex = (i == numThreads - 1) ? exclusiveEndingLinearIndex : startingIndex + (iterationsPerThread * step);

                tasks[i] = Task.Run(() => {
                    for (int j = startingIndex; j != endingIndex; j += step)
                    {
                        action(j, thisArray);
                    }
                });
            }

            Task.WaitAll(tasks);
        }
        public static void ForEachElement(this INumericArray thisArray, Action<int, INumericArray> action, int requestedThreads) =>
            thisArray.ForEachElement(action, 0, 0, requestedThreads);

        public static async Task ForEachElementAsync(this INumericArray thisArray, Action<int, INumericArray> action, int inclusiveStartingLinearIndex = 0, int exclusiveEndingLinearIndex = 0, int requestedThreads = 1) =>
            await Task.Run(() => ForEachElement(thisArray, action, inclusiveStartingLinearIndex, exclusiveEndingLinearIndex, requestedThreads));
        public static async Task ForEachElementAsync(this INumericArray thisArray, Action<int, INumericArray> action, int requestedThreads) =>
            await Task.Run(() => ForEachElement(thisArray, action, 0, 0, requestedThreads));
        #endregion

        //#region Dimensional Index
        //private static void ValidateForEachElementParameters(INumericArray numericArray, int[]? inclusiveStartingDimensionalIndex, int[]? exclusiveEndingDimensionalIndex) {
        //    if (numericArray == null)
        //    {
        //        throw new ArgumentNullException(nameof(numericArray));
        //    }

        //    inclusiveStartingDimensionalIndex ??= new int[numericArray.Rank];
        //    exclusiveEndingDimensionalIndex ??= numericArray.Shape;

        //    if (inclusiveStartingDimensionalIndex.Any(i => i < 0))
        //    {
        //        throw new ArgumentOutOfRangeException(nameof(inclusiveStartingDimensionalIndex));
        //    }

        //    if (exclusiveEndingDimensionalIndex.Zip(numericArray.Shape, (end, shape) => end > shape).Any(b => b == true))
        //    {
        //        throw new ArgumentException("exclusiveEndingDimensionalIndex must be less than or equal to the shape of the array for all dimensions");
        //    }

        //    if (inclusiveStartingDimensionalIndex.Zip(exclusiveEndingDimensionalIndex, (start, end) => start >= end).Any(b => b == true))
        //    {
        //        throw new ArgumentException("inclusiveStartingDimensionalIndex must be less than exclusiveEndingDimensionalIndex for all dimensions");
        //    }
        //}
        //private static void ValidateForEachElementParameters(INumericArray numericArray, Action<int[], INumericArray> action, int[]? inclusiveStartingDimensionalIndex, int[]? exclusiveEndingDimensionalIndex) {
        //    if (action == null)
        //    {
        //        throw new ArgumentNullException(nameof(action));
        //    }

        //    ValidateForEachElementParameters(numericArray, inclusiveStartingDimensionalIndex, exclusiveEndingDimensionalIndex);
        //}

        //public static void ForEachElement(this INumericArray thisArray, Action<int[], INumericArray> action, int[]? inclusiveStartingDimensionalIndex = null, int[]? exclusiveEndingDimensionalIndex = null, int requestedThreads = 1) {
        //    ValidateForEachElementParameters(thisArray, action, inclusiveStartingDimensionalIndex, exclusiveEndingDimensionalIndex);

        //    inclusiveStartingDimensionalIndex ??= new int[thisArray.Rank];
        //    exclusiveEndingDimensionalIndex ??= thisArray.Shape;

        //    throw new NotImplementedException();
        //}
        //public static async Task ForEachElementAsync(this INumericArray thisArray, Action<int[], INumericArray> action, int[]? inclusiveStartingDimensionalIndex = null, int[]? exclusiveEndingDimensionalIndex = null, int requestedThreads = 1) =>
        //    await Task.Run(() => thisArray.ForEachElement(action, inclusiveStartingDimensionalIndex, exclusiveEndingDimensionalIndex, requestedThreads));
        //#endregion
    }
}