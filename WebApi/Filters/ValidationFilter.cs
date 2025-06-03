using FluentValidation;

using Microsoft.AspNetCore.Mvc.Filters;


namespace WebApi.Filters
{
    public class ValidationFilter<T> : IAsyncActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dto = context.ActionArguments.Values.OfType<T>().FirstOrDefault();
            if (dto == null)
            {
                throw new Core.Utilities.Exceptions.ValidationException($"{nameof(dto)}, null olamaz");
            }
            var result = await _validator.ValidateAsync(dto);
            if (!result.IsValid)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new Core.Utilities.Exceptions.ValidationException("Veri Doğrulama Başarısız", errors);
            }

            await next();
        }
    }

}
