namespace NumericArrays.Concrete {
    using System;
    using System.ComponentModel;

    public interface IConcreteArrayConstructor {
        IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, Func<int, ValueType>? setValueFunc = null);
        IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, ValueType[] values);
        IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, ValueType value);

        IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, Func<int, ValueType>? setValueFunc = null)
            where TType : struct, IConvertible;
        IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, TType[] values)
            where TType: struct, IConvertible;
        IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, TType values)
            where TType : struct, IConvertible;
    }
}
