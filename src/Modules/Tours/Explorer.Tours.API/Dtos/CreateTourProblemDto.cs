using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CreateTourProblemDto
    {
        public long TouristId { get; set; }
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public CreateTourProblemDto(long touristId, long tourId, string name, string description)
        {
            TouristId = touristId;
            TourId = tourId;
            Name = name;
            Description = description;
        }
    }
}
