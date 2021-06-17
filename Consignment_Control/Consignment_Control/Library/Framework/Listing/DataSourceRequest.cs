using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Framework.Listing
{
    public class DataSourceRequest
    {
        public DataSourceRequest()
        {
        }

        /// <summary>
        /// Draw
        /// </summary>
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        public int PageNumber
        {
            get
            {
                var pageNumber = PageIndex / PageSize;
                if (PageIndex % PageSize != 0)
                    pageNumber += 1;

                return pageNumber;
            }
        }

        /// <summary>
        /// Page index
        /// </summary>
        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        public int PageIndex => Start;

        /// <summary>
        /// Page size
        /// </summary>
        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        public int PageSize => Length;

        /// <summary>
        /// Global Search for the table
        /// </summary>
        [JsonProperty(PropertyName = "search")]
        public DataSourceSearch Search { get; set; }
    }

    /// <summary>
    /// Represents search values entered into the table
    /// </summary>
    public sealed class DataSourceSearch
    {
        /// <summary>
        /// Global search value. To be applied to all columns which have searchable as true
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        /// <summary>
        /// true if the global filter should be treated as a regular expression for advanced 
        /// searching, false otherwise. Note that normally server-side processing scripts 
        /// will not perform regular expression searching for performance reasons on large 
        /// data sets, but it is technically possible and at the discretion of your script
        /// </summary>
        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }
    }
}