using Microsoft.EntityFrameworkCore;
using PeitsShop.Models;

namespace PeitsShop.DataContext
{
    public class Data : DbContext
    {
        public Data(DbContextOptions<Data> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new { CategoryId = 1, Name = "Birds" },
                new { CategoryId = 2, Name = "Fish" },
                new { CategoryId = 3, Name = "Dogs" }
            );
            modelBuilder.Entity<Animal>().HasData(
                new { 
                    AnimalId = 1,
                    Name = "ShinTzu",
                    Age = 4,
                    Image = "/Images/ShinTzu.jpeg", 
                    Description = "The Shih Tzu is a toy dog breed originating from Tibet and believed to be bred from the Pekingese and the Lhasa Apso.",
                    SoundVoice = "Scripts/Sounds/ShinTzu.mp4",
                    CategoryId = 3
                },
                new { 
                    AnimalId = 2,
                    Name = "Malinois",
                    Age = 3,
                    Image = "/Images/Malinois.jpeg", 
                    Description = "The Belgian Shepherd is a breed of medium-sized herding dog from Belgium.",
                    SoundVoice = "Scripts/Sounds/Shin-Tzu.mp4",
                    CategoryId = 3
                },
                new { 
                    AnimalId = 3,
                    Name = "Parrot",
                    Age = 2,
                    Image = "/Images/Parrots.jpeg", 
                    Description = "Parrots, also known as psittacines, are birds of the order Psittaciformes and are found mostly in tropical and subtropical regions. " +
                    "They are made up of four families that contain roughly 410 species in 101 genera.",
                    SoundVoice = "Scripts/Sounds/Parrot.mp4",
                    CategoryId = 1
                },
                new {
                    AnimalId = 4,
                    Name = "Eagel",
                    Age = 6,
                    Image = "/Images/Eagle.jpg", 
                    Description = "Eagle is the common name for many large birds of prey of the family Accipitridae. Eagles belong to several groups of genera, some of which are closely related.",
                    SoundVoice = "Scripts/Sounds/Eagel.mp4",
                    CategoryId = 1 
                },
                new {
                    AnimalId = 5,
                    Name = "Fish",
                    Age = 1,
                    Image = "/Images/Fish.jpeg",
                    Description = "A fish is an aquatic, craniate, gill-bearing animal that lacks limbs with digits. " +
                    "Included in this definition are the living hagfish, lampreys, and cartilaginous and bony fish as well as " +
                    "various extinct related groups.",
                    SoundVoice = "Scripts/Sounds/Fish.mp4",
                    CategoryId = 2
                }
            );
            modelBuilder.Entity<Comment>().HasData(
                new { CommentId = 1, AnimalId = 1, CommentContent = "So cute!" },
                new { CommentId = 2, AnimalId = 2, CommentContent = "He is like one of us at home." },
                new { CommentId = 3, AnimalId = 1, CommentContent = "I love that type of dogs!" },
                new { CommentId = 4, AnimalId = 5, CommentContent = "Everyone needs a gold-fish in life!" },
                new { CommentId = 5, AnimalId = 5, CommentContent = "Nice to look at him swimming." },
                new { CommentId = 6, AnimalId = 3, CommentContent = "Amazing." }
            ); ;
        }
    }
}
