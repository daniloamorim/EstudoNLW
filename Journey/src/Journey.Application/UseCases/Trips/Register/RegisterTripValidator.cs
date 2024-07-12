using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;
namespace Journey.Application.UseCases.Trips.Register;

public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
    public RegisterTripValidator()
    {/*RuleFor
      * Permite que eu crie as regras
      * essa primeira campo em branco
      * segunda maior ou igual]
      * terceira condição verdadeira
      */
        RuleFor(request => request.Name).NotEmpty().WithMessage(ResourceErrorMessages.NAME_EMPTY);

        RuleFor(request => request.StartDate.Date)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage(ResourceErrorMessages.START_DATE_TRIP);

        /*Must
         * Posso criar minha própria condição
         * precisa de uma validação que seja verdadeira */
        RuleFor(request => request)
            .Must(request => request.EndDate.Date >= request.StartDate.Date)
            .WithMessage(ResourceErrorMessages.END_DATE_TRIP);
    }
}
