using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace FilmsRanking.Models
{
    public class WishList
    {
        [Key]
        public int Id { get; set; }
        public string WishListName { get; set; }
        [ForeignKey("MediaContent")]
        public int? MovieId { get; set; }
        [ForeignKey("AppUser")]
        public int? UserID { get; set; }
        public AppUser? AppUser { get; set; }
        public MediaContent? MediaContent { get; set; }
    }
}
