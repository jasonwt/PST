namespace NumericArrays.Virtual {
    using System;

    using NumericArrays.Concrete;

    public abstract class VirtualArray<TType> : NumericArray<TType>, IVirtualArray<TType>
        where TType: struct, IConvertible {

        #region Protected Constructors
        protected VirtualArray(int[] arrayShape) : base(arrayShape) { }
        #endregion

        #region Public Static Methods
        //public static INumericArray<TType> GetNonGenericSourceArray(INumericArray sourceArray) {
        //    return sourceArray != null ?
        //        (sourceArray.ElementType == typeof(TType) ? (INumericArray<TType>)sourceArray : new Virtual<TType>(sourceArray)) :
        //        throw new ArgumentNullException(nameof(sourceArray));
        //}
        #endregion

        #region Public Methods
        public IConcreteArray<TType> ToConcrete(IConcreteArrayConstructor? concreteArrayConstructor = null) => 
            NA.ConstructConcreteArray<TType>(Shape, concreteArrayConstructor);

        IConcreteArray IVirtualArray.ToConcrete(IConcreteArrayConstructor? concreteArrayConstructor) =>
            ToConcrete(concreteArrayConstructor);
        #endregion
    }
}