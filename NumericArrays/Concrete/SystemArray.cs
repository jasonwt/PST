﻿namespace NumericArrays.Concrete {
    using System;
    using System.Diagnostics;

    public class SystemArray<TType> : NumericArray<TType>, IConcreteArray<TType>
        where TType: struct, IConvertible {

        #region Private Fields
        private TType[] elements;
        #endregion

        #region Public Constructors
        public SystemArray(int[] arrayShape) : base(arrayShape) {
            elements = new TType[Length];

        }
        #endregion

        #region Public Properties
        public override TType this[int linearIndex] {
            get {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length, "Linear index is out of bounds.");
                return elements[linearIndex];
            }
            set {
                Debug.Assert(linearIndex >= 0 && linearIndex < Length, "Linear index is out of bounds.");
                elements[linearIndex] = value;
            }
        }

        public override int MemoryUsage {
            get {
                int memoryUsage = base.MemoryUsage;

                memoryUsage += Length * ElementTypeCode switch {
                    TypeCode.Boolean => sizeof(bool),
                    TypeCode.SByte => sizeof(sbyte),
                    TypeCode.Byte => sizeof(byte),
                    TypeCode.Int16 => sizeof(short),
                    TypeCode.UInt16 => sizeof(ushort),
                    TypeCode.Int32 => sizeof(int),
                    TypeCode.UInt32 => sizeof(uint),
                    TypeCode.Int64 => sizeof(long),
                    TypeCode.UInt64 => sizeof(ulong),
                    TypeCode.Single => sizeof(float),
                    TypeCode.Double => sizeof(double),
                    TypeCode.Decimal => sizeof(decimal),
                    _ => throw new NotImplementedException()
                };

                return memoryUsage;
            }
        }
        #endregion

        #region Public Methods

        #region IConcreteArray<TType> Methods
        public IConcreteArray<TConstructedType> Construct<TConstructedType>(params int[] shape) 
            where TConstructedType : struct, IConvertible {

            return Type.GetTypeCode(typeof(TConstructedType)) switch {
                TypeCode.Boolean => (IConcreteArray<TConstructedType>) new SystemArray<bool>(shape),
                TypeCode.SByte => (IConcreteArray<TConstructedType>) new SystemArray<sbyte>(shape),
                TypeCode.Byte => (IConcreteArray<TConstructedType>) new SystemArray<byte>(shape),
                TypeCode.Int16 => (IConcreteArray<TConstructedType>) new SystemArray<short>(shape),
                TypeCode.UInt16 => (IConcreteArray<TConstructedType>) new SystemArray<ushort>(shape),
                TypeCode.Int32 => (IConcreteArray<TConstructedType>) new SystemArray<int>(shape),
                TypeCode.UInt32 => (IConcreteArray<TConstructedType>) new SystemArray<uint>(shape),
                TypeCode.Int64 => (IConcreteArray<TConstructedType>) new SystemArray<long>(shape),
                TypeCode.UInt64 => (IConcreteArray<TConstructedType>) new SystemArray<ulong>(shape),
                TypeCode.Single => (IConcreteArray<TConstructedType>) new SystemArray<float>(shape),
                TypeCode.Double => (IConcreteArray<TConstructedType>) new SystemArray<double>(shape),
                TypeCode.Decimal => (IConcreteArray<TConstructedType>) new SystemArray<decimal>(shape),
                _ => throw new NotImplementedException()
            };
        }

        public IConcreteArray Construct(Type type, params int[] shape) => Construct(type, shape);
        #endregion

        #region ICloneable Methods
        public override INumericArray<TType> Clone() {
            var newSystemArray = new SystemArray<TType>(Shape);
            Array.Copy(elements, newSystemArray.elements, Length);

            return newSystemArray;
        }
        #endregion

        #endregion
    }
}