using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketHup.Models.MVVM
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("ID")]
        public int CommentID { get; set; }
        public int UserID { get; set; }

        [DisplayName("ÜRÜN ADI")]
        public int ProductID { get; set; }


        [Range(50, 150)] // alternatif olarak bu şekilde de yazılır: [StringLength(150,MinimumLength = 10)]
        [DisplayName("Yorum")]
        public string? Review { get; set; }
    }
}
