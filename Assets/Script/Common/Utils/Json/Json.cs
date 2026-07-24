
using Myutils;
using Newtonsoft.Json;
using UnityEngine;

namespace MyUtils{
    public class Json
    {
       private static JsonSerializerSettings _settings;

        public static JsonSerializerSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new JsonSerializerSettings();
                    _settings.Converters.Add(new Vector3Converter());
                }
                return _settings;
            }
        }
    }

}