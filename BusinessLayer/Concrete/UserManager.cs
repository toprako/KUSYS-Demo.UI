using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.Migrations;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUser _user;

        public UserManager(IUser user)
        {
            _user = user;
        }

        public async Task<string> TDelete(User TModel)
        {
           return await _user.Delete(TModel);
        }

        public User? TGetById(Guid Id)
        {
            return _user.GetById(Id);
        }

        public User TGetByIdWithStudent(Guid Id)
        {
            return _user.GetByIdWithStudent(Id);
        }

        public List<User> TGetList()
        {
            return _user.GetList();
        }

        public List<User> TGetListByFilter(Expression<Func<User, bool>> filter)
        {
            return _user.GetListByFilter(filter);
        }

        public List<User> TGetListWithStudent()
        {
            return _user.GetListWithStudent();
        }

        public async Task<string> TInsert(User TModel)
        {
            return await _user.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<User> TModels)
        {
            return await _user.InsertRange(TModels);
        }

        public async Task<string> TUpdate(User TModel)
        {
            return await _user.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<User> TModels)
        {
            return await _user.UpdateRange(TModels);
        }
    }
}
