using System;
using System.IO;
using CsvHelper;
using System.Globalization;
using System.Collections.Generic;

namespace dsi_coding_challenge.Utils
{
    public class DataUtil
    {
        public static Dictionary<string, List<GeoName>> readCsv()
        {
            Dictionary<string, List<GeoName>> byName = new Dictionary<string, List<GeoName>>();
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
                    GeoName thisGeoName = new GeoName
                    {
                        City = csvReader.GetField<string>("name"),
                        State = csvReader.GetField<string>("admin1"),
                        Country = csvReader.GetField<string>("country"),
                        AlternateNames = csvReader.GetField<string>("alt_name").Split(','),
                        Latitude = csvReader.GetField<double>("lat"),
                        Longitude = csvReader.GetField<double>("long")
                    };
                    if (thisGeoName.AlternateNames[0] == "")
                    {
                        thisGeoName.AlternateNames = Array.Empty<string>();
                    }
                    List<GeoName> thisList;
                    if (byName.ContainsKey(thisGeoName.City))
                    {
                        byName.TryGetValue(thisGeoName.City, out thisList);
                        thisList.Add(thisGeoName);
                    }
                    else
                    {
                        thisList = new List<GeoName>
                        {
                            thisGeoName
                        };
                        byName.Add(thisGeoName.City, thisList);
                    }
                }
            }

            return byName;
        }
    }
}
