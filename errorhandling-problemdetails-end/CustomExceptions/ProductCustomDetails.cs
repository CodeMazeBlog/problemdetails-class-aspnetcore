using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingProblemDetails.CustomExceptions
{
    public class ProductCustomDetails : ProblemDetails
    {
        public string AdditionalInfo { get; set; }
    }
}
