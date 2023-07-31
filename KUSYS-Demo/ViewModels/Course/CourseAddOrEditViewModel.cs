using System.ComponentModel.DataAnnotations;

namespace KUSYS_Demo.ViewModels.Course
{
    public class CourseAddOrEditViewModel
    {

        [Required]
        public string CourseId { get; set; }

        [Required]
        public string CourseName { get; set;}
    }
}
