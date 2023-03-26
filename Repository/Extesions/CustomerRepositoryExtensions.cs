using Entities.Models;

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
    }
}
