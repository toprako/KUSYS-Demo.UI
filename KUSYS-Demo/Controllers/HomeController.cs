using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using KUSYS_Demo.Models;
using KUSYS_Demo.ViewModels.Home;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class HomeController : Controller
    {
        private readonly UserManager _userLocalManager = new UserManager(new EFUser());
        private readonly ILogger<HomeController> _logger;
        private readonly StudentManager studentManager = new(new EFStudent());
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        public HomeController(ILogger<HomeController> logger, Microsoft.AspNetCore.Identity.UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return RedirectToAction("StudentsAll");
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Students()
        {
            var UserId = User.Identity.GetUserId();
            var user = _userLocalManager.TGetListByFilter(x => x.Id == Guid.Parse(UserId)).FirstOrDefault();
            var role = await _userManager.GetRolesAsync(user);
            if (role.Any(x => x.Equals("Admin")))
            {
                var studentList = studentManager.TGetStudentCourses();
                var model = new List<StudentViewModel>();
                foreach (var item in studentList)
                {
                    model.Add(new StudentViewModel()
                    {
                        Id = item.StudentId,
                        BirthDate = item.BirthDate,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        SelectedCourses = String.Join(" ,", item.CourseStudent.Select(x => x.Course.CourseName).ToList())
                    });
                }
                return View(model);
            }
            else
            {
                var student = studentManager.TGetStudentAndCourseById(user.StudentId ?? Guid.Empty);
                var model = new List<StudentViewModel>()
                {
                    new()
                    {
                        Id = student.StudentId,
                        BirthDate = student.BirthDate,
                        FirstName = student.FirstName,
                        LastName = student.LastName,    
                        SelectedCourses =  String.Join(" ,", student.CourseStudent.Select(x => x.Course.CourseName).ToList())
                    }
                };
                return View(model);
            }          
        }

        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> StudentsAll()
        {
            var UserId = User.Identity.GetUserId();
            var user = _userLocalManager.TGetListByFilter(x => x.Id == Guid.Parse(UserId)).FirstOrDefault();
            var role = await _userManager.GetRolesAsync(user);
            if (role.Any(x => x.Equals("Admin")))
            {
                var students = studentManager.TGetStudentCourses();
                List<StudentAllViewModel> ListStudents = new List<StudentAllViewModel>();
                foreach (var item in students)
                {
                    ListStudents.Add(new()
                    {
                        Id = item.StudentId,
                        BirthDate = item.BirthDate,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        SelectedCourses = String.Join(" ,", item.CourseStudent.Select(x => x.Course.CourseName).ToList())
                    });
                }
                return View(ListStudents);
            }
            else
            {
                var student = studentManager.TGetStudentAndCourseById(user.StudentId ?? Guid.NewGuid());
                return View(new List<StudentAllViewModel>()
                {
                    new()
                    {
                        Id = student.StudentId,
                        BirthDate= student.BirthDate,
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        SelectedCourses = String.Join(" ,", student.CourseStudent.Select(x => x.Course.CourseName).ToList())
                    }
                });
            }
            return View(new List<StudentAllViewModel>());
        }

        public JsonResult ViewStudent(string Id)
        {
            var student = studentManager.TGetStudentAndCourseById(Guid.Parse(Id));
            return Json(new
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                BirthDate = student.BirthDate.ToString("dd.MM.yyyy"),
                SelectedCourses = String.Join(" ,", student.CourseStudent.Select(x => x.Course.CourseName).ToList())
            });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}