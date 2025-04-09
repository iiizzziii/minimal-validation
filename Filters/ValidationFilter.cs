using System.ComponentModel.DataAnnotations;

namespace minimal_validation.Filters;

public class ValidationFilter<T> : IEndpointFilter where T : class
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context, 
        EndpointFilterDelegate next)
    {
        var requestModel = context.Arguments.FirstOrDefault(arg => arg is T) as T;
        if (requestModel == null) return Results.BadRequest($"___ {typeof(T).Name}");
        
        var validationContext = new ValidationContext(requestModel);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            requestModel, 
            validationContext, 
            validationResults, 
            validateAllProperties: true);

        if (!isValid) return Results.BadRequest(validationResults.Select(e => e.ErrorMessage ?? "_").ToList());

        return await next(context);
    }
}

// public class UniversalValidationFilter : IEndpointFilter
// {
//     public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
//     {
//         var validationErrors = new List<string>();
//
//         foreach (var argument in context.Arguments)
//         {
//             if (argument is null)
//                 continue;
//
//             var validationContext = new ValidationContext(argument);
//             var results = new List<ValidationResult>();
//
//             bool isValid = Validator.TryValidateObject(argument, validationContext, results, true);
//
//             if (!isValid)
//             {
//                 validationErrors.AddRange(results.Select(r => r.ErrorMessage ?? "Invalid value"));
//             }
//         }
//
//         if (validationErrors.Any())
//         {
//             return Results.BadRequest(new CustomErrorResponse
//             {
//                 Errors = validationErrors
//             });
//         }
//
//         return await next(context);
//     }
// }