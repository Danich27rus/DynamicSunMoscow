using Application.Interfaces;
using Domain.Entities;
using DynamicSunMoscow.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace DynamicSunMoscow.Controllers
{
    public class HomeController : Controller
    {
        //Кол-во элементов на одной странице
        private const int PAGE_SIZE = 10; 

        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IWeatherDataRepositoryAsync _weatherRepository;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment, IWeatherDataRepositoryAsync weatherRepository)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _weatherRepository = weatherRepository;

            Directory.CreateDirectory(_appEnvironment.WebRootPath + "/excelFiles/");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ShowInfo(string year, string month, int page = 1)
        {
            int count = await _weatherRepository.GetCountAsync();
            IEnumerable<Weather> weathers = await _weatherRepository.GetAllAsync();

            List<string> uniqueYears = weathers.Select(w => w.DateTime.Year).Distinct().Select(year => year.ToString()).ToList();
            uniqueYears.Insert(0, "Все года");

            List<string> uniqueMonths = weathers.Select(w => w.DateTime.Date.ToString("MMMM")).Distinct().ToList();
            uniqueMonths.Insert(0, "Все месяцы");

            if (!String.IsNullOrEmpty(year) && year != "Все года")
            {
                weathers = weathers.Where(w => w.DateTime.Year == Convert.ToInt16(year));
            }

            if (!String.IsNullOrEmpty(month) && month != "Все месяцы")
            {
                weathers = weathers.Where(w => w.DateTime.Month == DateTime.ParseExact(month, "MMMM", CultureInfo.CurrentCulture).Month);
            }

            weathers = weathers.Skip((page - 1) * PAGE_SIZE).Take(PAGE_SIZE);

            PageModel pageViewModel = new(count, page, PAGE_SIZE);
            WeatherViewModel viewModel = new(uniqueYears, uniqueMonths, year, month)
            {
                PageModel = pageViewModel,
                Weathers = weathers
            };

            return View(viewModel);
        }

        public IActionResult LoadArchives()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadArchives(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                ViewData["Error"] = "Файл не был загружен";
                return View();
            }

            if (uploadedFile != null)
            {
                var extension = System.IO.Path.GetExtension(uploadedFile.FileName);

                if (extension != ".xlsx")
                {
                    ViewData["Error"] = "Файл должен быть формата .xlsx";
                    return View();
                }

                string path = "/excelFiles/" + uploadedFile.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                await _weatherRepository.AddWeathersFromFile(_appEnvironment.WebRootPath + path);
            }

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
