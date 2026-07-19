using MyUI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;
using System.IO;
using UnityEngine.SceneManagement;

[AddComponentMenu("Manager/UIManager")]
public partial class UIManager : MonoBehaviour
{ 
    public static UIManager instance = null;

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

        InitialHP();
    }

    public void Initial()
    {
  
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
