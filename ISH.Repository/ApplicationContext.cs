using ISH.Data.Authentication;
using Microsoft.EntityFrameworkCore;

namespace ISH.Repository
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
