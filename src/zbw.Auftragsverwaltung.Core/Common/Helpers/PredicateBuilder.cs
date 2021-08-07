using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.Common.Helpers
{
    public static class PredicateBuilder
    {
        public static Expression<Func<TType, bool>> Or<TType>(this Expression<Func<TType, bool>> ex1,
            Expression<Func<TType, bool>> ex2)
        {
            var invokedEx = Expression.Invoke(ex2, ex1.Parameters);
            return Expression.Lambda<Func<TType, bool>>(Expression.OrElse(ex1.Body, invokedEx), ex1.Parameters);
        }

        public static Expression<Func<TType, bool>> And<TType>(this Expression<Func<TType, bool>> ex1,
            Expression<Func<TType, bool>> ex2)
        {
            var invokedEx = Expression.Invoke(ex2, ex1.Parameters);
            return Expression.Lambda<Func<TType, bool>>(Expression.AndAlso(ex1.Body, invokedEx), ex1.Parameters);
        }
        public static Expression<Func<T, bool>> True<T>() { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }
    }
}
