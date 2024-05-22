using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class PurchaseDto
    {
        public int Id { get; set; }
        public long TouristId { get; set; }
        public TourDto Tour { get; set; }
        public DateTime PurchaseDate { get; set; }

        public PurchaseDto(int id, long touristId, TourDto tour, DateTime purchaseDate)
        {
            Id = id;
            TouristId = touristId;
            Tour = tour;
            PurchaseDate = purchaseDate;
        }
    }
}
