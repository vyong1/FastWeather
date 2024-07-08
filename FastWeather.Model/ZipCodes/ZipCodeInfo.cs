using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastWeather.Model.ZipCodes
{
    /// <summary>
    /// See: US_ZipCodesReadme.txt for schema info
    /// </summary>
    public class ZipCodeInfo
    {
        /// <summary>
        /// iso country code, 2 characters
        /// </summary>
        public string? CountryCode { get; set; }

        public int ZipCode { get; set; }

        public string? PlaceName { get; set; }

        /// <summary>
        /// order subdivision (state)
        /// </summary>
        public string? AdminName1 { get; set; }

        /// <summary>
        /// order subdivision (state)
        /// </summary>
        public string? AdminCode1 { get; set; }

        /// <summary>
        /// order subdivision (county/province)
        /// </summary>
        public string? AdminName2 { get; set; }

        /// <summary>
        /// order subdivision (county/province)
        /// </summary>
        public string? AdminCode2 { get; set; }

        /// <summary>
        /// order subdivision (community)
        /// </summary>
        public string? AdminName3 { get; set; }

        /// <summary>
        /// order subdivision (community)
        /// </summary>
        public string? AdminCode3 { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        /// <summary>
        /// accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape
        /// </summary>
        public int Accuracy { get; set; }

        public ZipCodeInfo(int zipCode) 
        { 
            ZipCode = zipCode;
        }

        /// <summary>
        /// Schema:
        ///   country code
        ///   postal code 
        ///   place name  
        ///   admin name1 
        ///   admin code1 
        ///   admin name2 
        ///   admin code2 
        ///   admin name3 
        ///   admin code3 
        ///   latitude    
        ///   longitude   
        ///   accuracy    
        /// </summary>
        /// <param name="lineFromTheFile"></param>
        /// <exception cref="Exception"></exception>
        public ZipCodeInfo(string lineFromTheFile) // TODO - Move this somewhere else
        {
            string[] split = lineFromTheFile.Split('\t', StringSplitOptions.RemoveEmptyEntries);
            this.CountryCode = split[0];
            this.ZipCode = int.Parse(split[1]);
            this.PlaceName = split[2];
            // Accuracy is an optional parameter and may not show up in the text
            bool accuracyExists = false;
            if (int.TryParse(split[split.Length - 1], out int accuracy))
            {
                Accuracy = accuracy;
                accuracyExists = true;
            }
            int numbersAtTheEnd = accuracyExists ? 3 : 2;

            this.Latitude = float.Parse(split[split.Length - numbersAtTheEnd]);
            this.Longitude = float.Parse(split[split.Length - numbersAtTheEnd + 1]);

            // AdminName/AdminCode are optional parameters and may not show up in the text
            for (int i = 3; i < split.Length - 3; i++)
            {
                switch (i)
                {
                    case 3:
                        this.AdminName1 = split[i];
                        break;
                    case 4:
                        this.AdminCode1 = split[i];
                        break;
                    case 5:
                        this.AdminName2 = split[i];
                        break;
                    case 6:
                        this.AdminCode2 = split[i];
                        break;
                    case 7:
                        this.AdminName3 = split[i];
                        break;
                    case 8:
                        this.AdminCode3 = split[i];
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"{ZipCode}: {PlaceName} ({Latitude}, {Longitude})";
        }

        public static IEnumerable<ZipCodeInfo> ReadFromFile(string filename)
        {
            var lines = File.ReadAllLines(filename);
            List<ZipCodeInfo> zips = new();
            foreach (string line in lines)
            {
                zips.Add(new ZipCodeInfo(line));
            }
            return zips;
        }
    }
}
