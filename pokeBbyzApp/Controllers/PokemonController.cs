using Nelibur.ObjectMapper;
using pokeBbyzApp.BusinessLogic.Interfaces;
using pokeBbyzApp.DataAccess;
using pokeBbyzApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace pokeBbyzApp.Controllers
{
    public class PokemonController : Controller
    {
        private readonly IPokemonSpeciesService _pokemonSpeciesService;
        private readonly IPokemonTypesService _pokemonTypesService;

        public PokemonController(IPokemonSpeciesService pokemonSpeciesService, IPokemonTypesService pokemonTypesService)
        {
            _pokemonSpeciesService = pokemonSpeciesService;
            _pokemonTypesService = pokemonTypesService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int pokemonId = 0)
        {
            PokemonSpecy pokemonEntity = _pokemonSpeciesService.FindPokemonSpeciesById(pokemonId);
            TinyMapper.Bind<PokemonSpecy, PokemonSpeciesViewModel>();
            PokemonSpeciesViewModel model = TinyMapper.Map<PokemonSpeciesViewModel>(pokemonEntity);

            List<PokemonType> pokemonTypeList = _pokemonTypesService.GetAllTypes();
            model.Types = pokemonTypeList.Select(e => new PokemonTypeViewModel() { ID = e.ID, Name = e.Name }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PokemonSpeciesViewModel model)
        {
            if (model.UploadedFrontImage != null)
            {
                byte[] uploadedFrontFile = new byte[model.UploadedFrontImage.InputStream.Length];
                model.UploadedFrontImage.InputStream.Read(uploadedFrontFile, 0, uploadedFrontFile.Length);
                model.FrontImage = Convert.ToBase64String(uploadedFrontFile);
            }

            if (model.UploadedBackImage != null)
            {
                byte[] uploadedBackFile = new byte[model.UploadedBackImage.InputStream.Length];
                model.UploadedBackImage.InputStream.Read(uploadedBackFile, 0, uploadedBackFile.Length);
                model.BackImage = Convert.ToBase64String(uploadedBackFile);
            }

            TinyMapper.Bind<PokemonSpeciesViewModel, PokemonSpecy>();
            PokemonSpecy pokemonEntity = TinyMapper.Map<PokemonSpecy>(model);

            pokemonEntity.PokemonType = TinyMapper.Map<PokemonType>(model.PokemonType);
            pokemonEntity.PokemonType1 = model.PokemonType1 != null ? TinyMapper.Map<PokemonType>(model.PokemonType1) : null;

            _pokemonSpeciesService.UpdatePokemonSpecies(pokemonEntity);

            return RedirectToAction("Index", "Admin");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int pokemonId = 0)
        {
            PokemonSpecy entity = _pokemonSpeciesService.FindPokemonSpeciesById(pokemonId);
            TinyMapper.Bind<PokemonSpecy, PokemonSpeciesViewModel>();
            PokemonSpeciesViewModel model = TinyMapper.Map<PokemonSpeciesViewModel>(entity);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int pokemonId = 0)
        {
            PokemonSpecy entity = _pokemonSpeciesService.FindPokemonSpeciesById(pokemonId);
            TinyMapper.Bind<PokemonSpecy, PokemonSpeciesViewModel>();
            PokemonSpeciesViewModel model = TinyMapper.Map<PokemonSpeciesViewModel>(entity);
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePokemon(int pokemonId = 0)
        {
            _pokemonSpeciesService.Delete(pokemonId);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            PokemonSpeciesViewModel model = new PokemonSpeciesViewModel();
            List<PokemonType> pokemonTypeList = _pokemonTypesService.GetAllTypes();
            model.Types = pokemonTypeList.Select(e => new PokemonTypeViewModel() { ID = e.ID, Name = e.Name }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(PokemonSpeciesViewModel model)
        {
            if (ModelState.IsValid)
            {
                TinyMapper.Bind<PokemonSpeciesViewModel, PokemonSpecy>();
                PokemonSpecy pokemonEntity = TinyMapper.Map<PokemonSpecy>(model);

                pokemonEntity.PokemonType = TinyMapper.Map<PokemonType>(model.PokemonType);
                pokemonEntity.PokemonType1 = model.PokemonType1 != null ? TinyMapper.Map<PokemonType>(model.PokemonType1) : null;

                _pokemonSpeciesService.Add(pokemonEntity);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                List<PokemonType> pokemonTypeList = _pokemonTypesService.GetAllTypes();
                model.Types = pokemonTypeList.Select(e => new PokemonTypeViewModel() { ID = e.ID, Name = e.Name }).ToList();
                return View(model);
            }
        }

        public ActionResult StarterPokemon(int id, bool isStarter)
        {
            PokemonSpecy entity = _pokemonSpeciesService.FindPokemonSpeciesById(id);
            _pokemonSpeciesService.SetStarterPokemonStatus(id, isStarter);
            return View();
        }
    }
}