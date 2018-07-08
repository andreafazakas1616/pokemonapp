using Microsoft.AspNet.Identity;
using Nelibur.ObjectMapper;
using pokeBbyzApp.BusinessLogic.Services;
using pokeBbyzApp.DataAccess;
using pokeBbyzApp.DataAccess.Repositories;
using pokeBbyzApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace pokeBbyzApp.Controllers
{
    public class PlayerController : Controller
    {
        private readonly UserService _userService;
        private readonly PokemonSpeciesService _pokemonSpeciesService;
        private readonly PokemonService _pokemonService;
        private readonly UserRepository _userRepository;

        public PlayerController(UserService userService, PokemonSpeciesService pokemonSpeciesService, PokemonService pokemonService, UserRepository userRepository)
        {
            _userService = userService;
            _pokemonSpeciesService = pokemonSpeciesService;
            _pokemonService = pokemonService;
            _userRepository = userRepository;
        }
        // GET: Player
        public ActionResult ChooseStarter()
        {
            AdminPageViewModel model = new AdminPageViewModel();
            List<PokemonSpecy> pokemonSpecies = _pokemonSpeciesService.GetAllSpecies();
            model.PokemonSpeciesList = pokemonSpecies.Where(ps => ps.IsStarterPokemon == true).Select(ps => new PokemonSpeciesViewModel()
            {
                ID = ps.ID,
                PokemonType = TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType),
                PokemonType1 = ps.PokemonType1 != null ? TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType1) : null,
                Name = ps.Name,
                FrontImage = ps.FrontImage,
                BackImage = ps.BackImage,
            }).ToList();
            return View(model);
        }

        public ActionResult CreateStarterPokemon(int id)
        {
            PokemonViewModel model = new PokemonViewModel();
            var identityUser = System.Web.HttpContext.Current.User;
            string userId = identityUser.Identity.GetUserId();
            User user = _userService.FindUserById(userId);
            model.OwnerID = user.ID;
            model.Species = id;
            model.Gender = _pokemonService.GetPokemonGender();
            return PartialView("_CreateStarter", model);
        }

        [HttpPost]
        public ActionResult CreateStarter(PokemonViewModel pokemonViewModel)
        {
           TinyMapper.Bind<PokemonViewModel, Pokemon>(config =>
            {
                config.Ignore(x => x.Gender);
            });

            Pokemon pokemonEntity = TinyMapper.Map<Pokemon>(pokemonViewModel);
            if (pokemonViewModel.Gender == "Female")
            {
                pokemonEntity.Gender = true;
            }
            else
            {
                pokemonEntity.Gender = false;
            }

            _userService.SetUserStarterPokemon(pokemonViewModel.OwnerID);
            _pokemonService.SetPokemonStats(pokemonEntity);
            return RedirectToAction("PlayerProfile");
        }

        public ActionResult PlayerProfile()
        {
            return View();
        }
    }
}