using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

public partial class UIManager : MonoBehaviour
{
    Dictionary<string,GameObject>MsgBoxList = new Dictionary<string, GameObject>();
    Dictionary<string,AsyncOperationHandle<GameObject>>MsgBoxHandleList
        = new Dictionary<string,AsyncOperationHandle<GameObject>>();

    public string boxYNname="MessageBoxYN.prefab";
    public string boxName="MessageBox.prefab";

    async public void OpenMsgBox(string key,string msg,Vector2 size)
    {
        if (this != instance && instance != null)
        {
            instance.OpenMsgBox(key,msg,size);
            return ;
        }
        try
        {
            // Debug.Log($"ShutDownMsgBox 调用者：{this.gameObject.name} (ID:{this.GetInstanceID()})");
            var handle = Addressables.InstantiateAsync(boxName);
            GameObject set = await handle.Task;
            set.transform.SetParent(UIManager.instance.transform,false);
            
            MsgBoxList.Add(key,set);
            MsgBoxHandleList.Add(key,handle);
            
            MessageBox setting = set.GetComponent<MessageBox>();
            
            setting.SetSize(size);
            setting.SetKey(key);
            setting.SetMessage(msg);
            if(EventSystem.current != null)
            {
                setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
            }
            if(EventSystem.current == null)
            {
                Debug.Log("event null");
            }else
            {
                if (EventSystem.current.currentSelectedGameObject == null)
                {
                    Debug.Log("cur is null");
                }
                else Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            }
            await Task.Yield();
            setting.Open();
        }catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.LogError(e.ToString());
        }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", MsgBoxList.Keys));
    }
    

    /// <summary>
    /// 请记得在回调函数中加入关闭操作
    /// </summary>
    /// <param name="key"></param>
    /// <param name="msg"></param>
    /// <param name="yesAction"></param>
    /// <param name="noAction"></param>
    /// <param name="size"></param>
    async public void OpenMsgBoxYN(string key,string msg,Action yesAction,
        Action noAction, Vector2 size)
    {
        if (this != instance && instance != null)
        {
            instance.OpenMsgBoxYN(key,msg,yesAction,noAction,size);
            return ;
        }
        try
        {
            // Debug.Log($"ShutDownMsgBox 调用者：{this.gameObject.name} (ID:{this.GetInstanceID()})");
            var handle = Addressables.InstantiateAsync(boxYNname);
            GameObject set = await handle.Task;
            set.transform.SetParent(UIManager.instance.transform,false);
            
            MsgBoxList.Add(key,set);
            MsgBoxHandleList.Add(key,handle);
            
            MessageBoxYN setting = set.GetComponent<MessageBoxYN>();
            setting.SetStatus(msg,yesAction,noAction);
            setting.SetSize(size);
            if(EventSystem.current != null)
            {
                setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
            }
            await Task.Yield();
            setting.Open();
        }catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.LogError(e.ToString());
        }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", MsgBoxList.Keys));
    }

    public void ShutDownMsgBox(string key)
    {
        if (this != instance && instance != null)
        {
            instance.ShutDownMsgBox(key);
            return ;
        }
        if (MsgBoxList.ContainsKey(key))
        {
            // Debug.Log("In!");
            MsgBoxList[key].GetComponent<MessageBox>().Close();
            Addressables.ReleaseInstance(MsgBoxHandleList[key]);
            MsgBoxList.Remove(key);
            MsgBoxHandleList.Remove(key);
        }
    }

}
