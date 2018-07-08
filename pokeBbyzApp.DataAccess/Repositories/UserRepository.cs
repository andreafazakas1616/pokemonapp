using pokeBbyzApp.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace pokeBbyzApp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly pokeEntities _context;
        public UserRepository(pokeEntities context)
        {
            _context = context;
        }

        public User FindUserById(int id)
        {
            User user = _context.Users.Where(u => u.ID == id).First();
            return user;
        }

        public int AddNewUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.ID;
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void SetUserStarterPokemon(User user)
        {
            user.ChoseStarterPokemon = true;
            _context.SaveChanges();
        }

        public bool CheckIfAdmin(int userId)
        {
            if (_context.UserRoles.Any(ur => ur.User_ID == userId && ur.Role_ID == 1))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasStarterPokemon(int userId)
        {
            return _context.Users.Where(u => u.ID == userId).Any(u => u.ChoseStarterPokemon == true);
        }
    }
}
