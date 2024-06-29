using NumericArrays.Concrete;

namespace NumericArrays.Profile;

public static partial class ProfileApplication {
    public static void TestSum() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var sumInt = systemArray.Sum();
        var sumIntAxis0 = systemArray.Sum(0);
        var sumIntAxis1 = systemArray.Sum(1);
        var sumIntAxis2 = systemArray.Sum(2);
    }

    public static void TestProd() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var prodInt = systemArray.Prod();
        var prodIntAxis0 = systemArray.Prod(0);
        var prodIntAxis1 = systemArray.Prod(1);
        var prodIntAxis2 = systemArray.Prod(2);
    }

    public static void TestMean() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());
        

        var meanInt = systemArray.Mean();
        var meanIntAxis0 = systemArray.Mean(0);
        var meanIntAxis1 = systemArray.Mean(1);
        var meanIntAxis2 = systemArray.Mean(2);
    }

    public static void TestMin() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var minInt = systemArray.Min();
        var minIntAxis0 = systemArray.Min(0);
        var minIntAxis1 = systemArray.Min(1);
        var minIntAxis2 = systemArray.Min(2);
    }

    public static void TestMax() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var maxInt = systemArray.Max();
        var maxIntAxis0 = systemArray.Max(0);
        var maxIntAxis1 = systemArray.Max(1);
        var maxIntAxis2 = systemArray.Max(2);
    }

    public static void TestMedian() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var medianInt = systemArray.Median();
        var medianIntAxis0 = systemArray.Median(0);
        var medianIntAxis1 = systemArray.Median(1);
        var medianIntAxis2 = systemArray.Median(2);
    }

    public static void TestVar() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var varianceInt = systemArray.Var();
        var varianceIntAxis0 = systemArray.Var(0);
        var varianceIntAxis1 = systemArray.Var(1);
        var varianceIntAxis2 = systemArray.Var(2);
    }

    public static void TestStd() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var stdInt = systemArray.Std();
        var stdIntAxis0 = systemArray.Std(0);
        var stdIntAxis1 = systemArray.Std(1);
        var stdIntAxis2 = systemArray.Std(2);
    }

    public static void TestArgMax() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var argMaxInt = systemArray.ArgMax();
        var argMaxIntAxis0 = systemArray.ArgMax(0);
        var argMaxIntAxis1 = systemArray.ArgMax(1);
        var argMaxIntAxis2 = systemArray.ArgMax(2);
    }

    public static void TestArgMin() {
        var systemArray = new SystemArray<int>(new int[] { 2, 3, 4 });
        systemArray.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        var argMinInt = systemArray.ArgMin();
        var argMinIntAxis0 = systemArray.ArgMin(0);
        var argMinIntAxis1 = systemArray.ArgMin(1);
        var argMinIntAxis2 = systemArray.ArgMin(2);
    }

    public static void ReductionTests() {
        TestSum();
        TestProd();
        TestMean();
        TestMin();
        TestMax();
        TestMedian();
        TestVar();
        TestStd();
        TestArgMax();
        TestArgMin();
        //CumSum
        //Percentile
        //all (func)
        //any (func)

        //var systemArray1 = new SystemArray<int>(new int[] { 2, 3, 4 });
        //systemArray1.Fill(Enumerable.Range(1, 2 * 3 * 4).ToArray());

        //foreach (var index in systemArray1.LinearIndexIterator(2)) {
        //    Console.WriteLine(string.Join(",", index));
        //}
    }
}
