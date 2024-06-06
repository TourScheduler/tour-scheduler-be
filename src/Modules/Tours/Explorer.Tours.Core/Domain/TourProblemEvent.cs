using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourProblemEvent : ValueObject
    {
        public ProblemStatus Status { get; init; }
        public DateTime CreatedAt { get; init; }

        public TourProblemEvent(ProblemStatus status, DateTime createdAt)
        {
            Status = status;
            CreatedAt = createdAt;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Status;
            yield return CreatedAt;
        }
    }
}
