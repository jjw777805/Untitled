using UnityEngine;
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

    private void OnDestroy()
    {
        if (instance == this)
        {
            inputs?.Dispose(); 
            inputs = null;
        }
    }

    #region 生命周期函数
    void Awake()
    {
        #region 单例模式
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);  
            inputs = new MyInput();
            inputs.Enable();  
            SavingNumber = -1;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion 
    }
    void Start()
    {
        SaveStart();

    }

    // Update is called once per frame
    void Update()
    {
        PauseUpdate();
    }
    #endregion
}
