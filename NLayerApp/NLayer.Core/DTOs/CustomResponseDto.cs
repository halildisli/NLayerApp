using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CustomResponseDto<T>
    {
        public T? Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<string>? Errors { get; set; }

        //Bu kullanım Factory Design Pattern'dan gelir. Buna Static Factory Method denir. Bunun amacı cevap dönüleceği zaman instance alma işlemi class içerisinde yapılması amacıyla yapılır.

        public static CustomResponseDto<T> Success(int statusCode,T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode, Errors = null };
        }
        public static CustomResponseDto<T>Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }
        public static CustomResponseDto<T> Fail(int statusCode, string error)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> {error } };
        }
        public static CustomResponseDto<T> Fail(int statusCode,List<string> errors)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode,Errors=errors };
        }
    }
}
