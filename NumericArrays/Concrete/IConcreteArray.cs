namespace NumericArrays.Concrete {
    using System;

    public interface IConcreteArray : INumericArray {
        IConcreteArray<TConstructedType> Construct<TConstructedType>(params int[] shape)
            where TConstructedType : struct, IConvertible;
        IConcreteArray Construct(Type type, params int[] shape);
    }

    public interface IConcreteArray<TType> : INumericArray<TType>, IConcreteArray
        where TType : struct, IConvertible {

    }
}
