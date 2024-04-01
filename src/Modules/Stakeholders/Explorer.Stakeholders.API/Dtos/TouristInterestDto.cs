using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class TouristInterestDto
    {
        public InterestType Interest { get; set; }
    }

    public enum InterestType
    {
        Nature,
        Art,
        Sport,
        Shopping,
        Food
    }
}
