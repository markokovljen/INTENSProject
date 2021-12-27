using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Errors;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ApiError GenerateError(object currentObject,string objectName)
        {
            if (currentObject == null)
            {
                ApiError apiError = new ApiError(BadRequest().StatusCode,
                                                  "Invalid id for " + objectName,
                                                  "This error apear when provided " + objectName + " do not exists");
                return apiError;
            }

            return null;

        }
    }
}
