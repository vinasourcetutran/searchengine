using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace RLM.Core.Web.URLRewrite
{
    public class RewriterFactoryHandler : IHttpHandlerFactory
    {
        // Methods
        public virtual IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            context.Trace.Write("RewriterFactoryHandler", "Entering RewriterFactoryHandler");
            string sendToUrl = url;
            string filePath = pathTranslated;
            RewriterRuleCollection rules = RewriterConfiguration.GetConfig().Rules;
            for (int i = 0; i < rules.Count; i++)
            {
                Regex regex = new Regex("^" + RewriterUtils.ResolveUrl(context.Request.ApplicationPath, rules[i].LookFor) + "$", RegexOptions.IgnoreCase);
                if (regex.IsMatch(url))
                {
                    string str4;
                    sendToUrl = RewriterUtils.ResolveUrl(context.Request.ApplicationPath, regex.Replace(url, rules[i].SendTo));
                    context.Trace.Write("RewriterFactoryHandler", "Found match, rewriting to " + sendToUrl);
                    RewriterUtils.RewriteUrl(context, sendToUrl, out str4, out filePath);
                    context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");
                    return PageParser.GetCompiledPageInstance(str4, filePath, context);
                }
            }
            context.Trace.Write("RewriterFactoryHandler", "Exiting RewriterFactoryHandler");
            return PageParser.GetCompiledPageInstance(url, filePath, context);
        }

        public virtual void ReleaseHandler(IHttpHandler handler)
        {
        }
    }


}
