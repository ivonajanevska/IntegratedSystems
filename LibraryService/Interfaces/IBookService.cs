using LibraryDomain.Domain;
using LibraryDomain.Relationships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Interfaces
{
    public interface IBookService
    {

        Book Create(Book book);
        Book Update(Book book);
        Book Delete(Guid id);

        Book? GetById(Guid id);
        List<Book> GetAll();
        void AddBookToCart(BooksInShoppingCart booksInShoppingCart);
    }
}
