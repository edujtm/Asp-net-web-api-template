using Entities.Models;
using System.Linq.Dynamic.Core;

namespace Repository.Extesions
{
    public static class CustomerRepositoryExtensions
    {
        public static IQueryable<Customer> Search(this IQueryable<Customer> customers, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return customers;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return customers.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Customer> Sort(this IQueryable<Customer> customers, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return customers.OrderBy(e => e.Name);

            var orderByQuery = OrderByQueryBuilder.CreateOrderQuery<Customer>(orderByQueryString);

            return (string.IsNullOrWhiteSpace(orderByQuery)) ?
                customers.OrderBy(e => e.Name) :
                customers.OrderBy(orderByQuery);
        }
    }
}
