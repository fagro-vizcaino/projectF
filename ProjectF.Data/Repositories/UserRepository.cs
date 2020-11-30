using System.Linq;
using ProjectF.Data.Context;
using ProjectF.Data.Entities.Auth;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.EntityFrameworkCore;

namespace ProjectF.Data.Repositories
{
    public class UserRepository : _BaseRepository<User>
    {
        readonly _AppDbContext _context;
        public UserRepository(_AppDbContext context) : base(context)
        {
            _context = context;
        }

        public Either<Error, User> FindByUsername(string email)
        {
            var user = _context.Users
              .Include(u => u.Country)
              .FirstOrDefault(u => u.Email == email);

            if (user == null) return Error.New("user not found");
            return user;
        }

        public bool UserExits(string email)
          => _context.Users.Any(u => u.Email == email);
    }
}