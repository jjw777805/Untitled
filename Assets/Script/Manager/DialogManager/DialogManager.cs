using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyManager
{
    public partial class DialogManager : MonoBehaviour
    {
        public static DialogManager instance = null;
        // Start is called before the first frame update
        bool IsInstance()
        {
            if (this != instance && instance != null)return false;
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
}