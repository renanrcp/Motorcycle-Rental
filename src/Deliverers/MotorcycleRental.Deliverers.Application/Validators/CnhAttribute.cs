using System.ComponentModel.DataAnnotations;

namespace MotorcycleRental.Deliverers.Application.Validators;

public class CnhAttribute : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        return value is string str &&
        !string.IsNullOrWhiteSpace(str) &&
        IsCnh(str);
    }

    // https://gist.github.com/naldorp/4241ade12a427855e7c184cf0099060b
    private static bool IsCnh(string str)
    {
        var firstChar = str[0];

        if (str.Length != 11 || str == new string('1', 11))
        {
            return false;
        }

        var dsc = 0;
        var v = 0;

        for (int i = 0, j = 9; i < 9; i++, j--)
        {
            v += int.Parse(str[i].ToString()) * j;
        }

        var vl1 = v % 11;
        if (vl1 >= 10)
        {
            vl1 = 0;
            dsc = 2;
        }

        v = 0;
        for (int i = 0, j = 1; i < 9; ++i, ++j)
        {
            v += int.Parse(str[i].ToString()) * j;
        }

        var x = v % 11;
        var vl2 = (x >= 10) ? 0 : x - dsc;

        return vl1.ToString() + vl2.ToString() == str.Substring(str.Length - 2, 2);
    }
}
