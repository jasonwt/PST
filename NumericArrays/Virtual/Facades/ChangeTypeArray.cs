namespace NumericArrays.Virtual.Facades {
    using System;
    
    public class ChangeTypeArray<TType> : VirtualSourceArray<TType>, IVirtualArray<TType>
        where TType: struct, IConvertible {

        #region Public Constructors
        public ChangeTypeArray(INumericArray sourceArray) : 
            base(sourceArray) {
        }
        public ChangeTypeArray(INumericArray<TType> sourceArray) : 
            base(sourceArray) {
        }
        #endregion

        #region Public Methods

        #region ICloneable Methods
        public override INumericArray<TType> Clone() => new ChangeTypeArray<TType>(sourceArray);
        #endregion

        #endregion
    }
}