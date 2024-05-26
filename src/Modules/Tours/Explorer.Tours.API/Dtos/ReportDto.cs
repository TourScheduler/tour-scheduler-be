using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class ReportDto
    {
        public long Id { get; set; }
        public long AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedFor { get; set; }
        public long Count { get; set; }
        public double Sum { get; set; }
        public decimal Percentage { get; set; }
        public List<TourDto> BestSellingTours { get; set; }
        public List<TourDto> UnsoldedTours { get; set; }

        public ReportDto(long id, long authorId, DateTime createdAt, DateTime createdFor, long count, double sum, decimal percentage, List<TourDto> bestSellingTours, List<TourDto> unsoldedTours)
        {
            Id = id;
            AuthorId = authorId;
            CreatedAt = createdAt;
            CreatedFor = createdFor;
            Count = count;
            Sum = sum;
            Percentage = percentage;
            BestSellingTours = bestSellingTours;
            UnsoldedTours = unsoldedTours;
        }
    }
}
