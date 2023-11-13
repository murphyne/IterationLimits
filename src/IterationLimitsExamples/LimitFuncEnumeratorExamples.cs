using System;
using System.Collections.Generic;
using IterationLimits;

namespace IterationLimitsExamples;

public class LimitFuncEnumeratorExamples
{
    public static void ExampleBefore()
    {
        IEnumerator<int> enumerator = GetEnumerator();

        Func<bool> condition = () => enumerator.MoveNext();

        var start = DateTime.Now;

        while (condition.Invoke())
        {
            Console.WriteLine($"{enumerator.Current,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitCount()
    {
        IEnumerator<int> enumerator = GetEnumerator();

        Func<bool> condition = () => enumerator.MoveNext();
        Func<bool> conditionLimited = Limits.LimitCount(condition, 10);

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            Console.WriteLine($"{enumerator.Current,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitTime()
    {
        IEnumerator<int> enumerator = GetEnumerator();

        Func<bool> condition = () => enumerator.MoveNext();
        Func<bool> conditionLimited = Limits.LimitTime(condition, TimeSpan.FromSeconds(0.005));

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            Console.WriteLine($"{enumerator.Current,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitTimeExternal()
    {
        IEnumerator<int> enumerator = GetEnumerator();

        Func<bool> condition = () => enumerator.MoveNext();
        Func<bool> conditionLimited = Limits.LimitTime(condition, TimeSpan.FromSeconds(0.005), () => DateTime.Now);

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            Console.WriteLine($"{enumerator.Current,2} - {DateTime.Now - start}");
        }
    }

    private static IEnumerator<int> GetEnumerator()
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
