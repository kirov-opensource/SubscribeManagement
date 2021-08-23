using Microsoft.ClearScript.V8;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;
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
        public void TestVMessURIParse_ShouldBeOk()
        {
            using (var engine = new V8ScriptEngine())
            {
                dynamic connection = new ExpandoObject();
                connection.ConfigVersion = "1";
                connection.Remark = @"";
                connection.Address = "127.0.0.1";
                connection.Port = "10443";

                engine.AddHostType("helper", typeof(JavascriptEngineHelper));

                var scriptStr = @"
                                function parse(connection){ 
                                    let text = {
                                         v: connection.ConfigVersion,
                                         ps: connection.Remark,
                                         add: connection.Address,
                                         port: connection.Port,
                                         id: connection.Id,
                                         aid: connection.AlterId,
                                         net: connection.Network,
                                         type: connection.HeaderType,
                                         host: connection.RequestHost,
                                         path: connection.Path,
                                         tls: connection.StreamSecurity,
                                         sni: connection.SNI
                                    }
                                    let jsonStr = JSON.stringify(text);
                                    return `vmess://${helper.ToBase64(jsonStr)}`;
                                }
                                ";
                engine.Execute(scriptStr);
                var result = engine.Evaluate($"parse({System.Text.Json.JsonSerializer.Serialize(connection)})");

                Assert.Equal("vmess://eyJ2IjoiMSIsInBzIjoiIiwiYWRkIjoiMTI3LjAuMC4xIiwicG9ydCI6IjEwNDQzIn0=", result);
            }
        }

        [Fact]
        public void TestV2RaySubscribeParse_ShouldBeOk()
        {
            using (var engine = new V8ScriptEngine())
            {
                var connections = new object[]{
                    new { URI="vmess://eyJ2IjoiMSIsInBzIjoiIiwiYWRkIjoiMTI3LjAuMC4xIiwicG9ydCI6IjEwNDQzIn0=" }
                };

                engine.AddHostType("helper", typeof(JavascriptEngineHelper));

                var scriptStr = @"
                                function parse(connections){ 
                                    var text = '';
                                    if(Array.isArray(connections) && connections.length > 0) {
                                        for (let i = 0; i < connections.length; i++){
                                            text = text + connections[i].URI;
                                            if(i+1 !== connections.length){
                                                text += '\r\n';
                                            }
                                        }
                                    }
                                    return helper.ToBase64(text);
                                }
                                ";
                engine.Execute(scriptStr);
                var result = engine.Evaluate($"parse({JsonSerializer.Serialize(connections)})");
                Assert.Equal("dm1lc3M6Ly9leUoySWpvaU1TSXNJbkJ6SWpvaUlpd2lZV1JrSWpvaU1USTNMakF1TUM0eElpd2ljRzl5ZENJNklqRXdORFF6SW4wPQ==", result);
            }
        }
    }
}
