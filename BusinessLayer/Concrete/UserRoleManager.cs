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
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUserRole _userRole;

        public UserRoleManager(IUserRole userRole)
        {
            _userRole = userRole;
        }

        public async Task<string> TDelete(UserRole TModel)
        {
           return await _userRole.Delete(TModel);
        }

        public UserRole? TGetById(Guid Id)
        {
            return _userRole.GetById(Id);
        }

        public List<UserRole> TGetList()
        {
            return _userRole.GetList();
        }

        public List<UserRole> TGetListByFilter(Expression<Func<UserRole, bool>> filter)
        {
            return _userRole.GetListByFilter(filter);
        }

        public async Task<string> TInsert(UserRole TModel)
        {
           return await _userRole.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<UserRole> TModels)
        {
            return await _userRole.InsertRange(TModels);
        }

        public async Task<string> TUpdate(UserRole TModel)
        {
            return await _userRole.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<UserRole> TModels)
        {
           return await _userRole.UpdateRange(TModels);
        }
    }
}
