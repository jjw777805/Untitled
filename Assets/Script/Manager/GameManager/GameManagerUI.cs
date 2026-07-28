using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

public partial class GameManager 
{
    public Dictionary<string,GameObject>ClosableList = new Dictionary<string, GameObject>();
    public Dictionary<string,AsyncOperationHandle<GameObject>>ClosableHandleList
        = new Dictionary<string,AsyncOperationHandle<GameObject>>();
    async public void OpenClosable(string key)
    {
        if (this != instance && instance != null)
        {
            instance.OpenClosable(key);
            return ;
        }
        try
        {
            // Debug.Log($"ShutDownClosable 调用者：{this.gameObject.name} (ID:{this.GetInstanceID()})");
            var handle = Addressables.InstantiateAsync(key);
            GameObject set = await handle.Task;
            set.transform.SetParent(UIManager.instance.transform,false);
            ClosableList.Add(key,set);
            ClosableHandleList.Add(key,handle);
            
            Closable setting = set.GetComponent<Closable>();
            if(EventSystem.current != null)
            {
                // Debug.Log("has Point!");
                setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
            }
            await Task.Yield();
            setting.Open();
        }catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.LogError(e.ToString());
        }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", ClosableList.Keys));
    }

    public void ShutDownClosable(string key)
    {
        if (this != instance && instance != null)
        {
            instance.ShutDownClosable(key);
            return ;
        }
        if (ClosableList.ContainsKey(key))
        {
            // Debug.Log("In!");
            ClosableList[key].GetComponent<Closable>().Close();
            Addressables.ReleaseInstance(ClosableHandleList[key]);
            ClosableList.Remove(key);
            ClosableHandleList.Remove(key);
        }
    }

    void ReleaseAllClosables()
    {
        foreach (var kv in ClosableHandleList)
            Addressables.ReleaseInstance(kv.Value);
        ClosableList.Clear();
        ClosableHandleList.Clear();
    }
    bool isPause=false;
    float pauseTimeScale;
    public string pausePanelName="PausePanel.prefab";
    bool pauseTrigger = false;

    public void HasEsced()
    {
        pauseTrigger = true;
    }

    /// <summary>
    /// 停止/启动游戏的时间流动
    /// </summary>
    void Stop()
    {
        // Debug.Log(isPause);
        // Debug.Log(StackTraceUtility.ExtractStackTrace());
        if (!isPause)
        {
            pauseTimeScale = Time.timeScale;
            Time.timeScale = 0;
            isPause = true;
        }
        else
        {
            isPause = false;
            Time.timeScale = pauseTimeScale;
        }
        
    }
    void PauseUpdate()
    {
        if(SavingNumber == -1)return ;
        if(!inputs.UI.Cancel.WasPressedThisFrame() || pauseTrigger)return;
        pauseTrigger = true;
        if (!isPause)
        {
            // Debug.Log("isChecking");
            
            OpenClosable(pausePanelName);
            Stop();
        }
    }

    void ShutDownPause()
    {
        if(instance != this && instance != null)
        {
            instance.ShutDownPause();
            return ;
        }
        // Debug.Log("Getin???");
        pauseTrigger = true;
        Stop();
        ShutDownClosable(pausePanelName);
    }

    void PauseLateUpdate()
    {
        pauseTrigger = false;
    }
}
