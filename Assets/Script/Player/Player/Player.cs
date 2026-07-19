
using System;
using UnityEngine;

namespace MyPlayer
{
    
    using CD = BoxCollider2D ;
    [AddComponentMenu("MyPlayer/Player")]
    public partial class Player:MonoBehaviour
    {
        public static Player instance = null;

        PlayerData playerData;
        PlayerStatus ps;
        MyInput inputs;
        Rigidbody2D rb;
        SpriteRenderer sr;
        CD cd;
        Animator am;

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
            inputs = GameManager.instance.GetInputs();
            playerData = GameManager.instance.playerData;
            ps = PlayerStatus.instance;
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.gravityScale = downG;
            sr = gameObject.GetComponent<SpriteRenderer>();
            cd = gameObject.GetComponent<CD>();
            am = gameObject.GetComponent<Animator>();
        }
        void Update()
        {
            Move();
            Slide();
            Attack();
            Jump();
        }
    }
}