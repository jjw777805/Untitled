using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;
using MyPlayer;

public partial class GameManager 
{
    private int savingNumber=0;
    public PlayerData playerData;
    public bool HasSave(int i)
    {
       string saveFile = Path.Combine(Application.persistentDataPath,"save"+i.ToString()+".json");
       if(!File.Exists(saveFile))return false;
       else return true; 
    }

    public void LoadSave(int i)
    {
        string filename = "save"+i.ToString()+".json";
        playerData = PlayerData.Load(filename);
        savingNumber = i;
    }

    public void SaveSave()
    {
        string filename = "save"+savingNumber.ToString()+".json";
        playerData.Save(filename);
    }
}
