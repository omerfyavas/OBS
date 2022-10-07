using Login.Data;
using Login.Domain;
using Login.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Login.Controllers
{ 
    public class AdministrationController : Controller
    {
        [HttpGet]
        public IActionResult List()
        {
            using (var context = new ApplicationDbContext())
            {
                var accounts = context.Account.OrderByDescending(p => p.Id).ToList();

                return View(accounts);
            }            
        }

    }
}
