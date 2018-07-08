using Nelibur.ObjectMapper;
using pokeBbyzApp.BusinessLogic.Services;
using pokeBbyzApp.DataAccess;
using pokeBbyzApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using OfficeOpenXml;
using System.Web;
using System;
using pokeBbyzApp.BusinessLogic.Helpers;

namespace pokeBbyzApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly PokemonSpeciesService _pokemonSpeciesService;
        private readonly PokemonTypesService _pokemonTypesService;
        private readonly FileUploadHelper _fileUploadHelper;

        public AdminController(PokemonSpeciesService pokemonSpeciesService, PokemonTypesService pokemonTypesService, FileUploadHelper fileUploadHelper)
        {
            _pokemonSpeciesService = pokemonSpeciesService;
            _pokemonTypesService = pokemonTypesService;
            _fileUploadHelper = fileUploadHelper;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            AdminPageViewModel adminPageViewModel = new AdminPageViewModel();
            List<PokemonSpecy> pokemonSpecies = _pokemonSpeciesService.GetAllSpecies();

            adminPageViewModel.PokemonSpeciesList = pokemonSpecies.Select(ps => new PokemonSpeciesViewModel()
            {
                ID = ps.ID,
                PokemonType = TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType),
                PokemonType1 = ps.PokemonType1 != null ? TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType1) : null,
                Name = ps.Name,
                FrontImage = ps.FrontImage,
                BackImage = ps.BackImage,
                IsStarterPokemon = ps.IsStarterPokemon
            }).ToList();
            return View(adminPageViewModel);
        }

        [HttpGet]
        public ActionResult UploadPokemonExcel()
        {
            return PartialView("_PokemonExcelForm");
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult UploadPokemonExcel(ExcelViewModel model)
        {
            AdminPageViewModel adminPageViewModel = new AdminPageViewModel();

            if (Request != null)
            {
                List<PokemonSpecy> uploadedPokemonList = new List<PokemonSpecy>();
                List<string> errors = new List<string>();
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if (file.ContentType.Contains("spreadsheet") || file.FileName.Contains(".xls"))
                {
                    uploadedPokemonList = _fileUploadHelper.UploadPokemonFromExcel(file, errors);
                    adminPageViewModel.Errors.AddRange(errors);
                    _pokemonSpeciesService.AddPokemonList(uploadedPokemonList);
                }
                else
                {
                    adminPageViewModel.Errors.Add("You must upload an Excel file.");
                }
            }

            List<PokemonSpecy> pokemonSpecies = _pokemonSpeciesService.GetAllSpecies();
            adminPageViewModel.PokemonSpeciesList = pokemonSpecies.Select(ps => new PokemonSpeciesViewModel()
            {
                ID = ps.ID,
                PokemonType = TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType),
                PokemonType1 = ps.PokemonType1 != null ? TinyMapper.Map<PokemonTypeViewModel>(ps.PokemonType1) : null,
                Name = ps.Name,
                FrontImage = ps.FrontImage,
                BackImage = ps.BackImage,
                IsStarterPokemon = ps.IsStarterPokemon
            }).ToList();

            return View("Index", adminPageViewModel);
        }
    }
}
