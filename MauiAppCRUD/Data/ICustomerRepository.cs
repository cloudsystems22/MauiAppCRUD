using MauiAppCRUD.Model;

namespace MauiAppCRUD.Data
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerAsync();

        Task<List<Customer>> GetCustomerNameAsync(string name);

        Task<Customer> GetCustomerByIdAsync(string id);

        Task<int> InsertOrUpdateCustomerAsync(Customer customer);

        Task<int> DeleteCustomerAsync(Customer customer);

    }
}
