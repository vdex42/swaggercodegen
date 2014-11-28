using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Schema;

//Credit for this Gist goes to "rushfrisby" https://gist.github.com/rushfrisby/c8f58f346548bf31e045
namespace SwaggerParser.Services
{
    public class ConvertJsonSchemaToPoco
    {

        private const string Cyrillic = "Cyrillic";
        private const string Nullable = "?";

        public string Generate(string schemaText)
        {
            JsonSchema schemaresult = JsonSchema.Parse(schemaText);
            if (schemaresult == null) throw new Exception("Invalid Schema");
            var sb = ConvertJsonSchemaToPocos(schemaresult);
            var code = sb.ToString();
            //return code;
            return "";
        }

        private static StringBuilder ConvertJsonSchemaToPocos(JsonSchema schema)
        {
            if (schema.Type == null)
                throw new Exception("Schema does not specify a type.");

            var sb = new StringBuilder();

            switch (schema.Type)
            {
                case JsonSchemaType.Object:
                    sb.Append(ConvertJsonSchemaObjectToPoco(schema));
                    break;

                case JsonSchemaType.Array:
                    foreach (var item in schema.Items.Where(x => x.Type.HasValue && x.Type == JsonSchemaType.Object))
                    {
                        sb.Append(ConvertJsonSchemaObjectToPoco(item));
                    }
                    break;
            }

            return sb;
        }

        private static StringBuilder ConvertJsonSchemaObjectToPoco(JsonSchema schema)
        {
            string className;
            return ConvertJsonSchemaObjectToPoco(schema, out className);
        }

        private static StringBuilder ConvertJsonSchemaObjectToPoco(JsonSchema schema, out string className)
        {
            var sb = new StringBuilder();
            sb.Append("public class ");

            className = schema.Id;
            sb.Append(className);
            sb.AppendLine(" {");

            foreach (var item in schema.Properties)
            {
                sb.Append("public ");
                sb.Append(GetClrType(item.Value, sb));
                sb.Append(" ");
                sb.Append(item.Key.Trim());
                sb.AppendLine(" { get; set; }");
            }

            sb.AppendLine("}");
            return sb;
        }

        private static string GenerateSlug(string phrase)
        {
            var str = RemoveAccent(phrase);
            str = Regex.Replace(str, @"[^a-zA-Z\s-]", ""); // invalid chars
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space, trim
            str = Regex.Replace(str, @"\s", "_"); // convert spaces to underscores
            return str;
        }

        private static string RemoveAccent(string txt)
        {
            var bytes = Encoding.GetEncoding(Cyrillic).GetBytes(txt);
            return Encoding.ASCII.GetString(bytes);
        }

        private static string GetClrType(JsonSchema jsonSchema, StringBuilder sb)
        {
            switch (jsonSchema.Type)
            {
                case JsonSchemaType.Array:
                    if (jsonSchema.Items.Count == 0)
                        return "IEnumerable<object>";
                    if (jsonSchema.Items.Count == 1)
                        return String.Format("IEnumerable<{0}>", GetClrType(jsonSchema.Items.First(), sb));
                    throw new Exception("Not sure what type this will be.");

                case JsonSchemaType.Boolean:
                    return String.Format("bool{0}", jsonSchema.Required.HasValue && jsonSchema.Required.Value ? string.Empty : Nullable);
                    
                case JsonSchemaType.Float:
                    return String.Format("float{0}", jsonSchema.Required.HasValue && jsonSchema.Required.Value ? string.Empty : Nullable);

                case JsonSchemaType.Integer:
                    return String.Format("int{0}", jsonSchema.Required.HasValue && jsonSchema.Required.Value ? string.Empty : Nullable);

                case JsonSchemaType.String:
                    return "string";

                case JsonSchemaType.Object:
                    string className;
                    sb.Insert(0, ConvertJsonSchemaObjectToPoco(jsonSchema, out className));
                    return className;

                case JsonSchemaType.None:
                case JsonSchemaType.Null:
                default:
                    return "object";
            }
        }
    }
}