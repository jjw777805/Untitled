using System.Data.Common;
using MyEnemy;
using MyPlayer;
using UnityEngine;

namespace MyObject
{
    public class Tossable : MonoBehaviour
    {
        public TossSO bulletData;
        float aliveTime=3;
        float deltaSumTime = 0;
        bool begin = false;
        bool isConstant = true;
        bool waitingEnd = true;
        Vector3 InitialVelocity;
        float g=1f;
        int crashTime = 1;

        #region 设置属性函数
       
        public void SetCrashTime(int k)
        {
            crashTime = k;
        }
        public void SetAliveTime(float t)
        {
            aliveTime = t;
        }

        public void SetInitialVelocity(Vector3 t)
        {
            InitialVelocity = t;
        }
        public void SetIsConstant(bool f)
        {
            isConstant = f;
        }

        public void SetGravity(float k)
        {
            g=k;
        }

        #endregion 

        #region 控制函数
        void AdjustFace()
        {
            if(bulletData.forwardLeft)transform.right = -rb.velocity;
            else transform.right = rb.velocity;
        }
        #endregion

        void End()
        {
            if (this == null) return ;
            Destroy(gameObject);
        }

        #region 生命周期函数
        [SerializeField]
        Rigidbody2D rb;

        void Awake()
        {
            begin = false;
        }
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            if(!begin || Time.timeScale==0 || waitingEnd )return ;
            deltaSumTime += Time.deltaTime;
            if(deltaSumTime > aliveTime)End();

            if(isConstant)rb.velocity = InitialVelocity;
            AdjustFace();
        }

        void OnDestroy()
        {
            // CancelInvoke();
            // StopAllCoroutines();
        }
        #endregion

        #region 对外接口
        public void Begin()
        {
            begin = true;
            deltaSumTime = 0;
            waitingEnd = false;

            if(isConstant)rb.gravityScale = 0;
            else rb.gravityScale = g;

            rb.velocity = InitialVelocity;

            AdjustFace();
        }

        public void BeAttacked(Vector3 pos)
        {
            if(!bulletData.canAttack)return ;
            if (!bulletData.canAttackCrashed)
            {
                if (bulletData.canReflect)
                {
                    Vector3 delta = transform.position - pos;
                    Vector2 v = rb.velocity;
                    if(delta.x*v.x<0)v.x=-v.x;
                    rb.velocity = v;   
                }
                return ;
            }
            crashTime -- ;
            if(crashTime==0)End();
        }
        #endregion

        public void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log("Get In! : "+collision.name);
            bool attacked = false;
            if (collision.CompareTag("Player"))
            {
                Vector3 delta = transform.position - Player.instance.transform.position;
                if (bulletData.canBlock)
                {
                    if (bulletData.isReborn)
                    {
                        Player.instance.MayHurt(
                                delta,
                                bulletData.playerDamage,
                                Player.BlkType.Trap,
                                ()=>{End();},
                                ()=>{End();}
                            );
                    }
                    else Player.instance.MayHurt(
                            delta,
                            bulletData.playerDamage,
                            Player.BlkType.Hurt,
                            ()=>{End();},
                            ()=>{End();}
                        );
                    waitingEnd = true;
                    return ;
                }
                else Player.instance.Trap(bulletData.playerDamage);
                attacked = true;
            }
            else if (collision.CompareTag("Enemy"))
            {
                if (bulletData.canHurtEnemy)
                {
                    collision.GetComponent<Enemy>().Hurt(bulletData.enemyDamage);
                    attacked = true;
                }
            }
            else if(collision.CompareTag("Ground"))
            {
                if(!bulletData.canBounce)
                {
                    // Debug.Log("Here!");
                    End();
                }
            }

            if(attacked && bulletData.hurtVanish)End();;
        }
    
    }
}