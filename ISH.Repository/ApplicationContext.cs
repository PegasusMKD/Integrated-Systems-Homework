using ISH.Data;
using ISH.Data.Authentication;
using ISH.Data.Cart;
using ISH.Data.Orders;
using ISH.Data.Tickets;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ISH.Repository
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<ViewSlot> viewSlots { get; set; }
        public DbSet<MovieGenre> movieGenre { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
