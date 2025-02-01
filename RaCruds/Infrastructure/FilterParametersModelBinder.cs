using Microsoft.AspNetCore.Mvc.ModelBinding;
using RaCruds.Models.Statements;

namespace RaCruds.Infrastructure;

public class FilterParametersModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        if (bindingContext.ModelType != typeof(FilterParameters))
        {
            return Task.CompletedTask;
        }


        // Try to fetch the value of the argument by name
        var queryParameters = bindingContext.HttpContext.Request.Query;

        if (!queryParameters.Any())
        {
            var par = new FilterParameters();
            bindingContext.Result = ModelBindingResult.Success(par);
            return Task.CompletedTask;
        }

        var parser = new FilterParametersParser();
        var model = parser.ParseModel(queryParameters);

        if (model == null)
        {
            return Task.CompletedTask;
        }

        bindingContext.Result = ModelBindingResult.Success(model);

        return Task.CompletedTask;
    }


}
