using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class InterestDto
    {
        public InterestType Interest { get; set; }

        public InterestDto(InterestType interest)
        {
            Interest = interest;
        }
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
