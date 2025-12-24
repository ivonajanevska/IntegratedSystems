using LibraryDomain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryService.Interfaces
{
    public interface IShoppingCartService
    {
        ShoppingCart? GetByOwner(string ownerId);

    }
}
