using LibraryDomain.Identity;
using LibraryRepository.Interface;
using LibraryWeb.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private readonly DbSet<LibraryUser> entities;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<LibraryUser>();
        }
        public LibraryUser GetUserById(string id)
        {
            return entities.First(user => user.Id.Equals(id));
        }
    }
}
