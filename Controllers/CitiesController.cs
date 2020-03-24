
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using dsi_coding_challenge.Utils;

namespace dsi_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : ControllerBase
    {

        private readonly ILogger<CitiesController> _logger;
        private Dictionary<string, List<GeoName>> byName;

        public CitiesController(ILogger<CitiesController> logger, IDataUtil dataUtil)
        {
            _logger = logger;
            byName = dataUtil.GetData();
        }

        [HttpGet("{city}")]
        public GeoName GetFirst(string city)
        {
            List<GeoName> result;
            byName.TryGetValue(city, out result);

            if (result != null)
            {
                return result.First();
            }
            return new GeoName();
        }

        [HttpGet("")]
        public GeoName[] GetFirstLike(string like, double? latitude, double? longitude)
        {
            List<GeoName> result = new List<GeoName>();
            foreach(string key in byName.Keys)
            {
                if (key.Contains(like))
                {
                    List<GeoName> thisGeoNameList;
                    byName.TryGetValue(key, out thisGeoNameList);
                    GeoName thisGeoName = thisGeoNameList.First();
                    CalculateScore(thisGeoName, key, like, latitude, longitude);
                    result.Add(thisGeoName);
                }
            }

            return OrderAndTruncateList(result);
        }

        // I realize that this doesn't make a RESTful controller, but I wanted to make sure that it was clear that I understand there are more results that what is expected in the README
        [HttpGet("/all-cities/{city}")]
        public GeoName[] Get(string city)
        {
            List<GeoName> result;
            byName.TryGetValue(city, out result);

            if (result != null)
            {
                return result.ToArray();
            }
            return Array.Empty<GeoName>();
        }

        // I realize that this doesn't make a RESTful controller, but I wanted to make sure that it was clear that I understand there are more results that what is expected in the README
        [HttpGet("/all-cities")]
        public GeoName[] GetLike(string like, double? latitude, double? longitude)
        {
            List<GeoName> result = new List<GeoName>();
            foreach (string key in byName.Keys)
            {
                if (key.Contains(like))
                {
                    List<GeoName> thisGeoNameList;
                    byName.TryGetValue(key, out thisGeoNameList);
                    foreach (GeoName geoName in thisGeoNameList)
                    {
                        CalculateScore(geoName, key, like, latitude, longitude);
                    }
                    result.AddRange(thisGeoNameList);

                }
            }

            return OrderAndTruncateList(result);
        }

        private GeoName[] OrderAndTruncateList(List<GeoName> geoNamesList)
        {
            List<GeoName> sortedList = geoNamesList.OrderByDescending(g => g.Score).ToList();
            if (sortedList.Count < 25)
            {
                return sortedList.ToArray();
            }

            List<GeoName> result = new List<GeoName>();
            result.AddRange(sortedList.GetRange(0, 25));

            return result.ToArray();
        }

        private void CalculateScore(GeoName geoName, string city, string like, double? latitude, double? longitude)
        {
            double matchScore = (double)like.Length / (double)city.Length;
            double coordScore;

            if (latitude != null && longitude != null)
            {
                double latDiff = geoName.Latitude - (double)latitude;
                double longDiff = geoName.Longitude - (double)longitude;
                double distance = Math.Sqrt(Math.Pow(latDiff, 2) + Math.Pow(longDiff, 2));
                distance = Math.Min(255.0, distance);
                coordScore = 1.0 - (distance / 255.0);
            } else
            {
                coordScore = matchScore;
            }

            double avgScore = (matchScore + coordScore) / 2;
            geoName.Score = Math.Round(avgScore, 2);
        }
    }
}
