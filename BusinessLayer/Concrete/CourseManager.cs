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
    public class CourseManager : ICourseService
    {
        private readonly ICourse _course;

        public CourseManager(ICourse course)
        {
            _course = course;
        }

        public Course TGetCourseAndStudentById(string Id)
        {
            return _course.GetCourseAndStudentById(Id);
        }

        public async Task<string> TDelete(Course TModel)
        {
            return await _course.Delete(TModel);
        }

        public Course? TGetById(Guid Id)
        {
            return _course.GetById(Id);
        }

        public List<Course> TGetList()
        {
            return _course.GetList();
        }

        public List<Course> TGetListByFilter(Expression<Func<Course, bool>> filter)
        {
            return _course.GetListByFilter(filter); 
        }

        public async Task<string> TInsert(Course TModel)
        {
            return await _course.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<Course> TModels)
        {
            return await _course.InsertRange(TModels);
        }

        public async Task<string> TUpdate(Course TModel)
        {
            return await _course.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<Course> TModels)
        {
            return await _course.UpdateRange(TModels);
        }
    }
}
