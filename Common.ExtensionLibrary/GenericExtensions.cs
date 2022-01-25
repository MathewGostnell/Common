namespace Common.ExtensionLibrary;

using System;
using System.Linq.Expressions;

public static class GenericExtensions
{
    public static TNumeric? GetArithmeticMean<TNumeric>(
        this IEnumerable<TNumeric> values)
        where TNumeric : IConvertible
        => Divide<TNumeric>(
            Add(values.ToArray()),
            values.Count().As<TNumeric>());

    public static TNumeric? Add<TNumeric>(
        params TNumeric[] values)
    {
        Func<TNumeric, TNumeric, TNumeric>? addFunction
            = GetExpressionResult<TNumeric>(Expression.Add);

        return values.Aggregate(addFunction);
    }

    public static TNumeric? Divide<TNumeric>(
        TNumeric dividend,
        TNumeric? divisor)
    {
        Func<TNumeric, TNumeric, TNumeric>? divideFunction
            = GetExpressionResult<TNumeric>(Expression.Divide);

        return divideFunction(
            dividend,
            divisor);
    }

    public static TNumeric? Product<TNumeric>(
        params TNumeric[] values)
    {
        Func<TNumeric, TNumeric, TNumeric>? multiplyFunction
            = GetExpressionResult<TNumeric>(Expression.Multiply);

        return values.Aggregate(multiplyFunction);
    }

    private static Func<TValue, TValue, TValue> GetExpressionResult<TValue>(
        Func<ParameterExpression, ParameterExpression, BinaryExpression> expressionConstructor)
    {
        ParameterExpression? parameterCurrent = Expression.Parameter(
            typeof(TValue),
            "a");
        ParameterExpression? parameterNext = Expression.Parameter(
            typeof(TValue),
            "b");

        BinaryExpression? binaryExpression = expressionConstructor(
            parameterCurrent,
            parameterNext);

        return Expression.Lambda<
            Func<TValue, TValue, TValue>>(
                binaryExpression,
                parameterCurrent,
                parameterNext)
            .Compile();
    }
}