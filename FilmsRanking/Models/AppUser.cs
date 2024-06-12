using Microsoft.AspNetCore.Identity;

namespace FilmsRanking.Models
{
    public class AppUser : IdentityUser
    {
        public string? ProfileImageUrl { get; set; }
        public ICollection<WishList> WishLists { get; set; }
        public string? ImagePublicId { get; set; }
    }
}
