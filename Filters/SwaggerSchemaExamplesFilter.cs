using EcommerceAPI.DTOs;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class SwaggerSchemaExamplesFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(ClientDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["clientId"] = new OpenApiInteger(1),
                ["name"] = new OpenApiString("John Doe"),
                ["email"] = new OpenApiString("john.doe@example.com"),
                ["address"] = new OpenApiString("123 Main St, Anytown, USA"),
                ["password"] = new OpenApiString("SecureP@ssw0rd")
            };
        }
        else if (context.Type == typeof(ProductDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["productId"] = new OpenApiInteger(1),
                ["name"] = new OpenApiString("Laptop"),
                ["price"] = new OpenApiDouble(1500.99),
                ["description"] = new OpenApiString("A high-end gaming laptop")
            };
        }
        else if (context.Type == typeof(CartDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["cartId"] = new OpenApiInteger(1),
                ["clientId"] = new OpenApiInteger(1),
                ["items"] = new OpenApiArray
                {
                    new OpenApiObject
                    {
                        ["productId"] = new OpenApiInteger(1),
                        ["quantity"] = new OpenApiInteger(2)
                    },
                    new OpenApiObject
                    {
                        ["productId"] = new OpenApiInteger(2),
                        ["quantity"] = new OpenApiInteger(1)
                    }
                }
            };
        }
        else if (context.Type == typeof(AuthDTO))
        {
            schema.Example = new OpenApiObject
            {
                ["email"] = new OpenApiString("john.doe@example.com"),
                ["password"] = new OpenApiString("SecureP@ssw0rd")
            };
        }
    }
}
