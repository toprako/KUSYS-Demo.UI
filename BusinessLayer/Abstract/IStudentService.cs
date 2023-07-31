using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStudentService : IGenericService<Student>
    {
        public List<Student> TGetStudentCourses();
        public Student? TGetStudentAndCourseById(Guid Id);
    }
}
