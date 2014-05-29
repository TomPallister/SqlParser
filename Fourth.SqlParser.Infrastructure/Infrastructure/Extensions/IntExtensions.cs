using System.Globalization;

namespace Fourth.SqlParser.Infrastructure.Infrastructure.Extensions
{
    public static class IntExtensions
    {
        public static string PadLeft(this int value, int totalWidth, char paddingChar)
        {
            string valueString = value.ToString(CultureInfo.InvariantCulture);
            return valueString.Length > totalWidth ? valueString : valueString.PadLeft(totalWidth, paddingChar);
        }

        public static string ToParcelNumber(this int value)
        {
            return value.PadLeft(6, '0');
        }
    }
}