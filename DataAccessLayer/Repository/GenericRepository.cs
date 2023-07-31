using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository<T>:IGenericDal<T> where T : class
    {

        public async Task<string> Delete(T TModel)
        {
            using (DbContext context = new())
            {
                try
                {
                    context.Remove(TModel);
                    await context.SaveChangesAsync();
                    return string.Empty;
                }
                catch (Exception ex)
                {
                    return ex.InnerException?.Message is null ? ex.Message : ex.InnerException.Message;
                }
            }
        }

        public T? GetById(Guid Id)
        {
            using (DbContext context = new())
            {
                return context.Set<T>().Find(Id);
            }
        }

        public List<T> GetList()
        {
            using (DbContext context = new())
            {
                return context.Set<T>().ToList();
            }
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            using (DbContext context = new())
            {
                return context.Set<T>().Where(filter).ToList();
            }
        }

        public async Task<string> Insert(T TModel)
        {
            try
            {
                using (DbContext context = new())
                {
                    context.Add(TModel);
                    await context.SaveChangesAsync();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message is null ? ex.Message : ex.InnerException.Message;
            }
        }

        public async Task<string> InsertRange(List<T> TModels)
        {
            try
            {
                using (DbContext context = new())
                {
                    context.AddRange(TModels);
                    await context.SaveChangesAsync();
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message is null ? ex.Message : ex.InnerException.Message;
            }
        }

        public async Task<string> UpdateRange(List<T> TModels)
        {
            try
            {
                using (DbContext context = new())
                {
                    context.UpdateRange(TModels);
                    await context.SaveChangesAsync();
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message is null ? ex.Message : ex.InnerException.Message;
            }
        }

        public async Task<string> Update(T TModel)
        {
            try
            {
                using (DbContext context = new())
                {
                    context.Update(TModel);
                    await context.SaveChangesAsync();
                    return string.Empty;
                }   
            }
            catch (Exception ex)
            {
                return ex.InnerException?.Message is null ? ex.Message : ex.InnerException.Message;
            }
        }
    }
}
