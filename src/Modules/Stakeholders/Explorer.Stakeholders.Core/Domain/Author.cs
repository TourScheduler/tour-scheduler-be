using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Author : Entity
    {
        public long UserId { get; init; }
        public int Points { get; private set; }
        public bool IsAwarded { get; private set; }
        public List<Tour> Tours { get; init; }

        public Author()
        {
            Tours = new List<Tour>();
        }

        public Author(long userId, int points, bool isAwarded, List<Tour> tours)
        {
            UserId = userId;
            Tours = tours;
            Points = points;
            IsAwarded = isAwarded;
            Validate();
        }
        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
        }

        public void AddPoints()
        {
            Points++;
        }

        public void Award()
        {
            if (IsAwarded == false && Points >= 5)
            {
                IsAwarded = true;
            }
        }
    }
}
