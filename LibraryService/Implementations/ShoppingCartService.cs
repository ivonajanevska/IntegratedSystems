using LibraryDomain.Domain;
using LibraryDomain.Email;
using LibraryDomain.Relationships;
using LibraryRepository.Interface;
using LibraryService.Interfaces;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<BooksInOrder> booksInOrderRepository;
        private readonly IUserRepository _userRepository;
        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<BooksInOrder> booksInOrderRepository, IUserRepository userRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.orderRepository = orderRepository;
            this.booksInOrderRepository = booksInOrderRepository;
            this._userRepository = userRepository;
        }

        public ShoppingCart? GetByOwner(string ownerId)
        {
            return shoppingCartRepository
                .Get(selector: cart => cart, 
                    filter: cart => 
                        cart.OwnerId != null 
                        && cart.OwnerId.Equals(ownerId));
        }

        public ShoppingCart? GetByOwnerIncludeBooks(string ownerId)
        {
            return shoppingCartRepository
               .Get (selector: cart => cart,
                   filter: cart =>
                       cart.OwnerId != null
                       && cart.OwnerId.Equals(ownerId),
                   include: cart => 
                   cart.Include(i => i.BooksInShoppingCart)
                   .ThenInclude(y => y.Book));
        }

        public void OrderShoppingCart(string ownerId)
        {
            var shoppingCart = GetByOwnerIncludeBooks (ownerId);

            if (shoppingCart ==  null)
            {
                throw new Exception("Shopping cart not found");
            }    

            var order = new Order
            {
                Id = Guid.NewGuid(),
                OwnerId = ownerId,
            };

            orderRepository.Create(order);

            var booksInOrder = shoppingCart.BooksInShoppingCart?.Select(b => new BooksInOrder()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                BookId = b.BookId,
                Quantity = b.Quantity,
                Order = order,
                Book = b.Book,

            }).ToList();

            if (booksInOrder == null )
            {
                throw new Exception("Books in order not found");
            }

            foreach (var bookInOrder in booksInOrder)
            {
                booksInOrderRepository.Create(bookInOrder);
            }

            shoppingCart.BooksInShoppingCart?.Clear();
            shoppingCartRepository.Update(shoppingCart);

            var user = _userRepository.GetUserById(ownerId);

            if (user != null && user.Email != null) {
                var emailMessage = new EmailMessage
                {
                    Subject = "OrderInformation",
                    Body = $"Your order has been placed successfully. Oder ID: {order.Id}",
                    SendTo = ownerId,
                };
            }



        }
    }
}
