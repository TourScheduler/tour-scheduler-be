using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Tourist : Entity
    {
        public long UserId { get; init; }
        public List<Interest> Interests { get; private set; }

        public Tourist(long userId, List<Interest> interests) 
        {
            UserId = userId;
            Interests = interests;
            Validate();
        }
        private void Validate()
        {
            if (UserId == 0) throw new ArgumentException("Invalid UserId");
            if (Interests.Count == 0) throw new ArgumentException("Interests empty");
        }

        public void UpdateInterests(List<Interest> interests)
        {
            Interests = interests;
        }
    }
}
