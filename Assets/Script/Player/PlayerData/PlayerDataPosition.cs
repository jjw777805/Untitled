
using System;
using System.Threading.Tasks;
using MyManager;
using MyScene;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MyPlayer
{
    public partial class PlayerData
    {
        [JsonProperty]
        Vector3 rebornPosition = new Vector3(-10.99f,0.77f,0);
        [JsonProperty]
        string sceneName="";
        public void SetResetPos(Vector3  p)
        {
            rebornPosition = p;
            sceneName = SceneLoaderManager.instance.currentScene.sceneName;
        }
        public Vector3 GetResetPos()
        {
            return rebornPosition;
        }

        public string GetSceneName()
        {
            return sceneName;        
        }    

        void NewPosition()
        {
            rebornPosition = new Vector3(-10.99f,0.77f,0);
            sceneName="Tomb";
        }
    }
}