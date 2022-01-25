namespace Common.ExtensionLibrary;

using System;
using System.Linq.Expressions;

public static class GenericExtensions
{
    public static TNumeric? GetArithmeticMean<TNumeric>(
        this IEnumerable<TNumeric> values)
        where TNumeric : IConvertible
        => values is null
            ? default
            : Divide(
                Add(values.ToArray()),
                values.Count().As<TNumeric>());

    public static TNumeric? GetGeometricMean<TNumeric>(
        this IEnumerable<TNumeric> values)
        where TNumeric : IConvertible
        => values is null
            ? default
            : Power(
                Product(values.ToArray()),
                (1.0d / values.Count()).As<TNumeric>());

    public static TNumeric Add<TNumeric>(
        params TNumeric[] values)
    {
        Func<TNumeric, TNumeric, TNumeric> addFunction
            = GetExpressionResult<TNumeric>(Expression.Add);

        return values.Aggregate(addFunction);
    }

    public static TNumeric Divide<TNumeric>(
        TNumeric dividend,
        TNumeric divisor)
    {
        Func<TNumeric, TNumeric, TNumeric> divideFunction
            = GetExpressionResult<TNumeric>(Expression.Divide);

        return divideFunction(
            dividend,
            divisor);
    }

    public static TNumeric? Power<TNumeric>(
        TNumeric leftOperand,
        TNumeric? rightOperand)
    {
        if (rightOperand is null)
        {
            throw new ArgumentNullException(nameof(rightOperand));
        }

        Func<TNumeric, TNumeric, TNumeric> powerFunction
            = GetExpressionResult<TNumeric>(Expression.Power);

        return powerFunction(
            leftOperand,
            rightOperand);
    }

    public static TNumeric Power<TNumeric>(
        params TNumeric[] values)
    {
        Func<TNumeric, TNumeric, TNumeric>? powerFunction
            = GetExpressionResult<TNumeric>(Expression.Power);

        return values.Aggregate(powerFunction);
    }

    public static TNumeric Product<TNumeric>(
        params TNumeric[] values)
    {
        Func<TNumeric, TNumeric, TNumeric> multiplyFunction
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