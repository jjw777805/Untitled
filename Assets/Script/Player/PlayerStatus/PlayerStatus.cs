
using UnityEngine;

namespace MyPlayer
{
    [AddComponentMenu("MyPlayer/PlayerStatus")]
    public partial class PlayerStatus:MonoBehaviour
    {
        public static PlayerStatus instance;
        void CreateInstance()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);    
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void OnDestroy()
        {
            if(instance == this)instance = null;
        }

        public void Initialize()
        {
            GroundInitial();
            SlideInitial();
            JumpInitial();
            HurtInitial();
        }

        void StartBinding()
        {
            HurtBinding();  
        }

        void Awake()
        {
            CreateInstance();
        }

        void Start()
        {
            StartBinding();
            Initialize();   
        }

        void Update()
        {
            if(Time.deltaTime == 0)return ;
            OnGroundUpdate();
            JumpUpdate();
            SlideUpdate();
            AttackUpdate(); 
            HurtUpdate();
        }
    }
}