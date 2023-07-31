using DataAccessLayer.Abstract;
using DataAccessLayer.Context;
using DataAccessLayer.Repository;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EFUser : GenericRepository<User>, IUser
    {
        public User GetByIdWithStudent(Guid Id)
        {
            using (Context.DbContext context = new())
            {
                return context.Users.Include(i => i.Student).Where(x => x.Id == Id).FirstOrDefault();
            }
        }

        public List<User> GetListWithStudent()
        {
            using (Context.DbContext context = new())
            {
                return context.Users.Include(i => i.Student).ToList();
            }
        }
    }
}
