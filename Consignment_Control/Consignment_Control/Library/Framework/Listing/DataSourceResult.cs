using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Framework.Listing
{
    public class DataSourceResult
    {
        /// <summary>
        /// Page number
        /// </summary>
        [JsonProperty(PropertyName = "draw")]
        public int draw { get; set; }

        /// <summary>
        /// Data
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public IEnumerable data { get; set; }

        /// <summary>
        /// Total records
        /// </summary>
        [JsonProperty(PropertyName = "recordsTotal")]
        public int recordsTotal { get; set; }

        /// <summary>
        /// Total records has been filtered
        /// </summary>
        [JsonProperty(PropertyName = "recordsFiltered")]
        public int recordsFiltered { get; set; }
    }
}