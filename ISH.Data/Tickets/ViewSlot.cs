using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ISH.Data.Tickets
{
    [EntityTypeConfiguration(typeof(ViewSlotConfiguration))]
    public class ViewSlot : BaseEntity
    {
        /**
         * ViewSlot is just an aggregate for all available time slots in the cinema for the users to watch a movie.
         */
        [Required]
        public DateTime TimeSlot { get; set; }
        [Required]
        public string MovieName { get; set; }
        [Required]
        public MovieGenre Genre { get; set; }
    }

    internal class ViewSlotConfiguration : IEntityTypeConfiguration<ViewSlot>
    {
        public void Configure(EntityTypeBuilder<ViewSlot> builder)
        {
            builder.HasOne(x => x.Genre).WithMany();
        }
    }
}
