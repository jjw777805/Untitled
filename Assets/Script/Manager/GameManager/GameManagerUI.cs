using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

public partial class GameManager 
{
    [SerializeField]
    Canvas canvas;
    async public void OpenClosable(string key)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(key);
        GameObject prefab = await handle.Task;
        GameObject set = Instantiate(prefab);
        set.transform.SetParent(canvas.transform,false);
        Closable setting = set.GetComponent<Closable>();
        if(EventSystem.current != null)
        {
            setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
        }
        await Task.Yield();
        setting.Open();
        
    }

    public void ReturnMainMemu()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            SceneManager.LoadScene("MainMenu");
        }
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
