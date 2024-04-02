using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DifficultType Difficult { get; set; }
        public CategoryType Category { get; set; }
        public decimal Price { get; set; }
        public StatusType Status { get; set; }
    }
}
