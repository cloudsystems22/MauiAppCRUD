using MauiAppCRUD.ViewModel;

namespace MauiAppCRUD.View;

public partial class CustomerSavePage : ContentPage
{
    public CustomerSavePage(CustomerViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

}