using LibraryDomain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepository.Interface
{
    public interface IUserRepository
    {
        LibraryUser GetUserById(string id);

    }
}
