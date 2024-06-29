namespace NumericArrays.Virtual.Facades {
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class TransposingArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Private Fields
        private readonly int[] transposedAxises;
        private readonly int[] sourceArrayShape;
        private readonly int[] transposedStrides;
        #endregion

        #region Public Constructors
        public TransposingArray(INumericArray sourceArray, int[] transposedAxises) :
            base(sourceArray ?? throw new ArgumentNullException(nameof(sourceArray)), TransposedArray(sourceArray.Shape, transposedAxises)) {

            this.transposedAxises = transposedAxises.ToArray();
            sourceArrayShape = sourceArray.Shape.ToArray();
            transposedStrides = TransposedArray(sourceArray.Strides, transposedAxises);
        }
        public TransposingArray(INumericArray<TType> sourceArray, int[] transposedAxises) :
            this(sourceArray as INumericArray, transposedAxises) {
        }
        
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get => base[ComputeSourceArrayLinearIndex(linearIndex)];
            set => base[ComputeSourceArrayLinearIndex(linearIndex)] = value;
        }
        #endregion

        #region Protected Methods
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected int ComputeSourceArrayLinearIndex(int linearIndex) {
            int dataSourceLinearIndex = 0;

            for (int i = 0; i < Strides.Length; i++)
            {
                dataSourceLinearIndex += transposedStrides[i] * (linearIndex / ProtectedStrides[i]);
                linearIndex %= ProtectedStrides[i];
            }

            return dataSourceLinearIndex;
        }
        #endregion

        #region Public Static Methods
        public static int[] TransposedArray(int[] originalArray, int[] transposedArrayIndices) {
            if (originalArray == null)
            {
                throw new ArgumentNullException(nameof(originalArray));
            }

            if (originalArray.Length == 0)
            {
                throw new ArgumentException("Array must have at least one element.", nameof(originalArray));
            }

            if (transposedArrayIndices == null)
            {
                throw new ArgumentNullException(nameof(transposedArrayIndices));
            }

            if (transposedArrayIndices.Length != originalArray.Length)
            {
                throw new ArgumentException("Array must have the same length as the original array");
            }

            int minTransposedIndex = transposedArrayIndices.Min();
            int maxTransposedIndex = transposedArrayIndices.Max();

            if (minTransposedIndex < 0 || maxTransposedIndex >= originalArray.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(transposedArrayIndices));
            }

            if (transposedArrayIndices.Distinct().Count() != transposedArrayIndices.Length)
            {
                throw new ArgumentException("transposedArrayIndices must not contain duplicates.");
            }

            return transposedArrayIndices.Select(index => originalArray[index]).ToArray();
        }
        #endregion

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new TransposingArray<TType>(sourceArray, transposedAxises);
        #endregion

        #endregion
    }
}