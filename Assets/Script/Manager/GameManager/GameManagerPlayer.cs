using UnityEngine;
using System.IO;
using MyPlayer;
using MyManager;

public partial class GameManager 
{
    private int savingNumber=0;
    private int SavingNumber
    {
        set
        {
            savingNumber = value;
            // Debug.Log($"savingNumber 被修改！新值: {value}\n调用堆栈:\n{StackTraceUtility.ExtractStackTrace()}");
        }
        get
        {
            return savingNumber;
        }
    }
    public PlayerData playerData;

    void SaveStart()
    {
        // Debug.Log("Start!");
        if(Player.instance!=null)SavingNumber = 4;
        else SavingNumber = -1;
    }
    public bool HasSave(int i)
    {
        string saveFile = Path.Combine(Application.persistentDataPath,"save"+i.ToString()+".json");
        Debug.Log(saveFile);
        if(!File.Exists(saveFile))return false;
        else return true; 
    }

    public void LoadSave(int i)
    {
        if (this != instance && instance != null)
        {
            instance.LoadSave(i);
            return ;
        }
        if (!HasSave(i))
        {
            playerData = new PlayerData();
            SavingNumber = i;
            SaveSave();
        }
        string filename = "save"+i.ToString()+".json";
        playerData = PlayerData.Load(filename);
        SavingNumber = i;
        // Debug.Log("loadsavebefore:"+SavingNumber);
        Destroy(CameraManager.instance.gameObject);
        Destroy(UIManager.instance.gameObject);
        // ReleaseAllClosables();
        LoadScene(playerData.GetSceneName(),playerData.GetResetPos());
        // Debug.Log("loadsaveend:"+SavingNumber);
    }

    public void SaveSave()
    {
        if (this != instance && instance != null)
        {
            instance.SaveSave();
            return ;
        }
        string filename = "save"+SavingNumber.ToString()+".json";
        playerData.Save(filename);
    }

    public void DelSave(int i)
    {
        if (this != instance && instance != null)
        {
            instance.DelSave(i);
            return ;
        }
        string saveFile = Path.Combine(Application.persistentDataPath,"save"+i.ToString()+".json");
        if(!File.Exists(saveFile))
        {
            Debug.LogError("no save exist!");
        }
        else 
        {
            try
            {
                File.Delete(saveFile);
                Debug.Log($"存档已删除: {saveFile}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"删除存档失败: {e.Message}");
            }
        }
    }
}
