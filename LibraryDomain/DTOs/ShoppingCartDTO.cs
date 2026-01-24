using LibraryDomain.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDomain.DTOs
{
    public class ShoppingCartDTO
    {
        public List<BooksInShoppingCart> BooksInShoppingCart { get; set; } = new();

        public double TotalPrice { get; set; }
    }
}
