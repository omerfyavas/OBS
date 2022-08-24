using Microsoft.AspNetCore.Mvc;
using Login.Models;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Login.Controllers
{
    public class DerslerController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }
    }
}
