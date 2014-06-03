using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using gcss.Models.Grid;



namespace LinqExtension
{
	public static partial class LINQExtension
	{
		
		private static readonly MethodInfo ContainsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
		private static readonly MethodInfo ToUpperMethod = typeof(string).GetMethod("ToUpper", new Type[] { });

		public static IQueryable<T> Where<T>(this IQueryable<T> query, Dictionary<string, ComboFilterField<T>> comboFilterList)
		{
			Expression whereExpression = null;
			ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
			foreach (KeyValuePair<string, ComboFilterField<T>> item in comboFilterList)
			{
				if (string.IsNullOrEmpty(item.Value.FilterValue))
					continue;
				
				var property = typeof(T).GetProperty(item.Key);
				if (null != property)
				{
					var filterValue = new Expression[] { Expression.Constant(item.Value.FilterValue.ToUpper()) };

					MemberExpression memberExpression = Expression.MakeMemberAccess(parameter, property);
					Expression upperFieldExpression = Expression.Call(memberExpression, ToUpperMethod);
					Expression call = Expression.Call(upperFieldExpression, ContainsMethod, filterValue);
					if (whereExpression == null)
					{
						whereExpression = call;
					}
					else
					{
						whereExpression = Expression.And(whereExpression, call);
					}
				}
			}

			if (whereExpression == null)
			{
				return query;
			}
			return query.Where(Expression.Lambda<Func<T, bool>>(whereExpression, parameter));
		}

		public static IQueryable<T> Where<T>(this IQueryable<T> query, ComboFilterField<T> comboFilter)
		{
			if (string.IsNullOrEmpty(comboFilter.FilterValue))
				return query;
			
			ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
			var property = typeof(T).GetProperty(comboFilter.FieldName);
			if (property == null)
				return query;

			var filterValue = new Expression[] { Expression.Constant(comboFilter.FilterValue.ToUpper()) };
			MemberExpression memberExpression = Expression.MakeMemberAccess(parameter, property);
			Expression upperFieldExpression = Expression.Call(memberExpression, ToUpperMethod);
			Expression whereExpression = Expression.Call(upperFieldExpression, ContainsMethod, filterValue);
			return query.Where(Expression.Lambda<Func<T, bool>>(whereExpression, parameter));
		}

	}

}