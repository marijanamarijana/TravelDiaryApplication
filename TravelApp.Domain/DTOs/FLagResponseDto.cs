using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelApp.Domain.DTOs
{
    public class FlagResponseDto
    {
        public bool Error { get; set; }
        public string Msg { get; set; }
        public FlagDataDto Data { get; set; }
    }
}
