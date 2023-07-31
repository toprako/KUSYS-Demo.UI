namespace KUSYS_Demo.ViewModels.User
{
    public class UserAddOrUpdateViewModel
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string StudentId { get; set; }
        public string? SelectedUser { get; set; }    
        public string? SelectedRole { get; set; }
        public Dictionary<string,string> Students { get; set; }  
        public Dictionary<string, string> Role { get; set; }
    }
}
