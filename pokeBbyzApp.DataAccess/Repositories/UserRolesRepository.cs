using System.Collections.Generic;
using System.Linq;

namespace pokeBbyzApp.DataAccess.Repositories
{
    public class UserRolesRepository
    {
        private readonly pokeEntities _context;
        private readonly UserRepository _userRepository;
        public UserRolesRepository(pokeEntities context, UserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }

        public bool CheckIfAdmin(int userId)
        {
            IEnumerable<UserRole> userRoles = _context.UserRoles.ToList();
            if (_userRepository.CheckIfAdmin(userId))
            {
                return true;
            }
            return false;
        }
    }
}
