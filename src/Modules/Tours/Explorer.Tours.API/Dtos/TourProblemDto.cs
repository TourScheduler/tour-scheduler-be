using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourProblemDto
    {
        public long Id { get; set; }
        public long TouristId { get; set; }
        public long TourId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProblemStatus Status { get; set; }

        public TourProblemDto(long id, long touristId, long tourId, string name, string description, ProblemStatus status)
        {
            Id = id;
            TouristId = touristId;
            TourId = tourId;
            Name = name;
            Description = description;
            Status = status;
        }
    }

    public enum ProblemStatus
    {
        Pending,
        Resolved,
        OnRevision,
        Rejected
    }
}
