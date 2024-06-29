namespace NumericArrays.Virtual.Facades {
    using NumericArrays.Virtual.Mathematics;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class BroadcastingArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Private Fields
        private readonly int[] broadcastToShape;
        private readonly int[] broadcastingShape;
        private readonly int[] broadcastingStrides;
        #endregion

        #region Public Constructors
        public BroadcastingArray(INumericArray sourceArray, int[] broadcastToShape) :
            base(sourceArray, broadcastToShape) {

            this.broadcastToShape = broadcastToShape.ToArray();

            broadcastingStrides = ComputeBroadcastingStrides(sourceArray.Shape, broadcastToShape);
            broadcastingShape = broadcastingStrides.
                Reverse().
                SkipWhile(x => x == 0).
                Reverse().
                ToArray();
        }
        public BroadcastingArray(INumericArray<TType> sourceArray, int[] broadcastToShape) :
            this(sourceArray as INumericArray, broadcastToShape) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get => base[ComputeSourceArrayLinearIndex(linearIndex)];
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }
        #endregion

        #region Protected Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeSourceArrayLinearIndex(int linearIndex) {
            int dataSourceLinearIndex = 0;

            for (int i = 0; i < broadcastingShape.Length && i < Rank; i++)
            {
                if (broadcastingShape[i] > 0)
                {
                    dataSourceLinearIndex += broadcastingShape[i] * (linearIndex / ProtectedStrides[i]);
                }

                linearIndex %= ProtectedStrides[i];
            }

            return dataSourceLinearIndex;
        }
        #endregion

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new BroadcastingArray<TType>(sourceArray, broadcastToShape);
        #endregion

        #endregion

        #region Public Static Methods
        public static int[] ComputeBroadcastingStrides(int[] shape1, int[] shape2) {
            var shape1List = shape1.ToList();
            var shape2List = shape2.ToList();

            // Pad the shapes to have the same length
            int maxLength = Math.Max(shape1List.Count, shape2List.Count);
            shape1List.InsertRange(0, Enumerable.Repeat(1, maxLength - shape1List.Count));
            shape2List.InsertRange(0, Enumerable.Repeat(1, maxLength - shape2List.Count));

            // Check if the shapes are compatible
            for (int i = 0; i < shape1List.Count; i++)
            {
                if (shape1List[i] != shape2List[i] && shape1List[i] != 1 && shape2List[i] != 1)
                {
                    throw new ArgumentException("Incompatible shapes for broadcasting.");
                }
            }

            int[] shape1Strides = ComputeStrides(shape1);
            int[] broadcastingStrides = new int[shape1.Length >= shape2.Length ? shape1.Length : shape2.Length];

            //TODO: above should be combined into this
            int shape1Index = shape1.Length - 1;

            for (int i = broadcastingStrides.Length - 1; i >= 0 && shape1Index >= 0; i--)
            {
                if (shape1[shape1Index] > 1)
                {
                    broadcastingStrides[i] = shape1Strides[shape1Index];
                }

                shape1Index--;
            }

            return broadcastingStrides;
        }
        #endregion
    }
}