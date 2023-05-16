using System;
using IterationLimits;

namespace IterationLimitsExamples;

public class LimitFuncExamples
{
    public static void ExampleBefore()
    {
        var counter = 0;

        Func<bool> condition = () => counter < 20;

        var start = DateTime.Now;

        while (condition.Invoke())
        {
            counter += 1;
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitCount()
    {
        var counter = 0;

        Func<bool> condition = () => counter < 20;
        Func<bool> conditionLimited = Limits.LimitCount(10, condition);

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            counter += 1;
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }

    public static void ExampleLimitTime()
    {
        var counter = 0;

        Func<bool> condition = () => counter < 20;
        Func<bool> conditionLimited = Limits.LimitTime(TimeSpan.FromSeconds(0.005), condition);

        var start = DateTime.Now;

        while (conditionLimited.Invoke())
        {
            counter += 1;
            Console.WriteLine($"{counter,2} - {DateTime.Now - start}");
        }
    }
}
