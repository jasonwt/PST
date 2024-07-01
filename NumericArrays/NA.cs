namespace NumericArrays {
    using System;
    using System.Linq;

    using NumericArrays.Concrete;
    using NumericArrays.Virtual;
    
    public static partial class NA {
        #region Construct Array Public Static Public Properties
        public static Func<IVirtualArray, bool>? ShouldConstructAsConcreteArrayFunc { get; set; } = null;
        #endregion

        #region Construct Array Public Static Factory Methods

        #region Construct Methods
        public static INumericArray<TType> ConstructVirtualArray<TType>(IVirtualArray<TType> virtualArray, bool? toConcrete)
            where TType : struct, IConvertible {

            //TODO: Not working correctly
            //return virtualArray.ToConcrete();

            toConcrete ??= ShouldConstructAsConcreteArrayFunc?.Invoke(virtualArray) ?? false;

            return toConcrete.Value ? virtualArray.ToConcrete() : virtualArray;
        }

        public static IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, IConcreteArrayConstructor? concreteArrayConstructor = null)
            where TType : struct, IConvertible {

            return concreteArrayConstructor?.ConstructConcreteArray<TType>(shape) ?? new SystemArray<TType>(shape);
        }
        #endregion

        #region Full Methods
        public static IConcreteArray<TType> Full<TType>(int[] shape, TType fillValue)
            where TType : struct, IConvertible {

            IConcreteArray<TType> concreteArray = ConstructConcreteArray<TType>(shape);
            concreteArray.Fill(fillValue);
            return concreteArray;
        }
        #endregion

        #region Zeros Methods
        public static IConcreteArray<TType> Zeros<TType>(int[] shape)
            where TType : struct, IConvertible {

            IConcreteArray<TType> concreteArray = ConstructConcreteArray<TType>(shape);
            return concreteArray;
        }
        #endregion

        #region Ones Methods
        public static IConcreteArray<TType> Ones<TType>(int[] shape)
            where TType : struct, IConvertible {

            IConcreteArray<TType> concreteArray = ConstructConcreteArray<TType>(shape);
            concreteArray.Fill(1);
            return concreteArray;
        }
        #endregion

        #region ARange Methods
        public static IConcreteArray<TType> ARange<TType>(double inclusiveStart, double inclusiveEnd, double step = 1)
            where TType : struct, IConvertible {

            step = Math.Abs(step);

            if (step == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(step), "Step cannot be zero.");
            }

            Type tType = typeof(TType);

            double doubleStart = Convert.ToDouble(inclusiveStart);
            double doubleEnd = Convert.ToDouble(inclusiveEnd);
            double doubleRange = Math.Abs(doubleEnd - doubleStart);
            int numIterations = (int)(doubleRange / step);

            IConcreteArray<TType> newArray = ConstructConcreteArray<TType>(new int[] { numIterations+1 });

            if (doubleStart < doubleEnd)
            {
                newArray.Fill((i) => doubleStart + (i * step));
            }
            else if (doubleStart > doubleEnd)
            {
                newArray.Fill((i) => doubleStart - (i * step));
            }
            else
            {
                newArray[0] = (TType)Convert.ChangeType(doubleStart, tType);
            }

            return newArray;

        }
        #endregion

        #region Linspace Methods
        public static IConcreteArray<TType> Linspace<TType>(TType start, TType end, int numPoints)
            where TType : struct, IConvertible {

            if (numPoints < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(numPoints), "Number of points must be at least 2.");
            }

            Type tType = typeof(TType);

            double doubleStart = Convert.ToDouble(start);
            double doubleEnd = Convert.ToDouble(end);
            double doubleRange = Math.Abs(doubleEnd - doubleStart);
            double step = doubleRange / (numPoints - 1);

            IConcreteArray<TType> newArray = ConstructConcreteArray<TType>(new int[] { numPoints });

            if (doubleStart > doubleEnd)
            {
                newArray.Fill((i) => doubleStart - (i * step));
            }
            else
            {
                newArray.Fill((i) => doubleStart + (i * step));
            }

            return newArray;
        }
        #endregion

        #region Eye Methods
        public static IConcreteArray<TType> Eye<TType>(int size, int rank = 2)
            where TType : struct, IConvertible {

            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Size must be greater than zero.");
            }

            if (rank < 2)
            {
                throw new ArgumentOutOfRangeException(nameof(rank), "Rank must be greater than or equal to two.");
            }

            Type tType = typeof(TType);

            int[] arraySize = Enumerable.Repeat(size, rank).ToArray();

            IConcreteArray<TType> newArray = ConstructConcreteArray<TType>(arraySize);

            for (int i = 0; i < size; i++)
            {
                int[] currentIndex = new int[rank];
                Array.Fill(currentIndex, i);
                newArray[currentIndex] = (TType)Convert.ChangeType(1, tType);
            }

            return newArray;
        }
        #endregion

        #endregion

        #region Helper Methods
        public static bool ValidateShape(int[] shape) {
            if (shape == null)
            {
                return false;
            }

            if (shape.Length == 0)
            {
                return false;
            }

            if (shape.Any((dim) => dim <= 0))
            {
                return false;
            }

            return true;
        }
        public static int[] ComputeStrides(int[] shape) {
            if (shape == null)
            {
                throw new ArgumentNullException(nameof(shape));
            }

            if (shape.Length == 0)
            {
                throw new ArgumentException("Shape can not be empty.", nameof(shape));
            }

            int[] strides = new int[shape.Length];

            strides[shape.Length - 1] = 1;

            for (int i = shape.Length - 2; i >= 0; i--)
            {
                if (shape[i + 1] <= 0)
                {
                    throw new ArgumentException("Shape dimensions must be greater than zero.");
                }

                strides[i] = strides[i + 1] * shape[i + 1];
            }

            return strides;
        }

        public static int[] ComputeBroadcastingShape(int[] shape1, int[] shape2) {
            if (shape1 == null)
            {
                throw new ArgumentNullException(nameof(shape1));
            }

            if (shape2 == null)
            {
                throw new ArgumentNullException(nameof(shape2));
            }

            if (shape1.Length == 0)
            {
                throw new ArgumentException("Shape can not be empty.", nameof(shape1));
            }

            if (shape2.Length == 0)
            {
                throw new ArgumentException("Shape can not be empty.", nameof(shape2));
            }

            var shape1List = shape1.ToList();
            var shape2List = shape2.ToList();

            // Pad the shapes to have the same length
            int maxLength = Math.Max(shape1List.Count, shape2List.Count);
            shape1List.InsertRange(0, Enumerable.Repeat(1, maxLength - shape1List.Count));
            shape2List.InsertRange(0, Enumerable.Repeat(1, maxLength - shape2List.Count));

            int[] broadcastingShape = new int[maxLength];
            // Check if the shapes are compatible
            for (int i = 0; i < shape1List.Count; i++)
            {
                if (shape1List[i] != shape2List[i] && shape1List[i] != 1 && shape2List[i] != 1)
                {
                    throw new ArgumentException("Shapes are not compatible for broadcasting.");
                }

                broadcastingShape[i] = shape1List[i] > shape2List[i] ? shape1List[i] : shape2List[i];
            }

            return broadcastingShape;
        }
        #endregion
    }
}