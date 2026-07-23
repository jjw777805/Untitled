using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using MyPlayer;
using MyManager;

public partial class GameManager 
{
    public void ReturnMainMemu()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ScenesReload()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        PlayerStatus.instance.Reset();
        CameraManager.instance.Reset();
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            Debug.Log("exit已经触发");
        #else 
            Application.Quit();
        #endif
    }
}
