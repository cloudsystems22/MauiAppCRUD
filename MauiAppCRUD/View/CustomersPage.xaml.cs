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

        if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            CollectionCustomers.ItemsLayout = LinearItemsLayout.Vertical;
        else
            CollectionCustomers.ItemsLayout = new GridItemsLayout(3, ItemsLayoutOrientation.Vertical);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _customerViewModel.LoadCustomersCommand.Execute(null);
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (string.IsNullOrEmpty(e.NewTextValue))
            if (((SearchBar)sender).SearchCommand?.CanExecute(e.NewTextValue) == true)
                ((SearchBar)sender).SearchCommand?.Execute(e.NewTextValue);

    }

}