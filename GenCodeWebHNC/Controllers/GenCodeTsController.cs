using GenCodeWebHNC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GenCodeWebHNC.Controllers
{
    public class GenCodeTsController : Controller
    {
        public GenCodeTsController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
