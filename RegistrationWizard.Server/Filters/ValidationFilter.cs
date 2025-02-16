
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace RegistrationWizard.Server.Filters
{
    public class ValidationFilter<T> : ActionFilterAttribute
    {

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var validator  = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if (validator != null)
            {

                var entity = context.ActionArguments.Values.OfType<T>().FirstOrDefault();
                if (entity != null)
                {
                    var validation = await validator.ValidateAsync(entity);
                    if (validation.IsValid)
                    {
                        await next();
                        return;
                    }
                    context.Result = new BadRequestObjectResult(validation.ToDictionary());
                    return;
                    
                }
            }

            await next();
        }

    }
}
