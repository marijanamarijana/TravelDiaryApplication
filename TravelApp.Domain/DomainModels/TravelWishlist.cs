using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.Identity;

namespace TravelApp.Domain.DomainModels
{
    public class TravelWishlist : BaseEntity
    {
        public string? TravelAppUserId { get; set; }
        public TravelAppUser? TravelAppUser { get; set; }
        public Guid TravelId { get; set; }
        public Travel? Travel { get; set; }
    }
}
