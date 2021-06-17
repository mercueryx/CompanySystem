using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Consignment_Control.Library.Framework.Mvc
{
    /// <summary>
    /// Base Model
    /// </summary>
    [ModelBinder(typeof(ModelBinder))]
    public abstract partial class BaseModel
    {
        /// <summary>
        /// Assign Model
        /// </summary>
        public virtual void BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
        }

        /// <summary>
        /// Developers can override this method in custom partial classes
        /// in order to add some custom initialization code to constructors
        /// </summary>
        protected virtual void PostInitialize()
        {

        }

        /// <summary>
        /// Use this property to store any custom value for your models. 
        /// </summary>
        public Dictionary<string, object> CustomProperties { get; set; }
    }
}