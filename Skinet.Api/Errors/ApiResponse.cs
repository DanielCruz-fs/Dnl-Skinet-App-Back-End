using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skinet.Api.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? this.GetDefaultMessageForStatusCode(StatusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Generates a custom error response message yoda style
        /// </summary>
        /// <param name="statusCode">type integer</param>
        /// <returns>Custom error style as a string</returns>
        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Authorized, you are not",
                404 => "Resource found, it was not",
                500 => "Errors are the path to the dark side. Errors lead to anger. Anger leads to hate. " +
                "Hate leads to career change",
                _ => null
            };
        }
    }
}
