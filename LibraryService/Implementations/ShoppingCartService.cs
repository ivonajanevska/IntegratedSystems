using LibraryDomain.Domain;
using LibraryRepository.Interface;
using LibraryService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Implementations
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> shoppingCartRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public ShoppingCart? GetByOwner(string ownerId)
        {
            return shoppingCartRepository.Get(selector: cart => cart, 
                filter: cart => 
                cart.OwnerId != null 
                && cart.OwnerId.Equals(ownerId));
        }
    }
}
