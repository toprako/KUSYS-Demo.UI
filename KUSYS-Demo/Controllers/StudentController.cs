using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using KUSYS_Demo.ViewModels.General;
using KUSYS_Demo.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly CourseManager _courseService = new CourseManager(new EFCourse());
        private readonly StudentManager _studentService = new StudentManager(new EFStudent());
        private readonly CourseStudentManager _courseStudentManager = new CourseStudentManager(new EFCourseStudent());

        [HttpGet]
        public IActionResult AddStudent()
        {
            StudentViewModel model = new();
            List<CheckBoxesViewModel> selectListItems = new List<CheckBoxesViewModel>();
            var course = _courseService.TGetList();
            if (course != null && course.Count > 0)
            {
                foreach (var item in course)
                {
                    selectListItems.Add(new()
                    {
                        Text = item.CourseName,
                        Value = item.CourseId
                    });
                }
            }
            model.Courses = selectListItems;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Student entity = new()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    BirthDate = model.BirthDate,
                    IsActive = true,
                    StudentId = Guid.NewGuid(),
                };

                if (entity.CourseStudent is null)
                    entity.CourseStudent = new List<CourseStudent>();

                foreach (var item in model.Courses.Where(x => x.Checked))
                {
                    entity.CourseStudent.Add(new CourseStudent()
                    {
                        Student = entity,
                        CourseId = item.Value
                    });
                }
                var saveMessage = await _studentService.TInsert(entity);
                if (!string.IsNullOrEmpty(saveMessage))
                {
                    ViewBag.StudenSaveMessage = saveMessage;
                    return View(model);
                }
                return RedirectToAction("Students", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent(Guid Id)
        {
            var student = _studentService.TGetStudentAndCourseById(Id);
            var courses = _courseStudentManager.TGetListByFilter(x => x.StudentId == student.StudentId);
            foreach (var item in courses)
            {
                await _courseStudentManager.TDelete(item);
            }
            student.CourseStudent = null;
            string message = await _studentService.TDelete(student);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult UpdateStudent(Guid Id)
        {
            List<CheckBoxesViewModel> selectListItems = new List<CheckBoxesViewModel>();
            var student = _studentService.TGetStudentAndCourseById(Id);
            var course = _courseService.TGetList();
            foreach (var item in course)
            {
                selectListItems.Add(new()
                {
                    Text = item.CourseName,
                    Value = item.CourseId,
                    Checked = student.CourseStudent.Any(x => x.CourseId == item.CourseId),
                });
            }
            return View(new StudentViewModel()
            {
                Id = student.StudentId,
                BirthDate = student.BirthDate,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Courses = selectListItems,
                SelectedCourses = string.Empty
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudent(StudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var student = _studentService.TGetStudentAndCourseById(model.Id);
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.BirthDate = model.BirthDate;
                var courseStudent = _courseStudentManager.TGetListByFilter(x => x.StudentId == student.StudentId);
                foreach (var item in model.Courses)
                {
                    var existed = student.CourseStudent.Where(x => x.CourseId == item.Value).FirstOrDefault();
                    if (existed is not null)
                    {
                        if (!item.Checked)
                        {
                            var relation = courseStudent.Find(x => x.CourseId == item.Value);
                            await _courseStudentManager.TDelete(relation);
                        }
                        continue;
                    }
                    else
                    {
                        if (item.Checked)
                        {
                            CourseStudent courseRelation = new()
                            {
                                StudentId = student.StudentId,
                                CourseId = item.Value
                            };
                            await _courseStudentManager.TInsert(courseRelation);
                        }
                    }
                }
                var message = await _studentService.TUpdate(student);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
