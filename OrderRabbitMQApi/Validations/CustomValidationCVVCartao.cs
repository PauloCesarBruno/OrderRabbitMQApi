using System.ComponentModel.DataAnnotations;

namespace ValidacoesAPI.Validations
{
    public class CustomValidationCVVCartao : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {               
            if (value != null)
            {
                string cvv = value.ToString();
                if (cvv.Length  == 3 && cvv.All(Char.IsDigit))
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult("CVV inválido !");
        }
    }
}