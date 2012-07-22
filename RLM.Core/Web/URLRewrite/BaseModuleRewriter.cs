using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace RLM.Core.Web.URLRewrite
{
    public abstract class BaseModuleRewriter : IHttpModule
    {
        // Methods
        protected BaseModuleRewriter()
        {
        }

        protected virtual void BaseModuleRewriter_AuthorizeRequest(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            this.Rewrite(app.Request.Path, app);
        }

        public virtual void Dispose()
        {
        }

        public virtual void Init(HttpApplication app)
        {
            app.AuthorizeRequest += new EventHandler(this.BaseModuleRewriter_AuthorizeRequest);
        }

        protected abstract void Rewrite(string requestedPath, HttpApplication app);
    }

 

}
