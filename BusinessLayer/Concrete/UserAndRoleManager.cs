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
    public class UserAndRoleManager : IUserAndRoleService
    {
        private readonly IUserAndRole _userAndRole;

        public UserAndRoleManager(IUserAndRole userAndRole)
        {
            _userAndRole = userAndRole;
        }

        public async Task<string> TDelete(UserAndRole TModel)
        {
            return await _userAndRole.Delete(TModel);
        }

        public UserAndRole? TGetById(Guid Id)
        {
            return _userAndRole.GetById(Id);
        }

        public List<UserAndRole> TGetList()
        {
            return _userAndRole.GetList();
        }

        public List<UserAndRole> TGetListByFilter(Expression<Func<UserAndRole, bool>> filter)
        {
            return _userAndRole.GetListByFilter(filter);
        }

        public async Task<string> TInsert(UserAndRole TModel)
        {
            return await _userAndRole.Insert(TModel);
        }

        public async Task<string> TInsertRange(List<UserAndRole> TModels)
        {
            return await _userAndRole.InsertRange(TModels);
        }

        public async Task<string> TUpdate(UserAndRole TModel)
        {
            return await _userAndRole.Update(TModel);
        }

        public async Task<string> TUpdateRange(List<UserAndRole> TModels)
        {
            return await _userAndRole.UpdateRange(TModels);
        }
    }
}
