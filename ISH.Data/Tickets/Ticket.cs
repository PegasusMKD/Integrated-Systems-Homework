using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISH.Data.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ISH.Data.Tickets
{
    [EntityTypeConfiguration(typeof(TicketConfiguration))]
    public class Ticket
    {
        /**
         * Ticket represents a singular ticket that the cinema has created.
         */
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Guid { get; set; }
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Available;
        public int Price { get; set; }
        public string Seat { get; set; } // Will have some naming scheme "XYY" (where X is some letter for the row, and YY is the column, aka position in the row)
        public ViewSlot ViewSlot { get; set; }
        public User? BoughtBy { get; set; }
    }

    internal class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(x => x.TicketStatus).HasConversion<string>();
            builder.HasOne(x => x.ViewSlot).WithMany();
            builder.HasOne(x => x.BoughtBy).WithMany();
        }
    }
}
