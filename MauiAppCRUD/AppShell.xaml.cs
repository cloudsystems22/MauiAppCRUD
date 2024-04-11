using MauiAppCRUD.View;

namespace MauiAppCRUD
{
    public partial class AppShell : Shell
    {
        public Dictionary<string, Type> Routes { get; private set; } = new Dictionary<string, Type>();
        public AppShell()
        {
            InitializeComponent();
            Register();
        }

        void Register()
        {
            Routes.Add("customers", typeof(CustomersPage));
            Routes.Add("customersave", typeof(CustomerSavePage));

            foreach (var item in Routes)
            {
                Routing.RegisterRoute(item.Key, item.Value);
            }
        }
    }
}
