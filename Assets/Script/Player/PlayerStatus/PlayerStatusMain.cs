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