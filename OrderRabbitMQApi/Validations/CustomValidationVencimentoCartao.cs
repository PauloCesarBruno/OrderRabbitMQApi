using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ValidacoesAPI.Validations;

public class  MesAnoAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        DateTime dateTime;
        var isValid = DateTime.TryParseExact(Convert.ToString(value),
            "MM/yy",
            CultureInfo.CurrentCulture,
            DateTimeStyles.None,
            out dateTime);

        // Verifica se o ano é "00"
        if (dateTime.Year == 2000)
        {
            return false;
        }

        return isValid;
    }
}
