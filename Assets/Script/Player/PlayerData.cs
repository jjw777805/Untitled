using System;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

namespace MyPlayer
{
    public enum Weapon
    {
        NONE,JI,GUN,DAO,BIAN
    }

    [Serializable]
    public class PlayerData
    {
        public int HP = 3;
        public int damage = 5;
        public Weapon weapon = Weapon.NONE;

        private static string GetSavingPath(string filename)
        {
            return Path.Combine(Application.persistentDataPath,filename);
        }        
        public void Save(string filename)
        {
            string path =  GetSavingPath(filename);
            string content = JsonUtility.ToJson(this);
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
            return JsonUtility.FromJson<PlayerData>(content);
        }
    }
}