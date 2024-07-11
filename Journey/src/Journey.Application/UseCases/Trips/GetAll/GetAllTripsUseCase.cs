using Journey.Communication.Responses;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Trips.GetAll
{
    public class GetAllTripsUseCase
    {
        public ResponseTripsJson Execute()
        {
            var dbContext = new JourneyDBContext();

            var trips = dbContext.Trips.ToList();

            return new ResponseTripsJson
            {
                Trips = trips.Select(trips => new ResponseShortTripJson
                {
                    Id = trips.Id,
                    Name = trips.Name,
                    StartDate = trips.StartDate,
                    EndDate = trips.EndDate
                }).ToList(),
            };
        }
    }
}
