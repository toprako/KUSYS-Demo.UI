using DataAccessLayer.Abstract;
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
    public class EFCourse : GenericRepository<Course>, ICourse
    {
        public Course GetCourseAndStudentById(string Id)
        {
            using (Context.DbContext context = new())
            {
                return context.Courses.Include(x => x.CourseStudent).ThenInclude(y => y.Student).Where(x => x.CourseId == Id).FirstOrDefault();
            }
        }
    }
}
