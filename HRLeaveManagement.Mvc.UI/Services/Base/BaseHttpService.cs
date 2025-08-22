namespace HRLeaveManagement.Mvc.UI.Services.Base
{
    public class BaseHttpService
    {
        protected readonly IClient _client;

        public BaseHttpService(IClient client)
        {
            _client = client;
        }

        protected Response<Guid> ConvertApiExceptions(ApiException exception)
        {
            if (exception.StatusCode == 400)
            {
                return new Response<Guid>
                {
                    Success = false,
                    Message = "Bad Request: The request was invalid or cannot be served. Please check the input data.",
                    Error = exception.Response
                };
            }
            else if(exception.StatusCode == 404)
            {
                return new Response<Guid>
                {
                    Success = false,
                    Message = "Not Found: The requested resource could not be found. Please check the URL or resource ID.",
                    Error = exception.Response
                };
            }
            else
            {
                return new Response<Guid>
                {
                    Success = false,
                    Message = "An unexpected error occurred while processing your request.",
                    Error = exception.Response
                };
            }
        }
    }
}
