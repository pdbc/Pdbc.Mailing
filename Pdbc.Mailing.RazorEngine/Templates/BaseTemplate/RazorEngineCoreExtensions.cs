using System.Collections.Generic;
using System.Linq;

namespace Pdbc.Mailing.RazorEngine.Templates.BaseTemplate
{
    public static class RazorEngineCoreExtensions
    {
        public static BaseCompiledTemplate Compile(this RazorEngineCore.RazorEngine razorEngine, 
            string template, 
            IDictionary<string, string> parts)
        {
            return new BaseCompiledTemplate(
                razorEngine.Compile<BaseLayoutTemplate>(template),
                parts.ToDictionary(
                    k => k.Key,
                    v => razorEngine.Compile<BaseLayoutTemplate>(v.Value)));
        }
    }
}