using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BasePlatform.Library.Framework.Controllers
{
    /// <summary>
    /// Form Required / Form UI Control
    /// </summary>
    public class FormValueRequiredAttribute : ActionMethodSelectorAttribute
    {
        private readonly string[] _submitButtonNames;
        private readonly FormValueRequirement _requirement;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="submitButtonNames"></param>
        public FormValueRequiredAttribute(params string[] submitButtonNames) :
            this(FormValueRequirement.Equal, submitButtonNames)
        {
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="submitButtonNames"></param>
        public FormValueRequiredAttribute(FormValueRequirement requirement, params string[] submitButtonNames)
        {
            //at least one submit button should be found
            this._submitButtonNames = submitButtonNames;
            this._requirement = requirement;
        }

        /// <summary>
        /// UI Validation
        /// </summary>
        /// <param name="controllerContext">Controller</param>
        /// <param name="methodInfo">Access Methods</param>
        /// <returns>True = Valid</returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            foreach (string buttonName in _submitButtonNames)
            {
                try
                {
                    string value = "";
                    switch (this._requirement)
                    {
                        case FormValueRequirement.Equal:
                            {
                                //do not iterate because "Invalid request" exception can be thrown
                                value = controllerContext.HttpContext.Request.Form[buttonName];
                            }
                            break;
                        case FormValueRequirement.StartsWith:
                            {
                                foreach (var formValue in controllerContext.HttpContext.Request.Form.AllKeys)
                                {
                                    if (formValue.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase))
                                    {
                                        value = controllerContext.HttpContext.Request.Form[formValue];
                                        break;
                                    }
                                }
                            }
                            break;
                    }
                    if (!String.IsNullOrEmpty(value))
                        return true;
                }
                catch (Exception exc)
                {
                    //try-catch to ensure that 
                    Debug.WriteLine(exc.Message);
                }
            }
            return false;
        }
    }

    /// <summary>
    /// Form UI Control Operation
    /// </summary>
    public enum FormValueRequirement
    {
        /// <summary>
        /// Equal
        /// </summary>
        Equal,
        /// <summary>
        /// Start With
        /// </summary>
        StartsWith
    }
}