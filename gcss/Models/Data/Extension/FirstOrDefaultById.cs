using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;



namespace LinqExtension
{
	public static partial class LINQExtension
	{

		private static readonly MethodInfo EqualsMethodInt64 = typeof(Int64).GetMethod("Equals", new[] { typeof(Int64) });

		public static T FirstOrDefaultById<T>(this IQueryable<T> query, Int64 id)
		{
			var property = typeof(T).GetProperty("ID");
			if (property == null)
				throw new Exception();

			var filterValue = new Expression[] { Expression.Constant(id) };
			
			ParameterExpression parameter = Expression.Parameter(typeof(T), "t");
			MemberExpression memberExpression = Expression.MakeMemberAccess(parameter, property);
			Expression whereExpression = Expression.Call(memberExpression, EqualsMethodInt64, filterValue);
			var where = Expression.Lambda<Func<T, bool>>(whereExpression, parameter);

			return query.FirstOrDefault(where);
		}

		public static T FirstOrDefaultById<T>(this IQueryable<T> query, T item)
		{
			object id = item.GetType().GetProperty("ID").GetValue(item);
			return query.FirstOrDefaultById(Convert.ToInt64(id));
		}
	}

}