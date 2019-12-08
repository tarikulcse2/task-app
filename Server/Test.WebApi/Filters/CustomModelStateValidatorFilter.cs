using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Test.WebApi.Filters
{
    public sealed class CustomModelStateValidatorFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var errors = new Dictionary<string, string[]>();
            var builder = new System.Text.StringBuilder();
            foreach (var err in context.ModelState)
            {
                builder.Append("FieldName-" + err.Key + " ErrorMessage-" + err.Value.Errors.Select(a => a.ErrorMessage).FirstOrDefault() + " ");
            }
            var message = "Validation Failed @ " + builder.ToString();
            context.Result = new ObjectResult(new { Status = false, Message = message, MessageType = "ModelValidation", Data = (string)null });
        }
    }
}