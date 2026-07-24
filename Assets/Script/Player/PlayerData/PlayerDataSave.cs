
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MyUtils;
namespace MyPlayer
{
    public partial class PlayerData
    {
        private static string GetSavingPath(string filename)
        {
            return Path.Combine(Application.persistentDataPath,filename);
        }        
        public void Save(string filename)
        {
            string path =  GetSavingPath(filename);
            Debug.Log(path);
            string content = JsonConvert.SerializeObject(this,Json.Settings);
            File.WriteAllText(path,content);
        }

        public static PlayerData Load(string filename)
        {
            string path =  GetSavingPath(filename);
            if (!File.Exists(path))
            {
               Debug.LogError("No such File");
            }
            string content = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<PlayerData>(content,Json.Settings);
        }
    }
}