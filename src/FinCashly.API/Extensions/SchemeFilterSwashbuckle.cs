using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

#nullable disable
namespace FinCashly.API.Extensions;
public class SchemeFilterSwashbuckle : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (!context.Type.IsEnum) return;

        var enumType = context.Type;
        var enumValues = Enum.GetValues(enumType).Cast<Enum>();

        schema.Enum.Clear();

        foreach (var value in enumValues)
        {
            var description = value.GetDescription();
            schema.Enum.Add(new OpenApiString($"{description}"));
        }
    }
}

public static class EnumExtensions
{
    static public string GetDescription(this Enum enumValue)
    {
        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
            return enumValue.ToString();

        var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
        {
            return attribute.Description;
        }

        return enumValue.ToString();
    }
}