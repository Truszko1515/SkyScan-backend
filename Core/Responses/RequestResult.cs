using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Responses
{
    public class RequestResult<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static RequestResult<T> Success(string? message = null, T? data = default)
        {
            return new RequestResult<T> { IsSuccess = true, Message = message, Data = data };
        }

        public static RequestResult<T> Failure(string message)
        {
            return new RequestResult<T> { IsSuccess = false, Message = message };
        }
    }

    public class RequestResult
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static RequestResult Success(string? message = null, object? data = default)
        {
            return new RequestResult { IsSuccess = true, Message = message, Data = data };
        }

        public static RequestResult Failure(string message)
        {
            return new RequestResult { IsSuccess = false, Message = message };
        }
    }
}
