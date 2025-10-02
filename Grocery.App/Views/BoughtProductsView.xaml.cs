using Grocery.App.ViewModels;
using Grocery.Core.Models;
using Grocery.Core.Models.Enums;

namespace Grocery.App.Views;

public partial class BoughtProductsView : ContentPage
{
    private readonly BoughtProductsViewModel _viewModel;

    public BoughtProductsView(BoughtProductsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
       
        BindingContext = viewModel;
    }

   
    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        int selectedIndex = picker.SelectedIndex;

        if (selectedIndex != -1)
        {
            Product product = picker.SelectedItem as Product;
            _viewModel.NewSelectedProduct(product);
        }
    }
}