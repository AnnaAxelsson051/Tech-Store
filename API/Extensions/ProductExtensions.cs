using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

//Class handeling all actions with product entities

namespace API.Extensions
{
    public static class ProductExtensions
    {
        public static IQueryable<Product> Sort(this IQueryable<Product> query, string orderBy)

//if nothing in orderBy variable
        if (string.IsNullOrWhiteSpace(orderBy))return query.OrderBy(p = p.Name);
			query = orderBy switch 
			{
                //various search options
				"price" => query.OrderBy(p => p.Price)
				"priceDesc" => query.OrderByDescending(p => p.Price)
				_ => query.OrderBy(p => p.Name)
			};
            return query;
    }
}