using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Core.Entity
{
    public abstract partial class BaseEntity : CoreBaseEntity
    {
        /// <summary>
        /// Record Created's User ID
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Record System date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Record Last Transaction's User ID
        /// </summary>
        public int UpdatedBy { get; set; }

        /// <summary>
        /// Record System date
        /// </summary>
        public DateTime UpdatedDate { get; set; }
    }
}