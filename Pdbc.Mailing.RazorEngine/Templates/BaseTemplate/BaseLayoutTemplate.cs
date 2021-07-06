using System;
using RazorEngineCore;

namespace Pdbc.Mailing.RazorEngine.Templates.BaseTemplate
{
    public class BaseLayoutTemplate : RazorEngineTemplateBase
    {
        public Func<string, object, string> IncludeCallback { get; set; }

        public Func<string> RenderBodyCallback { get; set; }
        public Func<string> RenderTextBodyCallback { get; set; }

        public string Layout { get; set; }

        public string Include(string key, object model = null)
        {
            return this.IncludeCallback(key, model);
        }

        public string RenderBody()
        {
            return this.RenderBodyCallback();
        }

        public string RenderTextMail()
        {
            return this.RenderTextBodyCallback();
        }
    }
}
