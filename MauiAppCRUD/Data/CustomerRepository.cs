using MauiAppCRUD.Model;
using SQLite;

namespace MauiAppCRUD.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        SQLiteAsyncConnection Database;

        public CustomerRepository() { }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(SQLiteConstants.DatabasePath, SQLiteConstants.Flags);
            var result = await Database.CreateTableAsync<Customer>();
        }

        public async Task<List<Customer>> GetCustomerAsync()
        {
            await Init();
            return await Database.Table<Customer>().ToListAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            await Init();
            return await Database.Table<Customer>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Customer>> GetCustomerNameAsync(string name)
        {
            await Init();
            return await Database.Table<Customer>().Where(t => t.Name.Contains(name.ToUpper())).ToListAsync();
        }

        public async Task<int> InsertOrUpdateCustomerAsync(Customer customer)
        {
            await Init();
            if (!string.IsNullOrWhiteSpace(customer.Id))
            {
                return await Database.UpdateAsync(customer);
            }
            else
            {
                customer.Id = Guid.NewGuid().ToString().Substring(0, 4);
                return await Database.InsertAsync(customer);
            }
        }

        public async Task<int> DeleteCustomerAsync(Customer customer)
        {
            await Init();
            return await Database.DeleteAsync(customer);
        }

    }
}
