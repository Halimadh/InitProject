using Azure.Core;
using DataLayer.Context;
using DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ServiceLayer.ServiceLayer;
using System.Security.Claims;

namespace InitProjectCore.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly DataLayerContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtAuthenticationService _jwtAuthenticationService;

        public UserController(IConfiguration configuration, IJwtAuthenticationService jwtAuthenticationService ,DataLayerContext context)
        {
            _context = context;
            _configuration = configuration;
            _jwtAuthenticationService = jwtAuthenticationService;
        }
        // Login 
        [HttpPost]
   
        public IActionResult Login( LoginModel user)
        {
            using (ServiceUser srv = new ServiceUser(_context, _jwtAuthenticationService))
            {
              string  encryptedPassword = _jwtAuthenticationService.Encrypt(user.Password);
                User usr = srv.Authenticate(user.Username, encryptedPassword);

                if(usr!= null)
                {
                        var claims = new List<Claim>
                         {
                             new Claim(ClaimTypes.Name, usr.Name),
                        };
                        var token = _jwtAuthenticationService.GenerateToken( _configuration["Jwt:Key"], claims);

                    HttpContext.Session.SetString("Token", token);
                    return Ok(token);
                    
                
                }
                return NotFound("user not found");
            }
        }

        //Register
        [HttpPost]
        public IActionResult Register(User requeste )
        {
            using (ServiceUser srv = new ServiceUser(_context, _jwtAuthenticationService))
            {
                
                srv.AddUser(requeste);
                return Ok(new { message = "Registration successful" });

            } 
        }


        /*GET: UserController
        public ActionResult GetUserById(string id)
        {
            using (ServiceUser srv = new ServiceUser(context))
            {
                return srv.GetUserById(id);
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
