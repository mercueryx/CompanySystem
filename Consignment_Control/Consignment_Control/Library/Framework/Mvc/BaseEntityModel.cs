using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Consignment_Control.Library.Framework.Mvc
{
    public partial class BaseEntityModel : BaseModel
    {
        [Key]
        public virtual int Id { get; set; }
    }
}