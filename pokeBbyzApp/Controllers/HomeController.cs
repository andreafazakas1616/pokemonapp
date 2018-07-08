using Microsoft.AspNet.Identity;
using pokeBbyzApp.BusinessLogic.Services;
using System;
using System.Web.Mvc;

namespace pokeBbyzApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;

        public HomeController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var identityUser = System.Web.HttpContext.Current.User;
            string id =  identityUser.Identity.GetUserId();

            if (String.IsNullOrEmpty(id))
            {
                return RedirectToAction("Login", "Authorization");
            }
            var user = _userService.FindUserById(id);
            if (user == null)
            {
                return RedirectToAction("Login", "Authorization");
            }
            else
            {
                if (_userService.HasStarterPokemon(user.ID))
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("ChooseStarter", "Player");
                }
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}