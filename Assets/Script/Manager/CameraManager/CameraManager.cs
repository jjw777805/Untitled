using UnityEngine;

namespace MyManager
{
    [AddComponentMenu("Manager/CameraManager")]
    public partial class CameraManager : MonoBehaviour
    {
        public static CameraManager instance = null;


        // Start is called before the first frame update
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

        void OnDestroy()
        {
            if(instance == this)instance = null;
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
