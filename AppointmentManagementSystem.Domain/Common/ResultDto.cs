using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Common
{
    public class ResultDto<T> 
    {
       

        public bool Successed { get; private set; }
        public string Message { get; private set; }
        public T Data { get; private set; }
        public ResultDto()
        {
                
        }
        private ResultDto(bool success, string message, T data )
        {
            Successed = success;
            Message = message;
            Data = data;
        }

        public ResultDto(bool success, string message)
        {
            Successed = success;
            Message = message;
        }

        public static ResultDto<T> Success(T data, string message = "İşlem başarılı.")
        {
            return new ResultDto<T>(true, message, data);
        }

        public static ResultDto<T> Fail(string message = "İşlem başarısız.", T data = default)
        {
            return new ResultDto<T>(false, message, data);
        }

        public static ResultDto<T> Fail(string message = "İşlem başarısız.")
        {
            return new ResultDto<T>(false, message);
        }
    }
}
