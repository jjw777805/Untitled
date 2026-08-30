using UnityEngine;

namespace MyManager
{
    [AddComponentMenu("Manager/AudioManager")]
    public partial class AudioManager : MonoBehaviour
    {
        public static AudioManager instance = null;

        public AudioConfig config=null;
        // Start is called before the first frame update
        #region 生命周期函数
        void Awake()
        {
            #region 单例模式
            if(instance == null)
            {
                instance = this;
                config = LoadFromJson();
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
            if(instance == this)
            {
                instance = null;
                SaveToJson();
            }
        }
        void Start()
        {
            ResetVolumn();
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        #endregion
    }
 
}
