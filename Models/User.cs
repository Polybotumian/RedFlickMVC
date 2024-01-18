using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RedFlickMVC.Models
{
    //[Table("users")]
    public class User : IdentityUser
    {
        //[Key]
        //public int Id { get; set; }
        [StringLength(maximumLength: 8, MinimumLength = 3)]
        public override string UserName { get; set; }
        public override string Email
        {
            get => base.Email;
            set => base.Email = value;
        }
    }
}
