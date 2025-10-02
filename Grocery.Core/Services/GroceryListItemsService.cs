using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services {
    public class GroceryListItemsService : IGroceryListItemsService {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBoughtProductsService _iBoughtProductsService;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository, IBoughtProductsService iboughtProductsService) {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
            _iBoughtProductsService = iboughtProductsService;
        }

        public List<GroceryListItem> GetAll() {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId) {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item) {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item) {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id) {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item) {
            return _groceriesRepository.Update(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5) {
            List<BestSellingProducts> rankedProducts = new();
            List<Product> products = _productRepository.GetAll();
            if (products.Count == 0) return rankedProducts;

            foreach (var product in products) {
                List<BoughtProducts> boughtProductsList = _iBoughtProductsService.Get(product.Id);
                if (boughtProductsList.Count == 0)
                    continue;

                int soldAmount = 0;

                foreach (BoughtProducts boughtProduct in boughtProductsList) {
                    List<GroceryListItem> groceryListItems = _groceriesRepository.GetAllOnGroceryListId(boughtProduct.GroceryList.Id);
                    soldAmount += groceryListItems
                        .Where(li => li.ProductId == product.Id)
                        .Sum(li => li.Amount);
                }

                if (soldAmount <= 0)
                    continue;

                rankedProducts.Add(new BestSellingProducts(
                    product.Id,
                    product.name,
                    product.Stock,
                    soldAmount,
                    0
                ));
            }

            rankedProducts = rankedProducts
                .OrderByDescending(p => p.nrOfSells)
                .Take(topX)
                .ToList();

            for (int i = 0; i < rankedProducts.Count; i++)
                rankedProducts[i].ranking = i + 1;

            return rankedProducts;
        }

        private void FillService(List<GroceryListItem> groceryListItems) {
            foreach (GroceryListItem g in groceryListItems) {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
