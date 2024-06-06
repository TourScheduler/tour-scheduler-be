using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Dtos
{
    public class InterestDto
    {
        public InterestType Type { get; set; }

        public InterestDto(InterestType type)
        {
            Type = type;
        }
    }
}
