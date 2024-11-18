using Newtonsoft.Json;
using System.Net;
using WROBoxLabelGeneration.Infrastructure.HttpClients.Exceptions;

namespace WROBoxLabelGeneration.Infrastructure.HttpClients
{
    public abstract class BaseProxy
    {
        public async Task<T> ProcessResponseAsync<T>(HttpResponseMessage response, string endpointName, object dataForError = null)
        {
            await EnsureSuccessStatusAsync(response, endpointName, dataForError);

            var bodyResponse = await response.Content.ReadAsStringAsync();

            return DeserializeResponse<T>(bodyResponse);
        }

        public async Task ValidateResponseAsync(HttpResponseMessage response, string endpointName, object dataForError = null)
        {
            await EnsureSuccessStatusAsync(response, endpointName, dataForError);
        }

        private async Task EnsureSuccessStatusAsync(HttpResponseMessage response, string endpointName, object dataForError = null)
        {
            if (response.IsSuccessStatusCode) { return; }

            string className = GetType().Name;
            var bodyResponse = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new BadRequestException(className, endpointName, $"Bad request: {bodyResponse}");

                case HttpStatusCode.Unauthorized:
                    throw new UnauthorizedException(className, endpointName, $"Unauthorized access: {bodyResponse}");

                case HttpStatusCode.Forbidden:
                    throw new ForbiddenException(className, endpointName, $"Access forbidden: {bodyResponse}");

                case HttpStatusCode.NotFound:
                    string message = HandleNotFoundErrorMessage(endpointName, dataForError);
                    if (string.IsNullOrEmpty(message))
                    {
                        message = bodyResponse;
                    }
                    throw new NotFoundException(className, endpointName, message);

                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException(className, endpointName, $"Internal server error: {bodyResponse}");

                default:
                    throw new HttpApiManagerException(className, endpointName, response.StatusCode, HandleUnknownResponseErrorMessage(endpointName, response, bodyResponse));
            }
        }

        protected virtual T DeserializeResponse<T>(string bodyResponse)
        {
            return JsonConvert.DeserializeObject<T>(bodyResponse);
        }

        // This method can be overridden if you want to customize the Not Found message
        // for each of the endpoints that the class has. By default it will return an
        // empty string thus generating an exception with the response from the API.
        protected virtual string HandleNotFoundErrorMessage(string endpointName, object dataForError)
        {
            return string.Empty;
        }

        // This method can be overridden if you want to customize the UknowError message
        // for each of the endpoints that the class has. By default it will return an
        // empty string thus generating an exception with the response from the API.
        protected virtual string HandleUnknownResponseErrorMessage(string endpointName, HttpResponseMessage response, string bodyResponse)
        {
            return $"An error occurred while trying to connect to the ${endpointName}. Error: {(string.IsNullOrWhiteSpace(bodyResponse) ? response.StatusCode.ToString() : bodyResponse)}";
        }

    }
}
