using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudent _student;

        public StudentManager(IStudent student)
        {
            _student = student;
        }

        public List<Student> TGetStudentCourses()
        {
            return _student.GetStudentCourses();
        }

        public async Task<string> TDelete(Student TModel)
        {
            return await _student.Delete(TModel);
        }

        public Student? TGetById(Guid Id)
        {
            return _student.GetById(Id);
        }

        public List<Student> TGetList()
        {
            return _student.GetList();
        }

        public List<Student> TGetListByFilter(Expression<Func<Student, bool>> filter)
        {
            return _student.GetListByFilter(filter);
        }

        public async Task<string> TInsert(Student TModel)
        {
            return await _student.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<Student> TModels)
        {
            return await _student.InsertRange(TModels);
        }

        public async Task<string> TUpdate(Student TModel)
        {
            return await _student.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<Student> TModels)
        {
            return await _student.UpdateRange(TModels);
        }

        public Student? TGetStudentAndCourseById(Guid Id)
        {
            return _student.GetStudentAndCourseById(Id);
        }
    }
}
