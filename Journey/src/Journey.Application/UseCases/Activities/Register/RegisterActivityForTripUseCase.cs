using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityForTripUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
    {
        var dbContext = new JourneyDBContext();

        var trip = dbContext
            .Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        Validate(trip, request);

        var entity = new Activity
        {
            Name = request.Name,
            Date = request.Date,
            TripId = tripId,
        };

        dbContext.Activities.Add(entity);
        dbContext.SaveChanges();

        /*esse caso e preciso alterar o nome da tabela em alguns lugares por causa do banco de dados que
         * estou usando no MySql fuincionaria por exemplo
         * var dbContext = new JourneyDBContext();

        var trip = dbContext
            .Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id == tripId);

        Validate(trip, request);

        var entity = new Activity
        {
            Name = request.Name,
            Date = request.Date
        };
        //a exclamação é apenas para dizer para o compilador
        //que pode fazer pois ja estou validando que nao vai ser nulo
        trip!.Activities.Add(entity);
        //nessa linha ele ja relaciona a chave primaria da outra tabela
        dbContext.Trips.Update(trip);
        dbContext.SaveChanges();*/

        return new ResponseActivityJson
        {
            Id = entity.Id,
            Date = entity.Date,
            Name = entity.Name,
            Status = (Communication.Enums.ActivityStatus)entity.Status
        };
    }
    /*
     * Como preciso validar regra de neócio é direto no UseCase
     */
    private void Validate(Trip? trip, RequestRegisterActivityJson request)
    {
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
        }

        //Crio a instancia do meu validator
        var validator = new RegisterActivityValidator();
        //Utilizo ele para validar a minha request
        var result = validator.Validate(request);

        //Regra de negocio para validar a data das atividades durante a viagem
        if((request.Date >= trip.StartDate && request.Date <= trip.EndDate) == false)
        {
            result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.DATE_NOT_WITHIN_TRAVEL_PERIOD));
        }

        if (result.IsValid == false)
        {
            //Crio ma lista com meus erros caso a condição entre aqui e no final trasformo em lista
            var errosMessages = result.Errors.Select(error => error.ErrorMessage).ToList(); 
            //passo minha lista como parametro para meu constrtor do error
            throw new ErrorOnValidationException(errosMessages);
        }
    }
}
