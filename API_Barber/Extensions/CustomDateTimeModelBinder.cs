using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System;
using System.Globalization;
using System.Threading.Tasks;

public class CustomDateTimeModelBinder : IModelBinder
{
    private readonly string _customFormat = "dd/MM/yyyy HH:mm";

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        if (DateTime.TryParseExact(value, _customFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
        {
            bindingContext.Result = ModelBindingResult.Success(parsedDate);
        }
        else
        {
            bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Invalid date format.");
        }

        return Task.CompletedTask;
    }
}
