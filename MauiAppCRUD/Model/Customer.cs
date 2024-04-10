using SQLite;

namespace MauiAppCRUD.Model
{
    public class Customer
    {
        [PrimaryKey]
        public string Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public int Age { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }
    }
}
