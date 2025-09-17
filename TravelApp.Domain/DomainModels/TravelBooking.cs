using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelApp.Domain.Identity;

namespace TravelApp.Domain.DomainModels
{
    public class TravelBooking : BaseEntity // Past Travel
    {
        public string? BookerId { get; set; }
        public TravelAppUser? Booker { get; set; }
        public Guid TravelId { get; set; }
        public Travel? Travel { get; set; }
        //public DateTime BookedOn { get; set; }  // koga si go zakazhal patuvanjeto ?
    }
}