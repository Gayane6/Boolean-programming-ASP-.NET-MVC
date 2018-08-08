using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Course_Work.Models;

namespace Course_Work.Controllers
{
    public class GeneratorController : Controller
    {
        // GET: Generator
        public ActionResult GeneratorInput()
        {
            Generator InputData = new Generator();
            return View(InputData);
        }
        [HttpPost]
        public ActionResult GeneratorInput(Generator input)
        {
            List<List<Result>> array1 = new List<List<Result>>();
            Generation result1 = new Generation(input);    
                array1 = result1.General();
                ViewBag.Result = array1;
            return View("ResultGeneratorInput", input);
        }

    }
}