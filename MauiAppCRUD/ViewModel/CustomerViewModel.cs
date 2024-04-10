using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppCRUD.Data;
using MauiAppCRUD.Model;
using MvvmHelpers;

namespace MauiAppCRUD.ViewModel
{
    public partial class CustomerViewModel : CommunityToolkit.Mvvm.ComponentModel.ObservableObject
    {
        public ObservableRangeCollection<Customer> Customers { get; set; } = new();

        [ObservableProperty]
        public Customer customer = new();

        [ObservableProperty]
        public string _id = string.Empty;

        private readonly ICustomerRepository _repository;

        public CustomerViewModel(ICustomerRepository repository)
        {
            _repository = repository;
            LoadCustomers();
        }


        [ObservableProperty]
        public string _name = "Novo Cliente:";


        [RelayCommand]
        public async Task NewCustomer()
        {
            Customer = new();
            
            await Shell.Current.GoToAsync("customersave");
        }

        [RelayCommand]
        public async Task Save()
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

        [RelayCommand]
        public async Task Delete(string id)
        {
            Customer = await GetCustomerByIdAsync(id);
            var response = await Shell.Current.DisplayActionSheet($"Deseja apagar? {Customer.Name}", null, null, "Sim", "Não");
            if(response == "Sim")
                await _repository.DeleteCustomerAsync(Customer);

            LoadCustomers();
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _repository.GetCustomerByIdAsync(id);
        }

        public async Task<int> InsertOrUpdate()
        {
            return await _repository.InsertOrUpdateCustomerAsync(Customer);
        }


        [RelayCommand]
        private async Task LoadCustomers()
        {
            _id = string.Empty;
            var customers = await _repository.GetCustomerAsync();

            if (Customers.Count > 0)
                Customers.Clear();

            if (customers.Count > 0)
                Customers.AddRange(customers);
        }

        [RelayCommand]
        private async Task SelectionChanged(string id)
        {
            Customer = await GetCustomerByIdAsync(id);
            _id = id;
            await Shell.Current.GoToAsync("customersave");
        }

        private async void CreateToast(string message)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CommunityToolkit.Maui.Core.ToastDuration duration = CommunityToolkit.Maui.Core.ToastDuration.Short;
            double fontSize = 15;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show(cts.Token);
        }

    }
}
