namespace RedFlickMVC.Models
{
    public class UserProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public List<String> Roles { get; set; }
    }
}
