using System.Net;

namespace Journey.Exception.ExceptionsBase
{
    public class NotFoundException : JourneyException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IList<string> GetErrorMessages()
        {
            /*Essa abaixo pode ser simplificada para (return [Message] ;)*/
            return new List<string>
            {
                Message
            };
        }

        public override HttpStatusCode GetStatusCode()
        {
            return HttpStatusCode.NotFound;
        }
    }
}
