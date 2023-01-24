using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StokEkstresi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace StokEkstresi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        TestContext _textContext;
        public HomeController(ILogger<HomeController> logger, TestContext textContext)
        {
            _logger = logger;
            _textContext = textContext;
        }

        public IActionResult Index()
        {
            var stiList =  _textContext.Stis.ToList();
            var dataList = _textContext.SearchCustomers("10086 SİEMENS", 42500, 42700).ToList();
            var dataList2 = _textContext.MalKoduArama("10086 SİEMENS").ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
