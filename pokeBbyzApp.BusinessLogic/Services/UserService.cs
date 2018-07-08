using pokeBbyzApp.DataAccess;
using pokeBbyzApp.DataAccess.Interfaces;
using pokeBbyzApp.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pokeBbyzApp.BusinessLogic.Services
{
    public class UserService: IUserService
    {
        private readonly PokemonService _pokemonService;
        private readonly PokemonRepository _pokemonRepository;
        private readonly UserRepository _userRepository;

        public UserService(PokemonService pokemonService, PokemonRepository pokemonRepository, UserRepository userRepository)
        {
            _pokemonService = pokemonService;
            _pokemonRepository = pokemonRepository;
            _userRepository = userRepository;
        }
        
        public void SetUserStarterPokemon(int userId)
        {
            User user = _userRepository.FindUserById(userId);
            _userRepository.SetUserStarterPokemon(user);
        }

        public bool CheckIfValidUsernameAndEmail(User user)
        {
            bool isValid = true;

            List<User> userList = _userRepository.GetUsers();
            if(userList.Any(u=>u.Username==user.Username || u.Email==user.Email))
            {
                isValid = false;
            }
            return isValid;
        }

        public bool HasStarterPokemon(int userId)
        {
            return _userRepository.HasStarterPokemon(userId);
        }

        public User FindUserById(object userId)
        {
            return _userRepository.FindUserById(Convert.ToInt32(userId));
        }
    }
}
