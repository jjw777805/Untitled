using MyUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using MyPlayer;
using MyManager;
using Newtonsoft.Json;
using System.Collections;
using System;
using System.Threading.Tasks;
using MyScene;

public partial class GameManager 
{
    public void ReturnMainMemu()
    {
        if (this != instance && instance != null)
        {
            instance.ReturnMainMemu();
            return ;
        }
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            // Debug.Log("ReturnMenu?");
            SaveSave();
            Destroy(PlayerStatus.instance.gameObject);
            Destroy(UIManager.instance.gameObject);
            SceneLoaderManager.instance.ReturnMainMenu();
            SavingNumber = -1;
        }
    }

    public void ScenesReload(Action onComplete = null)
    {
        if (this != instance && instance != null)
        {
            instance.ScenesReload(onComplete);
            return ;
        }
        // string sceneName = SceneManager.GetActiveScene().name;
        LoadScene(playerData.GetSceneName(),playerData.GetResetPos(),onComplete);
    }

    public void LoadScene(string name,Vector3 pos,Action onComplete=null)
    {
        if (this != instance && instance != null)
        {
            instance.LoadScene(name,pos,onComplete);
            return ;
        }
        SceneLoaderManager.instance.LoadSceneAsync(name,pos,onComplete);

    }

    async void LoadSceneAsync(string name,Vector3 pos)
    {
        try
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(name);
            async.completed += (op) =>
            {
                if (Player.instance != null)
                    Player.instance.transform.position = pos;
                if (CameraManager.instance != null)
                    CameraManager.instance.ResetStatus();
            };
            await Task.Yield();
        }catch (Exception e)
        {
            Debug.LogError(e);
        }     
    }
    public void ExitGame()
    {
        if (this != instance && instance != null)
        {
            instance.ExitGame();
            return ;
        }
        
        SaveSave();
        #if UNITY_EDITOR
            Debug.Log("exit已经触发");
        #else 
            Application.Quit();
        #endif
    }
}
