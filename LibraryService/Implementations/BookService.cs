using LibraryDomain.Domain;
using LibraryDomain.Relationships;
using LibraryRepository.Interface;
using LibraryService.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Implementations
{
    public class BookService : IBookService
    {
        public readonly IRepository<Book> bookRepository;
        public readonly IRepository<BooksInShoppingCart> booksInshoppingCartRepository;
        public BookService(IRepository<Book> bookRepository, IRepository<BooksInShoppingCart> booksInshoppingCartRepository)
        {
            this.bookRepository = bookRepository;
            this.booksInshoppingCartRepository = booksInshoppingCartRepository;
        }

        public void AddBookToCart(BooksInShoppingCart booksInShoppingCart)
        {
            var existingBookInShoppingCart = booksInshoppingCartRepository.Get(
                selector: b => b,
                filter: b => 
                b.BookId.Equals(booksInShoppingCart.BookId) 
                && b.ShoppingCartId.Equals(booksInShoppingCart.ShoppingCartId));

            if (existingBookInShoppingCart != null)
            {
                existingBookInShoppingCart.Quantity += booksInShoppingCart.Quantity;
                booksInshoppingCartRepository.Update(existingBookInShoppingCart);
            }

            else
            {
                booksInshoppingCartRepository.Create(booksInShoppingCart);
            }
        }

        public Book Create(Book book)
        {
            return bookRepository.Create(book);
        }

        public Book Delete(Guid id)
        {
            var book = GetById(id);

            if (book == null)
            {
                throw new Exception("Book not found");
            }
            return bookRepository.Delete(book);
        }

        public List<Book> GetAll()
        {
            return bookRepository.GetAll(selector: book => book).ToList();
        }

        public Book? GetById(Guid id)
        {
            return bookRepository.Get(selector: book => book, filter: book => book.Id.Equals(id));
        }

        public Book Update(Book book)
        {
            return bookRepository.Update(book);
        }
    }
}
