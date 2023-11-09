using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SudentCRUDOracleDB.Interface;
using SudentCRUDOracleDB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SudentCRUDOracleDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IStudentService _repository;

        public HomeController(ILogger<HomeController> logger, IStudentService repository)
        {
            _logger = logger;

            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Student> students = _repository.getAll();

            return View(students);
        }

        [HttpGet]
        public IActionResult Detail(int id)
        {
            Student student = _repository.getById(id);

            return View(student);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (student != null)
            {
                _repository.CreateStudent(student);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Student student = _repository.getById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Update(Student student)
        {
            if (student != null)
            {
                _repository.UpdateStudent(student);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = _repository.getById(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Delete(Student student)
        {
            _repository.DeleteStudent(student.id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
