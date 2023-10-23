using System.ComponentModel.DataAnnotations;

namespace ValidacoesAPI.Validations;

public class CustomValidationPrimeiraLetraMaiuscula : ValidationAttribute  // Herda sempre desta classe
{
    protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        var primeiraletra = value.ToString()[0].ToString();
        if (primeiraletra != primeiraletra.ToUpper())
        {
            return new ValidationResult("A primeira letra do nome do produto deve ser maiuscula.");
        }

        return ValidationResult.Success;
    }
}