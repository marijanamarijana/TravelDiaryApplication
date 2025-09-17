using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelApp.Domain.DomainModels;
using TravelApp.Domain.Identity;

namespace TravelApp.Repository
{
    public class ApplicationDbContext : IdentityDbContext<TravelAppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<TravelBooking> TravelsBookings { get; set; }
        public virtual DbSet<TravelWishlist> TravelWishlistItems { get; set; }
    }
}
