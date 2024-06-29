namespace NumericArrays.Virtual.Facades {
    using System;
    using System.Runtime.CompilerServices;

    public class ViewArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Private Fields
        private readonly int[] sourceArrayViewOffset;
        private readonly int[] sourceArrayViewShape;
        #endregion

        #region Public Constructors
        public ViewArray(INumericArray sourceArray, int[]? viewOffset = null, int[]? viewShape = null) :
            base(sourceArray ?? throw new ArgumentNullException(nameof(sourceArray)), viewShape ?? sourceArray.Shape) {

            viewOffset ??= new int[sourceArray.Rank];
            throw new NotImplementedException();
        }
        public ViewArray(INumericArray<TType> sourceArray, int[]? viewOffset = null, int[]? viewShape = null) :
            this(sourceArray as INumericArray, viewOffset, viewShape) {
        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get => base[ComputeSourceArrayLinearIndex(linearIndex)];
            set => throw new InvalidOperationException($"Can not set values on a {GetType().Name}.");
        }

        public override int MemoryUsage {
            get {
                int memoryUsage = base.MemoryUsage +
                    (sizeof(int) * sourceArrayViewOffset.Length) +
                    (sizeof(int) * sourceArrayViewShape.Length);

                return memoryUsage;
            }
        }
        #endregion

        #region Protected Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeSourceArrayLinearIndex(int linearIndex) {
            return linearIndex;
        }
        #endregion

        #region Public Static Methods
        #endregion

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ViewArray<TType>(sourceArray, sourceArrayViewOffset, sourceArrayViewShape);
        #endregion

        #endregion
    }
}