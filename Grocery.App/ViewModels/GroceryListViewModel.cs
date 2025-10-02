using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;
using System.Collections.ObjectModel;
using Grocery.Core.Models.Enums;

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;
        private readonly GlobalViewModel _global;

        public GroceryListViewModel(IGroceryListService groceryListService, GlobalViewModel global) 
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _global = global;
            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }
        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }

        public void ShowBoughtProducts() {
            Client currentClient = _global.Client;
            if (currentClient.UserRole == Enums.Role.Admin)
                Shell.Current.GoToAsync(nameof(Views.BoughtProductsView));
            else
                throw new InvalidNavigationException();
        }
    }
}
