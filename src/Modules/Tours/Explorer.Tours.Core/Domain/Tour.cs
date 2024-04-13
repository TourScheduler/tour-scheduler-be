using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class Tour : Entity
    {
        public long AuthorId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DifficultType Difficult { get; init; }
        public CategoryType Category { get; init; }
        public double Price { get; init; }
        public StatusType Status { get; private set; }
        public List<KeyPoint> KeyPoints { get; init; }

        public Tour(long authorId, string name, string description, DifficultType difficult, CategoryType category, double price, StatusType status, List<KeyPoint> keyPoints)
        {
            AuthorId = authorId;
            Name = name;
            Description = description;
            Difficult = difficult;
            Category = category;
            Price = price;
            Status = status;
            KeyPoints = keyPoints;
            Validate();
        }
        private void Validate()
        {
            if (AuthorId == 0) throw new ArgumentException("Invalid AuthorId");
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
            if (Price <= 0) throw new ArgumentException("Invalid Price");
        }

        public void Publish()
        {
            Validate();
            if (KeyPoints.Count >= 2)
            {
                Status = StatusType.Published;
            }
        }
    }
}
