using System.ComponentModel.DataAnnotations;

namespace FilmsRanking.Models
{
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<WishList> WishLists { get; set; }
    }
}
