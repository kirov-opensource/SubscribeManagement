using System;
using System.Text;

namespace SubscribeManagement.WebAPI.Helpers
{
    public class JavascriptEngineHelper
    {
        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }
    }
}
