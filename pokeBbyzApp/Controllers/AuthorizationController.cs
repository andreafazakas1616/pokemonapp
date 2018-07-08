using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using pokeBbyzApp.DataAccess;
using Microsoft.Owin.Security;
using pokeBbyzApp.Models;
using Microsoft.AspNet.Identity;
using Nelibur.ObjectMapper;
using pokeBbyzApp.DataAccess.Interfaces;
using pokeBbyzApp.DataAccess.Repositories;

namespace pokeBbyzApp.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly UserRolesRepository _userRolesRepository;

        public AuthorizationController(IUserService userService, IUserRepository userRepository, UserRolesRepository userRolesRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
            _userRolesRepository = userRolesRepository;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            IEnumerable<User> users = _userRepository.GetUsers();

            User currentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            if(currentUser!=null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, currentUser.Username),
                    new Claim(ClaimTypes.NameIdentifier, currentUser.ID.ToString()),
                    new Claim(ClaimTypes.Role, "User"),
                }, DefaultAuthenticationTypes.ApplicationCookie);

                if(_userRolesRepository.CheckIfAdmin(currentUser.ID) == true)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                }

                identity.AddClaim(new Claim("UserId", currentUser.ID.ToString()));

                AuthenticationManager.SignIn(new AuthenticationProperties()
                {
                    IsPersistent = user.RememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(1)
                }, identity);

                if (_userService.HasStarterPokemon(currentUser.ID))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return RedirectToAction("ChooseStarter", "Player");
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(user);
            }
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Authorization");
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View("Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserViewModel model)
        {
            var newUser = TinyMapper.Map<User>(model);
            bool nameAndEmailValid = _userService.CheckIfValidUsernameAndEmail(newUser);
            if(!nameAndEmailValid)
            {
                ModelState.AddModelError("", "Username or email already exists.");
                return View("Login", model);
            }
            if (!ModelState.IsValid)
            {
                return View("Login", model);
            }
            _userRepository.AddNewUser(newUser);

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, newUser.Username),
                new Claim(ClaimTypes.NameIdentifier, newUser.ID.ToString()),
                new Claim(ClaimTypes.Role, "User"),
            }, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim("UserId", newUser.ID.ToString()));

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1)
            }, identity);

            return RedirectToAction("ChooseStarter", "Player");
        }        
    }
}