namespace NumericArrays.Virtual.Facades {
    using System;
    using System.Runtime.CompilerServices;

    public class SlicingArray<TType> : VirtualSourceArray<TType>
        where TType : struct, IConvertible {

        #region Public SlicingMask Struct
        public struct SlicingMask {
            public SlicingMask(int offset, int length, int step) {
                Offset = offset;
                Length = length;
                Step = step;
            }

            public int Offset { get; }
            public int Length { get; }
            public int Step { get; }
        }
        #endregion

        #region Private Fields
        private readonly string slicingMaskString;
        private readonly int[] sourceArrayStrides;
        private readonly SlicingMask[] slicingMask;
        #endregion

        #region Public Constructors
        public SlicingArray(INumericArray sourceArray, string slicingMask) :
            base(sourceArray ?? throw new ArgumentNullException(nameof(sourceArray)), ComputeMaskShape(ComputeMask(slicingMask, sourceArray.Shape))) {

            this.slicingMaskString = slicingMask;
            sourceArrayStrides = sourceArray.Strides;
            this.slicingMask = ComputeMask(slicingMask, sourceArray.Shape);
        }
        public SlicingArray(INumericArray<TType> sourceArray, string slicingMask) :
            this(sourceArray as INumericArray, slicingMask) {
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
            int j = 0;

            for (int i = 0; i < slicingMask.Length; i++)
            {
                if (linearIndex > 0)
                {
                    // TODO: this is to slow.  going to make a copy of the Strides array each time this method is called
                    dataSourceLinearIndex += (slicingMask[i].Offset + linearIndex / ProtectedStrides[j] * slicingMask[i].Step) * sourceArrayStrides[i];

                    linearIndex %= ProtectedStrides[j];
                    j++;
                }
                else
                {
                    // TODO: this is to slow.  going to make a copy of the Strides array each time this method is called
                    dataSourceLinearIndex += slicingMask[i].Offset * sourceArrayStrides[i];
                }
            }

            return dataSourceLinearIndex;
        }
        #endregion

        #region Pulbic Static Methods
        public static int[] ComputeMaskShape(SlicingMask[] slicingMasks) {
            int rank = slicingMasks.Length;
            int[] shape = new int[rank];

            for (int i = 0; i < rank; i++)
            {
                shape[i] = slicingMasks[i].Length;
            }

            return shape;
        }
        public static SlicingMask[] ComputeMask(string maskString, int[] shape) {
            int rank = shape.Length;

            string[] maskParts = maskString.Trim().Split(',');

            if (maskParts.Length > rank)
            {
                throw new ArgumentException("mastParts.Length must be <= Rank");
            }

            var mask = new SlicingMask[rank];

            for (int i = 0; i < rank; i++)
            {
                if (i >= maskParts.Length)
                {
                    mask[i] = new SlicingMask(0, shape[i], 1);
                    continue;
                }

                maskParts[i] = maskParts[i].Trim();

                if (maskParts[i].Length == 0)
                {
                    mask[i] = new SlicingMask(0, shape[i], 1);
                    continue;
                }

                if (int.TryParse(maskParts[i], out int idx))
                {
                    if (idx >= shape[i])
                    {
                        throw new ArgumentException("Index out of range");
                    }

                    mask[i] = new SlicingMask(idx, 1, 1);
                    continue;
                }

                string[] maskPart = maskParts[i].Split(':');

                if (maskPart.Length is 0 or > 3)
                {
                    throw new ArgumentException("maskPart.Length must be > 0 and <= 3");
                }

                int offset = 0;
                int length = shape[i];
                int step = 1;

                for (int l = 0; l < maskPart.Length; l++)
                {
                    if (maskPart[l].Length == 0)
                    {
                        continue;
                    }

                    if (int.TryParse(maskPart[l], out int value))
                    {
                        if (l == 0)
                        {
                            if (value < 0 || value >= shape[i])
                            {
                                throw new ArgumentException("Index out of range");
                            }

                            offset = value;
                            length = shape[i] - offset;
                        }
                        else if (l == 1)
                        {

                            if (value < 0 || value > shape[i] || value < offset)
                            {
                                throw new ArgumentException("Index out of range");
                            }

                            length = value - offset;
                        }
                        else if (l == 2)
                        {
                            step = value;
                        }
                    }
                }

                mask[i] = new SlicingMask(offset, (int)Math.Ceiling(length / (double)step), step);
            }

            return mask;
        }
        #endregion 

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new SlicingArray<TType>(sourceArray, slicingMaskString);
        #endregion

        #endregion
    }
}