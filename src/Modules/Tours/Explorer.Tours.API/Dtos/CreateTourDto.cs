using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class CreateTourDto
    {
        public long AuthorId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DifficultType Difficult { get; set; }
        public CategoryType Category { get; set; }
        public double Price { get; set; }
        public StatusType Status { get; set; }
        public List<CreateKeyPointDto> KeyPoints { get; set; }

    }

    public enum DifficultType
    {
        Easy,
        Medium,
        Hard
    }

    public enum CategoryType
    {
        Nature,
        Art,
        Sport,
        Shopping,
        Food
    }

    public enum StatusType
    {
        Draft,
        Published,
        Archived
    }
}
