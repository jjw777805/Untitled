
using System.Collections.Generic;
using MyManager;
using MyPlayer;
using MyUtils;
using UnityEngine;

namespace MyEnemy
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LittleEnemy:Enemy
    {
        public EnemyData enemyData;
        public Vector3 bornPos;
        bool isKnowPlayer = false;
        Vector3 playerPos;
        Rigidbody2D rb;

        #region Motion
        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        /// 如果左边检测到在地面，十位置为1
        /// 如果右边检测到在地面，个位置为1
        /// </returns>
        int IsHalfOnGround()
        {   
            if(rb.velocity.y<-1f)return 0;
            Vector3 centerPos = 
                transform.position + Vector3.Scale(cd.offset,transform.localScale);
            Vector3 leftPos,RightPos;
            RightPos = leftPos = centerPos;
            leftPos.x = centerPos.x - transform.localScale.x * cd.size.x/2+0.1f;  
            RightPos.x = centerPos.x + transform.localScale.x * cd.size.x/2-0.1f;  
            RaycastHit2D hitLeft = Physics2D.Raycast(
                leftPos,
                -transform.up,
                transform.localScale.y*(cd.size.y+0.1f)/2,
                LayerMask.GetMask("Ground")
            );
            RaycastHit2D hitRight = Physics2D.Raycast(
                RightPos,
                -transform.up,
                transform.localScale.y*(cd.size.y+0.1f)/2,
                LayerMask.GetMask("Ground")
            );
            int ans=0;
            if(hitLeft.collider!=null)ans+=10;
            if(hitRight.collider!=null)ans+=1;
            return ans;
        }
        bool IsInsight()
        {
            Vector3 selfPos = transform.position;
            Vector3 deltaPos = playerPos - selfPos;
            float dis=Vector3.Magnitude(deltaPos);
            if (isKnowPlayer)
            {
                if(dis>=enemyData.catchDis && playerKnownCT.isStop())
                    playerKnownCT.Reset();

                return true;
            }
            else
            {
                if (dis < enemyData.catchDis)
                {
                    if (transform.right.x * deltaPos.x > 0.01f)
                    {
                        isKnowPlayer = true;
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
        }

        void MoveHorizontal(Vector3 Target , float speed)
        {
            Vector3 deltaPos = Target - transform.position;
            if(deltaPos.x * transform.right.x < 0.01f)
            {
                Vector3 newEuler = transform.eulerAngles;
                newEuler.y = (newEuler.y+180f)%360;
                transform.rotation = Quaternion.Euler(newEuler);
            }
            rb.velocity = new Vector2(speed * transform.right.x , 0);
        }
        void Catch()
        {
            float td = Vector3.Distance(playerPos,transform.position);
            if(td<enemyData.atkMaxDis && td>enemyData.atkMinDis)return;
            if(td>=enemyData.atkMaxDis)
                MoveHorizontal(playerPos,enemyData.catchSpeed);
            else
            {
                Vector3 delta = playerPos - transform.position;
                MoveHorizontal(transform.position-delta,enemyData.catchSpeed);
            }
        }

        void Patrol()
        {
            float left = bornPos.x - enemyData.patrolDis/2;
            float right = bornPos.x + enemyData.patrolDis/2;
            float nowPos = transform.position.x;
            // Debug.Log("Patrol:");
            if( left <= nowPos  && nowPos <= right)
            {
                rb.velocity = new Vector2(enemyData.patrolSpeed * transform.right.x , 0);
                // Debug.Log("speed="+rb.velocity);
            } 
            else MoveHorizontal(bornPos,enemyData.patrolSpeed);
        }

        int hp;
        TimeCount stunCoolDown = new TimeCount();
        bool isStun=false;

        public override void Death()
        {
            Drop();
            gameObject.SetActive(false);
        }
        public override void Hurt(int damage)
        {
            hp -= damage;
            if(hp<=0)
            {
                Death();
                return ;
            }

            isStun = true;
            anim.SetBool(StunHash,true);
            stunCoolDown.Reset();
            isKnowPlayer = true;
            rb.velocity=Vector2.zero;
            if(PlayerStatus.instance.Visible)return ;
            SetVisible(true);
            canSeeCT.Reset();
        }
        bool canAttack = true;
        public bool isAtk = false;
        bool Attack()
        {
            if(!canAttack || isAtk)return false;
            if(Vector3.Distance(playerPos,transform.position)<enemyData.atkMinDis)return false;
            var hit = Physics2D.Raycast(
                    transform.position,
                    transform.right,
                    enemyData.atkMaxDis,
                    LayerMask.GetMask("Player")  
                );
            if (hit.collider != null)
            {
                canAttack = false;
                isAtk = true;
                atkCoolDown.Reset();
                rb.velocity = new Vector2(0,rb.velocity.y);
                anim.SetTrigger(AttackHash);
                return true;
            }
            return false;
        }

        public override void Drop()
        {
            int money = enemyData.leftMoney;
            var ratios = enemyData.leftPossibility;
            var collections = enemyData.leftCollection;
            GameManager.instance.playerData.MoneyDelta(money);
            #warning need to be complete
        }

        TimeCount atkCoolDown;
        Animator anim;

        void MoveCheck()
        {
            if (!IsInsight())
            {
                int isHalfOnGround = IsHalfOnGround();
                if(transform.right.x == 1)
                {
                    if(isHalfOnGround%10==0)transform.right = - transform.right;
                }
                else
                {
                    if(isHalfOnGround/10==0)transform.right = - transform.right;
                }
                // Debug.Log(transform.right+" "+isHalfOnGround);
            }
            Vector3 newPos;
            newPos = ColliderUtils.AvailablePosBox(transform,cd,5.0f);
            if (Mathf.Abs(newPos.x) < 0.01f)
            {
                // Debug.Log(transform.position+":"+transform.right+","+newPos);
                transform.right = -transform.right;
                rb.velocity= new Vector2(0,rb.velocity.y);
            }
            
        }
        #endregion 
        
        #region Interface
        public void ResetStatus()
        {
            gameObject.transform.position = bornPos;
            hp = enemyData.hp;
            transform.rotation = Quaternion.Euler(0,0,0);
            gameObject.SetActive(true);
            SetVisible(PlayerStatus.instance.Visible);
            isStun = isAtk = isKnowPlayer = false;
            canAttack = true;
            gameObject.SetActive(true);
            Initial();
        }

        public void SetVisible(bool visible)
        {
            foreach (var i in srList)
            {
                i.enabled = visible;
            }
        }

        #endregion 
        public static readonly int StunHash = Animator.StringToHash("Stun");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        TimeCount playerKnownCT = new TimeCount();
        BoxCollider2D cd;
        SpriteRenderer sr;
        TimeCount canSeeCT = new TimeCount();
        List<SpriteRenderer>srList = new List<SpriteRenderer>();

        void Initial()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            cd = GetComponent<BoxCollider2D>();
            sr = GetComponent<SpriteRenderer>();
            GetComponentsInChildren<SpriteRenderer>(true,srList);
            SetVisible(PlayerStatus.instance.Visible);
            hp = enemyData.hp;
            atkCoolDown = new TimeCount(enemyData.atkSeq);
            atkCoolDown.On_End.AddListener(()=>{canAttack=true;});
            stunCoolDown = new TimeCount(enemyData.stunTime);
            stunCoolDown.On_End.AddListener(()=>{isStun=false;anim.SetBool(StunHash,false);});
            playerKnownCT = new TimeCount(enemyData.lossPlayerTime);
            playerKnownCT.On_End.AddListener(()=>isKnowPlayer = false);
            canSeeCT.SetTime(enemyData.canSeeTime);
            canSeeCT.On_End.AddListener(()=>{SetVisible(false);});
        }
        void Start()
        {
            SceneLoaderManager.instance.onSceneReset+=ResetStatus;
            Initial();
        }
        void Update()
        {
            
            if(Time.deltaTime ==0)return;
            atkCoolDown.Update(Time.deltaTime);
            stunCoolDown.Update(Time.deltaTime);
            playerKnownCT.Update(Time.deltaTime);
            canSeeCT.Update(Time.deltaTime);
            if(isStun || isAtk)return;
            playerPos = Player.instance.transform.position;
            bool isInsight = IsInsight();
            if(!isInsight)Patrol();
            else
            {
                if(!Attack())Catch();
            }
            MoveCheck();
        }

        void OnDestroy()
        {
            SceneLoaderManager.instance.onSceneReset -= ResetStatus;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 delta = transform.position - Player.instance.transform.position;
                if(PlayerStatus.instance.Visible)return ;
                SetVisible(true);
                canSeeCT.Reset();
                Player.instance.MayHurt(
                    delta,
                    (int)enemyData.attackDamage,
                    Player.BlkType.Hurt,
                    ()=>{},
                    ()=>{}
                );
            }
        }
    }
}