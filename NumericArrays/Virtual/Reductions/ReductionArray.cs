namespace NumericArrays.Virtual.Reductions {
    using System;
    using System.Linq;

    public abstract class ReductionArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Protected Readonly Fields
        protected readonly int? axis;
        protected readonly int step;
        #endregion

        #region Protected Constructors
        protected ReductionArray(INumericArray sourceArray, int? axis = null) :
            base(sourceArray, ComputeShape(sourceArray, axis)) {

            this.axis = axis;

            step = axis == null ? sourceArray.Length : sourceArray.Shape[axis.Value];
        }
        #endregion

        #region Public Static Methods
        public static int[] ComputeShape(INumericArray sourceArray, int? rank) {
            if (sourceArray == null)
            {
                throw new ArgumentNullException(nameof(sourceArray));
            }

            if (rank == null)
            {
                return new int[] { 1 };
            }

            if (rank < 0 || rank >= sourceArray.Rank)
            {
                throw new ArgumentOutOfRangeException(nameof(rank));
            }

            return sourceArray.Shape.Where((shape, index) => index != rank).ToArray();
        }
        #endregion
    }
}