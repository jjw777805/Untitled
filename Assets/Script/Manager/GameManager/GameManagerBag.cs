

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyObject;
using MyUI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;

public partial class GameManager 
{
    public void PlayerBagAdd(string key,CollectionCommonData sc)
    {
        if (!IsInstance())
        {
            instance.PlayerBagAdd(key,sc);
        }

        if (playerData.HasCollected(key))
        {
            Debug.LogError("Error!");
        }

        playerData.BagAdd(key,sc);
    }

    public bool HasCollected(string key)
    {
        if(!IsInstance())return instance.HasCollected(key);
        return playerData.HasCollected(key);
    }   

    bool bagTrigger = false;
    public string bagPanelName = "BagPanel.prefab";
    void BagUpdate()
    {
        if(SavingNumber == -1)return ;
        if(!inputs.Player.Bag.WasPressedThisFrame() || bagTrigger)return;
        bagTrigger = true;
        if (!isPause)
        {
            if(Time.timeScale == 0 )return ;
            BagOpen(
                bagPanelName,
                playerData.collectionCommonDatas,
                playerData.collectionSprites
            );
            Stop();
        }
        else
        {
            ShutDownBag();
        }
    }

    async public void BagOpen(string key ,List<CollectionCommonData>data,
        List<Sprite> sp)
    {
        if (this != instance && instance != null)
        {
            instance.BagOpen(key,data,sp);
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

            var setting = set.GetComponent<BagPanel>();
            setting.SetData(data);
            setting.SetImage(sp);
            if(EventSystem.current != null)
            {
                // Debug.Log("has Point!");
                setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
            }
            setting.Create();
            await Task.Yield();
            setting.Open();
        }catch(Exception e)
        {
            Debug.Log(e.Message);
            Debug.LogError(e.ToString());
        }
        // Debug.Log("Open!: check "+name+ "\n"+string.Join(", ", ClosableList.Keys));
    }
    public void ShutDownBag()
    {
        if(instance != this && instance != null)
        {
            instance.ShutDownBag();
            return ;
        }
        // Debug.Log("Getin???");
        bagTrigger = true;
        pauseTrigger = true;
        Stop();
        ShutDownClosable(bagPanelName);
    }

    void BagLateUpdate()
    {
        bagTrigger = false;
    }
}
