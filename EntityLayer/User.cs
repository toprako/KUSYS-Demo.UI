using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class User : IdentityUser<Guid>
    {
        public Guid? StudentId { get; set; }
        public Student? Student { get; set; }
    }
}
