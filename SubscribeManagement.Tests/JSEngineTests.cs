using Microsoft.ClearScript.V8;
using SubscribeManagement.WebAPI.DA.Entities;
using System;
using System.Dynamic;
using Xunit;

namespace SubscribeManagement.Tests
{
    public class JSEngineTests
    {
        [Fact]
        public void Calculate()
        {
            using (var engine = new V8ScriptEngine())
            {
                var scriptStr = @"function sum(a,b) { return a+b; }";
                engine.Execute(scriptStr);
                var result = engine.Evaluate("sum(1,2)");
                Console.WriteLine($"result type:{result.GetType().Name},result is:{result}.");
            }
        }
        [Fact]
        public void CalculateByDefinedObject()
        {
            using (var engine = new V8ScriptEngine())
            {
                engine.Script.numberA = 2;
                engine.AddHostObject("numberB", new { value = 2 });
                var scriptStr = @"function sum() { return numberA+numberB.value; }";
                engine.Execute(scriptStr);
                var result = engine.Evaluate("sum()");
                Console.WriteLine($"result type:{result.GetType().Name},result is:{result}.");
            }
        }
        [Fact]
        public void VMess_Parse_ShouldBe_Ok()
        {
            using (var engine = new V8ScriptEngine())
            {
                dynamic expando = new ExpandoObject();
                expando.ConfigVersion = "1";
                expando.Remark = "";
                expando.Address = "127.0.0.1";
                expando.Port = "10443";

                engine.AddHostType("helper", typeof(JavascriptEngineHelper));
                engine.AddHostObject("data", expando);
                //        result = Consts.VMessProtocolPrefix + Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(qrCode)));
                var scriptStr = @"
                                function parse(){ 
                                    let text = {
                                         v: data.ConfigVersion,
                                         ps: data.Remark,
                                         add: data.Address,
                                         port: data.Port,
                                         id: data.Id,
                                         aid: data.AlterId,
                                         net: data.Network,
                                         type: data.HeaderType,
                                         host: data.RequestHost,
                                         path: data.Path,
                                         tls: data.StreamSecurity,
                                         sni: data.SNI
                                    }
                                    let jsonStr = JSON.stringify(text);
                                    return `vmess://${helper.ToBase64(jsonStr)}`;
                                }
                                ";
                engine.Execute(scriptStr);
                var result = engine.Evaluate("parse()");
                Console.WriteLine(result);
                Console.WriteLine($"result type:{result.GetType().Name},result is:{result}.");
            }
        }
    }
}
