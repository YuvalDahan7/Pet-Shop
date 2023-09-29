using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PeitsShop.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Please fill a name.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please.")]
        [StringLength(10, MinimumLength = 1)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please fill the age.")]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Use numbers only please.")]
        [Range(1, 20, ErrorMessage = "The age value must be between 1 - 20.")]
        public int Age { get; set; }

        public string Image { get; set; }

        [Required(ErrorMessage = "Please fill a description.")]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set;}

        public string SoundVoice { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
