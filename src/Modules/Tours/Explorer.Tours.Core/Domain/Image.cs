using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain
{
    public class Image : ValueObject
    {
        public string Filename { get; init; }
        public byte[] Data { get; init; }
        [JsonConstructor]
        public Image(string filename, byte[] data)
        {
            Filename = filename;
            Data = data;
            Validate();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Filename;
            yield return Data;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Filename)) throw new ArgumentException("Invalid Filename");
            if (Data.Length <= 0) throw new ArgumentException("Invalid Data");
        }
    }
}
