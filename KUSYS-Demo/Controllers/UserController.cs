using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer;
using KUSYS_Demo.ViewModels.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KUSYS_Demo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager userManager = new(new EFUser());
        private readonly UserRoleManager userRoleManager = new(new EFUserRole());
        private readonly UserAndRoleManager userAndRoleManager = new(new EFUserAndRole());
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _identityUserManager;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<UserRole> _roleManager;
        private readonly StudentManager studentManager = new(new EFStudent());

        public UserController(Microsoft.AspNetCore.Identity.UserManager<User> identityUserManager, Microsoft.AspNetCore.Identity.RoleManager<UserRole> roleManager)
        {
            _identityUserManager = identityUserManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = userManager.TGetListWithStudent();
            List<UserListViewModel> list = new List<UserListViewModel>();
            foreach (var item in users)
            {
                list.Add(new()
                {
                    Id = item.Id,
                    Email = item.Email,
                    UserName = item.UserName,
                    StudentName = item.Student != null ? string.Concat(item.Student.FirstName, " ", item.Student.LastName) : string.Empty,
                });
            }
            return View(list);
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            var roles = _roleManager.Roles.ToList();
            var students = studentManager.TGetList();
            UserAddOrUpdateViewModel userAddOrUpdateView = new();
            userAddOrUpdateView.Role = new();
            userAddOrUpdateView.Students = new();
            foreach (var item in roles)
            {
                userAddOrUpdateView.Role.Add(item.Id.ToString(), item.Name);
            }
            foreach (var item in students)
            {
                userAddOrUpdateView.Students.Add(item.StudentId.ToString(), string.Concat(item.FirstName, " ", item.LastName));
            }
            return View(userAddOrUpdateView);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddOrUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                EntityLayer.User user = new()
                {
                    Id = Guid.NewGuid(),
                    UserName = model.UserName,
                    Email = model.Email,
                    NormalizedEmail = model.Email,
                    NormalizedUserName = model.UserName,
                    StudentId = Guid.Parse(model.Students.Values.FirstOrDefault()),
                };
                var result = await _identityUserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    var role = userRoleManager.TGetById(Guid.Parse(model.Role.Values.FirstOrDefault()));
                    await _identityUserManager.AddToRoleAsync(user, role.Name);
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> EditUser(string Id)
        {
            var roles = _roleManager.Roles.ToList();
            var students = studentManager.TGetList();
            var user = userManager.TGetByIdWithStudent(Guid.Parse(Id));
            var role = await _identityUserManager.GetRolesAsync(user);
            UserAddOrUpdateViewModel userAddOrUpdateView = new();
            userAddOrUpdateView.UserName = user.UserName;
            userAddOrUpdateView.Id = user.Id.ToString();
            userAddOrUpdateView.Email = user.Email;
            userAddOrUpdateView.Password = "";
            userAddOrUpdateView.Role = new();
            userAddOrUpdateView.Students = new();

            foreach (var item in roles)
            {
                userAddOrUpdateView.Role.Add(item.Name, item.Name);
            }
            foreach (var item in students)
            {
                userAddOrUpdateView.Students.Add(item.StudentId.ToString(), string.Concat(item.FirstName, " ", item.LastName));
            }
            userAddOrUpdateView.SelectedUser = user.Student.StudentId.ToString();
            userAddOrUpdateView.SelectedRole = role.FirstOrDefault().ToString();
            return View(userAddOrUpdateView);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserAddOrUpdateViewModel model)
        {
            var user = userManager.TGetByIdWithStudent(Guid.Parse(model.Id));
            user.UserName = model.UserName;
            user.NormalizedUserName = model.UserName;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email;
            string message = await userManager.TUpdate(user);
            return RedirectToAction("Index"); 
        }
        public async Task<IActionResult> DeleteUser(string Id)
        {
            var userRelation = userAndRoleManager.TGetListByFilter(x => x.UserId == Guid.Parse(Id));
            foreach (var item in userRelation)
            {
                await userAndRoleManager.TDelete(item);
            }
            var user = userManager.TGetById(Guid.Parse(Id));
            await userManager.TDelete(user);
            return RedirectToAction("Index");
        }
    }
}
