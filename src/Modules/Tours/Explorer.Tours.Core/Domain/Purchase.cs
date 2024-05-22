using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Purchase : Entity
    {
        public long TouristId { get; init; }
        public long TourId { get; init; }
        public DateTime PurchaseDate { get; init; }

        public Purchase(long touristId, long tourId, DateTime purchaseDate)
        {
            TouristId = touristId;
            TourId = tourId;
            PurchaseDate = purchaseDate;
        }

        private void Validate()
        {
            if (TouristId == 0) throw new ArgumentException("Invalid TouristId");
            if (TourId == 0) throw new ArgumentException("Invalid TourId");
            if (PurchaseDate == DateTime.MinValue) throw new ArgumentException("Invalid PurchaseDate");
        }
    }
}
