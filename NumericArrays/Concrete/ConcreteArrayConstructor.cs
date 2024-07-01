namespace NumericArrays.Concrete {
    using System;

    public abstract class ConcreteArrayConstructor : IConcreteArrayConstructor {
        public abstract IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, Func<int, ValueType>? setValueFunc = null);

        public virtual IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, ValueType[] values) => ConstructConcreteArray(elementType, shape, (i => values[i]));
        public virtual IConcreteArray ConstructConcreteArray(Type elementType, int[] shape, ValueType value) => ConstructConcreteArray(elementType, shape, (i => value));

        public abstract IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, Func<int, ValueType>? setValueFunc = null)
            where TType : struct, IConvertible;
        public IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, TType[] values)
            where TType : struct, IConvertible => ConstructConcreteArray<TType>(shape, (i => values[i]));
        public IConcreteArray<TType> ConstructConcreteArray<TType>(int[] shape, TType values)
            where TType : struct, IConvertible => ConstructConcreteArray<TType>(shape, (i => values));
    }
}