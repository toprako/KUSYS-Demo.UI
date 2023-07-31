using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Container
{
    public static class Extensions
    {
        public static void ContainerDependencies(this IServiceCollection serviceCollection)
        {
            #region Bağımlılar
            serviceCollection.AddScoped<ICourseService, CourseManager>();
            serviceCollection.AddScoped<ICourse, EFCourse>();

            serviceCollection.AddScoped<IStudentService, StudentManager>();
            serviceCollection.AddScoped<IStudent, EFStudent>();
            
            serviceCollection.AddScoped<IUserAndRoleService,  UserAndRoleManager>();
            serviceCollection.AddScoped<IUserAndRole, EFUserAndRole>();

            serviceCollection.AddScoped<IUserService, UserManager>();
            serviceCollection.AddScoped<IUser,EFUser>();

            serviceCollection.AddScoped<ICourseStudentService, CourseStudentManager>();
            serviceCollection.AddScoped<ICourseStudent,EFCourseStudent>();
            #endregion
        }
    }
}
