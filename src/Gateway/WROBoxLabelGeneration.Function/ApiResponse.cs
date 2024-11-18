using Microsoft.AspNetCore.Http;

namespace WROBoxLabelGeneration.Function
{
    public class ApiResponse
    {
        public bool Success { get; }
        public object? Data { get; }
        public List<string> ErrorMessage { get; }
        public int StatusCode { get; }

        private ApiResponse(bool success, object? data, List<string> errorMessage, int statusCode)
        {
            Success = success;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        //public static IResult CreateResponse(Result result)
        //{
        //    var response = new ApiResponse(
        //        success: result.IsSuccess,
        //        data: null,
        //        errorMessage: result.ErrorResult.Errors,
        //        statusCode: (int) result.ResultType
        //    );

        //    return Results.Json(response, statusCode: (int)result.ResultType);
        //}

        //public static IResult CreateResponse<T>(Result<T> result)
        //{
        //    var response = new ApiResponse(
        //        success: result.IsSuccess,
        //        data: result.IsSuccess ? result.Value : null,
        //        errorMessage: result.ErrorResult.Errors,
        //        statusCode: (int)result.ResultType
        //    );

        //    return Results.Json(response, statusCode: (int)result.ResultType);
        //}
    }
}
