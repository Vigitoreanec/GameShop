using System.Linq.Expressions;

namespace GameShop.Extensions;

public static class QueryableExtension
{
    public static IQueryable<TSource> Between<TSource, TKey>(
        this IQueryable<TSource> source,
        Expression<Func<TSource, TKey>> keySelector,
              TKey low, TKey high) where TKey : IComparable<TKey>
    {
        Expression key = Expression.Invoke(keySelector,
             keySelector.Parameters.ToArray());

        Expression lowerBound = Expression.GreaterThanOrEqual
            (key, Expression.Constant(low));

        Expression upperBound = Expression.LessThanOrEqual
            (key, Expression.Constant(high));

        Expression and = Expression.AndAlso(lowerBound, upperBound);

        Expression<Func<TSource, bool>> lambda =
            Expression.Lambda<Func<TSource, bool>>(and, keySelector.Parameters);

        return source.Where(lambda);
    }

}
