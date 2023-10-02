using Domain.Enums;
using System.Net;

namespace Domain.Dtos
{
    public class BaseResponse<T> where T : class
    {
        public T? Data { get; set; }
        public ResponseStatu StatusCode { get; set; }
        public string? Message { get; set; }
        public BaseResponse() { }
        public BaseResponse(T data)
        {
            Data= data;
            StatusCode = ResponseStatu.Success;
        }
        public BaseResponse(string message)
        {
            Message= message;
            StatusCode = ResponseStatu.Error;
        }
        public BaseResponse(T data, string message)
        {
            Data = data;
            Message = message;
            StatusCode = ResponseStatu.Warning;
        }
    }
}
