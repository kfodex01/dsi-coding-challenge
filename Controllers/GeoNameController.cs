using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CsvHelper;
using System.Globalization;

namespace dsi_coding_challenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {

        private readonly ILogger<CityController> _logger;
        private Dictionary<string, List<GeoName>> byName = new Dictionary<string, List<GeoName>>();

        public CityController(ILogger<CityController> logger)
        {
            _logger = logger;
            using (StreamReader streamReader = new StreamReader("data/canada_usa_cities.tsv"))
            using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.Delimiter = "\t";
                csvReader.Configuration.HasHeaderRecord = true;
                csvReader.Configuration.BadDataFound = null;
                csvReader.Read();
                csvReader.ReadHeader();
                while (csvReader.Read())
                {
                    GeoName thisGeoName = new GeoName {
                        City = csvReader.GetField<string>("name"),
                        State = csvReader.GetField<string>("admin1"),
                        Country = csvReader.GetField<string>("country"),
                        AlternateNames = csvReader.GetField<string>("alt_name").Split(','),
                        Latitude = csvReader.GetField<double>("lat"),
                        Longitude = csvReader.GetField<double>("long")
                    };
                    List<GeoName> thisList;
                    if (byName.ContainsKey(thisGeoName.City))
                    {
                        byName.TryGetValue(thisGeoName.City, out thisList);
                        thisList.Add(thisGeoName);
                    } else
                    {
                        thisList = new List<GeoName>
                        {
                            thisGeoName
                        };
                        byName.Add(thisGeoName.City, thisList);
                    }
                }
            }
        }

        [HttpGet]
        public GeoName Get()
        {
            List<GeoName> result;
            byName.TryGetValue("Des Moines", out result);

            return result.First();
        }
    }
}
