using System;
using System.Threading.Tasks;
using MyPlayer;
using MyScene;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace MyManager
{
    public partial class SceneLoaderManager : MonoBehaviour
    {
        public SceneSO firstLoadScene;
        public SceneSO mainMenu;
        public SceneSO currentScene;

        protected void FirstLoad()
        {
            currentScene = firstLoadScene;
            currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
        }

        public async void ReturnMainMenu()
        {
            await LoadSceneAsync(mainMenu);
        }
        async public Task LoadSceneAsync(string name)
        {
            try
            {
                var newScene = await Addressables.LoadAssetAsync<SceneSO>(name).Task;
                await LoadSceneAsync(newScene);
            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }

        async public Task LoadSceneAsync(SceneSO newScene)
        {
            try
            {
                currentScene = newScene;
                if (newScene.sceneReference.IsValid()) return ;
                var async = currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
                await async.Task;
            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }

        public async void LoadSceneAsync(string name,Vector3 pos)
        {
            try
            {
                var newScene = await Addressables.LoadAssetAsync<SceneSO>(name).Task;
                // Debug.Log(newScene==null);
                currentScene = newScene;

                if (newScene.sceneReference.IsValid())
                {
                    if (PlayerStatus.instance!=null)
                        PlayerStatus.instance.ResetStatus(pos);
                    return ;
                }

                var async = currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
                Debug.Log("Get!");
                async.Completed += (op) =>
                {
                    if (PlayerStatus.instance!=null)
                        PlayerStatus.instance.ResetStatus(pos);
                    if (CameraManager.instance != null)
                        CameraManager.instance.ResetStatus();

                };
                await Task.Yield();
            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }
        public async void LoadSceneAsync(SceneSO newScene,Vector3 pos)
        {
            try
            {
                Debug.Log(newScene==null);
                currentScene = newScene;

                if (newScene.sceneReference.IsValid())
                {
                    if (PlayerStatus.instance!=null)
                        PlayerStatus.instance.ResetStatus(pos);
                    if (CameraManager.instance != null)
                        CameraManager.instance.ResetStatus();
                    return ;
                }

                var async = newScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
                Debug.Log("Get!");
                async.Completed += (op) =>
                {
                    if (PlayerStatus.instance!=null)
                        PlayerStatus.instance.ResetStatus(pos);
                    if (CameraManager.instance != null)
                        CameraManager.instance.ResetStatus();
                    currentScene = newScene;

                };
                await Task.Yield();
            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }

    }
 
}
