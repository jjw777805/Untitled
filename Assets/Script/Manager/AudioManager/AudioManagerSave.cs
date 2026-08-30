using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Audio;

namespace MyManager
{
    
    public partial class AudioManager : MonoBehaviour
    {
        public AudioMixer ad;
        public class AudioConfig
        {
            public Dictionary<string,float>dic;
            public AudioConfig()
            {
                dic["BGM"] = 0.8f;
                dic["Master"] = 0.8f;
                dic["Effect"] = 0.8f;
            }
        }

        string GetSavePath()
        {
            return Path.Combine(Application.persistentDataPath,"AudioConfig.json");
        }
        AudioConfig LoadFromJson()
        {
            string path = GetSavePath();
            AudioConfig tempConfig = null;
            if (!File.Exists(path))
            {
                Debug.LogError("?");
                tempConfig = new AudioConfig();
            }
            else
            {
                string text = File.ReadAllText(path);
                tempConfig = JsonConvert.DeserializeObject<AudioConfig>(text);
            }
            
            // ad.SetFloat()

            return tempConfig;
        }

        void SaveToJson()
        {
            string path = GetSavePath();
            string text = JsonConvert.SerializeObject(config);
        }
    }
 
}
