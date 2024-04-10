using MauiAppCRUD.ViewModel;

namespace MauiAppCRUD.View;

public partial class CustomersPage : ContentPage
{
    private readonly CustomerViewModel _customerViewModel;
    public CustomersPage(CustomerViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        _customerViewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _customerViewModel.LoadCustomersCommand.Execute(null);
    }


}