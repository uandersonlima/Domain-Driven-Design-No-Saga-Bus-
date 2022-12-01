﻿using System.Linq.Expressions;
using AscoreStore.Core.Filter.Models;

namespace AscoreStore.Core.Filter.Interpreters
{
    public class StartsWithInterpreter<TType> : FilterTypeInterpreter<TType>
    {
        public StartsWithInterpreter(FilterItem filterItem) : base(filterItem)
        {
        }

        internal override Expression CreateExpression(MemberExpression property, ConstantExpression constant)
        {
            var method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
            return Expression.Call(property, method, constant);
        }
    }
}
