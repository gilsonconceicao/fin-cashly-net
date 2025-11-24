namespace FinCashly.Application.Utils;

public static class StringUtils
{
    public static string GetAvailableValues<TEnum>() where TEnum : struct, Enum
    {
        return string.Join(", ",
            Enum.GetValues<TEnum>()
                .Select(v => $"{Convert.ToInt32(v)}: {v.GetDescription()}"));
    }
}