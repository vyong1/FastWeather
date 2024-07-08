using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastWeather.Model.ZipCodes
{
    /// <summary>
    /// 
    /// </summary>
    internal class ZipCodeInfo
    {
        /// <summary>
        /// iso country code, 2 characters
        /// </summary>
        public string CountryCode { get; set; }
        [Key]
        public string ZipCode { get; set; }
        public string PlaceName { get; set; }

        /// <summary>
        /// order subdivision (state)
        /// </summary>
        public string AdminName1 { get; set; }

        /// <summary>
        /// order subdivision (state)
        /// </summary>
        public string AdminCode1 { get; set; }

        /// <summary>
        /// order subdivision (county/province)
        /// </summary>
        public string AdminName2 { get; set; }

        /// <summary>
        /// order subdivision (county/province)
        /// </summary>
        public string AdminCode2 { get; set; }

        /// <summary>
        /// order subdivision (community)
        /// </summary>
        public string AdminName3 { get; set; }

        /// <summary>
        /// order subdivision (community)
        /// </summary>
        public string AdminCode3 { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        /// <summary>
        /// accuracy of lat/lng from 1=estimated, 4=geonameid, 6=centroid of addresses or shape
        /// </summary>
        public int Accuracy { get; set; }

        public ZipCodeInfo() { }
    }
}
