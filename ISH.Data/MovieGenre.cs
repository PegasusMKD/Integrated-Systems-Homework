using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISH.Data
{
    [EntityTypeConfiguration(typeof(MovieGenreConfiguration))]
    public class MovieGenre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }


    internal class MovieGenreConfiguration : IEntityTypeConfiguration<MovieGenre>
    {

        public void Configure(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
        }
    }

}
