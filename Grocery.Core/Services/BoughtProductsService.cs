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
                throw new NullReferenceException(nameof(productId));

            List<GroceryListItem> matchingItems = _groceryListItemsRepository
                .GetAll()
                .Where(li => li.ProductId == productId)
                .ToList();

            List<BoughtProducts> result = new List<BoughtProducts>();
            foreach (GroceryListItem listItem in matchingItems) {
                GroceryList? list = _groceryListRepository.Get(listItem.GroceryListId);
                if (list == null) 
                    continue;

                Client? client = _clientRepository.Get(list.ClientId);
                if (client == null) 
                    continue;

                Product? product = _productRepository.Get(listItem.ProductId);
                if (product == null) 
                    continue;

                result.Add(new BoughtProducts(client, list, product));
            }

            return result;
        }
    }
}
    