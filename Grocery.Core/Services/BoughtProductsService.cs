
using System.ComponentModel;
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services {
    public class BoughtProductsService : IBoughtProductsService {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;
        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository) {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }
        public List<BoughtProducts> Get(int? productId) {
            if (productId == null)
                throw new NullReferenceException();

            Product selectedProduct = _productRepository.Get((int)productId) ?? throw new InvalidOperationException();
            List<GroceryListItem> listItems = _groceryListItemsRepository.GetAll();
            List<BoughtProducts> returnProductsList = new List<BoughtProducts>();

            foreach (GroceryListItem item in listItems) {
                GroceryList list = _groceryListRepository.Get(item.GroceryListId) ?? throw new InvalidOperationException();
                Client user = _clientRepository.Get(list.ClientId) ?? throw new InvalidOperationException();
                returnProductsList.Add(new BoughtProducts(user, list, selectedProduct));
            }

            return returnProductsList ?? throw new NullReferenceException();
        }
    }
}
