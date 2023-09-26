using Bonheur.Outlets.Manager.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Bonheur.Outlets.API.Utils
{
    public class ApiController : ControllerBase
    {
        protected IConfiguration _configuration;
        protected IWebHostEnvironment _environment;
        public ApiController(
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            _configuration = configuration;
            _environment = environment;
        }

        [NonAction]
        public ActionResult ProcessAPIException(Exception ex)
        {
            if (ex is UnauthorizedAccessException)
            {
                return Forbid();
            }
            else
            {

                if (ex is OutletsDataException)
                {
                    if (((OutletsDataException)ex).ValidationMessages == null ||
                        ((OutletsDataException)ex).ValidationMessages.Count == 0)
                    {
                        ((OutletsDataException)ex).ValidationMessages = new Dictionary<string, string>
                        {
                            { "Message", "Invalid operation" }
                        };
                    }

                    return StatusCode(400, new
                    {
                        IsSuccess = false,
                        IsValidation = true,
                        Message = "",
                        Validation = ((OutletsDataException)ex).ValidationMessages
                    });
                }
                else
                {
                    string lstrMessage = "Unknown error occured";

                    if (!this._environment.IsProduction())
                        lstrMessage += ": " + ex.Message;

                    return StatusCode(400, new
                    {
                        IsSuccess = false,
                        IsValidation = false,
                        Message = lstrMessage
                    });
                }
            }
        }
    }
}
