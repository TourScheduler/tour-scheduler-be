using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Interest : ValueObject
    {
        public InterestType Type { get; private set; }

        [JsonConstructor]
        public Interest(InterestType type)
        {
            Type = type;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
        }
    }

    public enum InterestType
    {
        Nature,
        Art,
        Sport,
        Shopping,
        Food
    }
}
