using System.ComponentModel.DataAnnotations;

namespace IcePayment.API.Helpers
{
    public class ValidationMethods
    {
        public static ValidationResult? ValidatePositive(decimal value, ValidationContext context)
        {
            if (value is > 0 and <= decimal.MaxValue)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(
                $"The field {context.MemberName} must be a valid value greater than 0.",
                new List<string>() { context.MemberName });
        }
    }
}
