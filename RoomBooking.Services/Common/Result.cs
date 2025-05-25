using RoomBooking.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoomBooking.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public StatusCode StatusCode { get; private set; }
        public string Message { get; private set; }

        protected Result(bool isSuccess, string message, StatusCode statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCode = statusCode;
        }

        public static Result Success() => new(true, string.Empty, Enums.StatusCode.Ok);
        public static Result Failure(string errorMessage, StatusCode statusCode) => new(false, errorMessage, statusCode);
    }
}
