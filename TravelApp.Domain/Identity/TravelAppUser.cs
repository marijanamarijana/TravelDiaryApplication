using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.DomainModels;

namespace TravelApp.Domain.Identity
{
    public class TravelAppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<TravelBooking>? BookedTravels { get; set; }
        public virtual ICollection<TravelWishlist>? Wishlist { get; set; }
    }
}
