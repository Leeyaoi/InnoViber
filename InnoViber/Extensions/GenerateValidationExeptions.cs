using FluentValidation.Results;
using System.Text;

namespace InnoViber.API.Extensions;

 public static class GenerateValidationExeptions
{
    public static void GenerateValidationExeption(this ValidationResult result)
    {
        var exceptionMessage = new StringBuilder();
        foreach(var error in result.Errors)
        {
            exceptionMessage.AppendLine($"Property: {error.PropertyName} Severity: {error.Severity}");
        }

        throw new Exception(exceptionMessage.ToString());
    }
}
