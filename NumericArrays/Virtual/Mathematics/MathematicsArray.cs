namespace NumericArrays.Virtual.Mathematics {
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class MathematicsArray<TType> : VirtualSourceArray<TType>
        where TType: struct, IConvertible{

        #region Protected Fields
        protected readonly INumericArray rightArray;
        protected readonly int[] rightArrayShape;
        protected readonly int[] rightArrayStrides;

        protected readonly bool isLeftArrayBroadcasting;
        protected readonly bool isRightArrayBroadcasting;
        #endregion

        #region Protected Constructors
        protected MathematicsArray(INumericArray leftArray, INumericArray rightArray) : 
            base(leftArray, ComputeBroadcastingShape(leftArray.Shape, rightArray.Shape)) {

            this.rightArray = rightArray ?? throw new ArgumentNullException(nameof(rightArray));
            rightArrayShape = rightArray.Shape;
            rightArrayStrides = rightArray.Strides;
            isLeftArrayBroadcasting = !leftArray.Shape.SequenceEqual(Shape);
            isRightArrayBroadcasting = !rightArray.Shape.SequenceEqual(Shape);
        }

        public MathematicsArray(INumericArray<TType> leftArray, INumericArray rightArray) :
            this(leftArray as INumericArray, rightArray) {
        }
        #endregion

        #region Protected Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeLeftArrayLinearIndex(int linearIndex) {
            int broadcastingRank = Rank;
            int actualLinearIndex = 0;

            for (int i = 0, j = sourceArrayShape.Length - broadcastingRank; i < broadcastingRank && linearIndex > 0; i++, j++)
            {
                int dimensionalIndex = linearIndex / ProtectedStrides[i];

                if (j >= 0 && sourceArrayShape[j] > 1)
                {
                    actualLinearIndex += (dimensionalIndex * sourceArrayStrides[j]);
                }

                linearIndex -= dimensionalIndex * ProtectedStrides[i];
            }

            return actualLinearIndex;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeRightArrayLinearIndex(int linearIndex) {
            int broadcastingRank = Rank;
            int actualLinearIndex = 0;

            for (int i = 0, j = rightArrayShape.Length - broadcastingRank; i < broadcastingRank && linearIndex > 0; i++, j++)
            {
                int dimensionalIndex = linearIndex / ProtectedStrides[i];

                if (j >= 0 && rightArrayShape[j] > 1)
                {
                    actualLinearIndex += (dimensionalIndex * rightArrayStrides[j]);
                }

                linearIndex -= dimensionalIndex * ProtectedStrides[i];
            }

            return actualLinearIndex;
        }
        #endregion

        #region Public Static Methods
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
