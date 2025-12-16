using LibraryDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Relationships
{
    public class BooksInShoppingCart
    {
        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public Guid ShoppingCartId { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }

        public int Quantity { get; set; }

    }
}