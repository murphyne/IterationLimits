using System;
using System.Collections.Generic;
using IterationLimits;

namespace IterationLimitsExamples;

public class LimitEnumerableExamples
{
    public static void ExampleBefore()
    {
        IEnumerable<int> enumerable = GetEnumerable();

        var start = DateTime.Now;

        foreach (var item in enumerable)
        {
            Console.WriteLine($"{item,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitCount()
    {
        IEnumerable<int> enumerable = GetEnumerable();
        IEnumerable<int> enumerableLimited = Limits.LimitCount(enumerable, 10);

        var start = DateTime.Now;

        foreach (var item in enumerableLimited)
        {
            Console.WriteLine($"{item,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitTime()
    {
        IEnumerable<int> enumerable = GetEnumerable();
        IEnumerable<int> enumerableLimited = Limits.LimitTime(enumerable, TimeSpan.FromSeconds(0.005));

        var start = DateTime.Now;

        foreach (var item in enumerableLimited)
        {
            Console.WriteLine($"{item,2} - {DateTime.Now - start}");
        }
    }

    private static IEnumerable<int> GetEnumerable()
    {
        yield return  1;
        yield return  2;
        yield return  3;
        yield return  4;
        yield return  5;
        yield return  6;
        yield return  7;
        yield return  8;
        yield return  9;
        yield return 10;
        yield return 11;
        yield return 12;
        yield return 13;
        yield return 14;
        yield return 15;
        yield return 16;
        yield return 17;
        yield return 18;
        yield return 19;
        yield return 20;
    }
}
