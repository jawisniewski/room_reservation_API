using RoomBooking.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Common
{
    public class Result<T> : Result
    {
        public T? Value { get; private set; }
        private Result(T? value, bool isSuccess, string message, StatusCode statusCode)
            : base(isSuccess, message, statusCode)
        {
            Value = value;
        }
        public static Result<T> Success(T value)
            => new(value, true, string.Empty, StatusCode.Ok);
        public new static Result<T> Failure(string errorMessage, StatusCode statusCode) 
            => new(default,false, errorMessage, statusCode);

    }
}
