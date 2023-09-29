using System.ComponentModel.DataAnnotations;

namespace PeitsShop.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public string CommentContent { get; set; }
    }
}
