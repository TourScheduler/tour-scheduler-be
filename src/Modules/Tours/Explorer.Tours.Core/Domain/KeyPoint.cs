using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Explorer.Tours.Core.Domain
{
    public class KeyPoint : ValueObject
    {
        public double Longitude {  get; init; }
        public double Latitude { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public Image Image { get; init; }

        [JsonConstructor]
        public KeyPoint(double longitude, double latitude, string name, string description, Image image)
        {
            Longitude = longitude;
            Latitude = latitude;
            Name = name;
            Description = description;
            Image = image;
            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
            yield return Name;
            yield return Description;
            yield return Image;
        }

        private void Validate()
        {
            if (Longitude == 0) throw new ArgumentException("Invalid Longitude");
            if (Latitude == 0) throw new ArgumentException("Invalid Latitude");
            if (string.IsNullOrWhiteSpace(Name)) throw new ArgumentException("Invalid Name");
            if (string.IsNullOrWhiteSpace(Description)) throw new ArgumentException("Invalid Description");
        }
    }
}
