using System.ComponentModel.DataAnnotations;

namespace Framework.Enums
{
    public enum ApiResultStatusCode
    {
        [Display(Name = "The Request was successful.")]
        Success = 0,
        
        [Display(Name = "The Server encountered error(s).")]
        ServerError = 1,
        
        [Display(Name = "Validation errors or Bad request options.")]
        BadRequest = 2,
        
        [Display(Name = "Requested entity was not found.")]
        NotFound = 3,
        
        [Display(Name = "Application logic error occured.")]
        LogicError = 4,
        
        [Display(Name = "UnAuthorize request.")]
        UnAuthorized = 5
    }
}