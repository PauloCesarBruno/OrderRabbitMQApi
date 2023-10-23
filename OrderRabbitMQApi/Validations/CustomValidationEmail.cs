using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ValidacoesAPI.Validations
{
    public class CustomValidationEmail : ValidationAttribute
    {
        public CustomValidationEmail()
        {
        }

        protected override ValidationResult IsValid(
            object value,
            ValidationContext validationContext)
        {

            // Verifica se o Valor é nulo
            if (value == null)
            {
                value = "";
            }

            // Retira espaços do final da literal
            value = value.ToString().TrimEnd();                 


            // Atribui expressao Regex
            Regex regExpEmail = new Regex(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$");


            // Valida a expressao
            Match match = regExpEmail.Match(value.ToString());

            if (match.Success)
            {
                return ValidationResult.Success; ;
            }

            // Devolve o erro padrao se a expressao nao for valida.
            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }
    }
}