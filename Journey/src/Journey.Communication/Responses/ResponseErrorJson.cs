namespace Journey.Communication.Responses
{
    public class ResponseErrorJson
    {
        /* Com essa linha retorno uma lista iniciando ela vazia
         * em versao mais antiga usaria = new List<string>();
         */
        public IList<string> Errors { get; set; } = [];

        public ResponseErrorJson(IList<string> errors)
        {
           Errors = errors;
        }
    }
}
