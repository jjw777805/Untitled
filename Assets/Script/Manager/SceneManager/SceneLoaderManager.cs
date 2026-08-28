using UnityEngine;

namespace MyManager
{
    [AddComponentMenu("Manager/SceneLoaderManager")]
    public partial class SceneLoaderManager : MonoBehaviour
    {
        public static SceneLoaderManager instance = null;


        // Start is called before the first frame update
        #region 生命周期函数
        void Awake()
        {
            #region 单例模式
            if(instance == null)
            {
                instance = this;
                RefInitial();
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
            RefClear();
            if(instance == this)instance = null;
        }
        void Start()
        {
            FirstLoad();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion
    }
 
}
