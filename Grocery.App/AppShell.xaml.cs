    using CommunityToolkit.Mvvm.DependencyInjection;
using Grocery.App.ViewModels;
using Grocery.App.Views;
using Grocery.Core.Models.Enums;
using static Grocery.Core.Models.Enums.Enums;

namespace Grocery.App {
    public partial class AppShell : Shell {
        private GlobalViewModel? _global;

        public AppShell() {
            InitializeComponent();
            RegisterRoutes();
        }

        public void RefreshAfterLogin() {
            TryResolveGlobal();
            UpdateAdminTabVisibility();
        }

        private void TryResolveGlobal() {
            _global ??= Handler?.MauiContext?.Services.GetService<GlobalViewModel>();
        }

        private void RegisterRoutes() {
            Routing.RegisterRoute(nameof(GroceryListItemsView), typeof(GroceryListItemsView));
            Routing.RegisterRoute(nameof(ProductView), typeof(ProductView));
            Routing.RegisterRoute(nameof(ChangeColorView), typeof(ChangeColorView));
            Routing.RegisterRoute("Login", typeof(LoginView));
            Routing.RegisterRoute(nameof(BestSellingProductsView), typeof(BestSellingProductsView));
            Routing.RegisterRoute(nameof(BoughtProductsView), typeof(BoughtProductsView));
        }

        private void UpdateAdminTabVisibility() {
            if (BoughtProductsTab is not null && _global?.Client?.UserRole == Role.Admin)
                BoughtProductsTab.IsVisible = true;
        }
    }
}
