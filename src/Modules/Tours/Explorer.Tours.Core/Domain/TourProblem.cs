using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourProblem : Entity
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public ProblemStatus Status { get; private set; }
        public List<TourProblemEvent> Events = new List<TourProblemEvent>();

        public TourProblem(long touristId, long tourId, string name, string description, ProblemStatus status)
        {
            TouristId = touristId;
            TourId = tourId;
            Name = name;
            Description = description;
            Status = status;
            Validate();
            AddEvent(new TourProblemEvent(Status, DateTime.Now));
        }

        public void ChangeStatus(ProblemStatus newStatus)
        {
            Status = newStatus;
            AddEvent(new TourProblemEvent(newStatus, DateTime.Now));
        }

        private void AddEvent(TourProblemEvent @event)
        {
            Events.Add(@event);
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (TourId == 0) throw new ArgumentException("Invalid TourId");
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
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
