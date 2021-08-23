using Microsoft.ClearScript.V8;
using SubscribeManagement.WebAPI.Helpers;
using System;

namespace SubscribeManagement.WebAPI.Extensions
{
    public static class JavascriptExtensions
    {
        /// <summary>
        /// 执行Javascript
        /// </summary>
        public static (bool, string) Evaluate(this string scriptString, object data)
        {
            using (var engine = new V8ScriptEngine())
            {
                engine.AddHostType("helper", typeof(JavascriptEngineHelper));

                engine.Execute(scriptString);
                try
                {
                    var result = engine.Evaluate($"parse({System.Text.Json.JsonSerializer.Serialize(data)})");
                    return (true, result.ToString());
                }
                catch (Exception e)
                {
                    return (false, e.ToString());
                }
            }
        }
    }
}
