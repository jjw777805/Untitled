using System;
using System.Threading.Tasks;
using MyPlayer;
using MyScene;
using MyTransition;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace MyManager
{
    public partial class SceneLoaderManager : MonoBehaviour
    {
        public SceneSO firstLoadScene;
        public SceneSO mainMenu;
        public SceneSO currentScene;

        public AssetReference transRef;

        TransitionTemplate trans;

        void RefClear()
        {
            if(trans!=null)Destroy(trans.gameObject);
            if(transRef.IsValid())transRef.ReleaseAsset();
        }
        async void RefInitial()
        {
            GameObject t;
            if (!transRef.IsValid())
            {
                await transRef.LoadAssetAsync<GameObject>().Task;
            }
            t = Instantiate((GameObject)transRef.Asset);
            DontDestroyOnLoad(t);
            trans = t.GetComponent<TransitionTemplate>();
        }

        protected void FirstLoad()
        {
            currentScene = firstLoadScene;
            currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
        }

        public async void ReturnMainMenu()
        {
            await LoadSceneAsync(mainMenu);
        }
        async public Task LoadSceneAsync(string name,Action onComplete = null)
        {
            try
            {
                trans.SetIn();
                while(!trans.finished)await Task.Yield();

                var newScene = await Addressables.LoadAssetAsync<SceneSO>(name).Task;
                await LoadSceneAsync(newScene);

                trans.SetOut();
                while(!trans.finished)await Task.Yield();
                onComplete?.Invoke();
                await Task.Yield();

            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }

        async protected Task LoadSceneAsync(SceneSO newScene)
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

        public async void LoadSceneAsync(string name,Vector3 pos,Action onComplete = null)
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
                    onComplete?.Invoke();
                    return ;
                }

                trans.SetIn();
                while(!trans.finished)await Task.Yield();

                var async = currentScene.sceneReference.LoadSceneAsync(LoadSceneMode.Single);
                // Debug.Log("Get!");
                async.Completed += (op) =>
                {
                    if (PlayerStatus.instance!=null)
                        PlayerStatus.instance.ResetStatus(pos);
                    if (CameraManager.instance != null)
                        CameraManager.instance.ResetStatus();

                    trans.SetOut();
                };
                while(!trans.finished)await Task.Yield();
                onComplete?.Invoke();
                await Task.Yield();
            }catch (Exception e)
            {
                Debug.LogError(e);
            }     
        }
        protected async void LoadSceneAsync(SceneSO newScene,Vector3 pos)
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
