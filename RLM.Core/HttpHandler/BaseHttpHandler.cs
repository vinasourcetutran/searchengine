using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RLM.Core.HttpHandler
{
    public class BaseHttpHandler : System.Web.IHttpHandler
    {
        public virtual void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("RLM.Core.HttpHandler.BaseHttpHandler");
        }
        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
