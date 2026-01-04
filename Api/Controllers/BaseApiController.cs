using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public abstract class BaseApiController : ControllerBase
{
    protected ObjectResult BadRequestWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Bad Request",
            message = message
        })
        {
            StatusCode = 400
        };
    }

    protected ObjectResult UnauthorizedWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Unauthorized",
            message = message
        })
        {
            StatusCode = 401
        };
    }

    protected ObjectResult ForbiddenWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Forbidden",
            message = message
        })
        {
            StatusCode = 403
        };
    }

    protected ObjectResult NotFoundWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Not Found",
            message = message
        })
        {
            StatusCode = 404
        };
    }

    protected ObjectResult ConflictWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Conflict",
            message = message
        })
        {
            StatusCode = 409
        };
    }

    protected ObjectResult UnprocessableEntityWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Unprocessable Entity",
            message = message
        })
        {
            StatusCode = 422
        };
    }

    protected ObjectResult InternalServerErrorWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Internal Server Error",
            message = message
        })
        {
            StatusCode = 500
        };
    }

    protected ObjectResult ServiceUnavailableWithMessage(string message)
    {
        return new ObjectResult(new
        {
            error = "Service Unavailable",
            message = message
        })
        {
            StatusCode = 503
        };
    }

    // Generic method for any status code
    protected ObjectResult ErrorWithMessage(int statusCode, string message)
    {
        return new ObjectResult(new
        {
            error = statusCode switch
            {
                400 => "Bad Request",
                401 => "Unauthorized",
                403 => "Forbidden",
                404 => "Not Found",
                409 => "Conflict",
                422 => "Unprocessable Entity",
                500 => "Internal Server Error",
                503 => "Service Unavailable",
                _ => "Error"
            },
            message = message
        })
        {
            StatusCode = statusCode
        };
    }
}