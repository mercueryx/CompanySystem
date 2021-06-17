using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Consignment_Control.Library.Framework.Mvc
{
    /// <summary>
    /// Model Binder
    /// </summary>
    public class ModelBinder : DefaultModelBinder
    {
        /// <summary>
        /// Bind Model
        /// </summary>
        /// <param name="controllerContext">Controller Context</param>
        /// <param name="bindingContext">Binding Context</param>
        /// <returns></returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
            if (model is BaseModel)
            {
                ((BaseModel)model).BindModel(controllerContext, bindingContext);
            }
            return model;
        }
    }
}