namespace NumericArraysTests {
    using NumericArrays;
    using NumericArrays.Concrete;

    public abstract class ArrayReductionTests : ArrayTests {
        [TestMethod()]
        public virtual void TestSum() {            
            var arrayToTest = CreateArray<int>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var sumInt = arrayToTest.Sum();
            Assert.IsTrue(sumInt.SequenceEqual(new int[] { 300 }));
            
            var sumIntAxis0 = arrayToTest.Sum(0);
            Assert.IsTrue(sumIntAxis0.SequenceEqual(new int[] { 
                14, 16, 18, 20,
                22, 24, 26, 28,
                30, 32, 34, 36
            }));

            var sumIntAxis1 = arrayToTest.Sum(1);
            Assert.IsTrue(sumIntAxis1.SequenceEqual(new int[] { 
                15, 18, 21, 24,
                51, 54, 57, 60
            }));

            var sumIntAxis2 = arrayToTest.Sum(2);
            Assert.IsTrue(sumIntAxis2.SequenceEqual(new int[] { 
                10, 26, 42,
                58, 74, 90
            }));
        }

        [TestMethod()]
        public virtual void TestProd() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            //var doubleProd = arrayToTest.Prod();
            //Console.WriteLine(doubleProd);
            //Assert.IsTrue(doubleProd.SequenceEqual(new double[] { 300 }));

            var doubleProd0 = arrayToTest.Prod(0);
            Assert.IsTrue(doubleProd0.SequenceEqual(new double[] {
                13, 28, 45, 64,
                85, 108, 133, 160,
                189, 220, 253, 288
            }));

            var doubleProd1 = arrayToTest.Prod(1);
            Assert.IsTrue(doubleProd1.SequenceEqual(new double[] {
                45, 120, 231, 384,
                4641, 5544, 6555, 7680
            }));

            var doubleProd2 = arrayToTest.Prod(2);
            Assert.IsTrue(doubleProd2.SequenceEqual(new double[] {
                24, 1680, 11880,
                43680, 116280, 255024
            }));
        }

        [TestMethod()]
        public virtual void TestMean() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var doubleMean = arrayToTest.Mean();
            Assert.IsTrue(doubleMean.SequenceEqual(new double[] { 12.5 }));

            var doubleMean0 = arrayToTest.Mean(0);
            Assert.IsTrue(doubleMean0.SequenceEqual(new double[] {
                7, 8, 9, 10,
                11, 12, 13, 14,
                15, 16, 17, 18
            }));

            var doubleMean1 = arrayToTest.Mean(1);
            Assert.IsTrue(doubleMean1.SequenceEqual(new double[] {
                5, 6, 7, 8,
                17, 18, 19, 20
            }));

            var doubleMean2 = arrayToTest.Mean(2);
            Assert.IsTrue(doubleMean2.SequenceEqual(new double[] {
                2.5, 6.5, 10.5,
                14.5, 18.5, 22.5
            }));
        }

        [TestMethod()]
        public virtual void TestMin() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var doubleMin = arrayToTest.Min();
            Assert.IsTrue(doubleMin.SequenceEqual(new double[] { 1 }));

            var doubleMin0 = arrayToTest.Min(0);
            Assert.IsTrue(doubleMin0.SequenceEqual(new double[] {
                1, 2, 3, 4,
                5, 6, 7, 8,
                9, 10, 11, 12
            }));

            var doubleMin1 = arrayToTest.Min(1);
            Assert.IsTrue(doubleMin1.SequenceEqual(new double[] {
                1, 2, 3, 4,
                13, 14, 15, 16
            }));

            var doubleMin2 = arrayToTest.Min(2);
            Assert.IsTrue(doubleMin2.SequenceEqual(new double[] {
                1, 5, 9,
                13, 17, 21
            }));
        }

        [TestMethod()]
        public virtual void TestMax() {
            var arrayToTest = CreateArray<int>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var intMax = arrayToTest.Max();
            Assert.IsTrue(intMax.SequenceEqual(new int[] { 24 }));

            var intMax0 = arrayToTest.Max(0);
            Assert.IsTrue(intMax0.SequenceEqual(new int[] {
                13, 14, 15, 16,
                17, 18, 19, 20,
                21, 22, 23, 24
            }));

            var intMax1 = arrayToTest.Max(1);
            Assert.IsTrue(intMax1.SequenceEqual(new int[] {
                9, 10, 11, 12,
                21, 22, 23, 24
            }));

            var intMax2 = arrayToTest.Max(2);
            Assert.IsTrue(intMax2.SequenceEqual(new int[] {
                4, 8, 12,
                16, 20, 24
            }));
        }

        [TestMethod()]
        public virtual void TestMedian() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var doubleMedian = arrayToTest.Median();
            Assert.IsTrue(doubleMedian.SequenceEqual(new double[] { 12.5 }));

            var doubleMedian0 = arrayToTest.Median(0);
            Assert.IsTrue(doubleMedian0.SequenceEqual(new double[] {
                7, 8, 9, 10,
                11, 12, 13, 14,
                15, 16, 17, 18
            }));

            var doubleMedian1 = arrayToTest.Median(1);
            Assert.IsTrue(doubleMedian1.SequenceEqual(new double[] {
                5, 6, 7, 8,
                17, 18, 19, 20
            }));

            var doubleMedian2 = arrayToTest.Median(2);
            Assert.IsTrue(doubleMedian2.SequenceEqual(new double[] {
                2.5, 6.5, 10.5,
                14.5, 18.5, 22.5
            }));
        }

        [TestMethod()]
        public virtual void TestVar() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var doubleVar = arrayToTest.Var();
            Assert.IsTrue(doubleVar.SequenceEqual(new double[] { 47.916666666666664 }));

            var doubleVar0 = arrayToTest.Var(0);
            Assert.IsTrue(doubleVar0.SequenceEqual(new double[] {
                36, 36, 36, 36,
                36, 36, 36, 36,
                36, 36, 36, 36                
            }));

            var doubleVar1 = arrayToTest.Var(1);
            Console.WriteLine(doubleVar1);
            Assert.IsTrue(doubleVar1.SequenceEqual(new double[] {
                10.666666666666666, 10.666666666666666, 10.666666666666666, 10.666666666666666,
                10.666666666666666, 10.666666666666666, 10.666666666666666, 10.666666666666666
            }));

            var doubleVar2 = arrayToTest.Var(2);
            Assert.IsTrue(doubleVar2.SequenceEqual(new double[] {
                1.25, 1.25, 1.25,
                1.25, 1.25, 1.25
            }));
        }

        [TestMethod()]
        public virtual void TestStd() {
            var arrayToTest = CreateArray<double>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var doubleStd = arrayToTest.Std();
            Assert.IsTrue(doubleStd.SequenceEqual(new double[] { 6.922186552431729 }));

            var doubleStd0 = arrayToTest.Std(0);
            Assert.IsTrue(doubleStd0.SequenceEqual(new double[] {
                6, 6, 6, 6,
                6, 6, 6, 6,
                6, 6, 6, 6
            }));

            var doubleStd1 = arrayToTest.Std(1);
            Assert.IsTrue(doubleStd1.SequenceEqual(new double[] {
                3.265986323710904, 3.265986323710904, 3.265986323710904, 3.265986323710904,
                3.265986323710904, 3.265986323710904, 3.265986323710904, 3.265986323710904
            }));

            var doubleStd2 = arrayToTest.Std(2);
            Assert.IsTrue(doubleStd2.SequenceEqual(new double[] {
                1.118033988749895, 1.118033988749895, 1.118033988749895,
                1.118033988749895, 1.118033988749895, 1.118033988749895
            }));
        }

        [TestMethod()]
        public virtual void TestArgMax() {
            var arrayToTest = CreateArray<int>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var intArgMax = arrayToTest.ArgMax();
            Assert.IsTrue(intArgMax.SequenceEqual(new int[] { 23 }));

            var intArgMax0 = arrayToTest.ArgMax(0);
            Console.WriteLine(intArgMax0);
            Assert.IsTrue(intArgMax0.SequenceEqual(new int[] {
                1, 1, 1, 1,
                1, 1, 1, 1,
                1, 1, 1, 1
            }));

            var intArgMax1 = arrayToTest.ArgMax(1);
            Console.WriteLine(intArgMax1);
            Assert.IsTrue(intArgMax1.SequenceEqual(new int[] {
                2, 2, 2, 2,
                2, 2, 2, 2
            }));

            var intArgMax2 = arrayToTest.ArgMax(2);
            Console.WriteLine(intArgMax2);
            Assert.IsTrue(intArgMax2.SequenceEqual(new int[] {
                3, 3, 3,
                3, 3, 3
            }));
        }

        [TestMethod()]
        public virtual void TestArgMin() {
            var arrayToTest = CreateArray<int>(new int[] { 2, 3, 4 }, Enumerable.Range(1, 2 * 3 * 4).Cast<ValueType>().ToArray());

            var intArgMin = arrayToTest.ArgMin();
            Assert.IsTrue(intArgMin.SequenceEqual(new int[] { 300 }));

            var intArgMin0 = arrayToTest.ArgMin(0);
            Assert.IsTrue(intArgMin0.SequenceEqual(new int[] {
                14, 16, 18, 20,
                22, 24, 26, 28,
                30, 32, 34, 36
            }));

            var intArgMin1 = arrayToTest.ArgMin(1);
            Assert.IsTrue(intArgMin1.SequenceEqual(new int[] {
                15, 18, 21, 24,
                51, 54, 57, 60
            }));

            var intArgMin2 = arrayToTest.ArgMin(2);
            Assert.IsTrue(intArgMin2.SequenceEqual(new int[] {
                10, 26, 42,
                58, 74, 90
            }));
        }
    }
}