using System.Linq.Expressions;

namespace Sigmatech.Helpers.Params
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
        Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                expr1 = f => false;
            }
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                (Expression.OrElse(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
            Expression<Func<T, bool>> expr2)
        {
            if (expr1 == null)
            {
                expr1 = f => true;
            }
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                (Expression.AndAlso (expr1.Body, invokedExpr), expr1.Parameters);
        }
    }
}