using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class GeoCodeDataResult
    {
        public Item[] items { get; set; }
    }

    public class Item
    {
        public Address address { get; set; }
        public Position position { get; set; }

        [JsonIgnore]
        public string title { get; set; }

        [JsonIgnore]
        public string id { get; set; }

        [JsonIgnore]
        public string resultType { get; set; }

        [JsonIgnore]
        public string localityType { get; set; }

        [JsonIgnore]
        public Mapview mapView { get; set; }

        [JsonIgnore]
        public Scoring scoring { get; set; }
    }

    public class Address
    {
        public string countryName { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }

        [JsonIgnore]
        public string label { get; set; }

        [JsonIgnore]
        public string countryCode { get; set; }

        [JsonIgnore]
        public string stateCode { get; set; }

        [JsonIgnore]
        public string countyCode { get; set; }

        [JsonIgnore]
        public string county { get; set; }
    }

    public class Position
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Mapview
    {
        public double west { get; set; }
        public double south { get; set; }
        public double east { get; set; }
        public double north { get; set; }
    }

    public class Scoring
    {
        [JsonIgnore]
        public int queryScore { get; set; }

        [JsonIgnore]
        public Fieldscore fieldScore { get; set; }
    }

    public class Fieldscore
    {
        public int city { get; set; }
    }

}
