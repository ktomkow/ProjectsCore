using System;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Parser;
using System.Linq.Expressions;

namespace ProjectsCore.DynamicLambda
{
    public static class DynamicLambdaFactory
    {
        public static Func<TInput, TOutput> CreateFunc<TInput, TOutput>(this string lambda)
        {
            ParameterExpression x = Expression.Parameter(typeof(TInput), "x");

            var symbols = new[] { x };

            Expression body = new ExpressionParser(symbols, lambda, symbols, new ParsingConfig()).Parse(typeof(TOutput));

            LambdaExpression e = Expression.Lambda(body, new ParameterExpression[] { x });

            Func<TInput, TOutput> func = e.Compile() as Func<TInput, TOutput>;

            return func;
        }
    }
}
