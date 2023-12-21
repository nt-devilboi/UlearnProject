using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using UdvBackend.Infrastructure.Result;

namespace EduControl;

public class ApiResult<T> : Result<T, string>, IConvertToActionResult
{
    private int statusCode { get; set; }

    public ApiResult(T value) : base(value)
    { 
        statusCode = 200;
    }

    public ApiResult(string error, string? errorExplain, int statusCode) : base(error, errorExplain)
    {
        this.statusCode = statusCode;
    }


    public static implicit operator ApiResult<T>(T value) => new(value);

    public IActionResult Convert()
    {
        return new ObjectResult(this)
        {
            DeclaredType = typeof (Result<T, string>),
            StatusCode = statusCode
        };
    }
}