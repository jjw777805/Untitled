using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
