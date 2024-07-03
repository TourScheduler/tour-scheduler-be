using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Report : Entity
    {
        public long AuthorId { get; init; }
        public DateTime CreatedAt { get; init; }
        public DateTime CreatedFor { get; init; }
        public long Count { get; init; }
        public double Sum { get; init; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Percentage {  get; init; }
        public List<int> BestSellingTours { get; init; }
        public List<int> UnsoldedTours { get; init; }

        public Report(long authorId, DateTime createdAt, DateTime createdFor, long count, double sum, decimal percentage, List<int> bestSellingTours, List<int> unsoldedTours)
        {
            AuthorId = authorId;
            CreatedAt = createdAt;
            CreatedFor = createdFor;
            Count = count;
            Sum = sum;
            Percentage = percentage;
            BestSellingTours = bestSellingTours;
            UnsoldedTours = unsoldedTours;
            Validate();
        }

        private void Validate()
        {
            if (AuthorId == 0) throw new ArgumentException("Invalid AuthorId");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("Invalid CreatedAt");
            if (CreatedFor == DateTime.MinValue) throw new ArgumentException("Invalid CreatedFor");
        }
    }
}
