using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionDiscovery.Services.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }

        public T Data { get; set; }

        public Exception Error { get; set; }
        public ServiceResult(T data, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Data = data;
        }

        public ServiceResult(Exception error)
        {
            IsSuccess = false;
            Error = error;
        }
    }
}
