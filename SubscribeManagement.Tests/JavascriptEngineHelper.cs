using System;
using System.Collections.Generic;
using System.Text;

namespace SubscribeManagement.Tests
{
    public static class JavascriptEngineHelper
    {
        public static string ToBase64(string text)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
        }
    }
}
