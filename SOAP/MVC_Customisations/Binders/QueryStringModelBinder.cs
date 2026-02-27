using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SOAP.MVC.ModelBinding;

class QueryStringNullOrEmptyModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        throw new NotImplementedException();
    }
}