
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

        public void Initialize()
        {
            GroundInitial();
            SlideInitial();
            JumpInitial();
            HurtInitial();
            RebornInitial();
        }

        void Awake()
        {
            CreateInstance();
        }

        void Start()
        {
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