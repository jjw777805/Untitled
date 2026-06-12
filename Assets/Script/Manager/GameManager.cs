using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

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
    [SerializeField]
    Panel setting;
    async public void OpenSetting()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("SettingPanel");
        GameObject prefab = await handle.Task;
        GameObject set = Instantiate(prefab);
        set.transform.SetParent(canvas.transform,false);
        setting = set.GetComponent<Panel>();
        if(setting!=null)Debug.Log("asdf");
        if(EventSystem.current != null)
        {
            setting.SetFrontSelected(EventSystem.current?.currentSelectedGameObject);
        }
        await Task.Yield();
        setting.Open();
        
    }

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
}
