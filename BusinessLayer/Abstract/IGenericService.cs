using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGenericService<T>
    {
        Task<string> TInsert(T TModel);
        Task<string> TDelete(T TModel);
        Task<string> TUpdate(T TModel);
        Task<string> TInsertRange(List<T> TModels);
        Task<string> TUpdateRange(List<T> TModels);
        List<T> TGetList();
        T? TGetById(Guid Id);
        List<T> TGetListByFilter(Expression<Func<T, bool>> filter);
    }
}
