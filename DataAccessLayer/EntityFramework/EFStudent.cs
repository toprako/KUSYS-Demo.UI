using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFStudent : GenericRepository<Student>, IStudent
    {
        public Student? GetStudentAndCourseById(Guid Id)
        {
            using (Context.DbContext context = new())
            {
                return context.Students.Include(x => x.CourseStudent).ThenInclude(y => y.Course).Where(x => x.StudentId == Id).FirstOrDefault();
            }
        }

        public List<Student> GetStudentCourses()
        {
            using (Context.DbContext context = new())
            {
                return context.Students.Include(x => x.CourseStudent).ThenInclude(y => y.Course).ToList();
            }
        }
    }
}
