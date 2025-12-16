using LibraryDomain.Identity;
using LibraryDomain.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public string? OwnerId { get; set; }

        public virtual LibraryUser? LibraryUser { get; set; }

        public virtual ICollection<BooksInShoppingCart>? BooksInShoppingCart { get; set; }
    }
}
 