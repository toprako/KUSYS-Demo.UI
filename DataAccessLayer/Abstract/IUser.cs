using EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IUser : IGenericDal<User>
    {
        public List<User> GetListWithStudent();
        public User GetByIdWithStudent(Guid Id);
    }
}
