namespace NumericArrays.Virtual {
    using NumericArrays.Concrete;
    using System;

    public interface IVirtualArray : INumericArray {
        IConcreteArray ToConcrete(IConcreteArrayConstructor? concreteArrayConstructor = null);
    }

    public interface IVirtualArray<TType> : INumericArray<TType>, IVirtualArray
        where TType : struct, IConvertible {

        new IConcreteArray<TType> ToConcrete(IConcreteArrayConstructor? concreteArrayConstructor = null);
    }
}