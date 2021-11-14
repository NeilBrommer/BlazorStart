using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Start.Server {
	/// <summary>Extension methods for LINQ queries</summary>
	public static class LinqExtensions {
		/// <summary>
		/// If <paramref name="condition"/> is true, then returns <c>query.Where(expression)</c>.
		/// Otherwise returns the <paramref name="query"/> unchanged.
		/// </summary>
		/// <param name="query">The query to potentially apply the expression to</param>
		/// <param name="condition">
		/// Determines whether or not the expression should be applied
		/// </param>
		/// <param name="expression">A function to test each element</param>
		public static IQueryable<T> IfWhere<T>(this IQueryable<T> query, bool condition,
			Expression<Func<T, bool>> expression) {
			if (condition)
				return query.Where(expression);

			return query;
		}

		/// <summary>
		/// If the <paramref name="condition"/> is true, apply the <paramref name="transform"/> to
		/// the query, otherwise return the query unchanged.
		/// </summary>
		/// <typeparam name="T">The <paramref name="query"/>'s type</typeparam>
		/// <param name="query">The query to potentially transform</param>
		/// <param name="condition">
		/// If true, apply the <paramref name="transform"/> to the <paramref name="query"/>
		/// </param>
		/// <param name="transform">
		/// A function to apply to the <paramref name="query"/> if the <paramref name="condition"/>
		/// is true
		/// </param>
		public static IQueryable<T> If<T>(this IQueryable<T> query, bool condition,
			Func<IQueryable<T>, IQueryable<T>> transform) {
			if (condition)
				return transform(query);

			return query;
		}
	}
}
