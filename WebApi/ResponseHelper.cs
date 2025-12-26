using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebApi;

public class ResponseHelper
{
    public static ActionResult GenerateResponse<T>(Result<T> data)
    {
        if (data.Successed)
        {
            var responseObject = new
            {
                messages = data.Messages,
                succeeded = data.Successed,
                data = data.Data,
                code=data.Code,
                exception = data.Exception,
                token = data.Token

            };

            return new OkObjectResult(responseObject);
        }
        else
        {
            var errorObject = new
            {
                messages = data.Messages,
                succeeded = data.Successed,
                data = data.Data,
                exception = data.Exception,
                code = data.Code,
                token = data.Token

            };

            return new ObjectResult(errorObject)
            {
                StatusCode = data.Code
            };
        }
    }
}
