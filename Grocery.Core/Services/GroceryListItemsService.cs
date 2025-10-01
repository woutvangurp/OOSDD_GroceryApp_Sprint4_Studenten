using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;
        private readonly IBoughtProductsService _iBoughtProductsService;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository, IBoughtProductsService iboughtProductsService)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
            _iBoughtProductsService = iboughtProductsService;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem Add(GroceryListItem item)
        {
            return _groceriesRepository.Add(item);
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Get(int id)
        {
            return _groceriesRepository.Get(id);
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        public List<BestSellingProducts> GetBestSellingProducts(int topX = 5)
        {
            Dictionary<string, int> boughtDictionary = new Dictionary<string, int>(); 
            int productCount = _productRepository.GetAll().Count;
            for (int i = 1; i < productCount; i++)
            {
                List<BoughtProducts> boughtProductsList = _iBoughtProductsService.Get(productCount);
                string productName = boughtProductsList.FirstOrDefault().Product.name ?? throw new NullReferenceException();
                int soldAmount = 0;
                foreach (BoughtProducts boughtProduct in boughtProductsList)
                {
                    List<GroceryListItem> groceryListItems = _groceriesRepository.GetAllOnGroceryListId(boughtProduct.GroceryList.Id);
                    soldAmount += groceryListItems.Where(listItem => listItem.Product.name == productName).Sum(listItem => listItem.Amount);
                }
                boughtDictionary.Add(productName, soldAmount);
                productName = string.Empty;
                soldAmount = 0;
            }

            //boughtdict zitten alle producten in die verkocht worden met de hoeveelheid dat ze in de

        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}
