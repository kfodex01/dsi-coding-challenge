using System;
namespace dsi_coding_challenge
{
    public class GeoName
    {
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string[] AlternateNames { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
