using KUSYS_Demo.ViewModels.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.ViewModels.Home
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(10)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(10)]
        public string LastName { get; set; }

        [DisplayName("Birth Date")]
        public DateTime BirthDate { get; set; }
        public List<CheckBoxesViewModel> Courses { get; set; }
        public string? SelectedCourses { get; set; }
    }  
}
