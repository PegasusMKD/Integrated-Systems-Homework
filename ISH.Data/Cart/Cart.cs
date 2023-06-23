using ISH.Data.Authentication;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ISH.Data.Tickets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ISH.Data.Cart
{
    public class Cart : BaseEntity
    {
        [Required]
        public User User { get; set; }
        [Required]
        public int CartPrice { get; set; }

        public List<Ticket> Tickets { get; } = new();
    }

    internal class CartConfiguration: IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.HasOne(x => x.User).WithOne();
            builder.HasMany(x => x.Tickets).WithMany();
        }
    }
}
