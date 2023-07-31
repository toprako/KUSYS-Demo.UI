using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer
{
    public class CourseStudent
    {
        public string CourseId { get; set; }
        public Guid StudentId { get; set; }
        
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
