using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

[AddComponentMenu("Manager/GameManager")]
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    // Start is called before the first frame update
    MyInput inputs;
    public MyInput GetInputs()
    {   
        return inputs;
    }

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
    public bool HasSave(int i)
    {
       string saveFile = Path.Combine(Application.persistentDataPath,"save"+i.ToString()+".json");
       if(!File.Exists(saveFile))return false;
       else return true; 
    }

    #region 生命周期函数
    void Awake()
    {
        #region 单例模式
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);    
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion

        inputs = new MyInput();
        inputs.Enable();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion
}
