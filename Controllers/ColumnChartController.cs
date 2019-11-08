using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleChart.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoogleChart.Controllers
{
    public class ColumnChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult PopulationChart()
        {
            var populationList = PopulationDataAccessaLayer.GetCityPopulationList();
            return Json(populationList);
        }

    }
}