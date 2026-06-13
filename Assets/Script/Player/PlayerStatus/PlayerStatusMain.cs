using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace MyPlayer
{
    public partial class PlayerStatus:MonoBehaviour
    {
        public static PlayerStatus instance;
        void CreateInstance()
        {
            if(instance = null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);    
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public bool canSlide,isSlide;

        int JumpTime;
        public int canJump;
        public float defTime;

        public bool isGround()
        {
            return true;
        }

        public void Initialize(PlayerData data)
        {
            
        }

        void Awake()
        {
            CreateInstance();
        }

        void Update()
        {
            
        }
    }
}