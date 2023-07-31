using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ICourse : IGenericDal<Course>
    {
        public Course GetCourseAndStudentById(string Id);
    }
}
