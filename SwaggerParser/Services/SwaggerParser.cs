using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using RazorEngine;
using SwaggerParser.Model.Api;
using SwaggerParser.Model.SwaggerApi;

namespace SwaggerParser.Services
{
    public class SwaggerParser
    {
        public void ParseSpecDefinition(string name, Listing spec)
        {
            ConvertJsonSchemaToPoco schemaConverter = new ConvertJsonSchemaToPoco();

            JObject rootSchema = new JObject();
            rootSchema["properties"] = spec.Models;
            rootSchema["type"] = "object";

            //We need to remove all empty "requires ellements since newtonsoft errors on those
            removeFields(rootSchema, new[] {"required"});
            string modelsCSharp = schemaConverter.Generate(rootSchema.ToString());


            ApiDefinition apiModel  = ConvertApiSpecToCode.GenerateApiDefinition(name, spec);


            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SwaggerParser.Templates.Api.template.cshtml";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string template = reader.ReadToEnd();
                string ret = Razor.Parse(template, apiModel);
            }

            

        }


        private void removeFields(JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                removeFields(el, fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }
        }
    }
}
