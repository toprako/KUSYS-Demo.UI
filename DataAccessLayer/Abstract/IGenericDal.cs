using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IGenericDal<T>
    {
        Task<string> Insert(T TModel);
        Task<string> Delete(T TModel);
        Task<string> Update(T TModel);
        Task<string> InsertRange(List<T> TModels);
        Task<string> UpdateRange(List<T> TModels);
        List<T> GetList();
        T? GetById(Guid Id);
        List<T> GetListByFilter(Expression<Func<T, bool>> filter);
    }
}
