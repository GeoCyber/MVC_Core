using FixedAssets.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FixedAssets.Controllers
{
    public class Fixed_Asset_WriteOffModule : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {


            return View("");

        }

        [HttpPost]
        public IActionResult Create(FixedAssetWriteOffModule model)
        {

            return View("");
        }
    }
}
