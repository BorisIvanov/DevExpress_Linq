using System;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.Data;
using DevExpress.Web.Mvc;



namespace LinqExtension
{
	public static partial class LINQExtension
	{
		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, GridViewModel gridViewModel)
		{
			IOrderedQueryable<T> resultQuery = null;
			ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
			foreach (GridViewColumnState column in gridViewModel.SortedColumns)
			{
				var property = typeof(T).GetProperty(column.FieldName);
				if (null != property)
				{
					var orderField = Expression.Lambda<Func<T, String>>(Expression.Convert(Expression.Property(parameter, property), typeof(String)), parameter);
					
					if (column.SortOrder == ColumnSortOrder.Ascending)
					{
						resultQuery = resultQuery == null ? query.OrderBy(orderField) : resultQuery.ThenBy(orderField);
					}
					else if (column.SortOrder == ColumnSortOrder.Descending)
					{
						resultQuery = resultQuery == null ? query.OrderByDescending(orderField) : resultQuery.ThenByDescending(orderField);
					}
				}
			}
			if (resultQuery == null)
			{
				var property = typeof(T).GetProperty("ID");
				var orderField = Expression.Lambda<Func<T, Int64>>(Expression.Convert(Expression.Property(parameter, property), typeof(Int64)), parameter);
				resultQuery = query.OrderBy(orderField);
			}
			return resultQuery;
		}
	
		public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string fieldName)
		{
			ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
			var property = typeof(T).GetProperty(fieldName);
			var orderField = Expression.Lambda<Func<T, String>>(Expression.Convert(Expression.Property(parameter, property), typeof(String)), parameter);
			return query.OrderBy(orderField);
		}

	}

}