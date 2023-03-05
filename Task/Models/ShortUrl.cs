using System.ComponentModel.DataAnnotations;

namespace TaskI.Models
{
    public class ShortUrl
    {

        [Key]
        public int Id { get; set; }

        
        [Required]
        public string OriginalUrl { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }

        public int UserId { get; set; }
        public Users User { get; set; }

    }
}
