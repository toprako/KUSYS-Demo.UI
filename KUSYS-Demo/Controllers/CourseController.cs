using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using KUSYS_Demo.ViewModels.Course;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CourseController : Controller
    {
        private readonly CourseManager courseManager = new(new EFCourse());
        private readonly CourseStudentManager courseStudentManager = new(new EFCourseStudent());

        [Authorize(Roles = "Admin,User")]
        public IActionResult Courses()
        {
            var courses = courseManager.TGetList();
            List<CourseAllViewModel> allCourses = new List<CourseAllViewModel>();
            foreach (var item in courses)
            {
                allCourses.Add(new CourseAllViewModel
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                });
            }
            return View(allCourses);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddCourse()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseAddOrEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string message = await courseManager.TInsert(new()
                {
                    CourseId = model.CourseId,
                    CourseName = model.CourseName,
                    CourseStudent = null
                });
                return RedirectToAction("Courses");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditCourse(string Id)
        {
            var course = courseManager.TGetListByFilter(x => x.CourseId == Id).FirstOrDefault();
            CourseAddOrEditViewModel model = new();
            model.CourseId = course.CourseId;
            model.CourseName = course.CourseName;
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditCourse(CourseAddOrEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var course = courseManager.TGetListByFilter(x => x.CourseId == model.CourseId).FirstOrDefault();
                course.CourseName = model.CourseName;
                await courseManager.TUpdate(course);
                return RedirectToAction("Courses");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCourse(string Id)
        {
            var course = courseManager.TGetListByFilter(x => x.CourseId == Id).FirstOrDefault();
            var courseRelationList = courseStudentManager.TGetListByFilter(x => x.CourseId == course.CourseId);
            foreach (var item in courseRelationList)
            {
                await courseStudentManager.TDelete(item);
            }            
            await courseManager.TDelete(course);
            return RedirectToAction("Courses");
        }
    }
}
