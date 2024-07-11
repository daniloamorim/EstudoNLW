using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Journey.Communication.Responses;

namespace Journey.Application.UseCases.Trips.Register
{
    public class RegisterTripUseCase
    {
        public ResponseShortTripJson Execute(RequestRegisterTripJson request)
        {
            Validate(request);

            var dbContext = new JourneyDBContext();

            var entity = new Trip
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
            };

            dbContext.Trips.Add(entity);

            dbContext.SaveChanges();

            return new ResponseShortTripJson
            {
                Id = entity.Id,
                Name = entity.Name,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
            };
        }

        private void Validate(RequestRegisterTripJson request) 
        {
            if(string.IsNullOrWhiteSpace(request.Name))
            {
                throw new JourneyException(ResourceErrorMessages.NAME_EMPTY);
            }

            if (request.StartDate.Date < DateTime.UtcNow.Date)
            {
                throw new JourneyException(ResourceErrorMessages.START_DATE_TRIP);
            }

            if (request.EndDate.Date < request.StartDate.Date)
            {
                throw new JourneyException(ResourceErrorMessages.END_DATE_TRIP);
            }
        }
    }
}
