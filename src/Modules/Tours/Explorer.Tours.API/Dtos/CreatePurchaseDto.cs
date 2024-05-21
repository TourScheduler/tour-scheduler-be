using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CreatePurchaseDto
    {
        public long TouristId { get; set; }
        public long TourId { get; set; }
    }
}
