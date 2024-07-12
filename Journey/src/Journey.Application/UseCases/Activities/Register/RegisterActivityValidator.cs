using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception;

namespace Journey.Application.UseCases.Activities.Register;
/*
 *A responsabilidade do me validator é apenas de validar os dados da REQUISIÇÃO
 *A resposabilidade do validator é testar propriedades em si
 */
public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.NAME_EMPTY);
    }
}
