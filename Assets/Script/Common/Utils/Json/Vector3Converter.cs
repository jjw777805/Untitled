using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using System;

namespace Myutils
{
    public class Vector3Converter : JsonConverter<Vector3>
    {
        public override void WriteJson(JsonWriter writer, Vector3 value, JsonSerializer serializer)
        {
            // 开始写一个 JSON 对象 { }
            writer.WriteStartObject();
            
            // 写 x 属性
            writer.WritePropertyName("x");
            writer.WriteValue(value.x);
            
            // 写 y 属性
            writer.WritePropertyName("y");
            writer.WriteValue(value.y);
            
            // 写 z 属性
            writer.WritePropertyName("z");
            writer.WriteValue(value.z);
            
            // 结束对象
            writer.WriteEndObject();
        }

        public override Vector3 ReadJson(JsonReader reader, Type objectType, Vector3 existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            float x = 0f, y = 0f, z = 0f;

            // 循环读取 JSON 对象中的所有属性
            while (reader.Read())
            {
                // 如果读取到属性名
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = reader.Value.ToString();
                    reader.Read(); // 读取属性值

                    switch (propertyName)
                    {
                        case "x":
                            x = Convert.ToSingle(reader.Value);
                            break;
                        case "y":
                            y = Convert.ToSingle(reader.Value);
                            break;
                        case "z":
                            z = Convert.ToSingle(reader.Value);
                            break;
                    }
                }
                // 如果读取到对象结束符，跳出循环
                else if (reader.TokenType == JsonToken.EndObject)
                {
                    break;
                }
            }

            return new Vector3(x, y, z);
        }
    }
}