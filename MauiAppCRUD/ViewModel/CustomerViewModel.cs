using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppCRUD.Data;
using MauiAppCRUD.Model;
using System.Collections.ObjectModel;

namespace MauiAppCRUD.ViewModel
{
    public partial class CustomerViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        private ObservableCollection<Customer> _customers = new ObservableCollection<Customer>();
        public ObservableCollection<Customer> Customers 
        { 
            get => _customers;
            set => SetProperty(ref _customers, value);
        }

        public ObservableCollection<Customer> itemCustomers = new ObservableCollection<Customer>();

        [ObservableProperty]
        public Customer customer = new();

        [ObservableProperty]
        public string _id = string.Empty;

        private readonly ICustomerRepository _repository;

        public CustomerViewModel(ICustomerRepository repository)
        {
            _repository = repository;
        }

        [ObservableProperty]
        public string _name = "Novo Cliente:";

        [RelayCommand]
        public void Search(string searchBar)
        {

            if (string.IsNullOrEmpty(searchBar))
            {
                Customers = new ObservableCollection<Customer>(itemCustomers);
                return;
            }

            var customers = _customers.Where(c => c.Name.StartsWith(searchBar, StringComparison.InvariantCultureIgnoreCase));
            Customers = new ObservableCollection<Customer>(customers);

        }

        [RelayCommand]
        public async Task NewCustomer()
        {
            Customer = new();
            
            await Shell.Current.GoToAsync("customersave");
        }

        [RelayCommand]
        public async Task Save()
        {
            if(Validate(customer))
            {
                int result = await InsertOrUpdate();
                if (result > 0)
                {
                    CreateToast("Sucesso!");
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    CreateToast("Erro!");
                    await Shell.Current.GoToAsync("..");
                }
            }
            else
            {
                CreateToast("Favor informar pelo menos Nome e Fone!");
            }
            
        }

        [RelayCommand]
        public async Task Delete(string id)
        {
            Customer = await GetCustomerByIdAsync(id);
            var response = await Shell.Current.DisplayActionSheet($"Deseja apagar? {Customer.Name}", null, null, "Sim", "Não");
            if(response == "Sim")
                await _repository.DeleteCustomerAsync(Customer);

            LoadCustomers();
        }

        [RelayCommand]
        private async Task LoadCustomers()
        {
            _id = string.Empty;
            _name = "Novo Cliente";
            var customers = await _repository.GetCustomerAsync();
            itemCustomers = new ObservableCollection<Customer>(customers);

            if (Customers.Count > 0)
                Customers.Clear();

            if (customers.Count > 0)
                Customers = new ObservableCollection<Customer>(customers);
        }

        [RelayCommand]
        private async Task SelectionChanged(string id)
        {
            Customer = await GetCustomerByIdAsync(id);
            _id = id;
            _name = customer.Name;
            await Shell.Current.GoToAsync("customersave");
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _repository.GetCustomerByIdAsync(id);
        }

        public async Task<int> InsertOrUpdate()
        {
            return await _repository.InsertOrUpdateCustomerAsync(Customer);
        }

        private async void CreateToast(string message)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CommunityToolkit.Maui.Core.ToastDuration duration = CommunityToolkit.Maui.Core.ToastDuration.Short;
            double fontSize = 15;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cts.Token);
        }

        private bool Validate(Customer customer)
        {
            if(!string.IsNullOrEmpty(customer.Name) &&
                !string.IsNullOrEmpty(customer.Phone))
            {
                return true;
            }
            
            return false;
        }

    }
}
