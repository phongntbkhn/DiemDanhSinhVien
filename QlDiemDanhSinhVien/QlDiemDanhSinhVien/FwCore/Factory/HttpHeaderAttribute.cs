using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.FwCore.Factory
{
    public class HttpHeaderAttribute : ActionFilterAttribute
    {
        /// 
        /// Gets or sets the name of the HTTP Header.
        /// 
        /// The name.
        public string Name { get; set; }

        /// 
        /// Gets or sets the value of the HTTP Header.
        /// 
        /// The value.
        public string Value { get; set; }

        /// 
        /// Initializes a new instance of the  class.
        /// 
        /// The name.
        /// The value.
        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.AppendHeader(Name, Value);
            base.OnResultExecuted(filterContext);
        }
    }
}