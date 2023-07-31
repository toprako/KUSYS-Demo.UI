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
    public class CourseStudentManager : ICourseStudentService
    {
        private readonly ICourseStudent _courseStudent;

        public CourseStudentManager(ICourseStudent courseStudent)
        {
            _courseStudent = courseStudent;
        }

        public async Task<string> TDelete(CourseStudent TModel)
        {
            return await _courseStudent.Delete(TModel);
        }

        public CourseStudent? TGetById(Guid Id)
        {
            return _courseStudent.GetById(Id);
        }

        public List<CourseStudent> TGetList()
        {
            return _courseStudent.GetList();
        }

        public List<CourseStudent> TGetListByFilter(Expression<Func<CourseStudent, bool>> filter)
        {
            return _courseStudent.GetListByFilter(filter);
        }

        public async Task<string> TInsert(CourseStudent TModel)
        {
            return await _courseStudent.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<CourseStudent> TModels)
        {
            return await _courseStudent.InsertRange(TModels);
        }

        public async Task<string> TUpdate(CourseStudent TModel)
        {
            return await _courseStudent.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<CourseStudent> TModels)
        {
            return await _courseStudent.UpdateRange(TModels);
        }
    }
}
