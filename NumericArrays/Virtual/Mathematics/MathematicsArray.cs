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
            base(leftArray, NA.ComputeBroadcastingShape(leftArray.Shape, rightArray.Shape)) {

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
    }
}
