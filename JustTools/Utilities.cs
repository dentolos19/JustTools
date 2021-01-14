using System;
using System.Text;

namespace JustTools
{

    public static class Utilities
    {

        public static string EncodeToBase64(string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            return Uri.EscapeUriString(Convert.ToBase64String(bytes));
        }

        public static string DecodeFromBase64(string text)
        {
            var bytes = Convert.FromBase64String(Uri.UnescapeDataString(text));
            return Encoding.UTF8.GetString(bytes);
        }

    }

}