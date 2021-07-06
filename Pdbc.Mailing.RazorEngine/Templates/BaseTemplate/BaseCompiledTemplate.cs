using System;
using System.Collections.Generic;
using System.IO;
using RazorEngineCore;

namespace Pdbc.Mailing.RazorEngine.Templates.BaseTemplate
{
    public class BaseCompiledTemplate
    {
        private readonly IRazorEngineCompiledTemplate<BaseLayoutTemplate> _compiledTemplate;
        private readonly Dictionary<string, IRazorEngineCompiledTemplate<BaseLayoutTemplate>> _compiledParts;

        public BaseCompiledTemplate(IRazorEngineCompiledTemplate<BaseLayoutTemplate> compiledTemplate, Dictionary<string, IRazorEngineCompiledTemplate<BaseLayoutTemplate>> compiledParts)
        {
            this._compiledTemplate = compiledTemplate;
            this._compiledParts = compiledParts;
        }

        public string Run(object model)
        {
            return this.Run(this._compiledTemplate, model);
        }

        public string Run(IRazorEngineCompiledTemplate<BaseLayoutTemplate> template, object model)
        {
            BaseLayoutTemplate templateReference = null;

            string result = template.Run(instance =>
            {
                if (!(model is InternalAnonymousTypeWrapper))
                {
                    model = new InternalAnonymousTypeWrapper(model);
                }

                instance.Model = model;
                instance.IncludeCallback = (key, includeModel) => this.Run(this._compiledParts[key], includeModel);

                templateReference = instance;
            });

            if (templateReference.Layout == null)
            {
                return result;
            }

            return this._compiledParts[templateReference.Layout].Run(instance =>
            {
                if (!(model is InternalAnonymousTypeWrapper))
                {
                    model = new InternalAnonymousTypeWrapper(model);
                }

                instance.Model = model;
                instance.IncludeCallback = (key, includeModel) => this.Run(this._compiledParts[key], includeModel);
                instance.RenderBodyCallback = () => result;
                instance.RenderTextBodyCallback = () => "The text of the mail should come here !!!!";
            });
        }

        public void Save(String path, string key)
        {
            var filename = Path.Combine(path, key);
            this._compiledTemplate.SaveToFile(filename);

            foreach (var compiledPart in this._compiledParts)
            {
                var partFilename = Path.Combine(path, $"{key}_{compiledPart.Key}");
                compiledPart.Value.SaveToFile(partFilename);
            }
        }

        public void Load(String path, string key)
        {
            
        }
    }
}