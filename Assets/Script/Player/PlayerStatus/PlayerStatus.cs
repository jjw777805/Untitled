
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
            SlideInitial();
            JumpInitial();
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
            OnGroundUpdate();
            JumpUpdate();
            SlideUpdate();
            AttackUpdate(); 
        }
    }
}