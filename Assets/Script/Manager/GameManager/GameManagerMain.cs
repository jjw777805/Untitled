using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

[AddComponentMenu("Manager/GameManager")]
public partial class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    // Start is called before the first frame update
    MyInput inputs;
    public MyInput GetInputs()
    {   
        return inputs;
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
