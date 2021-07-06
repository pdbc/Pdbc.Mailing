using System;


namespace Pdbc.Mailing.RazorEngine.Extensions
{
    public static class StringExtensions
    {
        public static String SafeTrim(this string s)
        {
            if (s == null)
                return String.Empty;

            return s.Trim();
        }
    }
}
