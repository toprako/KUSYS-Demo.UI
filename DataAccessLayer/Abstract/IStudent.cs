using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStudent : IGenericDal<Student>
    {
        public List<Student> GetStudentCourses();
        public Student? GetStudentAndCourseById(Guid Id);
    }
}
