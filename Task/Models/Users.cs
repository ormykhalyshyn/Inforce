using System.ComponentModel.DataAnnotations;

namespace TaskI.Models
{
    public class Users
    {

        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Username")]
        [Display(Name = "Please Enter Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [Display(Name = "Please Enter Password")]
        public string Password { get; set; }

        public bool isAdmin { get; set; }

        public virtual List<ShortUrl> ShortUrls { get; set; }
    }


}
