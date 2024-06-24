using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Deliverers.Application.Validators;

public class CnpjAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is string str &&
        !string.IsNullOrWhiteSpace(str) &&
        IsCnpj(str);
    }

    // https://www.macoratti.net/11/09/c_val1.htm
    private static bool IsCnpj(string str)
    {
        var multiplier1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplier2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int sum;
        int rest;
        string digit;
        string tempCnpj;

        str = str.Trim();
        str = str.Replace(".", "").Replace("-", "").Replace("/", "");

        if (str.Length != 14)
            return false;

        tempCnpj = str.Substring(0, 12);
        sum = 0;

        for (var i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        rest = sum % 11;

        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit = rest.ToString();
        tempCnpj += digit;
        sum = 0;

        for (var i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        rest = sum % 11;
        if (rest < 2)
            rest = 0;
        else
            rest = 11 - rest;

        digit += rest.ToString();

        return str.EndsWith(digit);
    }
}
