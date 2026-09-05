using MyManager;
using UnityEngine;

namespace MyAudio
{
    public class BGM :MonoBehaviour
    {
        public static BGM instance = null;
        public AudioSource audioS;
        string sceneShowName = "";

        #region 生命周期函数
        void Awake()
        {
            if(instance == null)
            {
                instance = this;
                if (   SceneLoaderManager.instance != null
                    && SceneLoaderManager.instance.currentScene != null)
                {
                    sceneShowName = SceneLoaderManager.instance.currentScene.showName;
                }
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                if(SceneLoaderManager.instance == null)
                {
                    Debug.LogError("Why Loader Empty?");
                    Debug.Break();
                }
                else
                {
                    if(instance.sceneShowName == SceneLoaderManager.instance.currentScene.showName)
                    {
                        Destroy(gameObject);   
                    }
                    else
                    {
                        sceneShowName = SceneLoaderManager.instance.currentScene.showName;
                        Destroy(instance.gameObject);
                        instance = this;
                    }
                }
            }
        }

        void Update()
        {
            if(instance.sceneShowName == "")
            {
                sceneShowName = SceneLoaderManager.instance.currentScene.showName;
            }
            // if(Time.timeScale == 0 && audioS.isPlaying )audioS.Pause();
            // else if(Time.timeScale == 1 && !audioS.isPlaying)audioS.Play();
        }
        #endregion
    
    }
}