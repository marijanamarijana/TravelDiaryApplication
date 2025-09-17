using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DomainModels
{
    public class Travel : BaseEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool VisaNeeded { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
        //public Guid CityId { get; set; }
        //public City? City { get; set; } // change to more cities mayhaps
        public virtual ICollection<TravelBooking>? Bookings { get; set; }
        public virtual ICollection<TravelWishlist>? Wishlists { get; set; }
    }
}
