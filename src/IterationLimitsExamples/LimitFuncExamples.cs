using System;
using IterationLimits;

namespace IterationLimitsExamples;

public class LimitFuncExamples
{
    public static void ExampleBefore()
    {
        var counter = 0;

        Func<bool> condition = () => counter++ < 20;

        var start = DateTime.Now;

        while (condition.Invoke())
        {
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitCount()
    {
        var counter = 0;

        Func<bool> condition = () => counter++ < 20;
        Func<bool> conditionLimited = Limits.LimitCount(condition, 10);

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitTime()
    {
        var counter = 0;

        Func<bool> condition = () => counter++ < 20;
        Func<bool> conditionLimited = Limits.LimitTime(condition, TimeSpan.FromSeconds(0.005));

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }
}
