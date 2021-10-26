using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Parser;
using System.Linq.Expressions;

namespace ProjectsCore.DynamicLambda
{
    public static class DynamicLambdaFactory
    {
        private static readonly Dictionary<string, Delegate> functions = new Dictionary<string, Delegate>();

        public static Func<TInput, TOutput> CreateFunc<TInput, TOutput>(this string lambda)
        {
            if(functions.TryGetValue(lambda, out var dupa))
            {
                return dupa as Func<TInput, TOutput>;
            }

            var del = Construct<TInput, TOutput>(lambda);

            functions.Add(lambda, del);

            return del as Func<TInput, TOutput>; ;
        }

        public static Delegate Construct<TInput, TOutput>(string lambda)
        {
            ParameterExpression x = Expression.Parameter(typeof(TInput), "x");

            var symbols = new[] { x };

            Expression body = new ExpressionParser(symbols, lambda, symbols, new ParsingConfig()).Parse(typeof(TOutput));

            LambdaExpression e = Expression.Lambda(body, new ParameterExpression[] { x });

            Delegate compiled = e.Compile(); ;
            return compiled;
        }
    }
}
