# IterationLimits

This is a small library to help you limit potentially infinite loops in your code.


## Features

The `Limits` class provides two ways to limit iterations:

- `LimitCount` to limit the number of iterations.
- `LimitTime` to limit the time duration of iterations.

The `Limits` class supports three types of iterations:

- `Func<bool>` to limit based on condition.
- `IEnumerator<T>` to limit enumerator.
- `IEnumerable<T>` to limit enumerable.

The `LimitTime` method provides three overloads to measure time:

- Use the value of `DateTime.Now` by omitting the 3rd argument.
- Use the value of custom `Func<DateTime>` by providing argument of this type.
- Use the value of custom `Func<TimeSpan>` by providing argument of this type.


## Examples

> [!NOTE]
> More examples are available in `IterationLimitsExamples` project.

`LimitCount(Func<bool> condition, int limit)`

```csharp
Func<bool> condition = () => true;
Func<bool> conditionLimited = Limits.LimitCount(condition, 10);

while (conditionLimited.Invoke()) {}
```

`LimitTime<T>(IEnumerator<T> enumerator, TimeSpan limit)`

```csharp
IEnumerator<int> enumerator = GetEnumerator();
IEnumerator<int> enumeratorLimited = Limits.LimitTime(enumerator, TimeSpan.FromSeconds(1));

while (enumeratorLimited.MoveNext()) {}
```

`LimitTime<T>(IEnumerable<T> enumerable, TimeSpan limit, Func<DateTime> measure)`

```csharp
IEnumerable<int> enumerable = GetEnumerable();
IEnumerable<int> enumerableLimited = Limits.LimitTime(enumerable, TimeSpan.FromSeconds(1), () => DateTime.Now);

foreach (var item in enumerableLimited) {}
```
