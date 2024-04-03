using Explorer.BuildingBlocks.Core.Domain;
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
        public List<Tour> Tours { get; init; }

        public Author()
        {
            Tours = new List<Tour>();
        }

        public Author(long userId, List<Tour> tours)
        {
            UserId = userId;
            Tours = tours;
            Validate();
        }
        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
        }
    }
}
