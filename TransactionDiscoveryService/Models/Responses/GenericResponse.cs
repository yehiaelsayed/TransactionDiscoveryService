using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactonDiscovery.Utils;

namespace TransactionDiscovery.API.Models.Responses
{
    public class GenericResponse<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public object Error { get; set; }

        public void Success(T data)
        {
            Data = data;
            IsSuccess = true;
        }
        public void Failed(string errorMessage)
        {
            IsSuccess = false;
            Error = new { message = errorMessage, ErrorId = Logger.CorrelationID };
        }
    }
}
