using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Taller_CodingAssesment_CarGame.Models;

namespace Taller_CodingAssesment_CarGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        private List<CarViewModel> cars = new List<CarViewModel>(){
            new CarViewModel { Id = 1, Make = "Audi", Model = "R8", Year = 2018, Doors = 2, Color = "Red", Price = 79995 },
            new CarViewModel { Id = 2, Make = "Tesla", Model = "3", Year = 2018, Doors = 4, Color = "Black", Price = 54995 },
            new CarViewModel { Id = 3, Make = "Porsche", Model = "911 991", Year = 2020, Doors = 2, Color = "White", Price = 155000 },
            new CarViewModel { Id = 4, Make = "Mercedes-Benz", Model = "GLE 63S", Year = 2021, Doors = 5, Color = "Blue", Price = 83995 },
            new CarViewModel { Id = 5, Make = "BMW", Model = "X6 M", Year = 2020, Doors = 5, Color = "Silver", Price = 62995 },
        };

        public IActionResult Index()
        {
            return View(cars);
        }

        // GET: CarController/Create
        public ActionResult Create(CarViewModel car)
        {
            return View();
        }

        // POST: CarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection form)
        {
            try
            {


                var car = new CarViewModel
                {
                    Id = cars.Max(x => x.Id) + 1,
                    Make = form["Make"],
                    Model = form["Model"],
                    Year = int.Parse(form["Year"]),
                    Doors = int.Parse(form["Doors"]),
                    Color = form["Color"],
                    Price = decimal.Parse(form["Price"])
                };

                cars.Add(car);

                return View("Index", cars);
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Edit/5
        public ActionResult Edit(int id)
        {
            var car = cars.First(x => x.Id == id);
            return View(car);
        }

        // POST: CarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection form)
        {
            try
            {
                var car = cars.First(x => x.Id == id);
                cars.Remove(car);

                car.Make = form["Make"];
                car.Model = form["Model"];
                car.Doors = int.Parse(form["Doors"]);
                car.Year = int.Parse(form["Year"]);
                car.Doors = int.Parse(form["Doors"]);
                car.Color = form["Color"];
                car.Price = decimal.Parse(form["Price"]);

                cars.Add(car);

                return View("Index", cars);
            }
            catch
            {
                return View();
            }
        }

        // GET: CarController/Delete/5
        public ActionResult Delete(int id)
        {
            var car = cars.First(x => x.Id == id);
            return View(car);
        }

        // POST: CarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection form)
        {
            try
            {
                var car = cars.First(x => x.Id == id);
                cars.Remove(car);

                return View("Index", cars);
            }
            catch
            {
                return View();
            }
        }

        public IActionResult Guess(int id)
        {
            CarViewModel car = cars.FirstOrDefault(c => c.Id == id);
            _logger.LogDebug("");
            return View(car);
        }

        [HttpPost]
        public IActionResult Guess(int id, int guess)
        {
            CarViewModel car = cars.FirstOrDefault(c => c.Id == id);

            int result = Math.Abs((int)car.Price - guess);
            if (result <= 5000)
            {
                ViewBag.Message = "Great job!";
                ViewBag.Color = "green";
            }
            else
            {
                ViewBag.Message = "Try again!";
                ViewBag.Color = "red";
            }
            return View(car);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
