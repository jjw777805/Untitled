
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

        void OnDestroy()
        {
            if(instance == this)instance = null;
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
            sr = gameObject.GetComponent<SpriteRenderer>();
            BlockInitial();
            Show();
        }
        void Update()
        {
            if(Time.timeScale==0 && !ps.IsBlock())return ;
            Move();
            Slide();
            Block();   
            if(Time.timeScale==0)return;
            Attack();
            Jump();
            Heal();
        }

        void LateUpdate()
        {
            SlideLateUpdate();
        }
    }
}