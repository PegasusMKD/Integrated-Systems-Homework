using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ISH.Data.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace ISH.Data.Tickets
{
    [EntityTypeConfiguration(typeof(TicketConfiguration))]
    public class Ticket : BaseEntity
    {
        /**
         * Ticket represents a singular ticket that the cinema has created.
         */
        public TicketStatus TicketStatus { get; set; } = TicketStatus.Available;
        public int Price { get; set; }
        public int SeatNumber { get; set; }
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
