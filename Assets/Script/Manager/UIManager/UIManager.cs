using UnityEngine;

[AddComponentMenu("Manager/UIManager")]
public partial class UIManager : MonoBehaviour
{ 
    public static UIManager instance = null;
    Canvas canvas;
    Camera UIcamera;
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
        Initial();
    }

    public void Initial()
    {
        InitialHP();
        
    }
    void Start()
    {
        canvas = GetComponent<Canvas>();
        UIcamera = canvas.worldCamera;
    }

    // Update is called once per frame
    void Update()
    {
    }
    #endregion
}
